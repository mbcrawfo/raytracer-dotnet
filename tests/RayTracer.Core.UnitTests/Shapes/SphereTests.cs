using System;
using System.Collections.Generic;
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
        private static readonly float Sqrt3Over3 = MathF.Sqrt(3f) / 3f;

        public static IEnumerable<object> LocalNormalAtTestCases =>
            new object[]
            {
                new object[] { new Point(1f, 0f, 0f), new Vector(1f, 0f, 0f) },
                new object[] { new Point(0f, 1f, 0f), new Vector(0f, 1f, 0f) },
                new object[] { new Point(0f, 0f, 1f), new Vector(0f, 0f, 1f) },
                new object[]
                {
                    new Point(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3),
                    new Vector(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3)
                }
            };

        [Theory]
        [MemberData(nameof(LocalNormalAtTestCases))]
        public void LocalNormalAt_ShouldReturnExpectedSurfaceNormal(
            Point point,
            Vector expected
        )
        {
            // arrange
            var sut = new Sphere();

            // act
            var actual = sut.LocalNormalAt(point);

            // assert
            using var _ = new AssertionScope();
            actual.X.Should().BeApproximately(expected.X, 1e-5f);
            actual.Y.Should().BeApproximately(expected.Y, 1e-5f);
            actual.Z.Should().BeApproximately(expected.Z, 1e-5f);
        }

        [Fact]
        public void
            LocalIntersect_ShouldReturnTwoIdenticalIntersections_WhenRayIsTangentToTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 1f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(5f, 5f);
            result[0].Shape.Should().BeSameAs(sut);
            result[1].Shape.Should().BeSameAs(sut);
        }

        [Fact]
        public void LocalIntersect_ShouldReturnTwoIntersections_WhenRayOriginatesInsideTheSphere()
        {
            // arrange
            var ray = new Ray(Point.Origin, new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(-1f, 1f);
            result[0].Shape.Should().BeSameAs(sut);
            result[1].Shape.Should().BeSameAs(sut);
        }

        [Fact]
        public void LocalIntersect_ShouldReturnTwoIntersections_WhenRayPassesThroughTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(4f, 6f);
            result[0].Shape.Should().BeSameAs(sut);
            result[1].Shape.Should().BeSameAs(sut);
        }

        [Fact]
        public void LocalIntersect_ShouldReturnTwoIntersections_WhenTheSphereIsBehindTheRay()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, 5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            using var _ = new AssertionScope();
            result.Should().HaveCount(2);
            result.Select(x => x.Time).Should().ContainInOrder(-6f, -4f);
            result[0].Shape.Should().BeSameAs(sut);
            result[1].Shape.Should().BeSameAs(sut);
        }

        [Fact]
        public void LocalIntersect_ShouldReturnZeroIntersections_WhenRayMissesTheSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 2f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere();

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void LocalNormalAt_ShouldReturnANormalizedVector()
        {
            // arrange
            var point = new Point(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3);
            var sut = new Sphere { Position = new Point(1f, 2f, 3f), Radius = 2f };

            // act
            var result = sut.LocalNormalAt(point);

            // assert
            result.Should().Be(result.Normalize());
        }
    }
}
