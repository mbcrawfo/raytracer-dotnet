using System;

namespace RayTracer.Core
{
    public sealed class Canvas
    {
        private readonly Color[,] _pixelData;

        public Canvas(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    width,
                    nameof(width) + " must be positive"
                );
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    nameof(height) + " must be positive"
                );
            }

            Width = width;
            Height = height;
            _pixelData = new Color[Width, Height];
        }

        public int Height { get; }

        public Color this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(x),
                        x,
                        $"{nameof(x)} must be in the range [0, {Width})"
                    );
                }

                if (y < 0 || y >= Height)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(y),
                        y,
                        $"{nameof(y)} must be in the range [0, {Height})"
                    );
                }

                return _pixelData[x, y];
            }
            set
            {
                if (x < 0 || x >= Width)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(x),
                        x,
                        $"{nameof(x)} must be in the range [0, {Width})"
                    );
                }

                if (y < 0 || y >= Height)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(y),
                        y,
                        $"{nameof(y)} must be in the range [0, {Height})"
                    );
                }

                _pixelData[x, y] = value;
            }
        }

        public int Width { get; }
    }
}
