using System;
using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class VectorTests
    {
        [Fact]
        public void Op__Addition_ShouldAddComponentsTogether()
        {
            // arrange
            var vec1 = new Vector(1f, 2f, 3f);
            var vec2 = new Vector(4f, 5f, 6f);
            var expected = new Vector(5f, 7f, 9f);

            // act
            var result = vec1 + vec2;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__Addition_ShouldBeCommutative()
        {
            // arrange
            var vec1 = new Vector(1f, 2f, 3f);
            var vec2 = new Vector(4f, 5f, 6f);

            // act
            var result1 = vec1 + vec2;
            var result2 = vec2 + vec1;

            // assert
            result1.Should().Be(result2);
        }

        [Fact]
        public void Op__DivideScalar_ShouldDivideAllComponentsByScalarOperand()
        {
            // arrange
            var sut = new Vector(2f, 4f, 6f);
            var expected = new Vector(1f, 2f, 3f);

            // act
            var result = sut / 2f;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__DivideScalar_ShouldThrowDivideByZeroException_WhenScalarIsZero()
        {
            // arrange
            var sut = new Vector(2f, 4f, 6f);

            // act
            Action act = () => { _ = sut / 0f; };

            // assert
            act.Should().Throw<DivideByZeroException>().WithMessage("*divide a vector by 0*");
        }

        [Fact]
        public void Op__MultiplyScalar_ShouldMultiplyAllComponentsByScalarOperand()
        {
            // arrange
            var sut = new Vector(1f, 2f, 3f);
            var expected = new Vector(2f, 4f, 6f);

            // act
            var result = sut * 2f;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__Negation_ShouldNegateAllComponents()
        {
            // arrange
            var sut = new Vector(1f, 2f, 3f);
            var expected = new Vector(-1f, -2f, -3f);

            // act
            var result = -sut;

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Op__Subtraction_ShouldSubtractComponents()
        {
            // arrange
            var vec1 = new Vector(1f, 2f, 3f);
            var vec2 = new Vector(4f, 5f, 6f);
            var expected = new Vector(-3f, -3f, -3f);

            // act
            var result = vec1 - vec2;

            // assert
            result.Should().Be(expected);
        }
    }
}
