using System;
using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public record Sphere : Shape
    {
        public Point Position { get; } = Point.Origin;

        public float Radius { get; } = 1f;

        public override IImmutableList<Intersection> Intersect(Ray ray)
        {
            var (origin, direction) = ray.Transform(TransformInverse);

            var sphereToRay = origin - Position;
            var a = direction.DotProduct(direction);
            var b = 2f * direction.DotProduct(sphereToRay);
            var c = sphereToRay.DotProduct(sphereToRay) - Radius;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0f)
            {
                return ImmutableArray<Intersection>.Empty;
            }

            var t1 = (-b - MathF.Sqrt(discriminant)) / (2f * a);
            var t2 = (-b + MathF.Sqrt(discriminant)) / (2f * a);

            var i1 = new Intersection(this, t1);
            var i2 = new Intersection(this, t2);

            return t1 <= t2 ? ImmutableArray.Create(i1, i2) : ImmutableArray.Create(i2, i1);
        }

        public Vector NormalAt(in Point worldPoint)
        {
            var objectPoint = TransformInverse * worldPoint;
            var objectNormal = objectPoint - Point.Origin;
            var worldNormal = TransformInverse.Transpose() * objectNormal;
            return worldNormal.Normalize();
        }
    }
}
