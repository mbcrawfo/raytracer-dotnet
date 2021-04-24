using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record Intersection(Shape Shape, float Time)
    {
        public static int TimeComparer(Intersection a, Intersection b) => a.Time.CompareTo(b.Time);
    }
}
