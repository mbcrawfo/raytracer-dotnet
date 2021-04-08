using System;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public sealed class Matrix2 : IEquatable<Matrix2>
    {
        private const int Size = 2;

        private readonly float[,] _elements;

        public Matrix2(float m00, float m01, float m10, float m11)
        {
            _elements = new[,] { { m00, m01 }, { m10, m11 } };
        }

        public Matrix2(float[,] elements)
        {
            if (elements.GetLength(0) != Size || elements.GetLength(1) != Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(elements),
                    elements,
                    "Value must be a 2x2 array"
                );
            }

            _elements = new float[Size, Size];

            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    _elements[x, y] = elements[x, y];
                }
            }
        }

        public Matrix2(Matrix2 other)
            : this(other._elements)
        {
        }

        public float this[int x, int y]
        {
            get
            {
                if (x is < 0 or >= Size || y is < 0 or >= Size)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{x}, {y}] is not valid for a 2x2 matrix",
                        (Exception?) null
                    );
                }

                return _elements[x, y];
            }
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Matrix2);

        /// <inheritdoc />
        public bool Equals(Matrix2? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    if (!_elements[x, y].ApproximatelyEquals(other._elements[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(_elements[0, 0], _elements[0, 1], _elements[1, 0], _elements[1, 1]);

        public static bool operator ==(Matrix2 lhs, Matrix2 rhs) => lhs.Equals(rhs);

        public static bool operator !=(Matrix2 lhs, Matrix2 rhs) => !lhs.Equals(rhs);
    }
}
