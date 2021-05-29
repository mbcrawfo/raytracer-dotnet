using System.Collections.Generic;
using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class SolidPatternTests
    {
        public static IEnumerable<object> ColorAtTestCases =>
            new[]
            {
                new object[] { Color.Black, Point.Origin },
                new object[] { Color.Blue, new Point(1f, 2f, 3f) },
                new object[] { Color.Green, new Point(4f, 5f, 6f) },
                new object[] { Color.Red, new Point(7f, 8f, 9f) },
            };

        [Theory]
        [MemberData(nameof(ColorAtTestCases))]
        public void ColorAt_ShouldAlwaysReturnThePatternColor(in Color expected, in Point point)
        {
            // arrange
            var sut = new SolidPattern(expected);

            // act
            var actual = sut.ColorAt(point);

            // assert
            actual.Should().Be(expected);
        }
    }
}
