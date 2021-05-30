using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class GradientTests
    {
        [Fact]
        public void ColorAt_ShouldLinearlyInterpolateBetweenTheStartAndEndColor()
        {
            // arrange
            var sut = new Gradient(Color.White, Color.Black);

            // act
            var results = new[]
            {
                sut.ColorAt(Point.Origin),
                sut.ColorAt(new Point(0.25f, 0f, 0f)),
                sut.ColorAt(new Point(0.5f, 0f, 0f)),
                sut.ColorAt(new Point(0.75f, 0f, 0f))
            };

            // assert
            results.Should()
                .ContainInOrder(
                    Color.White,
                    new Color(0.75f, 0.75f, 0.75f),
                    new Color(0.5f, 0.5f, 0.5f),
                    new Color(0.25f, 0.25f, 0.25f)
                );
        }
    }
}
