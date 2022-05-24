using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RayTracer.Core.Math
{
    public static class PerlinNoise
    {
        private static readonly IReadOnlyList<int> Permutations =
            BasePermutations.Concat(BasePermutations).ToArray();

        static PerlinNoise()
        {
            Debug.Assert(Permutations.Count == 512, "Permutations.Count == 512");
        }

        private static IEnumerable<int> BasePermutations =>
        // @formatter:off
            new[]
            {
                151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30,
                69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94,
                252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136,
                171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229,
                122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25,
                63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116,
                188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202,
                38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28,
                42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43,
                172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218,
                246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145,
                235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115,
                121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141,
                128, 195, 78, 66, 215, 61, 156, 180 
            };
        // @formatter:on

        public static float Generate(float x, float y, float z)
        {
            // Find a unit cube that contains the point
            var cubeX = (int) MathF.Floor(x) & 255;
            var cubeY = (int) MathF.Floor(y) & 255;
            var cubeZ = (int) MathF.Floor(z) & 255;

            // Find relative position of the point in the cube
            var relativeX = x - MathF.Floor(x);
            var relativeY = y - MathF.Floor(y);
            var relativeZ = z - MathF.Floor(z);

            // Compute fade curves for x, y, z
            var u = Fade(relativeX);
            var v = Fade(relativeY);
            var w = Fade(relativeZ);

            // Hash the coordinates of the 8 cube corners
            var a = Permutations[cubeX] + cubeY;
            var aa = Permutations[a] + cubeZ;
            var ab = Permutations[a + 1] + cubeZ;
            var b = Permutations[cubeX + 1] + cubeY;
            var ba = Permutations[b] + cubeZ;
            var bb = Permutations[b + 1] + cubeZ;

            return Lerp(
                w,
                Lerp(
                    v,
                    Lerp(
                        u,
                        Gradient(Permutations[aa], relativeX, relativeY, relativeZ),
                        Gradient(Permutations[ba], relativeX - 1, relativeY, relativeZ)
                    ),
                    Lerp(
                        u,
                        Gradient(Permutations[ab], relativeX, relativeY - 1, relativeZ),
                        Gradient(Permutations[bb], relativeX - 1, relativeY - 1, relativeZ)
                    )
                ),
                Lerp(
                    v,
                    Lerp(
                        u,
                        Gradient(Permutations[aa + 1], relativeX, relativeY, relativeZ - 1),
                        Gradient(Permutations[ba + 1], relativeX - 1, relativeY, relativeZ - 1)
                    ),
                    Lerp(
                        u,
                        Gradient(Permutations[ab + 1], relativeX, relativeY - 1, relativeZ - 1),
                        Gradient(Permutations[bb + 1], relativeX - 1, relativeY - 1, relativeZ - 1)
                    )
                )
            );
        }

        private static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);

        private static float Gradient(int hash, float x, float y, float z)
        {
            var h = hash & 0xff;
            var u = h < 8 ? x : y;
            var v = h switch
            {
                < 4 => y,
                12 or 14 => x,
                _ => z
            };

            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        private static float Lerp(float t, float a, float b) => a + t * (b - a);
    }
}
