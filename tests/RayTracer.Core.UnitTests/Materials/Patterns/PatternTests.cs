using FluentAssertions;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials.Patterns
{
    public class PatternTests
    {
        private record PatternImpl : Pattern
        {
            /// <inheritdoc />
            protected override Color LocalColorAt(in Point localPoint) =>
                new(localPoint.X, localPoint.Y, localPoint.Z);
        }

        [Fact]
        public void ColorAt_ShouldTransformPoint_WhenObjectAndPatternBothHaveTransforms()
        {
            // arrange
            var shape = new Sphere { Transform = Matrix4.Scaling(2f, 2f, 2f) };
            var worldPoint = new Point(2.5f, 3f, 3.5f);
            var sut = new PatternImpl { Transform = Matrix4.Translation(0.5f, 1f, 1.5f) };

            // act
            var result = sut.ColorAt(worldPoint, shape);

            // assert
            result.Should().Be(new Color(0.75f, 0.5f, 0.25f));
        }

        [Fact]
        public void ColorAt_ShouldTransformPoint_WhenObjectHasATransform()
        {
            // arrange
            var shape = new Sphere { Transform = Matrix4.Scaling(2f, 2f, 2f) };
            var worldPoint = new Point(2f, 3f, 4f);
            var sut = new PatternImpl();

            // act
            var result = sut.ColorAt(worldPoint, shape);

            // assert
            result.Should().Be(new Color(1f, 1.5f, 2f));
        }

        [Fact]
        public void ColorAt_ShouldTransformPoint_WhenPatternHasATransform()
        {
            // arrange
            var shape = new Sphere();
            var worldPoint = new Point(2f, 3f, 4f);
            var sut = new PatternImpl { Transform = Matrix4.Scaling(2f, 2f, 2f) };

            // act
            var result = sut.ColorAt(worldPoint, shape);

            // assert
            result.Should().Be(new Color(1f, 1.5f, 2f));
        }
    }
}
