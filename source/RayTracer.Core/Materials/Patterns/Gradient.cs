using System;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record Gradient(Color Start, Color End) : Pattern
    {
        /// <inheritdoc />
        public override Color ColorAt(in Point localPoint)
        {
            var distance = End - Start;
            var fraction = localPoint.X - MathF.Floor(localPoint.X);
            return Start + distance * fraction;
        }
    }
}
