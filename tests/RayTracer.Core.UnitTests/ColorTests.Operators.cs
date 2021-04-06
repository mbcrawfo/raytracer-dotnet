using FluentAssertions;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class ColorTests
    {
        [Fact]
        public void Op__AddColors_ShouldAddComponentsTogether()
        {
            // arrange
            var color1 = new Color(1f, 2f, 3f);
            var color2 = new Color(4f, 5f, 6f);
            var expected = new Color(5f, 7f, 9f);

            // act
            var actual = color1 + color2;

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Op__AddColors_ShouldBeCommutative()
        {
            // arrange
            var color1 = new Color(1f, 2f, 3f);
            var color2 = new Color(4f, 5f, 6f);

            // act
            var result1 = color1 + color2;
            var result2 = color2 + color1;

            // assert
            result1.Should().Be(result2);
        }

        [Fact]
        public void Op__MultiplyColorsAndScalar_ShouldMultiplyComponentsByRhs()
        {
            // arrange
            var color = new Color(1f, 2f, 3f);
            var expected = new Color(2f, 4f, 6f);

            // act
            var actual = color * 2;

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Op__SubtractColors_ShouldSubtractComponents()
        {
            // arrange
            var color1 = new Color(1f, 2f, 3f);
            var color2 = new Color(4f, 5f, 6f);
            var expected = new Color(-3f, -3f, -3f);

            // act
            var actual = color1 - color2;

            // assert
            actual.Should().Be(expected);
        }
    }
}
