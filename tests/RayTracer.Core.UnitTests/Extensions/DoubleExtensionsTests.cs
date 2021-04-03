using FluentAssertions;
using RayTracer.Core.Extensions;
using Xunit;

namespace RayTracer.Core.UnitTests.Extensions
{
    public class DoubleExtensionsTests
    {
        [Theory]
        [InlineData(0.0, 0.0, true)]
        [InlineData(0.0, double.Epsilon, true)]
        [InlineData(1.0 / 3.0, 0.333333333, true)]
        [InlineData(0.0, 1.0, false)]
        [InlineData(0.0, -1.0, false)]
        public void ApproximatelyEquals_ShouldReturnExpectedResult(
            double lhs,
            double rhs,
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
