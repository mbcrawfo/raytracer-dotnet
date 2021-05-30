using System.Collections.Generic;
using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class GradientTests
    {
        public static IEnumerable<object> ColorAtTestCases =>
            new[]
            {
                new object[] { new Point(-0.75f, 0f, 0f), new Color(0.75f, 0.75f, 0.75f) },
                new object[] { new Point(-0.5f, 0f, 0f), new Color(0.5f, 0.5f, 0.5f) },
                new object[] { new Point(-0.25f, 0f, 0f), new Color(0.25f, 0.25f, 0.25f) },
                new object[] { Point.Origin, Color.White },
                new object[] { new Point(0.25f, 0f, 0f), new Color(0.75f, 0.75f, 0.75f) },
                new object[] { new Point(0.5f, 0f, 0f), new Color(0.5f, 0.5f, 0.5f) },
                new object[] { new Point(0.75f, 0f, 0f), new Color(0.25f, 0.25f, 0.25f) },
            };

        [Theory]
        [MemberData(nameof(ColorAtTestCases))]
        public void
            ColorAt_ShouldLinearlyInterpolateBetweenTheStartAndEndColor_WhenMovingAlongTheXAxis(
                in Point point,
                in Color expected
            )
        {
            // arrange
            var sut = new Gradient(Color.White, Color.Black);

            // act
            var actual = sut.ColorAt(point);

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(1f)]
        [InlineData(2f)]
        public void ColorAt_ShouldNotChangeColor_WhenMovingAlongTheYAxis(float y)
        {
            // arrange
            var point = new Point(0f, y, 0f);
            var sut = new Gradient(Color.White, Color.Black);

            // act
            var result = sut.ColorAt(point);

            // assert
            result.Should().Be(Color.White);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(1f)]
        [InlineData(2f)]
        public void ColorAt_ShouldNotChangeColor_WhenMovingAlongTheZAxis(float z)
        {
            // arrange
            var point = new Point(0f, 0f, z);
            var sut = new Gradient(Color.White, Color.Black);

            // act
            var result = sut.ColorAt(point);

            // assert
            result.Should().Be(Color.White);
        }
    }
}
