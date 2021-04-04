using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public struct Point : IEquatable<Point>
    {
        public static readonly Point Zero = new(0f, 0f, 0f);

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(Point other)
            : this(other.X, other.Y, other.Z)
        {
        }

        public float X { get; init; }

        public float Y { get; init; }

        public float Z { get; init; }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Point other && Equals(other);

        /// <inheritdoc />
        public override string ToString() => $"Point({X}, {Y}, {Z})";

        /// <inheritdoc />
        public bool Equals(Point other) =>
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

        public void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public static bool operator ==(Point lhs, Point rhs) => lhs.Equals(rhs);

        public static bool operator !=(Point lhs, Point rhs) => !lhs.Equals(rhs);

        public static Point operator +(Point lhs, Vector rhs) =>
            new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);

        public static Point operator -(Point lhs, Vector rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

        public static Vector operator -(Point lhs, Point rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
    }
}
