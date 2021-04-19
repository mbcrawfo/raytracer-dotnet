using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record IntersectionComputations(
        Shape Shape,
        float Time,
        Point Point,
        Vector Eye,
        Vector Normal,
        bool HitInside
    );
}
