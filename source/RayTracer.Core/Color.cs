using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core
{
    public readonly struct Color : IEquatable<Color>
    {
        private static readonly Color BlackValue = new(0f, 0f, 0f);
        private static readonly Color BlueValue = new(0f, 0f, 1f);
        private static readonly Color GreenValue = new(0f, 1f, 0f);
        private static readonly Color RedValue = new(1f, 0f, 0f);
        private static readonly Color WhiteValue = new(1f, 1f, 1f);

        public static ref readonly Color Black => ref BlackValue;

        public static ref readonly Color Blue => ref BlueValue;

        public static ref readonly Color Green => ref GreenValue;

        public static ref readonly Color Red => ref RedValue;

        public static ref readonly Color White => ref WhiteValue;

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(in Color other)
            : this(other.R, other.G, other.B)
        {
        }

        public float R { get; }

        public float G { get; }

        public float B { get; }

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Color other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() =>
            throw new NotSupportedException(
                nameof(Color) +
                " is not suitable for use as a key because it relies on approximate equality"
            );

        /// <inheritdoc />
        public override string ToString() => $"Color({R}, {G}, {B})";

        /// <inheritdoc />
        public bool Equals(Color other) =>
            R.ApproximatelyEquals(other.R) &&
            G.ApproximatelyEquals(other.G) &&
            B.ApproximatelyEquals(other.B);

        /// <summary>
        ///     Returns a new <see cref="Color" /> with all components clamped to the range [0, 1].
        /// </summary>
        /// <returns></returns>
        public Color Clamp() => new(R.Clamp(0f, 1f), G.Clamp(0f, 1f), B.Clamp(0f, 1f));

        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        public Color HadamardProduct(in Color other) => new(R * other.R, G * other.G, B * other.B);

        public static bool operator ==(in Color lhs, in Color rhs) => lhs.Equals(rhs);

        public static bool operator !=(in Color lhs, in Color rhs) => !lhs.Equals(rhs);

        public static Color operator +(in Color lhs, in Color rhs) =>
            new(lhs.R + rhs.R, lhs.G + rhs.G, lhs.B + rhs.B);

        public static Color operator -(in Color lhs, in Color rhs) =>
            new(lhs.R - rhs.R, lhs.G - rhs.G, lhs.B - rhs.B);

        public static Color operator *(in Color lhs, float rhs) =>
            new(lhs.R * rhs, lhs.G * rhs, lhs.B * rhs);
    }
}
