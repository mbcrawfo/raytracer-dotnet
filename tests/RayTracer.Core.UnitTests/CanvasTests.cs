using System;
using System.Collections.Immutable;
using System.Diagnostics;
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
            // ReSharper disable once UseObjectOrCollectionInitializer
            var sut = new Canvas(10, 20);

            // act
            sut[x, y] = expected;
            var actual = sut[x, y];

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void SerializeToBitmap_ShouldWriteBmpHeaderAtTheStartOfTheOutput()
        {
            // arrange
            const int imageDimension = 4;
            const int headerSize = 54;
            const int pixelDataSize = imageDimension * imageDimension * 3;
            Debug.Assert(pixelDataSize % 4 == 0, nameof(pixelDataSize) + " is a multiple of 4");

            using var stream = new MemoryStream();
            var sut = new Canvas(imageDimension, imageDimension);

            // act
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                sut.SerializeToBitmap(writer);
            }

            stream.Position = 0;
            using var reader = new BinaryReader(stream);

            // assert
            using var _ = new AssertionScope();
            reader.ReadChars(2)
                .Should()
                .ContainInOrder(new[] { 'B', 'M' }, "offset 0x00 must be the BM file type marker");
            reader.ReadUInt32()
                .Should()
                .Be(headerSize + pixelDataSize, "offset 0x02 must be the 4 byte total file size");
            reader.ReadUInt32().Should().Be(0, "offset 0x06 must be 4 bytes zeroed (unused)");
            reader.ReadUInt32()
                .Should()
                .Be(
                    headerSize,
                    "offset 0x0A must be the 4 byte offset where the pixel data starts (total header size)"
                );
        }

        [Fact]
        public void SerializeToBitmap_ShouldWriteDibHeaderAfterBmpHeader()
        {
            // arrange
            const int bmpHeaderSize = 14;
            const int dibHeaderSize = 40;
            const int expectedWidth = 12;
            const int expectedHeight = 8;
            const int pixelDataSize = expectedWidth * expectedHeight * 3;
            Debug.Assert(pixelDataSize % 4 == 0, nameof(pixelDataSize) + " is a multiple of 4");

            using var stream = new MemoryStream();
            var sut = new Canvas(expectedWidth, expectedHeight);

            // act
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                sut.SerializeToBitmap(writer);
            }

            stream.Position = 0;
            using var reader = new BinaryReader(stream);
            _ = reader.ReadBytes(bmpHeaderSize);

            // assert
            using var a = new AssertionScope();
            reader.ReadUInt32()
                .Should()
                .Be(dibHeaderSize, "offset 0x0E must be the 4 byte DIB header size");
            reader.ReadUInt32()
                .Should()
                .Be(expectedWidth, "offset 0x12 must be the 4 byte image width");
            reader.ReadUInt32()
                .Should()
                .Be(expectedHeight, "offset 0x16 must be the 4 byte image height");
            reader.ReadUInt16().Should().Be(1, "offset 0x1A must be the 2 byte color plane count");
            reader.ReadUInt16()
                .Should()
                .Be(24, "offset 0x1C must be the 2 byte bits per pixel count");
            reader.ReadUInt32()
                .Should()
                .Be(0, "offset 0x1E must be the 4 byte BI_RGB pixel format indicator");
            reader.ReadUInt32()
                .Should()
                .Be(pixelDataSize, "offset 0x22 must be the 4 byte pixel data array size");
            reader.ReadUInt32()
                .Should()
                .Be(2835, "offset 0x26 must be the pixel/m print resolution for 72dpi");
            reader.ReadUInt32()
                .Should()
                .Be(2835, "offset 0x2A must be the pixel/m print resolution for 72dpi");
            reader.ReadUInt32()
                .Should()
                .Be(0, "offset 0x2E should be the 4 byte count of colors in the palette");
            reader.ReadUInt32()
                .Should()
                .Be(0, "offset 0x32 should be the 4 byte indicator of important colors");
        }

        [Fact]
        public void
            SerializeToBitmap_ShouldWritePixelArrayFollowingHeaders_WhenPixelDataDoesNotRequirePadding()
        {
            // arrange
            const int pixelDataOffset = 54;
            const int imageWidth = 4;
            const int imageHeight = 4;
            const int pixelRowSize = imageWidth * 3;

            using var stream = new MemoryStream();
            var sut = new Canvas(imageWidth, imageHeight)
            {
                [0, 0] = Color.Red,
                [1, 1] = Color.Green,
                [2, 2] = Color.Blue,
                [3, 3] = Color.White
            };

            var expectedPixelData = ImmutableList.Create(
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255 },
                new byte[] { 0, 0, 0, 0, 0, 0, 255, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 255, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            );

            // act
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                sut.SerializeToBitmap(writer);
            }

            stream.Position = 0;
            using var reader = new BinaryReader(stream);
            _ = reader.ReadBytes(pixelDataOffset);

            // assert
            using var a = new AssertionScope();
            foreach (var row in Enumerable.Range(0, imageHeight))
            {
                reader.ReadBytes(pixelRowSize)
                    .Should()
                    .ContainInOrder(
                        expectedPixelData[row],
                        "Row {0} should contain expected bytes",
                        row
                    );
            }

            stream.Position.Should()
                .Be(stream.Length, "should not be any data after pixel array is read");
        }

        [Fact]
        public void
            SerializeToBitmap_ShouldWritePixelArrayWithPaddingBytes_WhenPixelRowSizeIsNot4ByteAligned()
        {
            // arrange
            const int pixelDataOffset = 54;
            const int imageWidth = 2;
            const int imageHeight = 2;
            const int pixelRowSize = imageWidth * 3 + 2;

            using var stream = new MemoryStream();
            var sut = new Canvas(imageWidth, imageHeight)
            {
                [0, 0] = Color.White, [1, 1] = Color.White,
            };

            var expectedPixelData = ImmutableList.Create(
                new byte[] { 0, 0, 0, 255, 255, 255, 0, 0 },
                new byte[] { 255, 255, 255, 0, 0, 0, 0, 0 }
            );

            // act
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                sut.SerializeToBitmap(writer);
            }

            stream.Position = 0;
            using var reader = new BinaryReader(stream);
            _ = reader.ReadBytes(pixelDataOffset);

            // assert
            using var a = new AssertionScope();
            foreach (var row in Enumerable.Range(0, imageHeight))
            {
                reader.ReadBytes(pixelRowSize)
                    .Should()
                    .ContainInOrder(
                        expectedPixelData[row],
                        "Row {0} should contain expected bytes",
                        row
                    );
            }

            stream.Position.Should()
                .Be(stream.Length, "should not be any data after pixel array is read");
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
                .And.EndWith("255", "third header line must be the max pixel value");
        }
    }
}
