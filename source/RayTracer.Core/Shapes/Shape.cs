using System.Collections.Immutable;
using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Shape
    {
        private readonly Matrix4 _transform = Matrix4.Identity;

        public Matrix4 Transform
        {
            get => _transform;
            init
            {
                _transform = value;
                TransformInverse = value.Inverse();
            }
        }

        protected Matrix4 TransformInverse { get; init; } = Matrix4.Identity;

        public abstract IImmutableList<Intersection> Intersect(Ray ray);
    }
}
