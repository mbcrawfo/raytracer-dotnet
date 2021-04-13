using FluentAssertions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Shapes
{
    public class SphereTests
    {
        [Fact]
        public void Intersect_ShouldReturnNotReturnAnyTimePoints_WhenRayMissesTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 2f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Intersect_ShouldReturnTwoIdenticalTimePoints_WhenRayIsTangentToTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 1f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().HaveCount(2).And.ContainInOrder(5f, 5f);
        }

        [Fact]
        public void Intersect_ShouldReturnTwoTimePoints_WhenRayOriginatesInsideTheSphere()
        {
            // arrange
            var ray = new Ray(Point.Origin, new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().HaveCount(2).And.ContainInOrder(-1f, 1f);
        }

        [Fact]
        public void Intersect_ShouldReturnTwoTimePoints_WhenRayPassesThroughTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().HaveCount(2).And.ContainInOrder(4f, 6f);
        }

        [Fact]
        public void Intersect_ShouldReturnTwoTimePoints_WhenTheSphereIsBehindTheRay()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, 5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().HaveCount(2).And.ContainInOrder(-6f, -4f);
        }
    }
}
