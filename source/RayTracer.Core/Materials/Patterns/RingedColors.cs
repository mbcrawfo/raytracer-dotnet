using System;
using System.Collections.Immutable;
using System.Diagnostics;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record RingedColors : Pattern
    {
        public RingedColors()
        {
        }

        public RingedColors(params Color[] colors)
        {
            Colors = colors.ToImmutableArray();
        }
        
        public IImmutableList<Color> Colors { get; init; } = ImmutableArray<Color>.Empty;

        /// <inheritdoc />
        public override Color ColorAt(in Point localPoint)
        {
            Debug.Assert(Colors.Count > 0, "Colors.Count > 0");

            var distance = MathF.Sqrt(localPoint.X * localPoint.X + localPoint.Z * localPoint.Z);
            var index = (int) MathF.Floor(distance) % Colors.Count;
            return Colors[index];
        }
    }
}
