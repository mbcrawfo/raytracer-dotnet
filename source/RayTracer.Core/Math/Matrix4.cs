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
        public override Matrix4 Inverse() => new(InverseElements());

        public Matrix4 RotateX(float radians) => RotationX(radians) * this;

        public Matrix4 RotateY(float radians) => RotationY(radians) * this;

        public Matrix4 RotateZ(float radians) => RotationZ(radians) * this;

        public Matrix4 Scale(float x, float y, float z) => Scaling(x, y, z) * this;

        public Matrix4 Shear(
            float xToY = 0f,
            float xToZ = 0f,
            float yToX = 0f,
            float yToZ = 0f,
            float zToX = 0f,
            float zToY = 0f
        ) =>
            Shearing(xToY, xToZ, yToX, yToZ, zToX, zToY) * this;

        /// <inheritdoc />
        public override Matrix3 SubMatrix(int row, int column)
        {
            if (row is < 0 or >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(row),
                    row,
                    $"Value must be in range [0, {Size})"
                );
            }

            if (column is < 0 or >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(column),
                    column,
                    $"Value must be in range [0, {Size})"
                );
            }

            return new Matrix3(SubMatrixElements(row, column));
        }

        public Matrix4 Translate(float x, float y, float z) => Translation(x, y, z) * this;

        /// <inheritdoc />
        public override Matrix4 Transpose() => new(TransposeElements());

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            var result = new Matrix4();

            for (var row = 0; row < Size; row += 1)
            {
                for (var col = 0; col < Size; col += 1)
                {
                    result.Elements[row, col] =
                        lhs.Elements[row, 0] * rhs.Elements[0, col] +
                        lhs.Elements[row, 1] * rhs.Elements[1, col] +
                        lhs.Elements[row, 2] * rhs.Elements[2, col] +
                        lhs.Elements[row, 3] * rhs.Elements[3, col];
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

        public static Matrix4 RotationX(float radians)
        {
            var cos = MathF.Cos(radians);
            var sin = MathF.Sin(radians);

            var result = new Matrix4(Identity);
            result.Elements[1, 1] = cos;
            result.Elements[1, 2] = -sin;
            result.Elements[2, 1] = sin;
            result.Elements[2, 2] = cos;

            return result;
        }

        public static Matrix4 RotationY(float radians)
        {
            var cos = MathF.Cos(radians);
            var sin = MathF.Sin(radians);

            var result = new Matrix4(Identity);
            result.Elements[0, 0] = cos;
            result.Elements[0, 2] = sin;
            result.Elements[2, 0] = -sin;
            result.Elements[2, 2] = cos;

            return result;
        }

        public static Matrix4 RotationZ(float radians)
        {
            var cos = MathF.Cos(radians);
            var sin = MathF.Sin(radians);

            var result = new Matrix4(Identity);
            result.Elements[0, 0] = cos;
            result.Elements[0, 1] = -sin;
            result.Elements[1, 0] = sin;
            result.Elements[1, 1] = cos;

            return result;
        }

        public static Matrix4 Scaling(float x, float y, float z)
        {
            var result = new Matrix4(Identity);
            result.Elements[0, 0] = x;
            result.Elements[1, 1] = y;
            result.Elements[2, 2] = z;

            return result;
        }

        public static Matrix4 Shearing(
            float xToY = 0f,
            float xToZ = 0f,
            float yToX = 0f,
            float yToZ = 0f,
            float zToX = 0f,
            float zToY = 0f
        )
        {
            var result = new Matrix4(Identity);
            result.Elements[0, 1] = xToY;
            result.Elements[0, 2] = xToZ;
            result.Elements[1, 0] = yToX;
            result.Elements[1, 2] = yToZ;
            result.Elements[2, 0] = zToX;
            result.Elements[2, 1] = zToY;

            return result;
        }

        public static Matrix4 Translation(float x, float y, float z)
        {
            var result = new Matrix4(Identity);
            result.Elements[0, 3] = x;
            result.Elements[1, 3] = y;
            result.Elements[2, 3] = z;

            return result;
        }

        public static Matrix4 ViewTransform(in Point from, in Point to, in Vector up)
        {
            var forward = (to - from).Normalize();
            var left = forward.CrossProduct(up.Normalize());
            var trueUp = left.CrossProduct(forward);

            var orientation = new Matrix4(
                new[,]
                {
                    { left.X, left.Y, left.Z, 0f },
                    { trueUp.X, trueUp.Y, trueUp.Z, 0f },
                    { -forward.X, -forward.Y, -forward.Z, 0f },
                    { 0f, 0f, 0f, 1f }
                }
            );

            return orientation * Translation(-from.X, -from.Y, -from.Z);
        }
    }
}
