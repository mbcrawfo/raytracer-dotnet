using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Materials;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
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
                Material = PhongMaterial.Default with { AmbientReflection = 1f }
            };
            var innerSphere = World.Default.Shapes[1] with
            {
                Material = PhongMaterial.Default with { AmbientReflection = 1f }
            };
            var sut = World.Default with
            {
                Shapes = ImmutableArray.Create(outerSphere, innerSphere)
            };
            var expected = PhongMaterial.Default.Color;

            // act
            var actual = sut.ColorAt(ray);

            // assert
            actual.Should().Be(expected);
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
        public void IsInShadow_ShouldReturnFalse_WhenAnObjectIsBehindTheLight()
        {
            // arrange
            var sut = World.Default;
            var point = new Point(-20f, 20f, 20f);

            // act
            var result = sut.IsInShadow(point, sut.Lights[0]);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsInShadow_ShouldReturnFalse_WhenAnObjectIsBehindThePoint()
        {
            // arrange
            var sut = World.Default;
            var point = new Point(-2f, 2f, -2f);

            // act
            var result = sut.IsInShadow(point, sut.Lights[0]);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsInShadow_ShouldReturnFalse_WhenNothingIsCollinearWithPointAndLight()
        {
            // arrange
            var sut = World.Default;
            var point = new Point(0f, 10f, 0f);

            // act
            var result = sut.IsInShadow(point, sut.Lights[0]);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsInShadow_ShouldReturnTrue_WhenAnObjectIsBetweenThePointAndTheLight()
        {
            // arrange
            var sut = World.Default;
            var point = new Point(10f, -10f, 10f);

            // act
            var result = sut.IsInShadow(point, sut.Lights[0]);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ShadeHit_ShouldShadeAnIntersection()
        {
            // arrange
            var sut = World.Default;
            var computations = new IntersectionComputations(
                new Ray(new(0f, 0f, -5f), Vector.UnitZ),
                new Intersection(sut.Shapes[0], 4f)
            );

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
            var computations = new IntersectionComputations(
                new Ray(Point.Origin, Vector.UnitZ),
                new Intersection(sut.Shapes[1], 0.5f)
            );

            // act
            var result = sut.ShadeHit(computations);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(0.90498f, 1e-5f);
            result.G.Should().BeApproximately(0.90498f, 1e-5f);
            result.B.Should().BeApproximately(0.90498f, 1e-5f);
        }

        [Fact]
        public void ShadeHit_ShouldShadeAnIntersection_WhenTheIntersectionPointIsInShadow()
        {
            // arrange
            var sut = new World
            {
                Lights = ImmutableArray.Create(new PointLight(new(0f, 0f, -10f), Color.White)),
                Shapes = ImmutableArray.Create<Shape>(
                    new Sphere(),
                    new Sphere { Transform = Matrix4.Translation(0f, 0f, 10f) }
                )
            };
            var computations = new IntersectionComputations(
                new Ray(new Point(0f, 0f, 5f), Vector.UnitZ),
                new Intersection(sut.Shapes[1], 4f)
            );

            // act
            var result = sut.ShadeHit(computations);

            // assert
            result.Should().Be(new Color(0.1f, 0.1f, 0.1f));
        }
    }
}
