using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RayTracer.Core.Extensions;

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

        public void SerializeToBitmap(BinaryWriter writer)
        {
            const int bmpHeaderSize = 14;
            const int dibHeaderSize = 40;
            // The number of bytes written for each row must be a multiple of 4
            var rowPaddingRequired = Width * 3 % 4;
            var bytesPerRow = Width * 3 + rowPaddingRequired;
            var pixelArraySize = Height * bytesPerRow;

            // BMP header
            writer.Write((ushort) 0x4d42); // 'BM' magic number 
            writer.Write((uint) (bmpHeaderSize + dibHeaderSize + pixelArraySize)); // file size
            writer.Write((uint) 0); // unused app specific
            writer.Write((uint) (bmpHeaderSize + dibHeaderSize)); // pixel array offset

            // DIB header
            writer.Write((uint) dibHeaderSize);
            writer.Write((uint) Width);
            writer.Write((uint) Height);
            writer.Write((ushort) 1); // color planes
            writer.Write((ushort) 24); // bits per pixel
            writer.Write((uint) 0); // BI_RGB, no pixel array compression
            writer.Write((uint) pixelArraySize);
            writer.Write((uint) 2835); // horizontal print resolution in pixels/meter => 72 dpi
            writer.Write((uint) 2835); // vertical print resolution in pixels/meter => 72 dpi
            writer.Write((uint) 0); // colors in the palette
            writer.Write((uint) 0); // all colors are important

            var rowData = new byte[bytesPerRow];
            foreach (var y in Enumerable.Range(1, Height).Select(v => Height - v))
            {
                foreach (var x in Enumerable.Range(0, Width))
                {
                    var rowDataOffset = x * 3;
                    var (r, g, b) = _pixelData[x, y];

                    // Pixel values need to be "little endian" even though they don't make up
                    // a full integer
                    rowData[rowDataOffset] = GetScaledColorComponent(b);
                    rowData[rowDataOffset + 1] = GetScaledColorComponent(g);
                    rowData[rowDataOffset + 2] = GetScaledColorComponent(r);
                }

                writer.Write(rowData);
            }

            static byte GetScaledColorComponent(float value) =>
                (byte) (int) MathF.Round(value.Clamp(0f, 1f) * 255f);
        }

        public void SerializeToPortablePixmap(TextWriter writer)
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
                    var (r, g, b) = _pixelData[x, y];
                    yield return GetScaledColorComponent(r);
                    yield return GetScaledColorComponent(g);
                    yield return GetScaledColorComponent(b);
                }
            }

            static string GetScaledColorComponent(float value) =>
                ((int) MathF.Round(value.Clamp(0f, 1f) * maxPixelValue)).ToString();
        }
    }
}
