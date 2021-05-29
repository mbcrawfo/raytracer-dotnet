using RayTracer.Core.Math;

namespace RayTracer.Core.Shapes
{
    public abstract record Transformable
    {
        private readonly Matrix4 _transform = Matrix4.Identity;
        private readonly Matrix4 _transformInverse = Matrix4.Identity;
        private readonly Matrix4 _transformInverseTranspose = Matrix4.Identity;

        public Matrix4 Transform
        {
            get => _transform;
            init
            {
                _transform = value;
                _transformInverse = _transform.Inverse();
                _transformInverseTranspose = _transformInverse.Transpose();
            }
        }

        public Vector LocalVectorToWorldVector(in Vector localVector) =>
            _transformInverseTranspose * localVector;

        public Point WorldPointToLocalPoint(in Point worldPoint) => _transformInverse * worldPoint;

        public Ray WorldRayToLocalRay(Ray worldRay) => worldRay.Transform(_transformInverse);
    }
}
