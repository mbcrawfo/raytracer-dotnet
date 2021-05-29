using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Transformable
    {
        private readonly Matrix4 _transform = Matrix4.Identity;

        public Matrix4 Transform
        {
            get => _transform;
            init
            {
                _transform = value;
                TransformInverse = value.Inverse();
                TransformInverseTranspose = TransformInverse.Transpose();
            }
        }

        protected Matrix4 TransformInverse { get; private init; } = Matrix4.Identity;

        protected Matrix4 TransformInverseTranspose { get; private init; } =
            Matrix4.Identity.Transpose();
    }
}
