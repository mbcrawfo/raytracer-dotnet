using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape : Transformable
    {
        public Material Material { get; init; } = Material.Default;

        public abstract IImmutableList<Intersection> Intersect(Ray ray);

        public abstract Vector NormalAt(in Point worldPoint);
    }
}
