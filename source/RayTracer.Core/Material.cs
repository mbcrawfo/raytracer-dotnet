using System;
using RayTracer.Core.Math;

namespace RayTracer.Core
{
    public record Material
    {
        public static readonly Material Default = new()
        {
            AmbientReflection = 0.1f,
            Color = Color.White,
            DiffuseReflection = 0.9f,
            Shininess = 200f,
            SpecularReflection = 0.9f
        };

        public float AmbientReflection { get; init; }

        public Color Color { get; init; }

        public float DiffuseReflection { get; init; }

        public float Shininess { get; init; }

        public float SpecularReflection { get; init; }

        public Color Lighting(PointLight light, in Point point, in Vector eye, in Vector normal)
        {
            var effectiveColor = Color * light.Intensity;
            var lightVector = (light.Position - point).Normalize();
            var ambient = effectiveColor * AmbientReflection;

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
