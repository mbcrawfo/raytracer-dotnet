using System;
using System.Collections.Immutable;
using System.Diagnostics;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record RingedPattern : Pattern
    {
        public RingedPattern()
        {
        }

        public RingedPattern(params Color[] colors)
        {
            Colors = colors.ToImmutableArray();
        }
        
        public IImmutableList<Color> Colors { get; init; } = ImmutableArray<Color>.Empty;

        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint)
        {
            Debug.Assert(Colors.Count > 0, "Colors.Count > 0");

            var distance = MathF.Sqrt(localPoint.X * localPoint.X + localPoint.Z * localPoint.Z);
            var index = (int) MathF.Floor(distance) % Colors.Count;
            return Colors[index];
        }
    }
}
