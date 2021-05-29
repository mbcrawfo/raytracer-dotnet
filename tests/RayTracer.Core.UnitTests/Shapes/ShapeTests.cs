using System.Collections.Immutable;
using FluentAssertions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Shapes
{
    public class ShapeTests
    {
        private record ShapeImpl : Shape
        {
            public Ray? LastLocalIntersectInput { get; private set; }

            public Point? LastLocalNormalAtInput { get; private set; }

            public IImmutableList<Intersection> LocalIntersectResult =>
                ImmutableArray.Create(new Intersection(this, 1f), new Intersection(this, 2f));

            public Vector LocalNormalAtResult => new(1.23f, 4.56f, 7.89f);

            /// <inheritdoc />
            public override IImmutableList<Intersection> LocalIntersect(Ray localRay)
            {
                LastLocalIntersectInput = localRay;
                return LocalIntersectResult;
            }

            /// <inheritdoc />
            public override Vector LocalNormalAt(in Point localPoint)
            {
                LastLocalNormalAtInput = localPoint;
                return LocalNormalAtResult;
            }
        }

        [Fact]
        public void Intersect_ShouldReturnTheResultOfLocalIntersect()
        {
            // arrange
            var sut = new ShapeImpl();
            var expected = sut.LocalIntersectResult;

            // act
            var actual = sut.Intersect(new Ray(Point.Origin, Vector.Zero));

            // assert
            actual.Should().ContainInOrder(expected);
        }

        [Fact]
        public void Intersect_ShouldTransformTheWorldRayInputToALocalRay()
        {
            // arrange
            var ray = new Ray(new Point(1f, 2f, 3f), new Vector(4f, 5f, 6f));
            var sut = new ShapeImpl
            {
                Transform = Matrix4.Identity.RotateX(0.1f).RotateY(0.2f).RotateZ(0.3f)
            };
            var expected = ray.Transform(sut.Transform.Inverse());

            // act
            _ = sut.Intersect(ray);
            var actual = sut.LastLocalIntersectInput;

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void NormalAt_ShouldTransformTheLocalNormalReturnedByLocalNormalAtToAWorldNormal()
        {
            // arrange
            var sut = new ShapeImpl
            {
                Transform = Matrix4.Identity.RotateX(0.1f).RotateY(0.2f).RotateZ(0.3f)
            };
            var expected =
                (sut.Transform.Inverse().Transpose() * sut.LocalNormalAtResult).Normalize();

            // act
            var actual = sut.NormalAt(Point.Origin);

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void NormalAt_ShouldTransformTheWorldPointInputToALocalPoint()
        {
            // arrange
            var point = new Point(1.23f, 4.56f, 7.89f);
            var sut = new ShapeImpl
            {
                Transform = Matrix4.Identity.RotateX(0.1f).RotateY(0.2f).RotateZ(0.3f)
            };
            var expected = sut.Transform.Inverse() * point;

            // act
            _ = sut.NormalAt(point);
            var actual = sut.LastLocalNormalAtInput;

            // assert
            actual.Should().Be(expected);
        }
    }
}
