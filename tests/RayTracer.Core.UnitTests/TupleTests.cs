using System;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.UnitTests.Assertions;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class TupleTests
    {
        [Theory]
        [MemberData(nameof(PairsOfTuplesWhereOneOperandIsAPoint))]
        public void CrossProduct_ShouldThrowInvalidOperationException_WhenEitherTupleIsAPoint(
            Tuple sut,
            Tuple other
        )
        {
            // arrange
            // act
            Action act = () => { _ = sut.CrossProduct(other); };

            // assert
            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("*only valid when both tuples are vectors*");
        }

        [Theory]
        [MemberData(nameof(CrossProductTestCases))]
        public void CrossProduct_ShouldCalculateTheExpectedVector_WhenBothTuplesAreVectors(
            Tuple sut,
            Tuple other,
            Tuple expected
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
        public void DotProduct_ShouldCalculateTheExpectedValue_WhenBothTuplesAreVectors(
            Tuple sut,
            Tuple other,
            double expected
        )
        {
            // arrange
            // act
            var actual = sut.DotProduct(other);

            // assert
            actual.Should().BeApproximately(expected);
        }

        [Theory]
        [MemberData(nameof(PairsOfTuplesWhereOneOperandIsAPoint))]
        public void DotProduct_ShouldThrowInvalidOperationException_WhenEitherTupleIsAPoint(
            Tuple sut,
            Tuple other
        )
        {
            // arrange
            // act
            Action act = () => { _ = sut.DotProduct(other); };

            // assert
            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("*only valid when both tuples are vectors*");
        }

        [Theory]
        [MemberData(nameof(MagnitudeTestCases))]
        public void Magnitude_ShouldCalculateTheExpectedValue_WhenTupleIsAVector(
            Tuple sut,
            double expected
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
        public void Normalize_ShouldCalculateTheExpectedUnitVector_WhenTupleIsAVector(
            Tuple sut,
            Tuple expected
        )
        {
            // arrange
            // act
            var actual = sut.Normalize();

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4.5, 5.6, 6.7)]
        [InlineData(-1, -2, -3)]
        public void Point_ShouldCreateTupleWithProvidedValues(double x, double y, double z)
        {
            // arrange
            // act
            var result = Tuple.Point(x, y, z);

            // assert
            using var _ = new AssertionScope();
            result.W.Should().Be(1.0, "W must be 1 for all points");
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4.5, 5.6, 6.7)]
        [InlineData(-1, -2, -3)]
        public void Vector_ShouldCreateTupleWithProvidedValues(double x, double y, double z)
        {
            // arrange
            // act
            var result = Tuple.Vector(x, y, z);

            // assert
            using var _ = new AssertionScope();
            result.W.Should().Be(0, "W must be 0 for all vectors");
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Z.Should().Be(z);
        }

        [Fact]
        public void Deconstruct__3_ShouldReturnExpectedComponentValues()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            var (x, y, z) = sut;

            // assert
            using var _ = new AssertionScope();
            x.Should().Be(sut.X);
            y.Should().Be(sut.Y);
            z.Should().Be(sut.Z);
        }

        [Fact]
        public void Deconstruct__4_ShouldReturnExpectedComponentValues()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            var (x, y, z, w) = sut;

            // assert
            using var _ = new AssertionScope();
            x.Should().Be(sut.X);
            y.Should().Be(sut.Y);
            z.Should().Be(sut.Z);
            w.Should().Be(1);
        }

        [Fact]
        public void Magnitude_ShouldThrowInvalidOperationException_WhenTupleIsAPoint()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            Action act = () => { _ = sut.Magnitude(); };

            // assert
            act.Should().Throw<InvalidOperationException>().WithMessage("*only valid on a vector*");
        }

        [Fact]
        public void Normalize_ShouldThrowInvalidOperationException_WhenTupleIsAPoint()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            Action act = () => { _ = sut.Normalize(); };

            // assert
            act.Should().Throw<InvalidOperationException>().WithMessage("*only valid on a vector*");
        }

        [Fact]
        public void Type_ShouldReturnPoint_WhenTupleWComponentIs1()
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            var result = sut.Type;

            // assert
            result.Should().Be(TupleType.Point);
        }

        [Fact]
        public void Type_ShouldReturnVector_WhenTupleWComponentIs0()
        {
            // arrange
            var sut = Tuple.Vector(1, 2, 3);

            // act
            var result = sut.Type;

            // assert
            result.Should().Be(TupleType.Vector);
        }

        [Fact]
        public void Type_ShouldThrowArgumentOutOfRangeException_WhenWComponentIsNot0or1()
        {
            // arrange
            var propertyInfo = typeof(Tuple).GetProperty(nameof(Tuple.W));
            Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");

            object boxedTuple = Tuple.Vector(1, 2, 3);
            propertyInfo.SetValue(boxedTuple, 5.0);
            var sut = (Tuple) boxedTuple;

            // act
            Action act = () => { _ = sut.Type; };

            // assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("*exactly 0 or 1*")
                .And
                .ParamName.Should()
                .Be(nameof(Tuple.W));
        }
    }
}
