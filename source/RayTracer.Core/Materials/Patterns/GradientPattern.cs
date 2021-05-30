using System;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record GradientPattern(Color Start, Color End) : Pattern
    {
        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint)
        {
            var distance = End - Start;
            var fraction = localPoint.X - MathF.Floor(localPoint.X);
            return Start + distance * fraction;
        }
    }
}
