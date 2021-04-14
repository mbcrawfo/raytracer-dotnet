using System.Collections.Immutable;
using FluentAssertions;
using RayTracer.Core.Extensions;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Extensions
{
    public class IntersectionCollectionExtensionsTests
    {
        [Fact]
        public void Hit_ShouldReturnNull_WhenAllIntersectionsHaveNegativeTime()
        {
            // arrange
            var sphere = new Sphere();
            var sut = ImmutableList.Create(
                new Intersection(sphere, -2f),
                new Intersection(sphere, -1f)
            );

            // act
            var actual = sut.Hit();

            // assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Hit_ShouldReturnTheFirstIntersection_WhenAllIntersectionsHavePositiveTime()
        {
            // arrange
            var sphere = new Sphere();
            var expected = new Intersection(sphere, 1f);
            var sut = ImmutableList.Create(new Intersection(sphere, 2f), expected);

            // act
            var actual = sut.Hit();

            // assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public void Hit_ShouldReturnTheFirstIntersection_WhenSomeIntersectionsHaveNegativeTime()
        {
            // arrange
            var sphere = new Sphere();
            var expected = new Intersection(sphere, 1f);
            var sut = ImmutableList.Create(expected, new Intersection(sphere, -1f));

            // act
            var actual = sut.Hit();

            // assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public void Hit_ShouldReturnTheLowestNonNegativeIntersection()
        {
            // arrange
            var sphere = new Sphere();
            var expected = new Intersection(sphere, 2f);
            var sut = ImmutableList.Create(
                new Intersection(sphere, 5f),
                new Intersection(sphere, 7f),
                new Intersection(sphere, -3f),
                expected
            );

            // act
            var actual = sut.Hit();

            // assert
            actual.Should().BeSameAs(expected);
        }
    }
}
