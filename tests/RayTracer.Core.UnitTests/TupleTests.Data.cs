using System.Collections.Generic;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.UnitTests
{
    public partial class TupleTests
    {
        private const double GreaterThanEpsilon = DoubleExtensions.ComparisonEpsilon * 10.0;
        private const double LessThanEpsilon = DoubleExtensions.ComparisonEpsilon / 10.0;

        public static IEnumerable<object> ObjectsThatAreNotTuples =>
            new object[]
            {
                new object[] { new() }, new object[] { 1 }, new object[] { "hello world" }
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
