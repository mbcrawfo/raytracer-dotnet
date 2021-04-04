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
        ///     The zero vector, with all components 0.
        /// </summary>
        public static readonly Tuple Zero = Vector(0.0, 0.0, 0.0);

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

        /// <summary>
        ///     Gets the W component of the tuple.
        /// </summary>
        public double W { get; private init; }

        /// <summary>
        ///     Gets the X component of the tuple.
        /// </summary>
        public double X { get; private init; }

        /// <summary>
        ///     Gets the Y component of the tuple.
        /// </summary>
        public double Y { get; private init; }

        /// <summary>
        ///     Gets the Z component of the tuple.
        /// </summary>
        public double Z { get; private init; }

        /// <summary>
        ///     Gets the type of the tuple, based on <see cref="W" />.
        /// </summary>
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

        /// <inheritdoc />
        public override string ToString()
        {
            var type = W switch
            {
                0.0 => "Vector",
                1.0 => "Point",
                _ => "InvalidTuple"
            };
            return $"{type}({X}, {Y}, {Z}, {W})";
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(W, X, Y, Z);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Tuple other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Tuple other) =>
            // W should never be affected by rounding
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            W == other.W &&
            X.ApproximatelyEquals(other.X) &&
            Y.ApproximatelyEquals(other.Y) &&
            Z.ApproximatelyEquals(other.Z);

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
    }
}
