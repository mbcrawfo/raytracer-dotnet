using System;

namespace RayTracer.Core.Math
{
    public sealed class Matrix4 : Matrix
    {
        private const int Size = 4;

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

        public Matrix4(Matrix4 other)
            : base(other.Elements)
        {
        }
    }
}
