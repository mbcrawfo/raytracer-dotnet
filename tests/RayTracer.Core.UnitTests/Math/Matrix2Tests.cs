using System;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix2Tests
    {
        [Theory]
        [InlineData(1f, 2f, 3f, 4f)]
        [InlineData(4.5f, 5.6f, 6.7f, 8.9f)]
        [InlineData(-1f, -2f, -3f, -4f)]
        public void Constructor__2x2Array_ShouldCreateMatrix2WithProvidedValues(
            float m00,
            float m01,
            float m10,
            float m11
        )
        {
            // arrange
            var array = new[,] { { m00, m01 }, { m10, m11 } };

            // act
            var result = new Matrix2(array);

            // assert
            using var _ = new AssertionScope();
            result[0, 0].Should().Be(m00);
            result[0, 1].Should().Be(m01);
            result[1, 0].Should().Be(m10);
            result[1, 1].Should().Be(m11);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 3)]
        public void Constructor__2x2Array_ShouldThrowArgumentOutOfRangeException_WhenInputIsNot2x2(
            int x,
            int y
        )
        {
            // arrange
            var array = new float[x, y];

            // act
            Action act = () => { _ = new Matrix2(array); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("*must be a 2x2 array*");
        }

        [Theory]
        [InlineData(1f, 2f, 3f, 4f)]
        [InlineData(4.5f, 5.6f, 6.7f, 8.9f)]
        [InlineData(-1f, -2f, -3f, -4f)]
        public void Constructor__4Floats_ShouldCreateMatrix2WithProvidedValues(
            float m00,
            float m01,
            float m10,
            float m11
        )
        {
            // arrange
            // act
            var result = new Matrix2(m00, m01, m10, m11);

            // assert
            using var _ = new AssertionScope();
            result[0, 0].Should().Be(m00);
            result[0, 1].Should().Be(m01);
            result[1, 0].Should().Be(m10);
            result[1, 1].Should().Be(m11);
        }

        [Theory]
        [InlineData(1f, 2f, 3f, 4f)]
        [InlineData(4.5f, 5.6f, 6.7f, 8.9f)]
        [InlineData(-1f, -2f, -3f, -4f)]
        public void CopyConstructor_ShouldCreateMatrix2WithProvidedValues(
            float m00,
            float m01,
            float m10,
            float m11
        )
        {
            // arrange
            var initialMatrix = new Matrix2(m00, m01, m10, m11);

            // act
            var result = new Matrix2(initialMatrix);

            // assert
            using var _ = new AssertionScope();
            result[0, 0].Should().Be(m00);
            result[0, 1].Should().Be(m01);
            result[1, 0].Should().Be(m10);
            result[1, 1].Should().Be(m11);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Index_ShouldReturnValueAtPosition(int x, int y)
        {
            // arrange
            var sut = (x, y) switch
            {
                (0, 0) => new Matrix2(MathF.PI, 0f, 0f, 0f),
                (0, 1) => new Matrix2(0f, MathF.PI, 0f, 0f),
                (1, 0) => new Matrix2(0f, 0f, MathF.PI, 0f),
                (1, 1) => new Matrix2(0f, 0f, 0f, MathF.PI),
                _ => throw new InvalidOperationException()
            };

            // act
            // assert
            sut[x, y].Should().Be(MathF.PI);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(2, 0)]
        [InlineData(2, 2)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        public void Indexer_ShouldThrowArgumentOutOfRangeException_WhenInputIsNotInTheMatrix(
            int x,
            int y
        )
        {
            // arrange
            var sut = new Matrix2(1f, 2f, 3f, 4f);

            // act
            Action act = () => { _ = sut[x, y]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage($"*[{x}, {y}] is not valid*");
        }

        [Fact]
        public void GetHashCode_ShouldThrowNotSupportedException()
        {
            // arrange
            var sut = new Matrix2(1f, 2f, 3f, 4f);

            // act
            Action act = () => { _ = sut.GetHashCode(); };

            // assert
            act.Should().Throw<NotSupportedException>();
        }
    }
}
