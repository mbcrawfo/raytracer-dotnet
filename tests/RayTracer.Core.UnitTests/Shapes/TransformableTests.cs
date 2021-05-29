using FluentAssertions;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Shapes
{
    public class TransformableTests
    {
        private record TransformableImpl : Transformable;

        [Fact]
        public void LocalVectorToWorldVector_ShouldConvertTheInputToWorldSpace()
        {
            // arrange
            var localVector = new Vector(1.23f, 4.56f, 7.89f);
            var transform = Matrix4.Identity.RotateX(0.1f)
                .RotateY(0.2f)
                .RotateZ(0.3f)
                .Scale(1f, 2f, 3f);
            var sut = new TransformableImpl { Transform = transform };
            var expected = transform.Inverse().Transpose() * localVector;

            // act
            var actual = sut.LocalVectorToWorldVector(localVector);

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WorldPointToLocalPoint_ShouldConvertTheInputToLocalSpace()
        {
            // arrange
            var worldPoint = new Point(1.23f, 4.56f, 7.89f);
            var transform = Matrix4.Identity.RotateX(0.1f)
                .RotateY(0.2f)
                .RotateZ(0.3f)
                .Scale(1f, 2f, 3f);
            var sut = new TransformableImpl { Transform = transform };
            var expected = transform.Inverse() * worldPoint;

            // act
            var actual = sut.WorldPointToLocalPoint(worldPoint);

            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WorldRayToLocalRay_ShouldConvertTheInputToLocalSpace()
        {
            // arrange
            var worldRay = new Ray(new Point(1f, 2f, 3f), new Vector(4f, 5f, 6f));
            var transform = Matrix4.Identity.RotateX(0.1f)
                .RotateY(0.2f)
                .RotateZ(0.3f)
                .Scale(1f, 2f, 3f);
            var sut = new TransformableImpl { Transform = transform };
            var expected = worldRay.Transform(transform.Inverse());

            // act
            var actual = sut.WorldRayToLocalRay(worldRay);

            // assert
            actual.Should().Be(expected);
        }
    }
}
