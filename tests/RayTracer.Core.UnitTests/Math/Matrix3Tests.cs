using System;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix3Tests
    {
        [Theory]
        [InlineData(0, 0, -12f, -12f)]
        [InlineData(1, 0, 25f, -25f)]
        public void Cofactor_ShouldReturnExpectedValue(
            int row,
            int column,
            float expectedMinor,
            float expectedCofactor
        )
        {
            // arrange
            var sut = new Matrix3(new[,] { { 3f, 5f, 0f }, { 2f, -1f, -7f }, { 6f, -1f, 5f } });

            // act
            var actualMinor = sut.Minor(row, column);
            var actualCofactor = sut.Cofactor(row, column);

            // assert
            using var _ = new AssertionScope();
            actualMinor.Should().Be(expectedMinor);
            actualCofactor.Should().Be(expectedCofactor);
        }

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

        [Theory]
        [MemberData(nameof(SubMatrixTestCases))]
        public void SubMatrix_ShouldReturnExpectedMatrix2(int row, int column, Matrix2 expected)
        {
            // arrange
            var sut = new Matrix3(new[,] { { 1f, 2f, 3f }, { 4f, 5f, 6f }, { 7f, 8f, 9f } });

            // act
            var actual = sut.SubMatrix(row, column);

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void SubMatrix_ShouldThrowArgumentOutOfRangeException_WhenRowToRemoveIsNotInMatrix(
            int row
        )
        {
            // arrange
            var sut = new Matrix3();

            // act
            Action act = () => { _ = sut.SubMatrix(row, 0); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in range [0, 3)*")
                .And.ParamName.Should()
                .Be(nameof(row));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void
            SubMatrix_ShouldThrowArgumentOutOfRangeException_WhenColumnToRemoveIsNotInMatrix(
                int column
            )
        {
            // arrange
            var sut = new Matrix3();

            // act
            Action act = () => { _ = sut.SubMatrix(0, column); };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*must be in range [0, 3)*")
                .And.ParamName.Should()
                .Be(nameof(column));
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
        public void Determinant_ShouldReturnExpectedValue()
        {
            // arrange
            var sut = new Matrix3(new[,] { { 1f, 2f, 6f }, { -5f, 8f, -4f }, { 2f, 6f, 4f } });

            // act
            var cofactor00 = sut.Cofactor(0, 0);
            var cofactor01 = sut.Cofactor(0, 1);
            var cofactor02 = sut.Cofactor(0, 2);
            var determinant = sut.Determinant();

            // assert
            using var _ = new AssertionScope();
            cofactor00.Should().Be(56f);
            cofactor01.Should().Be(12f);
            cofactor02.Should().Be(-46f);
            determinant.Should().Be(-196f);
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
        public void Minor_ShouldReturnTheDeterminantOfTheSubMatrix()
        {
            // arrange
            const int row = 1;
            const int column = 0;
            var sut = new Matrix3(new[,] { { 3f, 5f, 0f }, { 2f, -1f, -7f }, { 6f, -1f, 5f } });
            var subMatrix = sut.SubMatrix(row, column);

            // act
            var minor = sut.Minor(row, column);

            // assert
            minor.Should().Be(subMatrix.Determinant());
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
