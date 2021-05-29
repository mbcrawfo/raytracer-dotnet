using System.Collections.Immutable;
using RayTracer.Core.Extensions;
using RayTracer.Core.Materials;
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

            var computations = new IntersectionComputations(ray, hit);
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

        internal bool IsInShadow(in Point point, PointLight light)
        {
            var lightVector = light.Position - point;
            var lightDistance = lightVector.Magnitude();
            var lightRay = new Ray(point, lightVector.Normalize());

            return Intersect(lightRay).Hit() is { } hit && hit.Time < lightDistance;
        }

        internal Color ShadeHit(IntersectionComputations computations)
        {
            var result = Color.Black;

            for (var i = 0; i < Lights.Count; i += 1)
            {
                var light = Lights[i];
                var inShadow = IsInShadow(computations.OverPoint, light);
                
                result += computations.Shape.Material.Lighting(
                    light,
                    computations.Point,
                    computations.Eye,
                    computations.Normal,
                    inShadow
                );
            }

            return result;
        }
    }
}
