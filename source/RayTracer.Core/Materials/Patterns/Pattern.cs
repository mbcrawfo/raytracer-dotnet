using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core.Materials.Patterns
{
    public abstract record Pattern : Transformable
    {
        public abstract Color ColorAt(in Point localPoint);
    }
}
