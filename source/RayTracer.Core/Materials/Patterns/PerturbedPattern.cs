using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record PerturbedPattern(Pattern InnerPattern) : Pattern
    {
        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint)
        {
            var (x, y, z) = localPoint;
            x *= 0.9f;
            y *= 0.9f;
            z *= 0.9f;
            var jitteredPoint = new Point(
                x + PerlinNoise.Generate(x, y, z) * 0.1f,
                y + PerlinNoise.Generate(x, y, z + 1) * 0.1f,
                z + PerlinNoise.Generate(x, y, z + 2) * 0.1f
            );
            return InnerPattern.PatternColorAt(jitteredPoint);
        }
    }
}
