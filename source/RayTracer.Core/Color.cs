using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core
{
    public struct Color : IEquatable<Color>
    {
        public static readonly Color Black = new(0f, 0f, 0f);
        public static readonly Color Blue = new(0f, 0f, 1f);
        public static readonly Color Green = new(0f, 1f, 0f);
        public static readonly Color Red = new(1f, 0f, 0f);
        public static readonly Color White = new(1f, 1f, 1f);
        
        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(Color other)
            : this(other.R, other.G, other.B)
        {
        }

        public float R { get; init; }

        public float G { get; init; }

        public float B { get; init; }

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Color other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(R, G, B);

        /// <inheritdoc />
        public override string ToString() => $"Color({R}, {G}, {B})";

        /// <inheritdoc />
        public bool Equals(Color other) =>
            R.ApproximatelyEquals(other.R) &&
            G.ApproximatelyEquals(other.G) &&
            B.ApproximatelyEquals(other.B);

        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        public static bool operator ==(Color lhs, Color rhs) => lhs.Equals(rhs);

        public static bool operator !=(Color lhs, Color rhs) => !lhs.Equals(rhs);
    }
}
