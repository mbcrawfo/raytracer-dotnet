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
    internal static class Chapter9Planes
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

            var middleSphere = new Sphere
            {
                Material = Material.Default with
                {
                    DiffuseReflection = 0.7f,
                    Pattern = new SolidPattern(new Color(0.15f, 0.15f, 1f)),
                    SpecularReflection = 0.5f
                },
                Transform = Matrix4.Translation(-0.5f, 1f, 0.5f)
            };

            var rightSphere = new Sphere
            {
                Material = Material.Default with
                {
                    DiffuseReflection = 0.7f,
                    Pattern = new SolidPattern(new Color(0.1f, 1f, 0.1f)),
                    SpecularReflection = 0.3f
                },
                Transform = Matrix4.Identity
                    .Scale(0.5f, 0.5f, 0.5f)
                    .Translate(1.5f, 0.5f, -0.5f)
            };

            var leftSphere = new Sphere
            {
                Material = Material.Default with
                {
                    DiffuseReflection = 0.7f,
                    Pattern = new SolidPattern(new Color(1f, 0.1f, 0.1f)),
                    SpecularReflection = 0.3f
                },
                Transform = Matrix4.Identity
                    .Scale(0.33f, 0.33f, 0.33f)
                    .Translate(-1.5f, 0.33f, -0.75f)
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
                    middleSphere,
                    rightSphere,
                    leftSphere
                )
            };

            var camera = new Camera(1024, 768, MathF.PI / 3f)
            {
                Transform = Matrix4.ViewTransform(
                    new Point(0f, 1.5f, -5f),
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
