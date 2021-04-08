using System;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using RayTracer.Core.UnitTests.Assertions;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class VectorTests
    {
        [Theory]
        [InlineData(1f, 2f, 3f)]
        [InlineData(4.5f, 5.6f, 6.7f)]
        [InlineData(-1f, -2f, -3f)]
        public void Constructor_ShouldCreateVectorWithProvidedValues(float x, float y, float z)
        {
            // arrange
            // act
            var result = new Vector(x, y, z);

            // assert
            using var _ = new AssertionScope();
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Theory]
        [InlineData(1f, 2f, 3f)]
        [InlineData(4.5f, 5.6f, 6.7f)]
        [InlineData(-1f, -2f, -3f)]
        public void CopyConstructor_ShouldCreateVectorWithProvidedValues(float x, float y, float z)
        {
            // arrange
            var initialVector = new Vector(x, y, z);

            // act
            var result = new Vector(initialVector);

            // assert
            using var _ = new AssertionScope();
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Theory]
        [MemberData(nameof(CrossProductTestCases))]
        public void CrossProduct_ShouldCalculateTheExpectedVector_WhenBothTuplesAreVectors(
            Vector sut,
            Vector other,
            Vector expected
        )
        {
            // arrange
            // act
            var actual = sut.CrossProduct(other);

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(DotProductTestCases))]
        public void DotProduct_ShouldCalculateTheExpectedValue(
            Vector sut,
            Vector other,
            float expected
        )
        {
            // arrange
            // act
            var actual = sut.DotProduct(other);

            // assert
            actual.Should().BeApproximately(expected);
        }

        [Theory]
        [MemberData(nameof(MagnitudeTestCases))]
        public void Magnitude_ShouldCalculateTheExpectedValue(
            Vector sut,
            float expected
        )
        {
            // arrange
            // act
            var actual = sut.Magnitude();

            // assert
            actual.Should().BeApproximately(expected);
        }

        [Theory]
        [MemberData(nameof(NormalizationTestCases))]
        public void Normalize_ShouldCalculateTheExpectedUnitVector(
            Vector sut,
            Vector expected
        )
        {
            // arrange
            // act
            var actual = sut.Normalize();

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Deconstruct_ShouldReturnExpectedComponentValues()
        {
            // arrange
            var sut = new Vector(1f, 2f, 3f);

            // act
            var (x, y, z) = sut;

            // assert
            using var _ = new AssertionScope();
            x.Should().Be(sut.X);
            y.Should().Be(sut.Y);
            z.Should().Be(sut.Z);
        }

        [Fact]
        public void GetHashCode_ShouldThrowNotSupportedException()
        {
            // arrange
            var sut = new Vector(1f, 2f, 3f);

            // act
            Action act = () => { _ = sut.GetHashCode(); };

            // assert
            act.Should().Throw<NotSupportedException>();
        }
    }
}
