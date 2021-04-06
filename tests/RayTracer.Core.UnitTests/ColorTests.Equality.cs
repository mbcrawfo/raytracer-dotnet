using FluentAssertions;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class ColorTests
    {
        [Theory]
        [MemberData(nameof(ColorsThatAreNotEquivalent))]
        public void Equals__Color_ShouldReturnFalse_WhenRhsIsNotAnEquivalentColor(
            Color lhs,
            Color rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreEquivalent))]
        public void Equals__Color_ShouldReturnTrue_WhenRhsIsAnEquivalentColor(
            Color lhs,
            Color rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreNotEquivalent))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentColor(
            Color color,
            object obj
        )
        {
            // arrange
            // act
            var result = color.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ObjectsThatAreNotColors))]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotColor(object obj)
        {
            // arrange
            var sut = new Color(1, 2, 3);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreEquivalent))]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentColor(
            Color color,
            object obj
        )
        {
            // arrange
            // act
            var result = color.Equals(obj);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreNotEquivalent))]
        public void Op__Equality_ShouldReturnFalse_WhenRhsIsNotAnEquivalentColor(
            Color lhs,
            Color rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreEquivalent))]
        public void Op__Equality_ShouldReturnTrue_WhenRhsIsAnEquivalentColor(
            Color lhs,
            Color rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreEquivalent))]
        public void Op__Inequality_ShouldReturnFalse_WhenRhsIsAnEquivalentColor(
            Color lhs,
            Color rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ColorsThatAreNotEquivalent))]
        public void Op__Inequality_ShouldReturnTrue_WhenRhsIsNotAnEquivalentColor(
            Color lhs,
            Color rhs
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
