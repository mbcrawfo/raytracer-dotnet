using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public readonly struct Point : IEquatable<Point>
    {
        private static readonly Point OriginValue = new(0f, 0f, 0f);

        public static ref readonly Point Origin => ref OriginValue;

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(in Point other)
            : this(other.X, other.Y, other.Z)
        {
        }

        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        /// <inheritdoc />
        public override int GetHashCode() =>
            throw new NotSupportedException(
                nameof(Point) +
                " is not suitable for use as a key because it relies on approximate equality"
            );

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Point other && Equals(other);

        /// <inheritdoc />
        public override string ToString() => $"Point({X}, {Y}, {Z})";

        /// <inheritdoc />
        public bool Equals(Point other) =>
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public static bool operator ==(in Point lhs, in Point rhs) => lhs.Equals(rhs);

        public static bool operator !=(in Point lhs, in Point rhs) => !lhs.Equals(rhs);

        public static Point operator +(in Point lhs, in Vector rhs) =>
            new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);

        public static Point operator -(in Point lhs, in Vector rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

        public static Vector operator -(in Point lhs, in Point rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
    }
}
