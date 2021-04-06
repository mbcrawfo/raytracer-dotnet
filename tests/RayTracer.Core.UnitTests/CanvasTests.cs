using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
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
        public void Constructor__2_ShouldCreateCanvasOfTheCorrectSizeWithAllPixelsSetToBlack()
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
        public void Constructor__3_ShouldCreateCanvasOfTheCorrectSizeWithAllPixelsSetToBlack()
        {
            // arrange
            const int expectedWidth = 10;
            const int expectedHeight = 20;
            var expectedColor = Color.Red;

            // act
            var sut = new Canvas(expectedWidth, expectedHeight, expectedColor);

            // assert
            using var _ = new AssertionScope();
            sut.Width.Should().Be(expectedWidth);
            sut.Height.Should().Be(expectedHeight);
            foreach (var x in Enumerable.Range(0, expectedWidth))
            {
                foreach (var y in Enumerable.Range(0, expectedHeight))
                {
                    sut[x, y].Should().Be(expectedColor, "pixel at ({0}, {1}) should be red", x, y);
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

        [Fact]
        public void
            SerializeToPpm_ShouldSplitRowsAcrossMultipleDataLines_WhenLineIsMoreThan70Chars()
        {
            // arrange
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            var sut = new Canvas(10, 2, new Color(1f, 0.8f, 0.6f));

            // act
            sut.SerializeToPpm(writer);
            var dataLines = sb.ToString().Split(Environment.NewLine).Skip(3).ToImmutableList();

            // assert
            dataLines.Should()
                .ContainInOrder(
                    "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                    "153 255 204 153 255 204 153 255 204 153 255 204 153",
                    "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                    "153 255 204 153 255 204 153 255 204 153 255 204 153"
                );
        }

        [Fact]
        public void
            SerializeToPpm_ShouldTerminateDataWithANewline()
        {
            // arrange
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            var sut = new Canvas(10, 2, new Color(1f, 0.8f, 0.6f));

            // act
            sut.SerializeToPpm(writer);
            var data = sb.ToString();

            // assert
            data.Should().EndWith(Environment.NewLine);
        }

        [Fact]
        public void SerializeToPpm_ShouldWriteOneDataLinePerRow_WhenEachRowIsLessThan70Chars()
        {
            // arrange
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            var sut = new Canvas(5, 3)
            {
                [0, 0] = new(1.5f, 0f, 0f),
                [2, 1] = new(0f, 0.5f, 0f),
                [4, 2] = new(-0.5f, 0f, 1f)
            };

            // act
            sut.SerializeToPpm(writer);
            var dataLines = sb.ToString().Split(Environment.NewLine).Skip(3).ToImmutableList();

            // assert
            dataLines.Should()
                .ContainInOrder(
                    "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0",
                    "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0",
                    "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255"
                );
        }

        [Fact]
        public void SerializeToPpm_ShouldWritePpmFileHeader()
        {
            // arrange
            const int width = 10;
            const int height = 20;
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            var sut = new Canvas(width, height);

            // act
            sut.SerializeToPpm(writer);
            var headerLines = sb.ToString().Split(Environment.NewLine).Take(3).ToImmutableList();

            // assert
            headerLines.Should()
                .HaveCount(3)
                .And.StartWith("P3", "first header line must be the PPM magic number")
                .And.Contain($"{width} {height}", "second header line must be the image dimensions")
                .And.EndWith(
                    Canvas.PpmMaxPixelValue.ToString(),
                    "third header line must be the max pixel value"
                );
        }
    }
}
