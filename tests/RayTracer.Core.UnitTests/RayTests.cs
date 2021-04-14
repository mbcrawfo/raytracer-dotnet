using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class RayTests
    {
        public static IEnumerable<object> PositionTestCases =>
            new object[]
            {
                new object[] { 0f, new Point(2f, 3f, 4f) },
                new object[] { 1f, new Point(3f, 3f, 4f) },
                new object[] { -1f, new Point(1f, 3f, 4f) },
                new object[] { 2.5f, new Point(4.5f, 3f, 4f) },
            };

        [Theory]
        [MemberData(nameof(PositionTestCases))]
        public void Position_ShouldReturnExpectedPointForEachTime(float time, Point expected)
        {
            // arrange
            var sut = new Ray(new Point(2f, 3f, 4f), new Vector(1f, 0f, 0f));

            // act
            var actual = sut.Position(time);

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Constructor__PointAndVector_ShouldAssignPropertiesFromConstructorParams()
        {
            // arrange
            var expectedOrigin = new Point(1f, 2f, 3f);
            var expectedDirection = new Vector(4f, 5f, 6f);

            // act
            var result = new Ray(expectedOrigin, expectedDirection);

            // assert
            result.Should()
                .BeEquivalentTo(new { Origin = expectedOrigin, Direction = expectedDirection });
        }

        [Fact]
        public void Deconstruct_ShouldAssignPropertiesToOutputVariables()
        {
            // arrange
            var expectedOrigin = new Point(1f, 2f, 3f);
            var expectedDirection = new Vector(4f, 5f, 6f);
            var sut = new Ray(expectedOrigin, expectedDirection);

            // act
            var (actualOrigin, actualDirection) = sut;

            // assert
            using var _ = new AssertionScope();
            actualOrigin.Should().Be(expectedOrigin);
            actualDirection.Should().Be(expectedDirection);
        }

        [Fact]
        public void Transform_ShouldScaleARay()
        {
            // arrange
            var matrix = Matrix4.Scaling(2f, 3f, 4f);
            var sut = new Ray(new Point(1f, 2f, 3f), new Vector(0f, 1f, 0f));

            // act
            var result = sut.Transform(matrix);

            // assert
            using var _ = new AssertionScope();
            result.Origin.Should().Be(new Point(2f, 6f, 12f));
            result.Direction.Should().Be(new Vector(0f, 3f, 0f));
        }

        [Fact]
        public void Transform_ShouldTranslateARay()
        {
            // arrange
            var matrix = Matrix4.Translation(3f, 4f, 5f);
            var sut = new Ray(new Point(1f, 2f, 3f), new Vector(0f, 1f, 0f));

            // act
            var result = sut.Transform(matrix);

            // assert
            using var _ = new AssertionScope();
            result.Origin.Should().Be(new Point(4f, 6f, 8f));
            result.Direction.Should().Be(new Vector(0f, 1f, 0f));
        }
    }
}
