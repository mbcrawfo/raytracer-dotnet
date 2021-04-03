using FluentAssertions;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public partial class TupleTests
    {
        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreNotEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void EqualityOperator_ShouldReturnFalse_WhenRhsIsNotAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void EqualityOperator_ShouldReturnTrue_WhenRhsIsAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs == rhs;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreNotEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotAnEquivalentTuple(
            Tuple tuple,
            object obj
        )
        {
            // arrange
            // act
            var result = tuple.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.ObjectsThatAreNotTuples),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void Equals__Object_ShouldReturnFalse_WhenOtherObjectIsNotTuple(object obj)
        {
            // arrange
            var sut = Tuple.Point(1, 2, 3);

            // act
            var result = sut.Equals(obj);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void Equals__Object_ShouldReturnTrue_WhenOtherObjectIsAnEquivalentTuple(
            Tuple tuple,
            object obj
        )
        {
            // arrange
            // act
            var result = tuple.Equals(obj);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreNotEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void Equals__Tuple_ShouldReturnFalse_WhenRhsIsNotAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void Equals__Tuple_ShouldReturnTrue_WhenRhsIsAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs.Equals(rhs);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void InequalityOperator_ShouldReturnFalse_WhenRhsIsAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(
            nameof(TupleEquivalencyTestData.TuplesThatAreNotEquivalent),
            MemberType = typeof(TupleEquivalencyTestData)
        )]
        public void InequalityOperator_ShouldReturnTrue_WhenRhsIsNotAnEquivalentTuple(
            Tuple lhs,
            Tuple rhs
        )
        {
            // arrange
            // act
            var result = lhs != rhs;

            // assert
            result.Should().BeTrue();
        }
    }
}
