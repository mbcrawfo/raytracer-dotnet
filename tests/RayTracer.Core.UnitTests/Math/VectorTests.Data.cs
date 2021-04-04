using System;
using System.Collections.Generic;
using RayTracer.Core.Extensions;
using RayTracer.Core.Math;

namespace RayTracer.Core.UnitTests.Math
{
    public partial class VectorTests
    {
        private const float GreaterThanEpsilon = FloatExtensions.ComparisonEpsilon * 10f;
        private const float LessThanEpsilon = FloatExtensions.ComparisonEpsilon / 10f;

        public static IEnumerable<object> CrossProductTestCases =>
            new object[]
            {
                new object[] { Vector.UnitX, Vector.UnitY, Vector.UnitZ },
                new object[] { Vector.UnitY, Vector.UnitX, -Vector.UnitZ },
                new object[]
                {
                    new Vector(1, 2, 3), new Vector(2, 3, 4), new Vector(-1, 2, -1)
                },
                new object[] { new Vector(2, 3, 4), new Vector(1, 2, 3), new Vector(1, -2, 1) },
            };

        public static IEnumerable<object> DotProductTestCases =>
            new object[]
            {
                new object[] { Vector.UnitX, Vector.UnitX, 1 },
                new object[] { -Vector.UnitX, Vector.UnitX, -1 },
                new object[]
                {
                    new Vector(1f, 2f, 3f).Normalize(),
                    new Vector(1f, 2f, 3f).Normalize(),
                    1f
                },
                new object[]
                {
                    new Vector(-1f, -2f, -3f).Normalize(),
                    new Vector(1f, 2f, 3f).Normalize(),
                    -1f
                },
                new object[] { new Vector(1f, 2f, 3f), new Vector(2f, 3f, 4f), 20f },
            };

        public static IEnumerable<object> MagnitudeTestCases =>
            new object[]
            {
                new object[] { Vector.Zero, 0f },
                new object[] { Vector.UnitX, 1f },
                new object[] { Vector.UnitY, 1f },
                new object[] { Vector.UnitZ, 1f },
                new object[] { new Vector(1f, 2f, 3f), MathF.Sqrt(14f) },
                new object[] { new Vector(-1f, -2f, -3f), MathF.Sqrt(14f) },
                new object[] { new Vector(1f, 2f, 3f).Normalize(), 1f },
            };

        public static IEnumerable<object> NormalizationTestCases =>
            new object[]
            {
                new object[] { Vector.UnitX * 2f, Vector.UnitX },
                new object[] { Vector.UnitY * 2f, Vector.UnitY },
                new object[] { Vector.UnitZ * 2f, Vector.UnitZ },
                new object[]
                {
                    new Vector(1f, 2f, 3f),
                    new Vector(
                        1f / MathF.Sqrt(14f),
                        2f / MathF.Sqrt(14f),
                        3f / MathF.Sqrt(14F)
                    )
                },
                new object[]
                {
                    new Vector(-1f, -2f, -3f),
                    new Vector(
                        -1f / MathF.Sqrt(14f),
                        -2f / MathF.Sqrt(14f),
                        -3f / MathF.Sqrt(14f)
                    )
                },
            };

        public static IEnumerable<object> ObjectsThatAreNotVectors =>
            new object[]
            {
                new object?[] { null },
                new object[] { new() },
                new object[] { 1 },
                new object[] { "hello world" }
            };

        public static IEnumerable<object> VectorsThatAreEquivalent =>
            new object[]
            {
                new object[] { Vector.Zero, Vector.Zero },
                new object[]
                {
                    Vector.Zero, new Vector(LessThanEpsilon, LessThanEpsilon, LessThanEpsilon)
                },
                new object[] { new Vector(1f, 2f, 3f), new Vector(1f, 2f, 3f) },
                new object[] { new Vector(1.23f, 4.56f, 7.89f), new Vector(1.23f, 4.56f, 7.89f) },
                new object[] { new Vector(1f, 2f, 3f), new Vector(1f + LessThanEpsilon, 2f, 3f) },
                new object[] { new Vector(1f, 2f, 3f), new Vector(1f, 2f + LessThanEpsilon, 3f) },
                new object[] { new Vector(1f, 2f, 3f), new Vector(1f, 2f, 3f + LessThanEpsilon) },
                new object[]
                {
                    new Vector(1f, 2f, 3f),
                    new Vector(
                        1f + LessThanEpsilon,
                        2f - LessThanEpsilon,
                        3f + LessThanEpsilon
                    )
                },
            };

        public static IEnumerable<object> VectorsThatAreNotEquivalent =>
            new object[]
            {
                new object[] { new Vector(1f, 2f, 3f), new Vector(3f, 2f, 1f) },
                new object[] { new Vector(1.23f, 4.56f, 7.89f), new Vector(9.87f, 6.54f, 3.21f) },
                new object[]
                {
                    new Vector(1f, 2f, 3f), new Vector(1f + GreaterThanEpsilon, 2f, 3f)
                },
                new object[]
                {
                    new Vector(1f, 2f, 3f), new Vector(1f, 2f + GreaterThanEpsilon, 3f)
                },
                new object[]
                {
                    new Vector(1f, 2f, 3f), new Vector(1f, 2f, 3f + GreaterThanEpsilon)
                },
                new object[]
                {
                    new Vector(1f, 2f, 3f),
                    new Vector(
                        1f + GreaterThanEpsilon,
                        2f - GreaterThanEpsilon,
                        3f + GreaterThanEpsilon
                    )
                },
            };
    }
}
