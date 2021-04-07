using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

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

            var pixelArray = GetPixelData().ToImmutableArray();

            // BMP header
            writer.Write((ushort) 0x4d42); // 'BM' magic number 
            writer.Write((uint) (bmpHeaderSize + dibHeaderSize + pixelArray.Length)); // file size
            writer.Write((uint) 0); // unused app specific
            writer.Write((uint) (bmpHeaderSize + dibHeaderSize)); // pixel array offset

            // DIB header
            writer.Write((uint) dibHeaderSize);
            writer.Write((uint) Width);
            writer.Write((uint) Height);
            writer.Write((ushort) 1); // color planes
            writer.Write((ushort) 24); // bits per pixel
            writer.Write((uint) 0); // BI_RGB, no pixel array compression
            writer.Write((uint) pixelArray.Length);
            writer.Write((uint) 2835); // horizontal print resolution in pixels/meter => 72 dpi
            writer.Write((uint) 2835); // vertical print resolution in pixels/meter => 72 dpi
            writer.Write((uint) 0); // colors in the palette
            writer.Write((uint) 0); // all colors are important
            
            writer.Write(pixelArray.AsSpan());

            IEnumerable<byte> GetPixelData()
            {
                // The number of bytes written for each row must be a multiple of 4
                var rowPaddingRequired = (Width * 3) % 4;
                
                foreach (var y in Enumerable.Range(1, Height).Select(v => Height - v))
                {
                    foreach (var x in Enumerable.Range(0, Width))
                    {
                        var (r, g, b) = _pixelData[x, y].Clamp();
                        
                        // Pixel values need to be "little endian" even though they don't make up
                        // a full integer
                        yield return GetScaledColorComponent(b);
                        yield return GetScaledColorComponent(g);
                        yield return GetScaledColorComponent(r);
                    }
                    
                    foreach (var _ in Enumerable.Range(0, rowPaddingRequired))
                    {
                        yield return 0;
                    }
                }
            }

            static byte GetScaledColorComponent(float value) =>
                (byte) (int) MathF.Round(value * 255f);
        }

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
                    yield return GetScaledColorComponent(r);
                    yield return GetScaledColorComponent(g);
                    yield return GetScaledColorComponent(b);
                }
            }

            static string GetScaledColorComponent(float value) =>
                ((int) MathF.Round(value * maxPixelValue)).ToString();
        }
    }
}
