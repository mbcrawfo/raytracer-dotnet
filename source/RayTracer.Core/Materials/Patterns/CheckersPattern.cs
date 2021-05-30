using System;
using System.Collections.Immutable;
using System.Diagnostics;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record CheckersPattern : Pattern
    {
        public CheckersPattern()
        {
        }

        public CheckersPattern(params Color[] colors)
        {
            Colors = colors.ToImmutableArray();
        }

        public IImmutableList<Color> Colors { get; init; } = ImmutableArray<Color>.Empty;

        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint)
        {
            Debug.Assert(Colors.Count > 0, "Colors.Count > 0");

            var distance = MathF.Floor(localPoint.X) +
                MathF.Floor(localPoint.Y) +
                MathF.Floor(localPoint.Z);
            var index = (int) MathF.Abs(distance) % Colors.Count;
            return Colors[index];
        }
    }
}
