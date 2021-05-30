using System.Collections.Generic;
using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class RingedColorsTests
    {
        public static IEnumerable<object> ColorAtTestCases =>
            new[]
            {
                new object[] { new Point(-2.2f, 0f, -2.2f), Color.Red },
                new object[] { new Point(-2f, 0f, -2f), Color.Blue },
                new object[] { new Point(-2f, 0f, 0f), Color.Blue },
                new object[] { new Point(0f, 0f, -2f), Color.Blue },
                new object[] { new Point(-1f, 0f, -1f), Color.Green },
                new object[] { new Point(-1f, 0f, 0f), Color.Green },
                new object[] { new Point(0f, 0f, -1f), Color.Green },
                new object[] { Point.Origin, Color.Red },
                new object[] { new Point(1f, 0f, 0f), Color.Green },
                new object[] { new Point(0f, 0f, 1f), Color.Green },
                new object[] { new Point(1f, 0f, 1f), Color.Green },
                new object[] { new Point(2f, 0f, 0f), Color.Blue },
                new object[] { new Point(0f, 0f, 2f), Color.Blue },
                new object[] { new Point(2f, 0f, 2f), Color.Blue },
                new object[] { new Point(2.2f, 0f, 2.2f), Color.Red },
            };

        [Theory]
        [InlineData(0f)]
        [InlineData(1f)]
        [InlineData(2f)]
        public void ColorAt_ShouldNotChangeColor_WhenMovingAlongTheYAxis(float y)
        {
            // arrange
            var point = new Point(0f, y, 0f);
            var sut = new RingedColors(Color.White, Color.Black);

            // act
            var result = sut.ColorAt(point);

            // assert
            result.Should().Be(Color.White);
        }

        [Theory]
        [MemberData(nameof(ColorAtTestCases))]
        public void ColorAt_ShouldProduceColorRingsBasedOnXAndZMovement(
            in Point point,
            in Color expected
        )
        {
            // arrange
            var sut = new RingedColors(Color.Red, Color.Green, Color.Blue);

            // act
            var actual = sut.ColorAt(point);

            // assert
            actual.Should().Be(expected);
        }
    }
}
