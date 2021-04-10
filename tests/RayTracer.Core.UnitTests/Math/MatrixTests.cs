using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class MatrixTests
    {
        [Theory]
        [MemberData(nameof(ArrayConstructorInputs))]
        public void Constructor__2DArray_ShouldCreateMatrixMatchingTheInputArray(float[,] array)
        {
            // arrange
            // act
            var result = new TestMatrix(array);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(array.GetLength(0));
            result.Columns.Should().Be(array.GetLength(1));
            foreach (var row in Enumerable.Range(0, result.Rows))
            {
                foreach (var col in Enumerable.Range(0, result.Columns))
                {
                    result[row, col]
                        .Should()
                        .Be(
                            array[row, col],
                            "matrix[{0}, {1}] should equal array[{0}, {1}]",
                            row,
                            col
                        );
                }
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void
            Constructor__ColumnsAndRows_ShouldThrowArgumentOutOfRangeException_WhenColumnsIsNotAValidDimension(
                int columns
            )
        {
            // arrange
            // act
            Action act = () => { _ = new TestMatrix(1, columns); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be positive*")
                .And.ParamName.Should()
                .Be(nameof(columns));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void
            Constructor__ColumnsAndRows_ShouldThrowArgumentOutOfRangeException_WhenRowsIsNotAValidDimension(
                int rows
            )
        {
            // arrange
            // act
            Action act = () => { _ = new TestMatrix(rows, 1); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be positive*")
                .And.ParamName.Should()
                .Be(nameof(rows));
        }

        [Theory]
        [MemberData(nameof(ArrayConstructorInputs))]
        public void CopyConstructor_ShouldCreateMatrixMatchingTheInputMatrix(float[,] array)
        {
            // arrange
            var other = new TestMatrix(array);

            // act
            var result = new TestMatrix(other);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(other.Rows);
            result.Columns.Should().Be(other.Columns);
            foreach (var i in Enumerable.Range(0, result.Rows))
            {
                foreach (var j in Enumerable.Range(0, result.Columns))
                {
                    result[i, j]
                        .Should()
                        .Be(array[i, j], "matrix[{0}, {1}] should equal other[{0}, {1}]", i, j);
                }
            }
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 5)]
        [InlineData(4, 4)]
        [InlineData(10, 20)]
        public void Index_ShouldSetAndReturnValueAtPosition(int rows, int columns)
        {
            // arrange
            var row = rows / 2;
            var col = columns / 2;

            // act
            var sut = new TestMatrix(rows, columns) { [row, col] = MathF.PI };

            // assert
            sut[row, col].Should().Be(MathF.PI);
        }

        [Theory]
        [InlineData(1, 1, -1, -1)]
        [InlineData(1, 1, 0, -1)]
        [InlineData(1, 1, -1, 0)]
        [InlineData(2, 2, 2, 2)]
        [InlineData(2, 3, 2, 3)]
        public void Indexer__Set_ShouldThrowArgumentOutOfRangeException_WhenInputIsNotInTheMatrix(
            int rows,
            int columns,
            int rowToAccess,
            int colToAccess
        )
        {
            // arrange
            var sut = new TestMatrix(rows, columns);

            // act
            Action act = () => { _ = sut[rowToAccess, colToAccess]; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage($"*[{rowToAccess}, {colToAccess}] is not valid*");
        }

        [Fact]
        public void GetHashCode_ShouldThrowNotSupportedException()
        {
            // arrange
            var sut = new TestMatrix(1, 1);

            // act
            Action act = () => { _ = sut.GetHashCode(); };

            // assert
            act.Should().Throw<NotSupportedException>();
        }
    }
}
