using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class Matrix2Tests
    {
        [Theory]
        [MemberData(nameof(Matrix2ThatAreNotEquivalent))]
        public void Equals__Matrix2_ShouldReturnFalse_WhenRhsIsNotAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreEquivalent))]
        public void Equals__Matrix2_ShouldReturnTrue_WhenRhsIsAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreNotEquivalent))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentMatrix2(
            Matrix2 matrix,
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
        [MemberData(nameof(ObjectsThatAreNotMatrix2))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotMatrix2(object obj)
        {
            // arrange
            var sut = new Matrix2(1f, 2f, 3f, 4f);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreEquivalent))]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentMatrix2(
            Matrix2 matrix,
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
        [MemberData(nameof(Matrix2ThatAreNotEquivalent))]
        public void Op__Equality_ShouldReturnFalse_WhenRhsIsNotAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreEquivalent))]
        public void Op__Equality_ShouldReturnTrue_WhenRhsIsAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreEquivalent))]
        public void Op__Inequality_ShouldReturnFalse_WhenRhsIsAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Matrix2ThatAreNotEquivalent))]
        public void Op__Inequality_ShouldReturnTrue_WhenRhsIsNotAnEquivalentMatrix2(
            Matrix2 lhs,
            Matrix2 rhs
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
