using System;
using System.Diagnostics;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core
{
    public sealed record Camera : Transformable
    {
        private readonly float _halfHeight;
        private readonly float _halfWidth;

        public Camera(
            int horizontalSize = 160,
            int verticalSize = 120,
            float fieldOfView = MathF.PI / 2f
        )
        {
            HorizontalSize = horizontalSize;
            VerticalSize = verticalSize;
            FieldOfView = fieldOfView;

            var halfView = MathF.Tan(FieldOfView / 2f);
            var aspect = HorizontalSize / (float) VerticalSize;
            if (aspect >= 1f)
            {
                _halfWidth = halfView;
                _halfHeight = halfView / aspect;
            }
            else
            {
                _halfWidth = halfView * aspect;
                _halfHeight = halfView;
            }

            PixelSize = _halfWidth * 2f / horizontalSize;
        }

        public float FieldOfView { get; }

        public int HorizontalSize { get; }

        public float PixelSize { get; }

        public int VerticalSize { get; }

        public Ray RayForPixel(int x, int y)
        {
            Debug.Assert(x >= 0 && x < HorizontalSize, "x >= 0 && x < HorizontalSize");
            Debug.Assert(y >= 0 && y < VerticalSize, "y >= 0 && y < VerticalSize");

            // Offset from the edge of the canvas to the pixel's center
            var xOffset = (x + 0.5f) * PixelSize;
            var yOffset = (y + 0.5f) * PixelSize;

            // Untransformed coordinates of the pixel in world space
            var worldX = _halfWidth - xOffset;
            var worldY = _halfHeight - yOffset;

            var pixel = TransformInverse * new Point(worldX, worldY, -1f);
            var origin = TransformInverse * Point.Origin;
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World world)
        {
            var canvas = new Canvas(HorizontalSize, VerticalSize);

            for (var y = 0; y < VerticalSize; y += 1)
            {
                for (var x = 0; x < HorizontalSize; x += 1)
                {
                    var ray = RayForPixel(x, y);
                    canvas[x, y] = world.ColorAt(ray);
                }
            }

            return canvas;
        }
    }
}
