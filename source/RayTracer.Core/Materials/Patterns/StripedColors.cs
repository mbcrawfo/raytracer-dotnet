using System;
using System.Collections.Immutable;
using System.Diagnostics;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record StripedColors : Pattern
    {
        public StripedColors()
        {
        }

        public StripedColors(params Color[] colors)
        {
            Colors = colors.ToImmutableArray();
        }

        public IImmutableList<Color> Colors { get; init; } = ImmutableArray<Color>.Empty;

        /// <inheritdoc />
        public override Color ColorAt(in Point localPoint)
        {
            Debug.Assert(Colors.Count > 0, "Colors.Count > 0");

            var index = (int) MathF.Abs(MathF.Floor(localPoint.X)) % Colors.Count;
            return Colors[index];
        }
    }
}
