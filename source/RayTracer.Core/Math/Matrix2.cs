using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracer.Core.Math
{
    public sealed class Matrix2 : Matrix
    {
        private const int Size = 2;

        public Matrix2()
            : base(Size, Size)
        {
        }

        public Matrix2(float[,] elements)
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
        public Matrix2(Matrix2 other)
            : base(other)
        {
        }
    }
}
