using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracer.Core.Math
{
    public sealed class Matrix4 : Matrix
    {
        private const int Size = 4;

        public static readonly Matrix4 Identity = new()
        {
            [0, 0] = 1f,
            [1, 1] = 1f,
            [2, 2] = 1f,
            [3, 3] = 1f
        };

        public Matrix4()
            : base(Size, Size)
        {
        }

        public Matrix4(float[,] elements)
            : base(elements)
        {
            if (elements.GetLength(0) != Size || elements.GetLength(1) != Size)
            {
                throw new ArgumentException(
                    $"Value must be a {Size}x{Size} array",
                    nameof(elements)
                );
            }
        }

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public Matrix4(Matrix4 other)
            : base(other)
        {
        }

        /// <inheritdoc />
        public override float Determinant() => throw new NotImplementedException();

        /// <inheritdoc />
        public override Matrix3 SubMatrix(int rowToRemove, int columnToRemove)
        {
            if (rowToRemove is < 0 or >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(rowToRemove),
                    rowToRemove,
                    $"Value must be in range [0, {Size})"
                );
            }

            if (columnToRemove is < 0 or >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(columnToRemove),
                    columnToRemove,
                    $"Value must be in range [0, {Size})"
                );
            }

            return new Matrix3(SubMatrixElements(rowToRemove, columnToRemove));
        }

        /// <inheritdoc />
        public override Matrix4 Transpose() => new(TransposeElements());

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            var result = new Matrix4();

            for (var i = 0; i < Size; i += 1)
            {
                for (var j = 0; j < Size; j += 1)
                {
                    result.Elements[i, j] =
                        lhs.Elements[i, 0] * rhs.Elements[0, j] +
                        lhs.Elements[i, 1] * rhs.Elements[1, j] +
                        lhs.Elements[i, 2] * rhs.Elements[2, j] +
                        lhs.Elements[i, 3] * rhs.Elements[3, j];
                }
            }

            return result;
        }

        public static Point operator *(Matrix4 lhs, in Point rhs) =>
            new(
                lhs.Elements[0, 0] * rhs.X +
                lhs.Elements[0, 1] * rhs.Y +
                lhs.Elements[0, 2] * rhs.Z +
                lhs.Elements[0, 3],
                lhs.Elements[1, 0] * rhs.X +
                lhs.Elements[1, 1] * rhs.Y +
                lhs.Elements[1, 2] * rhs.Z +
                lhs.Elements[1, 3],
                lhs.Elements[2, 0] * rhs.X +
                lhs.Elements[2, 1] * rhs.Y +
                lhs.Elements[2, 2] * rhs.Z +
                lhs.Elements[2, 3]
            );

        public static Vector operator *(Matrix4 lhs, in Vector rhs) =>
            new(
                lhs.Elements[0, 0] * rhs.X +
                lhs.Elements[0, 1] * rhs.Y +
                lhs.Elements[0, 2] * rhs.Z,
                lhs.Elements[1, 0] * rhs.X +
                lhs.Elements[1, 1] * rhs.Y +
                lhs.Elements[1, 2] * rhs.Z,
                lhs.Elements[2, 0] * rhs.X +
                lhs.Elements[2, 1] * rhs.Y +
                lhs.Elements[2, 2] * rhs.Z
            );
    }
}
