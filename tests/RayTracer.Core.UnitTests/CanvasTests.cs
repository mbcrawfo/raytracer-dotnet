using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class CanvasTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenWidthIsNotPositive(
            int width
        )
        {
            // arrange
            // act
            Action act = () => { _ = new Canvas(width, 1); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be positive*")
                .And.ParamName.Should()
                .Be("width");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenHeightIsNotPositive(
            int height
        )
        {
            // arrange
            // act
            Action act = () => { _ = new Canvas(1, height); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be positive*")
                .And.ParamName.Should()
                .Be("height");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Indexer__Get_ShouldThrowArgumentOutOfRangeException_WhenXIsLessThanZero(int x)
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { _ = sut[x, 0]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 2)*")
                .And.ParamName.Should()
                .Be("x");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Indexer__Get_ShouldThrowArgumentOutOfRangeException_WhenYIsLessThanZero(int y)
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { _ = sut[0, y]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 3)*")
                .And.ParamName.Should()
                .Be("y");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void
            Indexer__Get_ShouldThrowArgumentOutOfRangeException_WhenXIsGreaterThanOrEqualToWidth(
                int x
            )
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { _ = sut[x, 0]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 2)*")
                .And.ParamName.Should()
                .Be("x");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void
            Indexer__Get_ShouldThrowArgumentOutOfRangeException_WhenYIsGreaterThanOrEqualToHeight(
                int y
            )
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { _ = sut[0, y]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 3)*")
                .And.ParamName.Should()
                .Be("y");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Indexer__Set_ShouldThrowArgumentOutOfRangeException_WhenXIsLessThanZero(int x)
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { sut[x, 0] = Color.White; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 2)*")
                .And.ParamName.Should()
                .Be("x");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Indexer__Set_ShouldThrowArgumentOutOfRangeException_WhenYIsLessThanZero(int y)
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { sut[0, y] = Color.White; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 3)*")
                .And.ParamName.Should()
                .Be("y");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void
            Indexer__Set_ShouldThrowArgumentOutOfRangeException_WhenXIsGreaterThanOrEqualToWidth(
                int x
            )
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { sut[x, 0] = Color.White; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 2)*")
                .And.ParamName.Should()
                .Be("x");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void
            Indexer__Set_ShouldThrowArgumentOutOfRangeException_WhenYIsGreaterThanOrEqualToHeight(
                int y
            )
        {
            // arrange
            var sut = new Canvas(2, 3);

            // act
            Action act = () => { sut[0, y] = Color.White; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in the range [0, 3)*")
                .And.ParamName.Should()
                .Be("y");
        }

        [Fact]
        public void Constructor_ShouldCreateCanvasOfTheCorrectSizeWithAllPixelsSetToBlack()
        {
            // arrange
            const int expectedWidth = 10;
            const int expectedHeight = 20;

            // act
            var sut = new Canvas(expectedWidth, expectedHeight);

            // assert
            using var _ = new AssertionScope();
            sut.Width.Should().Be(expectedWidth);
            sut.Height.Should().Be(expectedHeight);
            foreach (var x in Enumerable.Range(0, expectedWidth))
            {
                foreach (var y in Enumerable.Range(0, expectedHeight))
                {
                    sut[x, y].Should().Be(Color.Black, "pixel at ({0}, {1}) should be black", x, y);
                }
            }
        }

        [Fact]
        public void Indexer_ShouldSetAndReturnColorAtCoordinates()
        {
            // arrange
            const int x = 5;
            const int y = 10;
            var expected = new Color(0.25f, 0.3f, 0.75f);
            var sut = new Canvas(10, 20);

            // act
            sut[x, y] = expected;
            var actual = sut[x, y];

            // assert
            actual.Should().Be(expected);
        }
    }
}
