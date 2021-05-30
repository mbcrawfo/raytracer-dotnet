using System;
using System.Collections.Immutable;
using FluentAssertions;
using FluentAssertions.Execution;
using RayTracer.Core.Materials;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;
using RayTracer.Core.Shapes;
using Xunit;

namespace RayTracer.Core.UnitTests.Materials
{
    public class MaterialTests
    {
        [Fact]
        public void Lighting_ShouldApplyColorsBasedOnTheMaterialPattern()
        {
            // arrange
            var light = new PointLight(new(0f, 0f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, 0f, -1f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default with
            {
                AmbientReflection = 1f,
                DiffuseReflection = 0f,
                Pattern = new StripedPattern
                {
                    Patterns = ImmutableArray.Create<Pattern>(
                        new SolidPattern(Color.White),
                        new SolidPattern(Color.Black)
                    )
                },
                SpecularReflection = 0f
            };

            // act
            var color1 = sut.Lighting(light, shape, new Point(0.9f, 0f, 0f), eye, normal, true);
            var color2 = sut.Lighting(light, shape, new Point(1.1f, 0f, 0f), eye, normal, true);

            // assert
            using var _ = new AssertionScope();
            color1.Should().Be(Color.White);
            color2.Should().Be(Color.Black);
        }

        [Fact]
        public void Lighting_ShouldReturnAmbientLightIntensity_WhenTheLightIsBehindTheSurface()
        {
            // arrange
            var light = new PointLight(new(0f, 0f, 10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, 0f, -1f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, false);

            // assert
            result.Should().Be(new Color(0.1f, 0.1f, 0.1f));
        }

        [Fact]
        public void Lighting_ShouldReturnFullIntensity_WhenEyeIsBetweenTheLightAndTheSurface()
        {
            // arrange
            var light = new PointLight(new(0f, 0f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, 0f, -1f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, false);

            // assert
            result.Should().Be(new Color(1.9f, 1.9f, 1.9f));
        }

        [Fact]
        public void
            Lighting_ShouldReturnLightingWithFullSpecularComponent_WhenEyeIsInThePathOfTheReflectionVector()
        {
            // arrange
            var light = new PointLight(new(0f, 10f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, -MathF.Sqrt(2f) / 2f, -MathF.Sqrt(2f) / 2f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, false);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(1.6364f, 1e-4f);
            result.G.Should().BeApproximately(1.6364f, 1e-4f);
            result.B.Should().BeApproximately(1.6364f, 1e-4f);
        }

        [Fact]
        public void
            Lighting_ShouldReturnLightingWithoutSpecularComponent_WhenEyeIsBetweenTheLightAndTheSurfaceOffset45Degrees()
        {
            // arrange
            var light = new PointLight(new(0f, 0f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, MathF.Sqrt(2f) / 2f, -MathF.Sqrt(2f) / 2f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, false);

            // assert
            result.Should().Be(new Color(1f, 1f, 1f));
        }

        [Fact]
        public void
            Lighting_ShouldReturnLightingWithReducedIntensityAndNoSpecularComponent_WhenEyeIsOppositeSurfaceAndLightIsOffsetBy45Degrees()
        {
            // arrange
            var light = new PointLight(new(0f, 10f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, 0f, -1f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, false);

            // assert
            using var _ = new AssertionScope();
            result.R.Should().BeApproximately(0.7364f, 1e-4f);
            result.G.Should().BeApproximately(0.7364f, 1e-4f);
            result.B.Should().BeApproximately(0.7364f, 1e-4f);
        }

        [Fact]
        public void Lighting_ShouldReturnOnlyAmbientLightComponent_WhenPointIsInShadow()
        {
            // arrange
            var light = new PointLight(new(0f, 0f, -10f), Color.White);
            var shape = new Sphere();
            var eye = new Vector(0f, 0f, -1f);
            var normal = new Vector(0f, 0f, -1f);
            var sut = Material.Default;

            // act
            var result = sut.Lighting(light, shape, Point.Origin, eye, normal, true);

            // assert
            result.Should().Be(new Color(0.1f, 0.1f, 0.1f));
        }
    }
}
