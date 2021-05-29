using System.IO;
using System.Linq;
using RayTracer.Core;
using RayTracer.Core.Extensions;
using RayTracer.Core.Materials;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Cli.PuttingItTogether
{
    internal static class Chapter6ThreeDeeSphere
    {
        public static void GenerateImage()
        {
            const float wallZ = 10f;
            const float wallSize = 7f;
            const int imageSize = 1024;
            const float pixelSize = wallSize / imageSize;
            const float halfWallSize = wallSize / 2f;

            var rayOrigin = new Point(0f, 0f, -5f);
            var sphere = new Sphere
            {
                Material = Material.Default with
                {
                    Pattern = new SolidPattern(new Color(1f, 0.2f, 1f))
                }
            };
            var light = new PointLight(new(-10f, 10f, -10f), Color.White);
            var canvas = new Canvas(imageSize, imageSize, new Color(0.25f, 0.25f, 0.25f));

            foreach (var y in Enumerable.Range(0, imageSize))
            {
                var worldY = halfWallSize - pixelSize * y;

                foreach (var x in Enumerable.Range(0, imageSize))
                {
                    var worldX = -halfWallSize + pixelSize * x;

                    var rayTarget = new Point(worldX, worldY, wallZ);
                    var ray = new Ray(rayOrigin, (rayTarget - rayOrigin).Normalize());

                    if (sphere.Intersect(ray).Hit() is var (shape, time))
                    {
                        var point = ray.Position(time);
                        var eye = -ray.Direction;
                        var normal = sphere.NormalAt(point);
                        canvas[x, y] = shape.Material.Lighting(light, point, eye, normal, false);
                    }
                }
            }

            using var stream = File.OpenWrite("sphere.bmp");
            using var writer = new BinaryWriter(stream);
            canvas.SerializeToBitmap(writer);
        }
    }
}
