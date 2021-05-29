using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape : Transformable
    {
        public Material Material { get; init; } = Material.Default;

        public IImmutableList<Intersection> Intersect(Ray worldRay)
        {
            var localRay = worldRay.Transform(TransformInverse);
            return LocalIntersect(localRay);
        }

        public Vector NormalAt(in Point worldPoint)
        {
            var localPoint = TransformInverse * worldPoint;
            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = TransformInverseTranspose * localNormal;
            return worldNormal.Normalize();
        }

        public abstract IImmutableList<Intersection> LocalIntersect(Ray localRay);

        public abstract Vector LocalNormalAt(in Point localPoint);
    }
}
