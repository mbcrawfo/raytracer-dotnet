using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class MatrixTests
    {
        [Theory]
        [MemberData(nameof(MatricesThatAreNotEquivalent))]
        public void Equals__Matrix_ShouldReturnFalse_WhenRhsIsNotAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreEquivalent))]
        public void Equals__Matrix_ShouldReturnTrue_WhenRhsIsAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreNotEquivalent))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentMatrix(
            Matrix matrix,
            object obj
        )
        {
            // arrange
            // act
            var result = matrix.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ObjectsThatAreNotMatrices))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotMatrix(object obj)
        {
            // arrange
            var sut = new TestMatrix(2, 2);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreEquivalent))]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentMatrix(
            Matrix matrix,
            object obj
        )
        {
            // arrange
            // act
            var result = matrix.Equals(obj);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreNotEquivalent))]
        public void Op__Equality_ShouldReturnFalse_WhenRhsIsNotAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreEquivalent))]
        public void Op__Equality_ShouldReturnTrue_WhenRhsIsAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreEquivalent))]
        public void Op__Inequality_ShouldReturnFalse_WhenRhsIsAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MatricesThatAreNotEquivalent))]
        public void Op__Inequality_ShouldReturnTrue_WhenRhsIsNotAnEquivalentMatrix(
            Matrix lhs,
            Matrix rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeTrue();
        }
    }
}
