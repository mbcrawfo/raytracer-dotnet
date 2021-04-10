using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public class Matrix3Tests
    {
        public static IEnumerable<object> ArraysThatAreNot3X3 =>
            new object[]
            {
                new object[] { new float[2, 2] },
                new object[] { new float[2, 3] },
                new object[] { new float[3, 2] },
                new object[] { new float[4, 4] },
            };

        [Theory]
        [MemberData(nameof(ArraysThatAreNot3X3))]
        public void Constructor__Array_ShouldThrowArgumentException_WhenElementsArrayIsNot3x3(
            float[,] array
        )
        {
            // arrange
            // act
            Action act = () => { _ = new Matrix3(array); };

            // assert
            act.Should().Throw<ArgumentException>().WithMessage("*must be a 3x3 array*");
        }

        public static IEnumerable<object> SubMatrixTestCases =>
            new object[]
            {
                new object[] { 0, 0, new Matrix2(new[,] { { 5f, 6f }, { 8f, 9f } }) },
                new object[] { 0, 1, new Matrix2(new[,] { { 4f, 6f }, { 7f, 9f } }) },
                new object[] { 0, 2, new Matrix2(new[,] { { 4f, 5f }, { 7f, 8f } }) },
                new object[] { 1, 0, new Matrix2(new[,] { { 2f, 3f }, { 8f, 9f } }) },
                new object[] { 1, 1, new Matrix2(new[,] { { 1f, 3f }, { 7f, 9f } }) },
                new object[] { 1, 2, new Matrix2(new[,] { { 1f, 2f }, { 7f, 8f } }) },
                new object[] { 2, 0, new Matrix2(new[,] { { 2f, 3f }, { 5f, 6f } }) },
                new object[] { 2, 1, new Matrix2(new[,] { { 1f, 3f }, { 4f, 6f } }) },
                new object[] { 2, 2, new Matrix2(new[,] { { 1f, 2f }, { 4f, 5f } }) },
            };

        [Theory]
        [MemberData(nameof(SubMatrixTestCases))]
        public void SubMatrix_ShouldReturnExpectedMatrix2(
            int rowToRemove,
            int columnToRemove,
            Matrix2 expected
        )
        {
            // arrange
            var sut = new Matrix3(new[,] { { 1f, 2f, 3f }, { 4f, 5f, 6f }, { 7f, 8f, 9f } });

            // act
            var actual = sut.SubMatrix(rowToRemove, columnToRemove);

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void SubMatrix_ShouldThrowArgumentOutOfRangeException_WhenRowToRemoveIsNotInMatrix(
            int rowToRemove
        )
        {
            // arrange
            var sut = new Matrix3();

            // act
            Action act = () => { _ = sut.SubMatrix(rowToRemove, 0); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in range [0, 3)*")
                .And.ParamName.Should()
                .Be(nameof(rowToRemove));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void
            SubMatrix_ShouldThrowArgumentOutOfRangeException_WhenColumnToRemoveIsNotInMatrix(
                int columnToRemove
            )
        {
            // arrange
            var sut = new Matrix3();

            // act
            Action act = () => { _ = sut.SubMatrix(0, columnToRemove); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in range [0, 3)*")
                .And.ParamName.Should()
                .Be(nameof(columnToRemove));
        }

        [Fact]
        public void Constructor__Array_ShouldCreate3x3MatrixAndAssignElementsFromTheArray()
        {
            // arrange
            var array = new[,] { { 1f, 2f, 3f }, { 4f, 5f, 6f }, { 7f, 8f, 9f } };

            // act
            var result = new Matrix3(array);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(3);
            result.Columns.Should().Be(3);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[0, 2].Should().Be(3f);
            result[1, 0].Should().Be(4f);
            result[1, 1].Should().Be(5f);
            result[1, 2].Should().Be(6f);
            result[2, 0].Should().Be(7f);
            result[2, 1].Should().Be(8f);
            result[2, 2].Should().Be(9f);
        }

        [Fact]
        public void Constructor__Copy_ShouldCreate3x3MatrixAndAssignElementsFromTheOtherMatrix()
        {
            // arrange
            var other = new Matrix3(new[,] { { 1f, 2f, 3f }, { 4f, 5f, 6f }, { 7f, 8f, 9f } });

            // act
            var result = new Matrix3(other);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(3);
            result.Columns.Should().Be(3);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[0, 2].Should().Be(3f);
            result[1, 0].Should().Be(4f);
            result[1, 1].Should().Be(5f);
            result[1, 2].Should().Be(6f);
            result[2, 0].Should().Be(7f);
            result[2, 1].Should().Be(8f);
            result[2, 2].Should().Be(9f);
        }

        [Fact]
        public void Constructor__Default_ShouldCreate3x3Matrix()
        {
            // arrange
            // act
            var result = new Matrix3();

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(3);
            result.Columns.Should().Be(3);
        }

        [Fact]
        public void Transpose_ShouldSwapRowsAndColumns()
        {
            // arrange
            var sut = new Matrix3(new[,] { { 0f, 9f, 3f }, { 9f, 8f, 0f }, { 1f, 8f, 5f } });
            var expected = new Matrix3(new[,] { { 0f, 9f, 1f }, { 9f, 8f, 8f }, { 3f, 0f, 5f } });

            // act
            var actual = sut.Transpose();

            // assert
            actual.Should().Be(expected);
        }
    }
}
