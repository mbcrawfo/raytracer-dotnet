using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public Canvas(int width, int height, Color fillColor)
            : this(width, height)
        {
            foreach (var x in Enumerable.Range(0, Width))
            {
                foreach (var y in Enumerable.Range(0, Height))
                {
                    _pixelData[x, y] = fillColor;
                }
            }
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

        public void SerializeToPpm(TextWriter writer)
        {
            const int maxLineLength = 70;
            const int maxPixelValue = 255;

            writer.WriteLine("P3");
            writer.WriteLine("{0} {1}", Width, Height);
            writer.WriteLine(maxPixelValue);

            foreach (var y in Enumerable.Range(0, Height))
            {
                var currentLineLength = 0;
                
                foreach (var value in GetRowPixelValues(y))
                {
                    if (currentLineLength >= maxLineLength - value.Length - 1)
                    {
                        writer.WriteLine();
                        currentLineLength = 0;
                    }
                    else if (currentLineLength > 0)
                    {
                        writer.Write(' ');
                        currentLineLength += 1;
                    }

                    writer.Write(value);
                    currentLineLength += value.Length;
                }

                writer.WriteLine();
            }

            writer.WriteLine();

            IEnumerable<string> GetRowPixelValues(int y)
            {
                foreach (var x in Enumerable.Range(0, Width))
                {
                    var (r, g, b) = _pixelData[x, y].Clamp();
                    yield return GetScaledColorComponentString(r);
                    yield return GetScaledColorComponentString(g);
                    yield return GetScaledColorComponentString(b);
                }
            }

            static string GetScaledColorComponentString(float value)
            {
                var scaledValue = (int) MathF.Round(value * maxPixelValue);
                return scaledValue.ToString();
            }
        }
    }
}
