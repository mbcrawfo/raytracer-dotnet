using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record Intersection(Shape Shape, float Time)
    {
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

        public static int TimeComparer(Intersection a, Intersection b) => a.Time.CompareTo(b.Time);
    }
}
