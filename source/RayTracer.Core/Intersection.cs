using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record Intersection(Shape Shape, float Time);
}
