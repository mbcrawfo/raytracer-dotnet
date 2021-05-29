using System.Collections.Immutable;
using RayTracer.Core.Materials;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape : Transformable
    {
        public Material Material { get; init; } = Material.Default;

        public IImmutableList<Intersection> Intersect(Ray worldRay)
        {
            var localRay = WorldRayToLocalRay(worldRay);
            return LocalIntersect(localRay);
        }

        public abstract IImmutableList<Intersection> LocalIntersect(Ray localRay);

        public abstract Vector LocalNormalAt(in Point localPoint);

        public Vector NormalAt(in Point worldPoint)
        {
            var localPoint = WorldPointToLocalPoint(worldPoint);
            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = LocalVectorToWorldVector(localNormal);
            return worldNormal.Normalize();
        }
    }
}
