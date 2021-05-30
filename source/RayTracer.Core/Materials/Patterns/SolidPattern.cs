using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record SolidPattern(Color Color) : Pattern
    {
        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint) => Color;
    }
}
