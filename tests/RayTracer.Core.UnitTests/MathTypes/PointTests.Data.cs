using System.Collections.Generic;
using RayTracer.Core.Extensions;
using RayTracer.Core.MathTypes;

namespace RayTracer.Core.UnitTests.MathTypes
{
    public partial class PointTests
    {
        private const float GreaterThanEpsilon = FloatExtensions.ComparisonEpsilon * 10f;
        private const float LessThanEpsilon = FloatExtensions.ComparisonEpsilon / 10f;

        public static IEnumerable<object> ObjectsThatAreNotPoints =>
            new object[]
            {
                new object?[] { null },
                new object[] { new() },
                new object[] { 1 },
                new object[] { "hello world" }
            };

        public static IEnumerable<object> PointsThatAreEquivalent =>
            new object[]
            {
                new object[] { Point.Zero, Point.Zero },
                new object[]
                {
                    Point.Zero, new Point(LessThanEpsilon, LessThanEpsilon, LessThanEpsilon)
                },
                new object[] { new Point(1f, 2f, 3f), new Point(1f, 2f, 3f) },
                new object[] { new Point(1.23f, 4.56f, 7.89f), new Point(1.23f, 4.56f, 7.89f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f + LessThanEpsilon, 2f, 3f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f, 2f + LessThanEpsilon, 3f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f, 2f, 3f + LessThanEpsilon) },
                new object[]
                {
                    new Point(1f, 2f, 3f),
                    new Point(
                        1f + LessThanEpsilon,
                        2f - LessThanEpsilon,
                        3f + LessThanEpsilon
                    )
                },
            };

        public static IEnumerable<object> PointsThatAreNotEquivalent =>
            new object[]
            {
                new object[] { new Point(1f, 2f, 3f), new Point(3f, 2f, 1f) },
                new object[] { new Point(1.23f, 4.56f, 7.89f), new Point(9.87f, 6.54f, 3.21f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f + GreaterThanEpsilon, 2f, 3f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f, 2f + GreaterThanEpsilon, 3f) },
                new object[] { new Point(1f, 2f, 3f), new Point(1f, 2f, 3f + GreaterThanEpsilon) },
                new object[]
                {
                    new Point(1f, 2f, 3f),
                    new Point(
                        1f + GreaterThanEpsilon,
                        2f - GreaterThanEpsilon,
                        3f + GreaterThanEpsilon
                    )
                },
            };
    }
}
