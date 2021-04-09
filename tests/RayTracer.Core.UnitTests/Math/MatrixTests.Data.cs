using System.Collections.Generic;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class MatrixTests
    {
        private const float GreaterThanEpsilon = FloatExtensions.ComparisonEpsilon * 10f;
        private const float LessThanEpsilon = FloatExtensions.ComparisonEpsilon / 10f;

        private static readonly Matrix MatrixForReferenceEquality = new TestMatrix(1, 1);

        public static IEnumerable<object> ArrayConstructorInputs =>
            new object[]
            {
                new object[] { new[,] { { 1f } } },
                new object[] { new[,] { { 1f, 2f }, { 3f, 4f } } },
                new object[] { new[,] { { 1f, 2f }, { 3f, 4f }, { 5f, 6f } } },
                new object[] { new[,] { { 1f, 2f, 3f }, { 4f, 5f, 6f }, { 7f, 8f, 9f } } },
                new object[]
                {
                    new[,]
                    {
                        { 1f, 2f, 3f, 4f },
                        { 5f, 6f, 7f, 8f },
                        { 9f, 10f, 11f, 12f },
                        { 13f, 14f, 15f, 16f }
                    }
                },
            };

        public static IEnumerable<object> MatricesThatAreEquivalent =>
            new object[]
            {
                new object[] { MatrixForReferenceEquality, MatrixForReferenceEquality },
                new object[]
                {
                    new TestMatrix(2, 2),
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = LessThanEpsilon,
                        [0, 1] = LessThanEpsilon,
                        [1, 0] = LessThanEpsilon,
                        [1, 1] = LessThanEpsilon
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1f,
                        [0, 1] = 2f,
                        [1, 0] = 3f,
                        [1, 1] = 4f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1f,
                        [0, 1] = 2f,
                        [1, 0] = 3f,
                        [1, 1] = 4f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f + LessThanEpsilon,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f + LessThanEpsilon,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f + LessThanEpsilon,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f + LessThanEpsilon
                    }
                },
                new object[]
                {
                    new TestMatrix(3, 3) { [0, 0] = 1.23f, [1, 1] = 4.56f, [2, 2] = 7.89f },
                    new TestMatrix(3, 3) { [0, 0] = 1.23f, [1, 1] = 4.56f, [2, 2] = 7.89f }
                },
                new object[]
                {
                    new TestMatrix(4, 4)
                    {
                        [0, 0] = 1.23f,
                        [1, 1] = 4.56f,
                        [2, 2] = 7.89f,
                        [3, 3] = 10.11f
                    },
                    new TestMatrix(4, 4)
                    {
                        [0, 0] = 1.23f,
                        [1, 1] = 4.56f,
                        [2, 2] = 7.89f,
                        [3, 3] = 10.11f
                    }
                },
            };

        public static IEnumerable<object> MatricesThatAreNotEquivalent =>
            new object[]
            {
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1f,
                        [0, 1] = 2f,
                        [1, 0] = 3f,
                        [1, 1] = 4f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 4f,
                        [0, 1] = 3f,
                        [1, 0] = 2f,
                        [1, 1] = 1f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 10.11f,
                        [0, 1] = 9.87f,
                        [1, 0] = 4.56f,
                        [1, 1] = 1.23f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f + GreaterThanEpsilon,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f + GreaterThanEpsilon,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f + GreaterThanEpsilon,
                        [1, 1] = 10.11f
                    }
                },
                new object[]
                {
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f
                    },
                    new TestMatrix(2, 2)
                    {
                        [0, 0] = 1.23f,
                        [0, 1] = 4.56f,
                        [1, 0] = 7.89f,
                        [1, 1] = 10.11f + GreaterThanEpsilon
                    }
                },
                new object[] { new TestMatrix(2, 2), new TestMatrix(2, 3) },
                new object[] { new TestMatrix(2, 2), new TestMatrix(3, 2) },
                new object[] { new TestMatrix(2, 2), new TestMatrix(3, 3) },
                new object[] { new TestMatrix(3, 3), new TestMatrix(4, 4) },
            };

        public static IEnumerable<object> ObjectsThatAreNotMatrices =>
            new object[]
            {
                new object?[] { null },
                new object[] { new() },
                new object[] { 1 },
                new object[] { "hello world" }
            };

        private class TestMatrix : Matrix
        {
            /// <inheritdoc />
            public TestMatrix(int columns, int rows)
                : base(columns, rows)
            {
            }

            /// <inheritdoc />
            public TestMatrix(float[,] elements)
                : base(elements)
            {
            }

            /// <inheritdoc />
            public TestMatrix(Matrix other)
                : base(other)
            {
            }
        }
    }
}
