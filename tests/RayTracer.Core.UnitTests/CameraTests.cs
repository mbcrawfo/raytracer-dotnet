using System;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class CameraTests
    {
        [Fact]
        public void PixelSize_ShouldReturnExpectedValueForHorizontalCanvas()
        {
            // arrange
            var sut = new Camera(200, 125);

            // act
            var result = sut.PixelSize;

            // assert
            result.Should().Be(0.01f);
        }

        [Fact]
        public void PixelSize_ShouldReturnExpectedValueForVerticalCanvas()
        {
            // arrange
            var sut = new Camera(125, 200);

            // act
            var result = sut.PixelSize;

            // assert
            result.Should().Be(0.01f);
        }

        [Fact]
        public void RayForPixel_ShouldConstructRay_WhenTheCameraIsTransformed()
        {
            // arrange
            var sut = new Camera(201, 101)
            {
                Transform = Matrix4.RotationY(MathF.PI / 4) * Matrix4.Translation(0f, -2f, 5f)
            };

            // act
            var result = sut.RayForPixel(100, 50);

            // assert
            using var _ = new AssertionScope();
            result.Origin.Should().Be(new Point(0f, 2f, -5f));
            result.Direction.Should().Be(new Vector(MathF.Sqrt(2f) / 2f, 0f, -MathF.Sqrt(2f) / 2f));
        }

        [Fact]
        public void RayForPixel_ShouldConstructRayThroughACornerOfTheCanvas()
        {
            // arrange
            var sut = new Camera(201, 101);

            // act
            var result = sut.RayForPixel(0, 0);

            // assert
            using var _ = new AssertionScope();
            result.Origin.Should().Be(Point.Origin);
            result.Direction.X.Should().BeApproximately(0.66519f, 1e-5f);
            result.Direction.Y.Should().BeApproximately(0.33259f, 1e-5f);
            result.Direction.Z.Should().BeApproximately(-0.66851f, 1e-5f);
        }

        [Fact]
        public void RayForPixel_ShouldConstructRayThroughTheCenterOfTheCanvas()
        {
            // arrange
            var sut = new Camera(201, 101);

            // act
            var result = sut.RayForPixel(100, 50);

            // assert
            using var _ = new AssertionScope();
            result.Origin.Should().Be(Point.Origin);
            result.Direction.Should().Be(new Vector(0f, 0f, -1f));
        }

        [Fact]
        public void Render_ShouldRenderTheWorld()
        {
            // arrange
            var world = World.Default;
            var sut = new Camera(11, 11)
            {
                Transform = Matrix4.ViewTransform(
                    new Point(0f, 0f, -5f),
                    Point.Origin,
                    Vector.UnitY
                )
            };

            // act
            var result = sut.Render(world);

            // assert
            using var _ = new AssertionScope();
            result[5, 5].R.Should().BeApproximately(0.38066f, 1e-5f);
            result[5, 5].G.Should().BeApproximately(0.47583f, 1e-5f);
            result[5, 5].B.Should().BeApproximately(0.2855f, 1e-4f);
        }
    }
}
