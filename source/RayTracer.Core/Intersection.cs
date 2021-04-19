using System.Collections.Generic;
using System.Diagnostics;
using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record Intersection(Shape Shape, float Time)
    {
        public static readonly IComparer<Intersection> TimeComparer = new TimeComparerImpl();

        public IntersectionComputations PrepareComputations(Ray ray)
        {
            var point = ray.Position(Time);
            var eye = -ray.Direction;
            var normal = Shape.NormalAt(point);
            var inside = false;

            if (normal.DotProduct(eye) < 0f)
            {
                normal = -normal;
                inside = true;
            }

            return new IntersectionComputations(Shape, Time, point, eye, normal, inside);
        }

        private sealed class TimeComparerImpl : IComparer<Intersection>
        {
            /// <inheritdoc />
            public int Compare(Intersection? x, Intersection? y)
            {
                Debug.Assert(x != null, nameof(x) + " != null");
                Debug.Assert(y != null, nameof(y) + " != null");
                return x.Time.CompareTo(y.Time);
            }
        }
    }
}
