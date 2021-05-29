using System;
using RayTracer.Core.Materials.Patterns;
using RayTracer.Core.Math;

namespace RayTracer.Core.Materials
{
    public record Material
    {
        public static readonly Material Default = new()
        {
            AmbientReflection = 0.1f,
            DiffuseReflection = 0.9f,
            Pattern = new SolidPattern(Color.White),
            Shininess = 200f,
            SpecularReflection = 0.9f
        };

        public float AmbientReflection { get; init; }

        public float DiffuseReflection { get; init; }

        public Pattern Pattern { get; init; } = new SolidPattern(Color.White);

        public float Shininess { get; init; }

        public float SpecularReflection { get; init; }

        public Color Lighting(
            PointLight light,
            in Point point,
            in Vector eye,
            in Vector normal,
            bool pointLiesInShadow
        )
        {
            var effectiveColor = Pattern.ColorAt(point) * light.Intensity;
            var lightVector = (light.Position - point).Normalize();
            var ambient = effectiveColor * AmbientReflection;

            if (pointLiesInShadow)
            {
                return ambient;
            }

            var lightDotNormal = lightVector.DotProduct(normal);
            if (lightDotNormal < 0f)
            {
                return ambient;
            }

            var reflectionVector = (-lightVector).Reflect(normal);
            var reflectionDotEye = reflectionVector.DotProduct(eye);

            var diffuse = effectiveColor * DiffuseReflection * lightDotNormal;
            if (reflectionDotEye <= 0f)
            {
                return ambient + diffuse;
            }

            var specular = light.Intensity *
                SpecularReflection *
                MathF.Pow(reflectionDotEye, Shininess);
            return ambient + diffuse + specular;
        }
    }
}
