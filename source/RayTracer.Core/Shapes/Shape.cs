using System.Collections.Immutable;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape
    {
        public abstract IImmutableList<Intersection> Intersect(Ray ray);
    }
}
