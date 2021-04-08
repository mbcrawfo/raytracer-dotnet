using System.Collections.Generic;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix2Tests
    {
        private const float GreaterThanEpsilon = FloatExtensions.ComparisonEpsilon * 10f;
        private const float LessThanEpsilon = FloatExtensions.ComparisonEpsilon / 10f;

        public static IEnumerable<object> Matrix2ThatAreEquivalent =>
            new object[]
            {
                new object[] { new Matrix2(0f, 0f, 0f, 0f), new Matrix2(0f, 0f, 0f, 0f) },
                new object[]
                {
                    new Matrix2(0f, 0f, 0f, 0f),
                    new Matrix2(
                        LessThanEpsilon,
                        LessThanEpsilon,
                        LessThanEpsilon,
                        LessThanEpsilon
                    )
                },
                new object[] { new Matrix2(1f, 2f, 3f, 4f), new Matrix2(1f, 2f, 3f, 4f) },
                new object[]
                {
                    new Matrix2(1.23f, 4.56f, 7.89f, 10.11f),
                    new Matrix2(1.23f, 4.56f, 7.89f, 10.11f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f), new Matrix2(1f + LessThanEpsilon, 2f, 3f, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f), new Matrix2(1f, 2f + LessThanEpsilon, 3f, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f), new Matrix2(1f, 2f, 3f + LessThanEpsilon, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f), new Matrix2(1f, 2f, 3f, 4f + LessThanEpsilon)
                },
            };

        public static IEnumerable<object> Matrix2ThatAreNotEquivalent =>
            new object[]
            {
                new object[] { new Matrix2(1f, 2f, 3f, 4f), new Matrix2(4f, 3f, 2f, 1f) },
                new object[]
                {
                    new Matrix2(1.23f, 4.56f, 7.89f, 10.11f),
                    new Matrix2(11.10f, 9.87f, 6.54f, 3.21f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f),
                    new Matrix2(1f + GreaterThanEpsilon, 2f, 3f, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f),
                    new Matrix2(1f, 2f + GreaterThanEpsilon, 3f, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f),
                    new Matrix2(1f, 2f, 3f + GreaterThanEpsilon, 4f)
                },
                new object[]
                {
                    new Matrix2(1f, 2f, 3f, 4f),
                    new Matrix2(1f, 2f, 3f, 4f + GreaterThanEpsilon)
                },
            };

        public static IEnumerable<object> ObjectsThatAreNotMatrix2 =>
            new object[]
            {
                new object?[] { null },
                new object[] { new() },
                new object[] { 1 },
                new object[] { "hello world" }
            };
    }
}
