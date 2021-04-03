using System;

namespace RayTracer.Core.Extensions
{
    public static class DoubleExtensions
    {
        public const double ComparisonEpsilon = 1e-8;

        /// <summary>
        ///     Checks that two numbers are approximately equal, allowing for rounding errors.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool ApproximatelyEquals(this double lhs, double rhs) =>
            Math.Abs(lhs - rhs) < ComparisonEpsilon;
    }
}
