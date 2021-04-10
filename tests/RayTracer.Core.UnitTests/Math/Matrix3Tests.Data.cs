using System.Collections.Generic;
using RayTracer.Core.Math;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix3Tests
    {
        public static IEnumerable<object> ArraysThatAreNot3X3 =>
            new object[]
            {
                new object[] { new float[2, 2] },
                new object[] { new float[2, 3] },
                new object[] { new float[3, 2] },
                new object[] { new float[4, 4] },
            };

        public static IEnumerable<object> SubMatrixTestCases =>
            new object[]
            {
                new object[] { 0, 0, new Matrix2(new[,] { { 5f, 6f }, { 8f, 9f } }) },
                new object[] { 0, 1, new Matrix2(new[,] { { 4f, 6f }, { 7f, 9f } }) },
                new object[] { 0, 2, new Matrix2(new[,] { { 4f, 5f }, { 7f, 8f } }) },
                new object[] { 1, 0, new Matrix2(new[,] { { 2f, 3f }, { 8f, 9f } }) },
                new object[] { 1, 1, new Matrix2(new[,] { { 1f, 3f }, { 7f, 9f } }) },
                new object[] { 1, 2, new Matrix2(new[,] { { 1f, 2f }, { 7f, 8f } }) },
                new object[] { 2, 0, new Matrix2(new[,] { { 2f, 3f }, { 5f, 6f } }) },
                new object[] { 2, 1, new Matrix2(new[,] { { 1f, 3f }, { 4f, 6f } }) },
                new object[] { 2, 2, new Matrix2(new[,] { { 1f, 2f }, { 4f, 5f } }) },
            };
    }
}
