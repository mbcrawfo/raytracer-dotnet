using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape
    {
        public Matrix4 Transform { get; init; } = Matrix4.Identity;

        public abstract IImmutableList<Intersection> Intersect(Ray ray);
    }
}
