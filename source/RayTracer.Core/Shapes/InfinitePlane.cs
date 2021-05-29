using System.Collections.Immutable;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public record InfinitePlane : Shape
    {
        public Vector Normal { get; init; } = Vector.UnitY;

        /// <inheritdoc />
        public override IImmutableList<Intersection> LocalIntersect(Ray localRay)
        {
            var (origin, direction) = localRay;
            var denominator = Normal.DotProduct(direction);

            // the ray is parallel to the plane
            if (denominator.ApproximatelyEquals(0f))
            {
                return ImmutableArray<Intersection>.Empty;
            }

            var time = -(Normal.X * origin.X + Normal.Y * origin.Y + Normal.Z * origin.Z) /
                denominator;
            return ImmutableList.Create(new Intersection(this, time));
        }

        /// <inheritdoc />
        public override Vector LocalNormalAt(in Point localPoint) => Normal;
    }
}
