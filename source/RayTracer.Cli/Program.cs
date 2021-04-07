using System;
using System.IO;
using RayTracer.Core;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;

namespace RayTracer.Cli
{
    internal class Program
    {
        private const int ImageHeight = 768;
        private const int ImageWidth = 1024;

        internal static void Main()
        {
            var canvas = new Canvas(ImageWidth, ImageHeight);

            var env = new Environment(new Vector(0f, -0.1f, 0f), new Vector(-0.01f, 0f, 0f));
            var proj = new Projectile(
                new Point(0f, 1f, 0f),
                new Vector(1f, 1.8f, 0f).Normalize() * 12.1f
            );
            var tick = 0;

            while (proj.Position.Y >= 0.0)
            {
                proj = proj.Tick(env);
                var (x, y) = proj.CanvasPosition;
                for (var xOffset = -1; xOffset <= 1; xOffset++)
                {
                    for (var yOffset = -1; yOffset <= 1; yOffset++)
                    {
                        try
                        {
                            canvas[x + xOffset, y + yOffset] = Color.Red;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // oops, off canvas write
                        }
                    }
                }
                Console.WriteLine($"Tick {tick++}: {proj}");
            }

            using var stream = File.OpenWrite("projectile.bmp");
            using var writer = new BinaryWriter(stream);
            canvas.SerializeToBitmap(writer);
        }

        internal record Environment(Vector Gravity, Vector Wind);

        internal record Projectile(Point Position, Vector Velocity)
        {
            public (int x, int y) CanvasPosition =>
            (
                (int) MathF.Round(Position.X.Clamp(0f, ImageWidth - 1)),
                (int) MathF.Round(ImageHeight - 1 - Position.Y.Clamp(0f, ImageHeight - 1))
            );

            public Projectile Tick(Environment environment) =>
                this with
                {
                    Position = Position + Velocity,
                    Velocity = Velocity + environment.Gravity + environment.Wind
                };
        }
    }
}
