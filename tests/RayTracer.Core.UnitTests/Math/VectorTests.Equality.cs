using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class VectorTests
    {
        [Theory]
        [MemberData(nameof(VectorsThatAreNotEquivalent))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentVector(
            Vector tuple,
            object obj
        )
        {
            // arrange
            // act
            var result = tuple.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ObjectsThatAreNotVectors))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotVector(object obj)
        {
            // arrange
            var sut = new Vector(1, 2, 3);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreEquivalent))]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentVector(
            Vector tuple,
            object obj
        )
        {
            // arrange
            // act
            var result = tuple.Equals(obj);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreNotEquivalent))]
        public void Equals__Vector_ShouldReturnFalse_WhenRhsIsNotAnEquivalentVector(
            Vector lhs,
            Vector rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreEquivalent))]
        public void Equals__Vector_ShouldReturnTrue_WhenRhsIsAnEquivalentVector(
            Vector lhs,
            Vector rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreNotEquivalent))]
        public void Op__Equality_ShouldReturnFalse_WhenRhsIsNotAnEquivalentVector(
            Vector lhs,
            Vector rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreEquivalent))]
        public void Op__Equality_ShouldReturnTrue_WhenRhsIsAnEquivalentVector(
            Vector lhs,
            Vector rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreEquivalent))]
        public void Op__Inequality_ShouldReturnFalse_WhenRhsIsAnEquivalentVector(
            Vector lhs,
            Vector rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(VectorsThatAreNotEquivalent))]
        public void Op__Inequality_ShouldReturnTrue_WhenRhsIsNotAnEquivalentVector(
            Vector lhs,
            Vector rhs
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
