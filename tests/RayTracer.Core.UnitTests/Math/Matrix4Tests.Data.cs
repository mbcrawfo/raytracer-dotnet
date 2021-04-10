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

        public static IEnumerable<object> InverseTestCases =>
            new object[]
            {
                new object[]
                {
                    new Matrix4(
                        new[,]
                        {
                            { -5f, 2f, 6f, -8f },
                            { 1f, -5f, 1f, 8f },
                            { 7f, 7f, -6f, -7f },
                            { 1f, -3f, 7f, 4f }
                        }
                    ),
                    new Matrix4(
                        new[,]
                        {
                            { 0.21805f, 0.45113f, 0.24060f, -0.04511f },
                            { -0.80827f, -1.45677f, -0.44361f, 0.52068f },
                            { -0.07895f, -0.22368f, -0.05263f, 0.19737f },
                            { -0.52256f, -0.81391f, -0.30075f, 0.30639f }
                        }
                    )
                },
                new object[]
                {
                    new Matrix4(
                        new[,]
                        {
                            { 8f, -5f, 9f, 2f },
                            { 7f, 5f, 6f, 1f },
                            { -6f, 0f, 9f, 6f },
                            { -3f, 0f, -9f, -4f }
                        }
                    ),
                    new Matrix4(
                        new[,]
                        {
                            { -0.15385f, -0.15385f, -0.28205f, -0.53846f },
                            { -0.07692f, 0.12308f, 0.02564f, 0.03077f },
                            { 0.35897f, 0.35897f, 0.43590f, 0.92308f },
                            { -0.69231f, -0.69231f, -0.76923f, -1.92308f }
                        }
                    )
                },
                new object[]
                {
                    new Matrix4(
                        new[,]
                        {
                            { 9f, 3f, 0f, 9f },
                            { -5f, -2f, -6f, -3f },
                            { -4f, 9f, 6f, 4f },
                            { -7f, 6f, 6f, 2f }
                        }
                    ),
                    new Matrix4(
                        new[,]
                        {
                            { -0.04074f, -0.07778f, 0.14444f, -0.22222f },
                            { -0.07778f, 0.033333f, 0.36667f, -0.33333f },
                            { -0.02901f, -0.14630f, -0.10926f, 0.12963f },
                            { 0.17778f, 0.06667f, -0.26667f, 0.33333f }
                        }
                    )
                },
            };

        public static IEnumerable<object> ShearingTestCases =>
            new object[]
            {
                new object[] { 1f, 0f, 0f, 0f, 0f, 0f, new Point(5f, 3f, 4f) },
                new object[] { 0f, 1f, 0f, 0f, 0f, 0f, new Point(6f, 3f, 4f) },
                new object[] { 0f, 0f, 1f, 0f, 0f, 0f, new Point(2f, 5f, 4f) },
                new object[] { 0f, 0f, 0f, 1f, 0f, 0f, new Point(2f, 7f, 4f) },
                new object[] { 0f, 0f, 0f, 0f, 1f, 0f, new Point(2f, 3f, 6f) },
                new object[] { 0f, 0f, 0f, 0f, 0f, 1f, new Point(2f, 3f, 7f) },
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
