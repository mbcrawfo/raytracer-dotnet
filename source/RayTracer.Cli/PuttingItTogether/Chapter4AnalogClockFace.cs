using System;
using System.IO;
using System.Linq;
using RayTracer.Core;
using RayTracer.Core.Math;

namespace RayTracer.Cli.PuttingItTogether
{
    internal static class Chapter4AnalogClockFace
    {
        private const int ImageHeight = 1024;
        private const int ImageWidth = 1024;

        private static readonly Matrix4 PointToCanvasTranslation = Matrix4.Identity
            .Scale(ImageWidth / 2f, ImageHeight / 2f, 0f)
            .Translate(ImageWidth / 2f, ImageHeight / 2f, 0f);

        public static void GenerateImage()
        {
            var canvas = new Canvas(ImageWidth, ImageHeight, new Color(0.25f, 0.25f, 0.25f));

            foreach (var hour in Enumerable.Range(0, 12))
            {
                var center = Matrix4.RotationZ(hour * MathF.PI / 6f) * new Point(0f, 0.9f, 0f);
                var (centerX, centerY) = PointToCanvas(center);

                Console.WriteLine($"Drawing hour {hour} at {center} canvas [{centerX}, {centerY}]");
                foreach (var xOffset in Enumerable.Range(-3, 7))
                {
                    foreach (var yOffset in Enumerable.Range(-3, 7))
                    {
                        canvas[centerX + xOffset, centerY + yOffset] = Color.Green;
                    }
                }
            }

            using var stream = File.OpenWrite("clock_face.bmp");
            using var writer = new BinaryWriter(stream);
            canvas.SerializeToBitmap(writer);
        }

        private static (int x, int y) PointToCanvas(in Point point)
        {
            var (x, y, _) = PointToCanvasTranslation * point;
            return ((int) MathF.Round(x), (int) MathF.Round(ImageHeight - 1 - y));
        }
    }
}
