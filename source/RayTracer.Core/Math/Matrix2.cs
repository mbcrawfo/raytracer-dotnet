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

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    _elements[i, j] = elements[i, j];
                }
            }
        }

        public Matrix2(Matrix2 other)
            : this(other._elements)
        {
        }

        public float this[int i, int j]
        {
            get
            {
                if (i is < 0 or >= Size || j is < 0 or >= Size)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{i}, {j}] is not valid for a 2x2 matrix",
                        (Exception?) null
                    );
                }

                return _elements[i, j];
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

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (!_elements[i, j].ApproximatelyEquals(other._elements[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            throw new NotSupportedException(
                nameof(Matrix2) +
                " is not suitable for use as a key because it relies on approximate equality"
            );

        public static bool operator ==(Matrix2 lhs, Matrix2 rhs) => lhs.Equals(rhs);

        public static bool operator !=(Matrix2 lhs, Matrix2 rhs) => !lhs.Equals(rhs);
    }
}
