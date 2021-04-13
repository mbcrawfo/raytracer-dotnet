using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
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
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(5f, 5f);
            result[0].Object.Should().BeSameAs(sut);
            result[1].Object.Should().BeSameAs(sut);
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
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(-1f, 1f);
            result[0].Object.Should().BeSameAs(sut);
            result[1].Object.Should().BeSameAs(sut);
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
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(4f, 6f);
            result[0].Object.Should().BeSameAs(sut);
            result[1].Object.Should().BeSameAs(sut);
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
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(-6f, -4f);
            result[0].Object.Should().BeSameAs(sut);
            result[1].Object.Should().BeSameAs(sut);
        }
    }
}
