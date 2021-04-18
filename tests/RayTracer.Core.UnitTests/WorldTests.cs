using System.Linq;
using FluentAssertions;
using RayTracer.Core.Math;
using Xunit;

namespace RayTracer.Core.UnitTests
{
    public class WorldTests
    {
        [Fact]
        public void Intersect_ShouldReturnAllIntersectionsOfTheRayAndShapesInTheWorld()
        {
            // arrange
            var ray = new Ray(new(0f, 0f, -5f), Vector.UnitZ);
            var sut = World.Default;

            // act
            var result = sut.Intersect(ray);

            // assert
            result.Select(x => x.Time).Should().HaveCount(4).And.ContainInOrder(4f, 4.5f, 5.5f, 6f);
        }
    }
}
