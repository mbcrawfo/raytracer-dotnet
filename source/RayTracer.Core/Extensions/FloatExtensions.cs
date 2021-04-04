using System;

namespace RayTracer.Core.Extensions
{
    public static class FloatExtensions
    {
        public const float ComparisonEpsilon = 1e-6f;

        /// <summary>
        ///     Checks that two numbers are approximately equal, allowing for rounding errors.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool ApproximatelyEquals(this float lhs, float rhs) =>
            MathF.Abs(lhs - rhs) < ComparisonEpsilon;
    }
}
