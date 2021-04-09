using System;

namespace RayTracer.Core.Math
{
    public sealed class Matrix2 : Matrix
    {
        public Matrix2()
            : base(2, 2)
        {
        }

        public Matrix2(float[,] elements)
            : base(elements)
        {
            if (elements.GetLength(0) != 2 || elements.GetLength(1) != 2)
            {
                throw new ArgumentException("Value must be a 2x2 array", nameof(elements));
            }
        }

        public Matrix2(Matrix2 other)
            : base(other.Elements)
        {
        }
    }
}
