using System.Collections.Immutable;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record World
    {
        public static World Default =>
            new()
            {
                Lights = ImmutableArray.Create(new PointLight(new(-10f, 10f, -10f), Color.White)),
                Shapes = ImmutableArray.Create<Shape>(
                    new Sphere
                    {
                        Material = Material.Default with
                        {
                            Color = new(0.8f, 1f, 0.6f),
                            DiffuseReflection = 0.7f,
                            SpecularReflection = 0.2f
                        }
                    },
                    new Sphere { Transform = Matrix4.Scaling(0.5f, 0.5f, 0.5f) }
                )
            };

        public IImmutableList<PointLight> Lights { get; init; } = ImmutableArray<PointLight>.Empty;

        public IImmutableList<Shape> Shapes { get; init; } = ImmutableArray<Shape>.Empty;

        public Color ColorAt(Ray ray)
        {
            if (Intersect(ray).Hit() is not { } hit)
            {
                return Color.Black;
            }

            var computations = hit.PrepareComputations(ray);
            return ShadeHit(computations);
        }

        public IImmutableList<Intersection> Intersect(Ray ray)
        {
            var result = ImmutableList<Intersection>.Empty;

            for (var i = 0; i < Shapes.Count; i += 1)
            {
                result = result.AddRange(Shapes[i].Intersect(ray));
            }

            return result.Sort(Intersection.TimeComparer);
        }

        public Color ShadeHit(IntersectionComputations computations)
        {
            var result = Color.Black;

            for (var i = 0; i < Lights.Count; i += 1)
            {
                result += computations.Shape.Material.Lighting(
                    Lights[i],
                    computations.Point,
                    computations.Eye,
                    computations.Normal
                );
            }

            return result;
        }
    }
}
