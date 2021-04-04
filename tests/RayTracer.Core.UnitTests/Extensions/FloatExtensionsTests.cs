using FluentAssertions;
using RayTracer.Core.Extensions;
using Xunit;

namespace RayTracer.Core.UnitTests.Extensions
{
    public class FloatExtensionsTests
    {
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, float.Epsilon, true)]
        [InlineData(1f / 3f, 0.333333f, true)]
        [InlineData(0, 1, false)]
        [InlineData(0, -1, false)]
        public void ApproximatelyEquals_ShouldReturnExpectedResult(
            float lhs,
            float rhs,
            bool expected
        )
        {
            // arrange
            // act
            var actual = lhs.ApproximatelyEquals(rhs);

            // assert
            actual.Should().Be(expected, $"{lhs} should {(expected ? "" : "not ")} equal {rhs}");
        }
    }
}
