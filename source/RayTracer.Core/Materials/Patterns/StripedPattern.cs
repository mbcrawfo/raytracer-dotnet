using System;
using System.Collections.Immutable;
using System.Diagnostics;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials.Patterns
{
    public sealed record StripedPattern : Pattern
    {
        public IImmutableList<Pattern> Patterns { get; init; } = ImmutableArray<Pattern>.Empty;

        /// <inheritdoc />
        protected override Color LocalColorAt(in Point localPoint)
        {
            Debug.Assert(Patterns.Count > 0, "Patterns.Count > 0");

            var index = (int) MathF.Abs(MathF.Floor(localPoint.X)) % Patterns.Count;
            return Patterns[index].PatternColorAt(localPoint);
        }
    }
}
