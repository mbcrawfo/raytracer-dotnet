using RayTracer.Core.Extensions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record IntersectionComputations
    {
        public IntersectionComputations(Ray ray, Intersection intersection)
        {
            Shape = intersection.Shape;
            Time = intersection.Time;

            Point = ray.Position(Time);
            Eye = -ray.Direction;
            Normal = Shape.NormalAt(Point);

            if (Normal.DotProduct(Eye) < 0f)
            {
                Normal = -Normal;
                HitInside = true;
            }

            // Larger epsilon is required here to prevent acne, seems to be related to 
            // something funky with extremely deform spheres
            OverPoint = Point + Normal * FloatExtensions.ComparisonEpsilon * 10f;
        }

        public Vector Eye { get; }

        public bool HitInside { get; }

        public Vector Normal { get; }

        public Point OverPoint { get; }

        public Point Point { get; }

        public Shape Shape { get; }

        public float Time { get; }
    }
}
