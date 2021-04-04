using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core
{
    /// <summary>
    ///     A tuple representing a point or a vector.
    /// </summary>
    public struct Tuple : IEquatable<Tuple>
    {
        /// <summary>
        ///     The X axis unit vector.
        /// </summary>
        public static readonly Tuple UnitX = Vector(1.0, 0.0, 0.0);

        /// <summary>
        ///     The Y axis unit vector.
        /// </summary>
        public static readonly Tuple UnitY = Vector(0.0, 1.0, 0.0);

        /// <summary>
        ///     The Z axis unit vector.
        /// </summary>
        public static readonly Tuple UnitZ = Vector(0.0, 0.0, 1.0);

        /// <summary>
        ///     The zero vector, with all components 0.
        /// </summary>
        public static readonly Tuple Zero = Vector(0.0, 0.0, 0.0);

        /// <summary>
        ///     Gets the type of the tuple, based on <see cref="W" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The W component of this tuple has an unexpected value.
        /// </exception>
        public TupleType Type =>
            W switch
            {
                0.0 => TupleType.Vector,
                1.0 => TupleType.Point,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(W),
                    W,
                    nameof(W) + " must have a value of exactly 0 or 1"
                )
            };

        /// <summary>
        ///     Gets the W component of this tuple.
        /// </summary>
        public double W { get; private init; }

        /// <summary>
        ///     Gets the X component of this tuple.
        /// </summary>
        public double X { get; private init; }

        /// <summary>
        ///     Gets the Y component of this tuple.
        /// </summary>
        public double Y { get; private init; }

        /// <summary>
        ///     Gets the Z component of this tuple.
        /// </summary>
        public double Z { get; private init; }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(W, X, Y, Z);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Tuple other && Equals(other);

        /// <inheritdoc />
        public override string ToString()
        {
            // Does not call Type since tests may call ToString on a tuple that has been
            // intentionally made invalid
            var type = W switch
            {
                0.0 => "Vector",
                1.0 => "Point",
                _ => "InvalidTuple"
            };
            return $"{type}({X}, {Y}, {Z}, {W})";
        }

        /// <inheritdoc />
        public bool Equals(Tuple other) =>
            // W should never be affected by rounding
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            W == other.W &&
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

        /// <summary>
        ///     Calculates the cross product of this tuple with <paramref name="other"/>, if both
        ///     tuples are vectors.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Tuple CrossProduct(Tuple other) =>
            (W + other.W) switch
            {
                0.0 => Vector(
                    Y * other.Z - Z * other.Y,
                    Z * other.X - X * other.Z,
                    X * other.Y - Y * other.X
                ),
                _ => throw new InvalidOperationException(
                    nameof(CrossProduct) + " is only valid when both tuples are vectors"
                )
            };

        /// <summary>
        ///     Calculates the dot product of this tuple with <paramref name="other" />, if both
        ///     tuples are vectors.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The tuple is not a vector.
        /// </exception>
        public double DotProduct(Tuple other) =>
            (W + other.W) switch
            {
                0.0 => X * other.X + Y * other.Y + Z * other.Z,
                _ => throw new InvalidOperationException(
                    nameof(DotProduct) + " is only valid when both tuples are vectors"
                )
            };

        /// <summary>
        ///     Calculates the magnited of this tuple, if it is a vector.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The tuple is not a vector.
        /// </exception>
        public double Magnitude() =>
            W switch
            {
                0.0 => Math.Sqrt(X * X + Y * Y + Z * Z),
                _ => throw new InvalidOperationException(
                    nameof(Magnitude) + " is only valid on a vector"
                )
            };

        /// <summary>
        ///     Generates a unit vector representing the current tuple's direction, if it is a
        ///     vector.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The tuple is not a vector.
        /// </exception>
        public Tuple Normalize()
        {
            var magnitude = Math.Sqrt(X * X + Y * Y + Z * Z);
            return W switch
            {
                0.0 => Vector(X / magnitude, Y / magnitude, Z / magnitude),
                _ => throw new InvalidOperationException(
                    nameof(Normalize) + " is only valid on a vector"
                )
            };
        }

        /// <summary>
        ///     Construct a new <see cref="Tuple" /> representing a vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Tuple Vector(double x, double y, double z) =>
            new()
            {
                W = 0.0,
                X = x,
                Y = y,
                Z = z
            };

        /// <summary>
        ///     Construct a new <see cref="Tuple" /> representing a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Tuple Point(double x, double y, double z) =>
            new()
            {
                W = 1.0,
                X = x,
                Y = y,
                Z = z
            };

        public static bool operator ==(Tuple left, Tuple right) => left.Equals(right);

        public static bool operator !=(Tuple left, Tuple right) => !left.Equals(right);

        public static Tuple operator +(Tuple lhs, Tuple rhs)
        {
            var result = new Tuple
            {
                W = lhs.W + rhs.W,
                X = lhs.X + rhs.X,
                Y = lhs.Y + rhs.Y,
                Z = lhs.Z + rhs.Z,
            };

            return result.W switch
            {
                0.0 or 1.0 => result,
                _ => throw new InvalidOperationException(
                    "Adding two points is not a valid operation"
                )
            };
        }

        public static Tuple operator -(Tuple lhs, Tuple rhs)
        {
            var result = new Tuple
            {
                W = lhs.W - rhs.W,
                X = lhs.X - rhs.X,
                Y = lhs.Y - rhs.Y,
                Z = lhs.Z - rhs.Z,
            };

            return result.W switch
            {
                0.0 or 1.0 => result,
                _ => throw new InvalidOperationException(
                    $"Unexpected result when performing {lhs} - {rhs}"
                )
            };
        }

        public static Tuple operator -(Tuple tuple) =>
            new()
            {
                W = tuple.W,
                X = -tuple.X,
                Y = -tuple.Y,
                Z = -tuple.Z
            };

        public static Tuple operator *(Tuple lhs, double rhs) =>
            new()
            {
                W = lhs.W,
                X = lhs.X * rhs,
                Y = lhs.Y * rhs,
                Z = lhs.Z * rhs
            };

        public static Tuple operator /(Tuple lhs, double rhs)
        {
            if (rhs is 0.0)
            {
                throw new DivideByZeroException("Attempt to divide a tuple by 0");
            }

            return new Tuple
            {
                W = lhs.W,
                X = lhs.X / rhs,
                Y = lhs.Y / rhs,
                Z = lhs.Z / rhs
            };
        }
    }
}
