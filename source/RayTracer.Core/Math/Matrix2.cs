using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public readonly struct Matrix2 : IEquatable<Matrix2>
    {
        private readonly float _m00;
        private readonly float _m01;
        private readonly float _m10;
        private readonly float _m11;

        public Matrix2(float m00, float m01, float m10, float m11)
        {
            _m00 = m00;
            _m01 = m01;
            _m10 = m10;
            _m11 = m11;
        }

        public Matrix2(Matrix2 other)
            : this(other._m00, other._m01, other._m10, other._m11)
        {
        }

        public float this[int x, int y] =>
            (x, y) switch
            {
                (0, 0) => _m00,
                (0, 1) => _m01,
                (1, 0) => _m10,
                (1, 1) => _m11,
                _ => throw new ArgumentOutOfRangeException(
                    $"[{x}, {y}] is not valid for a 2x2 matrix",
                    (Exception?) null
                )
            };
        
        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Matrix2 other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(_m00, _m01, _m10, _m11);

        /// <inheritdoc />
        public bool Equals(Matrix2 other) =>
            _m00.ApproximatelyEquals(other._m00) &&
            _m01.ApproximatelyEquals(other._m01) &&
            _m10.ApproximatelyEquals(other._m10) &&
            _m11.ApproximatelyEquals(other._m11);
        
        public static bool operator ==(Matrix2 lhs, Matrix2 rhs) => lhs.Equals(rhs);
        
        public static bool operator !=(Matrix2 lhs, Matrix2 rhs) => !lhs.Equals(rhs);
    }
}
