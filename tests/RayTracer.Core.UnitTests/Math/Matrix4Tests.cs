using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests.Math
{
    public class Matrix4Tests
    {
        public static IEnumerable<object> ArraysThatAreNot4X4 =>
            new object[]
            {
                new object[] { new float[3, 3] },
                new object[] { new float[3, 4] },
                new object[] { new float[4, 3] },
                new object[] { new float[5, 5] },
            };

        [Theory]
        [MemberData(nameof(ArraysThatAreNot4X4))]
        public void Constructor__Array_ShouldThrowArgumentException_WhenElementsArrayIsNot4x4(
            float[,] array
        )
        {
            // arrange
            // act
            Action act = () => { _ = new Matrix4(array); };

            // assert
            act.Should().Throw<ArgumentException>().WithMessage("*must be a 4x4 array*");
        }

        [Fact]
        public void Constructor__Array_ShouldCreate4x4MatrixAndAssignElementsFromTheArray()
        {
            // arrange
            var array = new[,]
            {
                { 1f, 2f, 3f, 4f },
                { 5f, 6f, 7f, 8f },
                { 9f, 10f, 11f, 12f },
                { 13f, 14f, 15f, 16f }
            };

            // act
            var result = new Matrix4(array);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(4);
            result.Columns.Should().Be(4);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[0, 2].Should().Be(3f);
            result[0, 3].Should().Be(4f);
            result[1, 0].Should().Be(5f);
            result[1, 1].Should().Be(6f);
            result[1, 2].Should().Be(7f);
            result[1, 3].Should().Be(8f);
            result[2, 0].Should().Be(9f);
            result[2, 1].Should().Be(10f);
            result[2, 2].Should().Be(11f);
            result[2, 3].Should().Be(12f);
            result[3, 0].Should().Be(13f);
            result[3, 1].Should().Be(14f);
            result[3, 2].Should().Be(15f);
            result[3, 3].Should().Be(16f);
        }

        [Fact]
        public void Constructor__Copy_ShouldCreate4x4MatrixAndAssignElementsFromTheOtherMatrix()
        {
            // arrange
            var other = new Matrix4(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 5f, 6f, 7f, 8f },
                    { 9f, 10f, 11f, 12f },
                    { 13f, 14f, 15f, 16f }
                }
            );

            // act
            var result = new Matrix4(other);

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(4);
            result.Columns.Should().Be(4);
            result[0, 0].Should().Be(1f);
            result[0, 1].Should().Be(2f);
            result[0, 2].Should().Be(3f);
            result[0, 3].Should().Be(4f);
            result[1, 0].Should().Be(5f);
            result[1, 1].Should().Be(6f);
            result[1, 2].Should().Be(7f);
            result[1, 3].Should().Be(8f);
            result[2, 0].Should().Be(9f);
            result[2, 1].Should().Be(10f);
            result[2, 2].Should().Be(11f);
            result[2, 3].Should().Be(12f);
            result[3, 0].Should().Be(13f);
            result[3, 1].Should().Be(14f);
            result[3, 2].Should().Be(15f);
            result[3, 3].Should().Be(16f);
        }

        [Fact]
        public void Constructor__Default_ShouldCreate4x4Matrix()
        {
            // arrange
            // act
            var result = new Matrix4();

            // assert
            using var _ = new AssertionScope();
            result.Rows.Should().Be(4);
            result.Columns.Should().Be(4);
        }

        [Fact]
        public void Op__MultiplyMatrix4_ShouldReturnExpectedResultMatrix()
        {
            // arrange
            var lhs = new Matrix4(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 5f, 6f, 7f, 8f },
                    { 9f, 8f, 7f, 6f },
                    { 5f, 4f, 3f, 2f }
                }
            );
            var rhs = new Matrix4(
                new[,]
                {
                    { -2f, 1f, 2f, 3f },
                    { 3f, 2f, 1f, -1f },
                    { 4f, 3f, 6f, 5f },
                    { 1f, 2f, 7f, 8f }
                }
            );
            var expected = new Matrix4(
                new[,]
                {
                    { 20f, 22f, 50f, 48f },
                    { 44f, 54f, 114f, 108f },
                    { 40f, 58f, 110f, 102f },
                    { 16f, 26f, 46f, 42f }
                }
            );

            // act
            var actual = lhs * rhs;

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Op__MultiplyPoint_ShouldReturnExpectedResultPoint()
        {
            // arrange
            var lhs = new Matrix4(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 2f, 4f, 4f, 2f },
                    { 8f, 6f, 4f, 1f },
                    { 0f, 0f, 0f, 1f }
                }
            );
            var rhs = new Point(1f, 2f, 3f);
            var expected = new Point(18f, 24f, 33f);

            // act
            var actual = lhs * rhs;

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Op__MultiplyVector_ShouldReturnExpectedResultVector()
        {
            // arrange
            var lhs = new Matrix4(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 2f, 4f, 4f, 2f },
                    { 8f, 6f, 4f, 1f },
                    { 0f, 0f, 0f, 1f }
                }
            );
            var rhs = new Vector(1f, 2f, 3f);
            var expected = new Vector(14f, 22f, 32f);

            // act
            var actual = lhs * rhs;

            // assert
            actual.Should().Be(expected);
        }
    }
}
