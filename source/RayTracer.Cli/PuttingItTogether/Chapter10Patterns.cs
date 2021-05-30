using System;
using System.Collections.Immutable;
using System.IO;
using RayTracer.Core;
using RayTracer.Core.Materials;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Cli.PuttingItTogether
{
    internal static class Chapter10Patterns
    {
        public static void GenerateImage()
        {
            var boxMaterial = Material.Default with
            {
                Pattern = new SolidPattern(new Color(1f, 0.9f, 0.9f)), SpecularReflection = 0f
            };

            var floor = new InfinitePlane { Material = boxMaterial, Normal = Vector.UnitY };

            var leftWall = new InfinitePlane
            {
                Material = boxMaterial,
                Normal = Vector.UnitX,
                Transform = Matrix4.Translation(3f, 0f, 0f)
            };

            var rightWall = new InfinitePlane
            {
                Material = boxMaterial,
                Normal = Vector.UnitZ,
                Transform = Matrix4.Translation(0f, 0f, 3f)
            };

            var sphere = new Sphere
            {
                Material = Material.Default with
                {
                    DiffuseReflection = 0.7f,
                    Pattern = new StripedPattern
                    {
                        Patterns = ImmutableArray.Create<Pattern>(
                            new StripedPattern
                            {
                                Patterns = ImmutableArray.Create<Pattern>(
                                    new StripedPattern
                                    {
                                        Patterns = ImmutableArray.Create<Pattern>(
                                            new SolidPattern(Color.Red),
                                            new SolidPattern(Color.White)
                                        ),
                                        Transform = Matrix4.Identity.RotateZ(MathF.PI / 4f)
                                            .Scale(0.25f, 0.25f, 0.25f)
                                    },
                                    new StripedPattern
                                    {
                                        Patterns = ImmutableArray.Create<Pattern>(
                                            new SolidPattern(Color.Green),
                                            new SolidPattern(Color.White)
                                        ),
                                        Transform = Matrix4.Identity.RotateZ(-MathF.PI / 4f)
                                            .Scale(0.25f, 0.25f, 0.25f)
                                    }
                                ),
                                Transform = Matrix4.Identity.RotateZ(MathF.PI / 4f)
                                    .Scale(0.25f, 0.25f, 0.25f)
                            },
                            new StripedPattern
                            {
                                Patterns = ImmutableArray.Create<Pattern>(
                                    new StripedPattern
                                    {
                                        Patterns = ImmutableArray.Create<Pattern>(
                                            new SolidPattern(Color.Blue),
                                            new SolidPattern(Color.White)
                                        ),
                                        Transform = Matrix4.Identity.RotateZ(MathF.PI / 4f)
                                            .Scale(0.25f, 0.25f, 0.25f)
                                    },
                                    new StripedPattern
                                    {
                                        Patterns = ImmutableArray.Create<Pattern>(
                                            new SolidPattern(new Color(1f, 0.99f, 0.23f)),
                                            new SolidPattern(Color.White)
                                        ),
                                        Transform = Matrix4.Identity.RotateZ(-MathF.PI / 4f)
                                            .Scale(0.25f, 0.25f, 0.25f)
                                    }
                                ),
                                Transform = Matrix4.Identity.RotateZ(-MathF.PI / 4f)
                                    .Scale(0.25f, 0.25f, 0.25f)
                            }
                        ),
                        Transform = Matrix4.Identity.RotateZ(MathF.PI / 2f)
                            .Scale(0.25f, 0.25f, 0.25f)
                    },
                    SpecularReflection = 0.5f
                },
                Transform = Matrix4.Translation(0f, 1f, 0f)
            };

            var world = new World
            {
                Lights = ImmutableArray.Create(
                    new PointLight(new(-10f, 10f, -5f), Color.White),
                    new PointLight(new(0f, 3f, -10f), new Color(0.25f, 0.25f, 0.25f))
                ),
                Shapes = ImmutableArray.Create<Shape>(
                    floor,
                    leftWall,
                    rightWall,
                    sphere
                )
            };

            var camera = new Camera(1024, 768, MathF.PI / 4.5f)
            {
                Transform = Matrix4.ViewTransform(
                    new Point(-1.5f, 1.5f, -5f),
                    new Point(0f, 1f, 0f),
                    Vector.UnitY
                )
            };

            using var stream = File.OpenWrite("multi-sphere-scene.bmp");
            using var writer = new BinaryWriter(stream);
            camera.Render(world).SerializeToBitmap(writer);
        }
    }
}
