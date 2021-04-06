using System.Collections.Generic;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.UnitTests
{
    public partial class ColorTests
    {
        private const float GreaterThanEpsilon = FloatExtensions.ComparisonEpsilon * 10f;
        private const float LessThanEpsilon = FloatExtensions.ComparisonEpsilon / 10f;

        public static IEnumerable<object> ColorsThatAreEquivalent =>
            new object[]
            {
                new object[] { Color.Black, Color.Black },
                new object[]
                {
                    Color.Black, new Color(LessThanEpsilon, LessThanEpsilon, LessThanEpsilon)
                },
                new object[] { new Color(1f, 2f, 3f), new Color(1f, 2f, 3f) },
                new object[] { new Color(1.23f, 4.56f, 7.89f), new Color(1.23f, 4.56f, 7.89f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f + LessThanEpsilon, 2f, 3f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f, 2f + LessThanEpsilon, 3f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f, 2f, 3f + LessThanEpsilon) },
                new object[]
                {
                    new Color(1f, 2f, 3f),
                    new Color(
                        1f + LessThanEpsilon,
                        2f - LessThanEpsilon,
                        3f + LessThanEpsilon
                    )
                },
            };

        public static IEnumerable<object> ColorsThatAreNotEquivalent =>
            new object[]
            {
                new object[] { new Color(1f, 2f, 3f), new Color(3f, 2f, 1f) },
                new object[] { new Color(1.23f, 4.56f, 7.89f), new Color(9.87f, 6.54f, 3.21f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f + GreaterThanEpsilon, 2f, 3f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f, 2f + GreaterThanEpsilon, 3f) },
                new object[] { new Color(1f, 2f, 3f), new Color(1f, 2f, 3f + GreaterThanEpsilon) },
                new object[]
                {
                    new Color(1f, 2f, 3f),
                    new Color(
                        1f + GreaterThanEpsilon,
                        2f - GreaterThanEpsilon,
                        3f + GreaterThanEpsilon
                    )
                },
            };

        public static IEnumerable<object> ObjectsThatAreNotColors =>
            new object[]
            {
                new object?[] { null },
                new object[] { new() },
                new object[] { 1 },
                new object[] { "hello world" }
            };
    }
}
