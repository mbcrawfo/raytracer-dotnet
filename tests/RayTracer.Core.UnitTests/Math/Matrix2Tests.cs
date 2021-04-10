using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public class Matrix2Tests
    {
        public static IEnumerable<object> ArraysThatAreNot2X2 =>
            new object[]
            {
                new object[] { new float[1, 1] },
                new object[] { new float[2, 1] },
                new object[] { new float[1, 2] },
                new object[] { new float[3, 3] },
            };

        [Theory]
        [MemberData(nameof(ArraysThatAreNot2X2))]
        public void Constructor__Array_ShouldThrowArgumentException_WhenElementsArrayIsNot2x2(
            float[,] array
        )
        {
            // arrange
            // act
            Action act = () => { _ = new Matrix2(array); };

            // assert
            act.Should().Throw<ArgumentException>().WithMessage("*must be a 2x2 array*");
        }

        [Fact]
        public void Constructor__Array_ShouldCreate2x2MatrixAndAssignElementsFromTheArray()
        {
            // arrange
            var array = new[,] { { 1f, 2f }, { 3f, 4f } };

            // act
            var result = new Matrix2(array);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(2);
            result.Columns.Should().Be(2);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[1, 0].Should().Be(3f);
            result[1, 1].Should().Be(4f);
        }

        [Fact]
        public void Constructor__Copy_ShouldCreate2x2MatrixAndAssignElementsFromTheOtherMatrix()
        {
            // arrange
            var other = new Matrix2(new[,] { { 1f, 2f }, { 3f, 4f } });

            // act
            var result = new Matrix2(other);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(2);
            result.Columns.Should().Be(2);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[1, 0].Should().Be(3f);
            result[1, 1].Should().Be(4f);
        }

        [Fact]
        public void Constructor__Default_ShouldCreate2x2Matrix()
        {
            // arrange
            // act
            var result = new Matrix2();

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(2);
            result.Columns.Should().Be(2);
        }

        [Fact]
        public void Transpose_ShouldSwapRowsAndColumns()
        {
            // arrange
            var sut = new Matrix2(new[,] { { 0f, 9f }, { 9f, 8f } });
            var expected = new Matrix2(new[,] { { 0f, 9f }, { 9f, 8f } });

            // act
            var actual = sut.Transpose();

            // assert
            actual.Should().Be(expected);
        }
    }
}
