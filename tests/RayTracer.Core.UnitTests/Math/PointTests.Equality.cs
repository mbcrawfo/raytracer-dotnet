using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class PointTests
    {
        [Theory]
        [MemberData(nameof(PointsThatAreNotEquivalent))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentPoint(
            Point tuple,
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
        [MemberData(nameof(ObjectsThatAreNotPoints))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotPoint(object obj)
        {
            // arrange
            var sut = new Point(1, 2, 3);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreEquivalent))]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentPoint(
            Point tuple,
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
        [MemberData(nameof(PointsThatAreNotEquivalent))]
        public void Equals__Point_ShouldReturnFalse_WhenRhsIsNotAnEquivalentPoint(
            Point lhs,
            Point rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreEquivalent))]
        public void Equals__Point_ShouldReturnTrue_WhenRhsIsAnEquivalentPoint(
            Point lhs,
            Point rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreNotEquivalent))]
        public void Op__Equality_ShouldReturnFalse_WhenRhsIsNotAnEquivalentPoint(
            Point lhs,
            Point rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreEquivalent))]
        public void Op__Equality_ShouldReturnTrue_WhenRhsIsAnEquivalentPoint(
            Point lhs,
            Point rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreEquivalent))]
        public void Op__Inequality_ShouldReturnFalse_WhenRhsIsAnEquivalentPoint(
            Point lhs,
            Point rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PointsThatAreNotEquivalent))]
        public void Op__Inequality_ShouldReturnTrue_WhenRhsIsNotAnEquivalentPoint(
            Point lhs,
            Point rhs
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
