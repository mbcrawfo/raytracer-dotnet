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

        public static IEnumerable<object> NormalAtTestCases =>
            new object[]
            {
                new object[]
                {
                    Matrix4.Identity, new Point(1f, 0f, 0f), new Vector(1f, 0f, 0f)
                },
                new object[]
                {
                    Matrix4.Identity, new Point(0f, 1f, 0f), new Vector(0f, 1f, 0f)
                },
                new object[]
                {
                    Matrix4.Identity, new Point(0f, 0f, 1f), new Vector(0f, 0f, 1f)
                },
                new object[]
                {
                    Matrix4.Identity,
                    new Point(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3),
                    new Vector(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3)
                },
                new object[]
                {
                    Matrix4.Translation(0f, 1f, 0f),
                    new Point(0f, 1.70711f, -0.70711f),
                    new Vector(0f, 0.70711f, -0.70711f)
                },
                new object[]
                {
                    Matrix4.Scaling(1f, 0.5f, 1f) * Matrix4.RotationZ(MathF.PI / 5f),
                    new Point(0f, MathF.Sqrt(2f) / 2f, -MathF.Sqrt(2f) / 2f),
                    new Vector(0f, 0.97014f, -0.24254f)
                }
            };

        [Theory]
        [MemberData(nameof(NormalAtTestCases))]
        public void NormalAt_ShouldReturnExpectedSurfaceNormal(
            Matrix4 transform,
            Point point,
            Vector expected
        )
        {
            // arrange
            var sut = new Sphere { Transform = transform };

            // act
            var actual = sut.NormalAt(point);

            // assert
            using var _ = new AssertionScope();
            actual.X.Should().BeApproximately(expected.X, 1e-5f);
            actual.Y.Should().BeApproximately(expected.Y, 1e-5f);
            actual.Z.Should().BeApproximately(expected.Z, 1e-5f);
        }

        [Fact]
        public void Intersect_ShouldReturnTwoIdenticalIntersections_WhenRayIsTangentToTheSphere()
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
        public void Intersect_ShouldReturnTwoIntersections_WhenIntersectingAScaledSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere { Transform = Matrix4.Scaling(2f, 2f, 2f) };

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Select(x => x.Time).Should().HaveCount(2).And.ContainInOrder(3f, 7f);
        }

        [Fact]
        public void Intersect_ShouldReturnTwoIntersections_WhenRayOriginatesInsideTheSphere()
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
        public void Intersect_ShouldReturnTwoIntersections_WhenRayPassesThroughTheSphere()
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
        public void Intersect_ShouldReturnTwoIntersections_WhenTheSphereIsBehindTheRay()
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

        [Fact]
        public void Intersect_ShouldReturnZeroIntersections_WhenIntersectingATranslatedSphere()
        {
            // arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sut = new Sphere { Transform = Matrix4.Translation(5f, 0f, 0f) };

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Intersect_ShouldReturnZeroIntersections_WhenRayMissesTheSphere()
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
        public void NormalAt_ShouldReturnANormalizedVector()
        {
            // arrange
            var point = new Point(Sqrt3Over3, Sqrt3Over3, Sqrt3Over3);
            var sut = new Sphere();

            // act
            var result = sut.NormalAt(point);

            // assert
            result.Should().Be(result.Normalize());
        }
    }
}
