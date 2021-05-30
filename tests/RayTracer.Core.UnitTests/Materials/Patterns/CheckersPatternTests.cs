using System.Collections.Generic;
using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class CheckersPatternTests
    {
        public static IEnumerable<object> ColorAtXAxis =>
            new[]
            {
                new object[] { new Point(-2.3f, 0f, 0f), Color.Red },
                new object[] { new Point(-1.2f, 0f, 0f), Color.Blue },
                new object[] { new Point(-0.99f, 0f, 0f), Color.Green },
                new object[] { Point.Origin, Color.Red },
                new object[] { new Point(0.99f, 0f, 0f), Color.Red },
                new object[] { new Point(1.2f, 0f, 0f), Color.Green },
                new object[] { new Point(2.3f, 0f, 0f), Color.Blue },
                new object[] { new Point(3.4f, 0f, 0f), Color.Red },
            };

        public static IEnumerable<object> ColorAtYAxis =>
            new[]
            {
                new object[] { new Point(0f, -2.3f, 0f), Color.Red },
                new object[] { new Point(0f, -1.2f, 0f), Color.Blue },
                new object[] { new Point(0f, -0.99f, 0f), Color.Green },
                new object[] { Point.Origin, Color.Red },
                new object[] { new Point(0f, 0.99f, 0f), Color.Red },
                new object[] { new Point(0f, 1.2f, 0f), Color.Green },
                new object[] { new Point(0f, 2.3f, 0f), Color.Blue },
                new object[] { new Point(0f, 3.4f, 0f), Color.Red },
            };

        public static IEnumerable<object> ColorAtZAxis =>
            new[]
            {
                new object[] { new Point(0f, 0f, -2.3f), Color.Red },
                new object[] { new Point(0f, 0f, -1.2f), Color.Blue },
                new object[] { new Point(0f, 0f, -0.99f), Color.Green },
                new object[] { Point.Origin, Color.Red },
                new object[] { new Point(0f, 0f, 0.99f), Color.Red },
                new object[] { new Point(0f, 0f, 1.2f), Color.Green },
                new object[] { new Point(0f, 0f, 2.3f), Color.Blue },
                new object[] { new Point(0f, 0f, 3.4f), Color.Red },
            };

        [Theory]
        [MemberData(nameof(ColorAtXAxis))]
        [MemberData(nameof(ColorAtYAxis))]
        [MemberData(nameof(ColorAtZAxis))]
        public void ColorAt_ShouldRepeatColors(in Point point, in Color expected)
        {
            // arrange
            var sut = new CheckersPattern(Color.Red, Color.Green, Color.Blue);

            // act
            var actual = sut.ColorAt(point);

            // assert
            actual.Should().Be(expected);
        }
    }
}
