using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class StripedPatternTests
    {
        public static IEnumerable<object> ColorAtTestCases =>
            new[]
            {
                new object[] { -2f, Color.Blue },
                new object[] { -1.1f, Color.Blue },
                new object[] { -1f, Color.White },
                new object[] { -0.1f, Color.White },
                new object[] { 0f, Color.Red },
                new object[] { 0.9f, Color.Red },
                new object[] { 1f, Color.White },
                new object[] { 2f, Color.Blue },
                new object[] { 3f, Color.Red },
                new object[] { 4f, Color.White },
                new object[] { 5f, Color.Blue },
            };

        [Theory]
        [MemberData(nameof(ColorAtTestCases))]
        public void ColorAt_ShouldAlternateColors_WhenMovingAlongTheXAxis(
            float x,
            in Color expected
        )
        {
            // arrange
            var shape = new Sphere();
            var point = new Point(x, 0f, 0f);
            var sut = new StripedPattern
            {
                Patterns = ImmutableArray.Create<Pattern>(
                    new SolidPattern(Color.Red),
                    new SolidPattern(Color.White),
                    new SolidPattern(Color.Blue)
                )
            };

            // act
            var actual = sut.ColorAt(point, shape);

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
            var shape = new Sphere();
            var point = new Point(0f, y, 0f);
            var sut = new StripedPattern
            {
                Patterns = ImmutableArray.Create<Pattern>(
                    new SolidPattern(Color.White),
                    new SolidPattern(Color.Black)
                )
            };

            // act
            var result = sut.ColorAt(point, shape);

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
            var shape = new Sphere();
            var point = new Point(0f, 0f, z);
            var sut = new StripedPattern
            {
                Patterns = ImmutableArray.Create<Pattern>(
                    new SolidPattern(Color.White),
                    new SolidPattern(Color.Black)
                )
            };

            // act
            var result = sut.ColorAt(point, shape);

            // assert
            result.Should().Be(Color.White);
        }
    }
}
