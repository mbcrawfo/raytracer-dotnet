using FluentAssertions;
using FluentAssertions.Numeric;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.UnitTests.Assertions
{
    internal static class AssertionExtensions
    {
        /// <summary>
        ///     Wraps
        ///     <see
        ///         cref="NumericAssertionsExtensions.BeApproximately(NumericAssertions{double}, double, double, string, object[])" />
        ///     to provide the expected comparison epsilon.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="expectedValue"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public static AndConstraint<NumericAssertions<double>> BeApproximately(
            this NumericAssertions<double> parent,
            double expectedValue,
            string because = "",
            params object[] becauseArgs
        ) =>
            parent.BeApproximately(
                expectedValue,
                DoubleExtensions.ComparisonEpsilon,
                because,
                becauseArgs
            );
    }
}
