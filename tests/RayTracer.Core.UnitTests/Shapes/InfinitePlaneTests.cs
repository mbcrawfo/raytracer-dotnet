using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using RayTracer.Core.UnitTests.Assertions;
using Xunit;

namespace RayTracer.Core.UnitTests.Shapes
{
    public class InfinitePlaneTests
    {
        public static IEnumerable<object> LocalNormalAtTestCases =>
            new[]
            {
                new object[] { Vector.Zero, Point.Origin },
                new object[] { new Vector(1f, 2f, 3f), new Point(4f, 5f, 6f) },
                new object[] { new Vector(1.23f, 4.56f, 7.89f), new Point(1f, 2f, 3f) }
            };

        public static IEnumerable<object> RaysParallelToThePlane =>
            new[]
            {
                new object[] { Vector.UnitX, new Ray(Point.Origin, Vector.UnitY) },
                new object[] { Vector.UnitY, new Ray(Point.Origin, Vector.UnitZ) },
                new object[] { Vector.UnitZ, new Ray(Point.Origin, Vector.UnitX) },
                new object[] { Vector.UnitY, new Ray(new Point(0f, 10f, 0f), Vector.UnitZ) },
                new object[]
                {
                    new Vector(1f, 2f, 3f), new Ray(Point.Origin, new Vector(1f, -2f, 1f))
                },
                new object[]
                {
                    new Vector(1f, 2f, 3f), new Ray(Point.Origin, new Vector(-2f, 1f, 0f))
                },
            };

        public static IEnumerable<object> RaysThatIntersectThePlane =>
            new[]
            {
                new object[]
                {
                    Vector.UnitX, new Ray(new Point(1f, 0f, 0f), -Vector.UnitX), 1f
                },
                new object[]
                {
                    Vector.UnitX, new Ray(new Point(-1f, 0f, 0f), Vector.UnitX), 1f
                },
                new object[]
                {
                    Vector.UnitY, new Ray(new Point(0f, 1f, 0f), -Vector.UnitY), 1f
                },
                new object[]
                {
                    Vector.UnitY, new Ray(new Point(0f, -1f, 0f), Vector.UnitY), 1f
                },
                new object[]
                {
                    Vector.UnitZ, new Ray(new Point(0f, 0f, 1f), -Vector.UnitZ), 1f
                },
                new object[]
                {
                    Vector.UnitZ, new Ray(new Point(0f, 0f, -1f), Vector.UnitZ), 1f
                },
                new object[]
                {
                    new Vector(1f, 1f, 1f),
                    new Ray(new Point(1f, 1f, 1f), new Vector(-1f, -1f, -1f)),
                    1f
                },
                new object[]
                {
                    new Vector(1f, 1f, 1f),
                    new Ray(new Point(-1f, -1f, -1f), new Vector(-1f, -1f, -1f)),
                    -1f
                },
                new object[]
                {
                    new Vector(1f, 2f, 3f),
                    new Ray(new Point(4f, 5f, 6f), new Vector(-4f, -4f, 0f)),
                    8f / 3f
                },
            };

        [Theory]
        [MemberData(nameof(RaysThatIntersectThePlane))]
        public void LocalIntersect_ShouldReturnIntersection_WhenRayIsNotParallelToThePlane(
            in Vector normal,
            Ray ray,
            float expectedTime
        )
        {
            // arrange
            var sut = new InfinitePlane { Normal = normal };

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            using var _ = new AssertionScope();
            result.Should().HaveCount(1);
            result[0].Time.Should().BeApproximately(expectedTime);
            result[0].Shape.Should().BeSameAs(sut);
        }

        [Theory]
        [MemberData(nameof(RaysParallelToThePlane))]
        public void LocalIntersect_ShouldReturnNoIntersections_WhenRayIsParallelToThePlane(
            in Vector normal,
            Ray ray
        )
        {
            // arrange
            var sut = new InfinitePlane { Normal = normal };

            // act
            var result = sut.LocalIntersect(ray);

            // assert
            result.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(LocalNormalAtTestCases))]
        public void LocalNormalAt_ShouldAlwaysReturnTheNormalOfThePlane(
            in Vector expected,
            in Point point
        )
        {
            // arrange
            var sut = new InfinitePlane { Normal = expected };

            // act
            var actual = sut.LocalNormalAt(point);

            // assert
            actual.Should().Be(expected);
        }
    }
}
