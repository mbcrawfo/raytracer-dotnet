using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record SolidPattern(Color Color) : Pattern
    {
        /// <inheritdoc />
        public override Color ColorAt(in Point localPoint) => Color;
    }
}
