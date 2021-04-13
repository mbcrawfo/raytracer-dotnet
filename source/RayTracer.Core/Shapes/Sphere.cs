using System;
using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public record Sphere
    {
        public Point Position { get; } = Point.Origin;

        public float Radius { get; } = 1f;

        public IImmutableList<float> Intersect(Ray ray)
        {
            var sphereToRay = ray.Origin - Position;
            var a = ray.Direction.DotProduct(ray.Direction);
            var b = 2f * ray.Direction.DotProduct(sphereToRay);
            var c = sphereToRay.DotProduct(sphereToRay) - Radius;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0f)
            {
                return ImmutableArray<float>.Empty;
            }

            var t1 = (-b - MathF.Sqrt(discriminant)) / (2f * a);
            var t2 = (-b + MathF.Sqrt(discriminant)) / (2f * a);

            return t1 <= t2 ? ImmutableArray.Create(t1, t2) : ImmutableArray.Create(t2, t1);
        }
    }
}
