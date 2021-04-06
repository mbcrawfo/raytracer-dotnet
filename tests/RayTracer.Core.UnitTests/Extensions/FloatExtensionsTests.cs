using System.Collections.Generic;
using FluentAssertions;
using RayTracer.Core.Extensions;
using Xunit;

namespace RayTracer.Core.UnitTests.Extensions
{
    public class FloatExtensionsTests
    {
        public static IEnumerable<object> ClampTestCases =>
            new object[]
            {
                // initialValue, min, max, expectedValue
                new object[] { 0f, 0f, 1f, 0f },
                new object[] { 1f, 0f, 1f, 1f },
                new object[] { 0.5f, 0f, 1f, 0.5f },
                new object[] { -1f, 0f, 1f, 0f },
                new object[] { 2f, 0f, 1f, 1f },
                new object[] { 500f, 0f, 10f, 10f },
                new object[] { -500f, 0f, 10f, 0f },
            };

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

        [Theory]
        [MemberData(nameof(ClampTestCases))]
        public void Clamp_ShouldReturnExpectedValue(
            float initialValue,
            float min,
            float max,
            float expected
        )
        {
            // arrange
            // act
            var actual = initialValue.Clamp(min, max);

            // assert
            actual.Should().Be(expected);
        }
    }
}
