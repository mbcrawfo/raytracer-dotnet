using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class WorldTests
    {
        [Fact]
        public void ColorAt_ShouldReturnBlack_WhenRayMisses()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitY);
            var sut = World.Default;

            // act
            var result = sut.ColorAt(ray);

            // assert
            result.Should().Be(Color.Black);
        }

        [Fact]
        public void ColorAt_ShouldReturnExpectedColor_WhenRayHits()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var sut = World.Default;

            // act
            var result = sut.ColorAt(ray);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(0.38066f, 1e-5f);
            result.G.Should().BeApproximately(0.47583f, 1e-5f);
            result.B.Should().BeApproximately(0.2855f, 1e-4f);
        }

        [Fact]
        public void
            ColorAt_ShouldReturnTheColorOfTheInnerSphere_WhenTheRayIsInsideTheOuterSphereAndOutsideTheInnerSphere()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, 0.75f), -Vector.UnitZ);
            var outerSphere = World.Default.Shapes[0] with
            {
                Material = Material.Default with { AmbientReflection = 1f }
            };
            var innerSphere = World.Default.Shapes[1] with
            {
                Material = Material.Default with { AmbientReflection = 1f }
            };
            var sut = World.Default with
            {
                Shapes = ImmutableArray.Create(outerSphere, innerSphere)
            };

            // act
            var result = sut.ColorAt(ray);

            // assert
            result.Should().Be(innerSphere.Material.Color);
        }

        [Fact]
        public void Intersect_ShouldReturnAllIntersectionsOfTheRayAndShapesInTheWorld()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var sut = World.Default;

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Select(x => x.Time).Should().HaveCount(4).And.ContainInOrder(4f, 4.5f, 5.5f, 6f);
        }

        [Fact]
        public void ShadeHit_ShouldShadeAnIntersection()
        {
            // arrange
            var sut = World.Default;
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var computations = new Intersection(sut.Shapes[0], 4f).PrepareComputations(ray);

            // act
            var result = sut.ShadeHit(computations);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(0.38066f, 1e-5f);
            result.G.Should().BeApproximately(0.47583f, 1e-5f);
            result.B.Should().BeApproximately(0.2855f, 1e-4f);
        }

        [Fact]
        public void ShadeHit_ShouldShadeAnIntersection_WhenTheIntersectionIsInsideTheShape()
        {
            // arrange
            var sut = World.Default with
            {
                Lights = ImmutableArray.Create(new PointLight(new(0f, 0.25f, 0f), Color.White))
            };
            var ray = new Ray(Point.Origin, Vector.UnitZ);
            var computations = new Intersection(sut.Shapes[1], 0.5f).PrepareComputations(ray);

            // act
            var result = sut.ShadeHit(computations);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(0.90498f, 1e-5f);
            result.G.Should().BeApproximately(0.90498f, 1e-5f);
            result.B.Should().BeApproximately(0.90498f, 1e-5f);
        }
    }
}
