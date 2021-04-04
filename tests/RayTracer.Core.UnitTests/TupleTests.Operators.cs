using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class TupleTests
    {
        [Fact]
        public void Op__Addition_ShouldBeCommutative()
        {
            // arrange
            var vec1 = Tuple.Vector(1, 2, 3);
            var vec2 = Tuple.Vector(4, 5, 6);

            // act
            var result1 = vec1 + vec2;
            var result2 = vec2 + vec1;

            // assert
            result1.Should().Be(result2);
        }

        [Fact]
        public void Op__Addition_ShouldReturnPoint_WhenAddingVectorAndPoint()
        {
            // arrange
            var vec1 = Tuple.Point(1, 2, 3);
            var vec2 = Tuple.Vector(4, 5, 6);

            // act
            var result = vec1 + vec2;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Point,
                        W = 1,
                        X = 5,
                        Y = 7,
                        Z = 9
                    }
                );
        }

        [Fact]
        public void Op__Addition_ShouldReturnVector_WhenBothOperandsAreVectors()
        {
            // arrange
            var vec1 = Tuple.Vector(1, 2, 3);
            var vec2 = Tuple.Vector(4, 5, 6);

            // act
            var result = vec1 + vec2;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Vector,
                        W = 0,
                        X = 5,
                        Y = 7,
                        Z = 9
                    }
                );
        }

        [Fact]
        public void Op__Addition_ShouldThrowInvalidOperationException_WhenBothOperandsArePoints()
        {
            // arrange
            // act
            Action act = () => { _ = Tuple.Point(1, 2, 3) + Tuple.Point(1, 2, 3); };

            // assert
            act.Should().Throw<InvalidOperationException>().WithMessage("*not a valid operation*");
        }

        [Fact]
        public void Op__Negation_ShouldNegateXYZComponentsAndPreserveType_WhenOperandIsPoint()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            var result = -sut;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Point,
                        W = 1,
                        X = -1,
                        Y = -2,
                        Z = -3
                    }
                );
        }

        [Fact]
        public void Op__Negation_ShouldNegateXYZComponentsAndPreserveType_WhenOperandIsVector()
        {
            // arrange
            var sut = Tuple.Vector(1, 2, 3);

            // act
            var result = -sut;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Vector,
                        W = 0,
                        X = -1,
                        Y = -2,
                        Z = -3
                    }
                );
        }

        [Fact]
        public void Op__Subtraction_ShouldReturnPoint_WhenAddingVectorAndPoint()
        {
            // arrange
            var vec1 = Tuple.Point(1, 2, 3);
            var vec2 = Tuple.Vector(4, 5, 6);

            // act
            var result = vec1 - vec2;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Point,
                        W = 1,
                        X = -3,
                        Y = -3,
                        Z = -3
                    }
                );
        }

        [Fact]
        public void Op__Subtraction_ShouldReturnVector_WhenBothOperandsArePoints()
        {
            // arrange
            var vec1 = Tuple.Point(1, 2, 3);
            var vec2 = Tuple.Point(4, 5, 6);

            // act
            var result = vec1 - vec2;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Vector,
                        W = 0,
                        X = -3,
                        Y = -3,
                        Z = -3
                    }
                );
        }

        [Fact]
        public void Op__Subtraction_ShouldReturnVector_WhenBothOperandsAreVectors()
        {
            // arrange
            var vec1 = Tuple.Vector(1, 2, 3);
            var vec2 = Tuple.Vector(4, 5, 6);

            // act
            var result = vec1 - vec2;

            // assert
            result.Should()
                .BeEquivalentTo(
                    new
                    {
                        Type = TupleType.Vector,
                        W = 0,
                        X = -3,
                        Y = -3,
                        Z = -3
                    }
                );
        }

        [Fact]
        public void
            Op__Subtraction_ShouldThrowInvalidOperationException_WhenResultHasInvalidWComponent()
        {
            // arrange
            var propertyInfo = typeof(Tuple).GetProperty(nameof(Tuple.W));
            Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");

            object boxedTuple = Tuple.Vector(1, 2, 3);
            propertyInfo.SetValue(boxedTuple, 5.0);
            var lhs = (Tuple) boxedTuple;

            // act
            Action act = () => { _ = lhs - Tuple.Zero; };

            // assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Unexpected result*");
        }
    }
}
