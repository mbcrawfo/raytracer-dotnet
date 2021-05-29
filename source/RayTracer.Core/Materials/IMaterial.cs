using RayTracer.Core.Math;

namespace RayTracer.Core.Materials
{
    public interface IMaterial
    {
        Color Lighting(
            PointLight light,
            in Point point,
            in Vector eye,
            in Vector normal,
            bool pointLiesInShadow
        );
    }
}
