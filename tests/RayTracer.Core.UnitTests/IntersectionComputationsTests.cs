using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class IntersectionComputationsTests
    {
        [Fact]
        public void Constructor_ShouldOffsetOverPointUsingTheSurfaceNormal()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var intersection = new Intersection(
                new Sphere { Transform = Matrix4.Translation(0f, 0f, 1f) },
                5f
            );

            // act
            var result = new IntersectionComputations(ray, intersection);

            // assert
            using var _ = new AssertionScope();
            result.OverPoint.Z.Should().BeLessThan(-FloatExtensions.ComparisonEpsilon / 2f);
            result.Point.Z.Should().BeGreaterThan(result.OverPoint.Z);
        }

        [Fact]
        public void Constructor_ShouldPreComputeIntersectionState_WhenIntersectionIsOnTheInside()
        {
            // arrange
            var ray = new Ray(Point.Origin, Vector.UnitZ);
            var intersection = new Intersection(new Sphere(), 1f);

            // act
            var result = new IntersectionComputations(ray, intersection);

            // assert
            using var _ = new AssertionScope();
            result.Shape.Should().BeSameAs(intersection.Shape);
            result.Time.Should().Be(intersection.Time);
            result.Point.Should().Be(new Point(0f, 0f, 1f));
            result.Eye.Should().Be(new Vector(0f, 0f, -1f));
            result.Normal.Should().Be(new Vector(0f, 0f, -1f));
            result.HitInside.Should().BeTrue();
        }

        [Fact]
        public void Constructor_ShouldPreComputeIntersectionState_WhenIntersectionIsOnTheOutside()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var intersection = new Intersection(new Sphere(), 4f);

            // act
            var result = new IntersectionComputations(ray, intersection);

            // assert
            using var _ = new AssertionScope();
            result.Shape.Should().BeSameAs(intersection.Shape);
            result.Time.Should().Be(intersection.Time);
            result.Point.Should().Be(new Point(0f, 0f, -1f));
            result.Eye.Should().Be(new Vector(0f, 0f, -1f));
            result.Normal.Should().Be(new Vector(0f, 0f, -1f));
            result.HitInside.Should().BeFalse();
        }
    }
}
