using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class ColorTests
    {
        [Theory]
        [InlineData(1f, 0f, 0f)]
        [InlineData(0.1f, 0.2f, 0.3f)]
        [InlineData(1f, 2f, 3f)]
        public void Constructor_ShouldCreateColorWithProvidedValues(float r, float g, float b)
        {
            // arrange
            // act
            var result = new Color(r, g, b);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().Be(r);
            result.G.Should().Be(g);
            result.B.Should().Be(b);
        }

        [Theory]
        [InlineData(1f, 0f, 0f)]
        [InlineData(0.1f, 0.2f, 0.3f)]
        [InlineData(1f, 2f, 3f)]
        public void CopyConstructor_ShouldCreateColorWithProvidedValues(float r, float g, float b)
        {
            // arrange
            var initialColor = new Color(r, g, b);

            // act
            var result = new Color(initialColor);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().Be(r);
            result.G.Should().Be(g);
            result.B.Should().Be(b);
        }

        [Fact]
        public void
            Clamp_ShouldClampAllComponentsToBeBetweenZeroAndOne_WhenComponentsAreNotBetweenZeroAndOne()
        {
            // arrange
            var sut = new Color(5f, -10f, 1.5f);
            var expected = new Color(1f, 0f, 1f);

            // act
            var actual = sut.Clamp();

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Clamp_ShouldReturnExistingComponentValues_WhenComponentsAreBetweenZeroAndOne()
        {
            // arrange
            var expected = new Color(0.25f, 0.33f, 0.5f);

            // act
            var actual = expected.Clamp();

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deconstruct_ShouldReturnExpectedComponentValues()
        {
            // arrange
            var sut = new Color(1f, 2f, 3f);

            // act
            var (r, g, b) = sut;

            // assert
            using var _ = new AssertionScope();
            r.Should().Be(sut.R);
            g.Should().Be(sut.G);
            b.Should().Be(sut.B);
        }

        [Fact]
        public void HadamardProduct_ShouldMultipleComponentsWithOtherColorsComponents()
        {
            // arrange
            var sut = new Color(1, 2, 3);
            var other = new Color(2f, 3f, 4f);
            var expected = new Color(2f, 6f, 12f);

            // act
            var actual = sut.HadamardProduct(other);

            // assert
            actual.Should().Be(expected);
        }
    }
}
