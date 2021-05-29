using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Shapes
{
    public class TransformableTests
    {
        private record TransformableImpl : Transformable
        {
            public new Matrix4 TransformInverse => base.TransformInverse;

            public new Matrix4 TransformInverseTranspose => base.TransformInverseTranspose;
        }

        [Fact]
        public void Transform_ShouldSetTransformInverseAndTransformInverseTranspose()
        {
            // arrange
            var expectedTransform = Matrix4.Identity.RotateX(0.1f)
                .RotateY(0.25f)
                .RotateZ(0.333f)
                .Scale(0.5f, 0.5f, 0.5f);
            var expectedTransformInverse = expectedTransform.Inverse();
            var expectedTransformInverseTranspose = expectedTransformInverse.Transpose();

            // act
            var sut = new TransformableImpl { Transform = expectedTransform };

            // assert
            using var _ = new AssertionScope();
            sut.Transform.Should().Be(expectedTransform);
            sut.TransformInverse.Should().Be(expectedTransformInverse);
            sut.TransformInverseTranspose.Should().Be(expectedTransformInverseTranspose);
        }
    }
}
