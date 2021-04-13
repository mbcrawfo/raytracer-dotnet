using RayTracer.Core.Math;

namespace RayTracer.Core
{
    public sealed record Ray
    {
        public Ray()
        {
        }

        public Ray(in Point origin, in Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector Direction { get; init; }

        public Point Origin { get; init; }

        public void Deconstruct(out Point origin, out Vector direction)
        {
            origin = Origin;
            direction = Direction;
        }

        public Point Position(float time) => Origin + Direction * time;
    }
}
