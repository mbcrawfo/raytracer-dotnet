using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracer.Core.Math
{
    public sealed class Matrix3 : Matrix
    {
        private const int Size = 3;

        public Matrix3()
            : base(Size, Size)
        {
        }

        public Matrix3(float[,] elements)
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
        public Matrix3(Matrix3 other)
            : base(other)
        {
        }

        /// <inheritdoc />
        public override Matrix3 Transpose() => new(TransposeElements());
    }
}
