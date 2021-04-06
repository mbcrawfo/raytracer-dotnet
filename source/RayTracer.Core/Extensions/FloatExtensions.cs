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

        /// <summary>
        ///     Clamps <paramref name="value"/> to the range specified by <paramref name="min"/>
        ///     and <paramref name="max"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(this float value, float min, float max) =>
            MathF.Max(min, MathF.Min(max, value));
    }
}
