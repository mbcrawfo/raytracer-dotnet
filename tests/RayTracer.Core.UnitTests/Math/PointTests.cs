using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class PointTests
    {

        [Theory]
        [InlineData(1f, 2f, 3f)]
        [InlineData(4.5f, 5.6f, 6.7f)]
        [InlineData(-1f, -2f, -3f)]
        public void Constructor_ShouldCreatePointWithProvidedValues(float x, float y, float z)
        {
            // arrange
            // act
            var result = new Point(x, y, z);

            // assert
            using var _ = new AssertionScope();
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Theory]
        [InlineData(1f, 2f, 3f)]
        [InlineData(4.5f, 5.6f, 6.7f)]
        [InlineData(-1f, -2f, -3f)]
        public void CopyConstructor_ShouldCreatePointWithProvidedValues(float x, float y, float z)
        {
            // arrange
            var initialPoint = new Point(x, y, z);
            
            // act
            var result = new Point(initialPoint);

            // assert
            using var _ = new AssertionScope();
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Fact]
        public void Deconstruct_ShouldReturnExpectedComponentValues()
        {
            // arrange
            var sut = new Point(1, 2, 3);

            // act
            var (x, y, z) = sut;

            // assert
            using var _ = new AssertionScope();
            x.Should().Be(sut.X);
            y.Should().Be(sut.Y);
            z.Should().Be(sut.Z);
        }
    }
}
