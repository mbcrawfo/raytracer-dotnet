using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record SolidColor(Color Color) : Pattern
    {
        /// <inheritdoc />
        public override Color ColorAt(in Point localPoint) => Color;
    }
}
