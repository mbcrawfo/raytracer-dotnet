using System;
using System.Collections.Generic;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.UnitTests
{
    public partial class TupleTests
    {
        private const double GreaterThanEpsilon = DoubleExtensions.ComparisonEpsilon * 10.0;
        private const double LessThanEpsilon = DoubleExtensions.ComparisonEpsilon / 10.0;

        public static IEnumerable<object> CrossProductTestCases =>
            new object[]
            {
                new object[] { Tuple.UnitX, Tuple.UnitY, Tuple.UnitZ },
                new object[] { Tuple.UnitY, Tuple.UnitX, -Tuple.UnitZ },
                new object[]
                {
                    Tuple.Vector(1, 2, 3), Tuple.Vector(2, 3, 4), Tuple.Vector(-1, 2, -1)
                },
                new object[]
                {
                    Tuple.Vector(2, 3, 4), Tuple.Vector(1, 2, 3), Tuple.Vector(1, -2, 1)
                },
            };

        public static IEnumerable<object> DotProductTestCases =>
            new object[]
            {
                new object[] { Tuple.UnitX, Tuple.UnitX, 1 },
                new object[] { Tuple.UnitX * -1, Tuple.UnitX, -1 },
                new object[]
                {
                    Tuple.Vector(1, 2, 3).Normalize(), Tuple.Vector(1, 2, 3).Normalize(), 1
                },
                new object[]
                {
                    Tuple.Vector(-1, -2, -3).Normalize(),
                    Tuple.Vector(1, 2, 3).Normalize(),
                    -1
                },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(2, 3, 4), 20 },
            };

        public static IEnumerable<object> MagnitudeTestCases =>
            new object[]
            {
                new object[] { Tuple.Zero, 0 },
                new object[] { Tuple.UnitX, 1 },
                new object[] { Tuple.UnitY, 1 },
                new object[] { Tuple.UnitZ, 1 },
                new object[] { Tuple.Vector(1, 2, 3), Math.Sqrt(14) },
                new object[] { Tuple.Vector(-1, -2, -3), Math.Sqrt(14) },
                new object[] { Tuple.Vector(1, 2, 3).Normalize(), 1 },
            };

        public static IEnumerable<object> NormalizationTestCases =>
            new object[]
            {
                new object[] { Tuple.UnitX * 2, Tuple.UnitX },
                new object[] { Tuple.UnitY * 2, Tuple.UnitY },
                new object[] { Tuple.UnitZ * 2, Tuple.UnitZ },
                new object[]
                {
                    Tuple.Vector(1, 2, 3),
                    Tuple.Vector(1 / Math.Sqrt(14), 2 / Math.Sqrt(14), 3 / Math.Sqrt(14))
                },
                new object[]
                {
                    Tuple.Vector(-1, -2, -3),
                    Tuple.Vector(-1 / Math.Sqrt(14), -2 / Math.Sqrt(14), -3 / Math.Sqrt(14))
                },
            };

        public static IEnumerable<object> ObjectsThatAreNotTuples =>
            new object[]
            {
                new object[] { new() }, new object[] { 1 }, new object[] { "hello world" }
            };

        public static IEnumerable<object> PairsOfTuplesWhereOneOperandIsAPoint =>
            new object[]
            {
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2, 3) },
                new object[] { Tuple.UnitX, Tuple.Point(1, 2, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.UnitX },
            };

        public static IEnumerable<object> TuplesThatAreEquivalent =>
            new object[]
            {
                new object[] { Tuple.Zero, Tuple.Zero },
                new object[]
                {
                    Tuple.Zero, Tuple.Vector(LessThanEpsilon, LessThanEpsilon, LessThanEpsilon)
                },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2, 3) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1, 2, 3) },
                new object[] { Tuple.Point(1.23, 4.56, 7.89), Tuple.Point(1.23, 4.56, 7.89) },
                new object[] { Tuple.Vector(1.23, 4.56, 7.89), Tuple.Vector(1.23, 4.56, 7.89) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1 + LessThanEpsilon, 2, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2 + LessThanEpsilon, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2, 3 + LessThanEpsilon) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1 + LessThanEpsilon, 2, 3) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1, 2 + LessThanEpsilon, 3) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1, 2, 3 + LessThanEpsilon) },
                new object[]
                {
                    Tuple.Point(1, 2, 3),
                    Tuple.Point(
                        1 + LessThanEpsilon,
                        2 - LessThanEpsilon,
                        3 + LessThanEpsilon
                    )
                },
                new object[]
                {
                    Tuple.Vector(1, 2, 3),
                    Tuple.Vector(
                        1 + LessThanEpsilon,
                        2 - LessThanEpsilon,
                        3 + LessThanEpsilon
                    )
                },
            };

        public static IEnumerable<object> TuplesThatAreNotEquivalent =>
            new object[]
            {
                new object[] { Tuple.Point(1, 2, 3), Tuple.Vector(1, 2, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(3, 2, 1) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(3, 2, 1) },
                new object[] { Tuple.Point(1.23, 4.56, 7.89), Tuple.Point(9.87, 6.54, 3.21) },
                new object[] { Tuple.Vector(1.23, 4.56, 7.89), Tuple.Vector(9.87, 6.54, 3.21) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1 + GreaterThanEpsilon, 2, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2 + GreaterThanEpsilon, 3) },
                new object[] { Tuple.Point(1, 2, 3), Tuple.Point(1, 2, 3 + GreaterThanEpsilon) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1 + GreaterThanEpsilon, 2, 3) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1, 2 + GreaterThanEpsilon, 3) },
                new object[] { Tuple.Vector(1, 2, 3), Tuple.Vector(1, 2, 3 + GreaterThanEpsilon) },
                new object[]
                {
                    Tuple.Point(1, 2, 3),
                    Tuple.Point(
                        1 + GreaterThanEpsilon,
                        2 - GreaterThanEpsilon,
                        3 + GreaterThanEpsilon
                    )
                },
                new object[]
                {
                    Tuple.Vector(1, 2, 3),
                    Tuple.Vector(
                        1 + GreaterThanEpsilon,
                        2 - GreaterThanEpsilon,
                        3 + GreaterThanEpsilon
                    )
                },
            };
    }
}
