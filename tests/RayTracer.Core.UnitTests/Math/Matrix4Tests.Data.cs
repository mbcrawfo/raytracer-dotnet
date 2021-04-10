using System.Collections.Generic;
using RayTracer.Core.Math;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix4Tests
    {
        public static IEnumerable<object> ArraysThatAreNot4X4 =>
            new object[]
            {
                new object[] { new float[3, 3] },
                new object[] { new float[3, 4] },
                new object[] { new float[4, 3] },
                new object[] { new float[5, 5] },
            };

        public static IEnumerable<object> SubMatrixTestCases =>
            new object[]
            {
                new object[]
                {
                    0,
                    0,
                    new Matrix3(
                        new[,] { { 6f, 7f, 8f }, { 10f, 11f, 12f }, { 14f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    0,
                    1,
                    new Matrix3(
                        new[,] { { 5f, 7f, 8f }, { 9f, 11f, 12f }, { 13f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    0,
                    2,
                    new Matrix3(
                        new[,] { { 5f, 6f, 8f }, { 9f, 10f, 12f }, { 13f, 14f, 16f } }
                    )
                },
                new object[]
                {
                    0,
                    3,
                    new Matrix3(
                        new[,] { { 5f, 6f, 7f }, { 9f, 10f, 11f }, { 13f, 14f, 15f } }
                    )
                },
                new object[]
                {
                    1,
                    0,
                    new Matrix3(
                        new[,] { { 2f, 3f, 4f }, { 10f, 11f, 12f }, { 14f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    1,
                    1,
                    new Matrix3(
                        new[,] { { 1f, 3f, 4f }, { 9f, 11f, 12f }, { 13f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    1,
                    2,
                    new Matrix3(
                        new[,] { { 1f, 2f, 4f }, { 9f, 10f, 12f }, { 13f, 14f, 16f } }
                    )
                },
                new object[]
                {
                    1,
                    3,
                    new Matrix3(
                        new[,] { { 1f, 2f, 3f }, { 9f, 10f, 11f }, { 13f, 14f, 15f } }
                    )
                },
                new object[]
                {
                    2,
                    0,
                    new Matrix3(
                        new[,] { { 2f, 3f, 4f }, { 6f, 7f, 8f }, { 14f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    2,
                    1,
                    new Matrix3(
                        new[,] { { 1f, 3f, 4f }, { 5f, 7f, 8f }, { 13f, 15f, 16f } }
                    )
                },
                new object[]
                {
                    2,
                    2,
                    new Matrix3(
                        new[,] { { 1f, 2f, 4f }, { 5f, 6f, 8f }, { 13f, 14f, 16f } }
                    )
                },
                new object[]
                {
                    2,
                    3,
                    new Matrix3(
                        new[,] { { 1f, 2f, 3f }, { 5f, 6f, 7f }, { 13f, 14f, 15f } }
                    )
                },
                new object[]
                {
                    3,
                    0,
                    new Matrix3(
                        new[,] { { 2f, 3f, 4f }, { 6f, 7f, 8f }, { 10f, 11f, 12f } }
                    )
                },
                new object[]
                {
                    3,
                    1,
                    new Matrix3(new[,] { { 1f, 3f, 4f }, { 5f, 7f, 8f }, { 9f, 11f, 12f } })
                },
                new object[]
                {
                    3,
                    2,
                    new Matrix3(new[,] { { 1f, 2f, 4f }, { 5f, 6f, 8f }, { 9f, 10f, 12f } })
                },
                new object[]
                {
                    3,
                    3,
                    new Matrix3(new[,] { { 1f, 2f, 3f }, { 5f, 6f, 7f }, { 9f, 10f, 11f } })
                },
            };
    }
}
