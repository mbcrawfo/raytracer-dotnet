using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public readonly struct Vector : IEquatable<Vector>
    {
        private static readonly Vector UnitXValue = new(1f, 0f, 0f);
        private static readonly Vector UnitYValue = new(0f, 1f, 0f);
        private static readonly Vector UnitZValue = new(0f, 0f, 1f);
        private static readonly Vector ZeroValue = new(0f, 0f, 0f);

        public static ref readonly Vector UnitX => ref UnitXValue;

        public static ref readonly Vector UnitY => ref UnitYValue;

        public static ref readonly Vector UnitZ => ref UnitZValue;

        public static ref readonly Vector Zero => ref ZeroValue;

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(in Vector other)
            : this(other.X, other.Y, other.Z)
        {
        }

        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        /// <inheritdoc />
        public override int GetHashCode() =>
            throw new NotSupportedException(
                nameof(Vector) +
                " is not suitable for use as a key because it relies on approximate equality"
            );

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Vector other && Equals(other);

        /// <inheritdoc />
        public override string ToString() => $"Vector({X}, {Y}, {Z})";

        /// <inheritdoc />
        public bool Equals(Vector other) =>
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

        public Vector CrossProduct(in Vector other) =>
            new(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );

        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public float DotProduct(in Vector other) => X * other.X + Y * other.Y + Z * other.Z;

        public float Magnitude() => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public Vector Normalize()
        {
            var magnitude = Magnitude();
            return new Vector(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public Vector Reflect(in Vector normal) => this - normal * 2f * DotProduct(normal);

        public static Vector operator -(in Vector other) => new(-other.X, -other.Y, -other.Z);

        public static bool operator ==(in Vector lhs, in Vector rhs) => lhs.Equals(rhs);

        public static bool operator !=(in Vector lhs, in Vector rhs) => !lhs.Equals(rhs);

        public static Vector operator +(in Vector lhs, in Vector rhs) =>
            new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);

        public static Vector operator -(in Vector lhs, in Vector rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

        public static Vector operator *(in Vector lhs, float rhs) =>
            new(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);

        public static Vector operator /(in Vector lhs, float rhs)
        {
            if (rhs is 0f)
            {
                throw new DivideByZeroException("Attempt to divide a vector by 0");
            }

            return new Vector(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }
    }
}
