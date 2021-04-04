using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public struct Vector : IEquatable<Vector>
    {
        public static readonly Vector UnitX = new(1f, 0f, 0f);
        public static readonly Vector UnitY = new(0f, 1f, 0f);
        public static readonly Vector UnitZ = new(0f, 0f, 1f);
        public static readonly Vector Zero = new(0f, 0f, 0f);

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(Vector other)
            : this(other.X, other.Y, other.Z)
        {
        }

        public float X { get; init; }

        public float Y { get; init; }

        public float Z { get; init; }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Vector other && Equals(other);

        /// <inheritdoc />
        public override string ToString() => $"Vector({X}, {Y}, {Z})";

        /// <inheritdoc />
        public bool Equals(Vector other) =>
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

        public Vector CrossProduct(Vector other) =>
            new(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );

        public void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public float DotProduct(Vector other) => X * other.X + Y * other.Y + Z * other.Z;

        public float Magnitude() => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public Vector Normalize()
        {
            var magnitude = Magnitude();
            return new Vector(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public static Vector operator -(Vector other) => new(-other.X, -other.Y, -other.Z);

        public static bool operator ==(Vector lhs, Vector rhs) => lhs.Equals(rhs);

        public static bool operator !=(Vector lhs, Vector rhs) => !lhs.Equals(rhs);

        public static Vector operator +(Vector lhs, Vector rhs) =>
            new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        
        public static Vector operator -(Vector lhs, Vector rhs) =>
            new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

        public static Vector operator *(Vector lhs, float rhs) =>
            new(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);

        public static Vector operator /(Vector lhs, float rhs)
        {
            if (rhs is 0f)
            {
                throw new DivideByZeroException("Attempt to divide a vector by 0");
            }

            return new Vector(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }
    }
}
