using System;
using System.Collections.Immutable;
using FluentAssertions;
using RayTracer.Core.Extensions;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Extensions
{
    public partial class IntersectionCollectionExtensionsTests
    {
        [Theory]
        [MemberData(nameof(TestCasesWhenListDoesNotContainHit))]
        public void Hit_ShouldNotReturnAnIntersection_WhenListDoesNotContainAnyHits(
            IImmutableList<Intersection> sut
        )
        {
            // arrange
            // act
            var result = sut.Hit();

            // assert
            result.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TestCasesWhenListContainsHit))]
        public void Hit_ShouldReturnExpectedIntersection_WhenListContainsAHit(
            IImmutableList<Intersection> sut,
            float expectedTime
        )
        {
            // arrange
            // act
            var actual = sut.Hit()?.Time;

            // assert
            actual.Should().Be(expectedTime);
        }

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

        [Fact]
        public void
            Hit_ShouldThrowInvalidOperationException_WhenIntersectionsIsNotAnImmutableArrayOrImmutableList()
        {
            // arrange
            var sut = new TestImmutableList { Count = 10 };

            // act
            Action act = () => { _ = sut.Hit(); };

            // assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Unexpected collection*");
        }
    }
}
