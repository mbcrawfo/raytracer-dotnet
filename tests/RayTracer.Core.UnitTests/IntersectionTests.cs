using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class IntersectionTests
    {
        [Fact]
        public void
            PrepareComputations_ShouldPreComputeIntersectionState_WhenIntersectionIsOnTheInside()
        {
            // arrange
            var ray = new Ray(Point.Origin, Vector.UnitZ);
            var sut = new Intersection(new Sphere(), 1f);

            // act
            var result = sut.PrepareComputations(ray);

            // assert
            using var _ = new AssertionScope();
            result.Shape.Should().BeSameAs(sut.Shape);
            result.Time.Should().Be(sut.Time);
            result.Point.Should().Be(new Point(0f, 0f, 1f));
            result.Eye.Should().Be(new Vector(0f, 0f, -1f));
            result.Normal.Should().Be(new Vector(0f, 0f, -1f));
            result.HitInside.Should().BeTrue();
        }

        [Fact]
        public void
            PrepareComputations_ShouldPreComputeIntersectionState_WhenIntersectionIsOnTheOutside()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var sut = new Intersection(new Sphere(), 4f);

            // act
            var result = sut.PrepareComputations(ray);

            // assert
            using var _ = new AssertionScope();
            result.Shape.Should().BeSameAs(sut.Shape);
            result.Time.Should().Be(sut.Time);
            result.Point.Should().Be(new Point(0f, 0f, -1f));
            result.Eye.Should().Be(new Vector(0f, 0f, -1f));
            result.Normal.Should().Be(new Vector(0f, 0f, -1f));
            result.HitInside.Should().BeFalse();
        }
    }
}
