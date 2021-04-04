using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class PointTests
    {
        [Fact]
        public void Op__AddVectorToPoint_ShouldAddComponentsTogether()
        {
            // arrange
            var point = new Point(1f, 2f, 3f);
            var vector = new Vector(4f, 5f, 6f);
            var expected = new Point(5f, 7f, 9f);

            // act
            var result = point + vector;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__SubtractPointFromPoint_ShouldSubtractComponents()
        {
            // arrange
            var point1 = new Point(1f, 2f, 3f);
            var point2 = new Point(4f, 5f, 6f);
            var expected = new Vector(-3f, -3f, -3f);

            // act
            var result = point1 - point2;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__SubtractVectorFromPoint_ShouldSubtractComponents()
        {
            // arrange
            var point = new Point(1f, 2f, 3f);
            var vector = new Vector(4f, 5f, 6f);
            var expected = new Point(-3f, -3f, -3f);

            // act
            var result = point - vector;

            // assert
            result.Should().Be(expected);
        }
    }
}
