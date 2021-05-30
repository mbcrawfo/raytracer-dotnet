using RayTracer.Core.Math;
using RayTracer.Core.Shapes;

namespace RayTracer.Core.Materials.Patterns
{
    public abstract record Pattern : Transformable
    {
        public Color ColorAt(in Point worldPoint, Transformable @object)
        {
            var objectPoint = @object.WorldPointToLocalPoint(worldPoint);
            return PatternColorAt(objectPoint);
        }

        protected abstract Color LocalColorAt(in Point localPoint);

        public Color PatternColorAt(in Point objectPoint)
        {
            var patternPoint = WorldPointToLocalPoint(objectPoint);
            return LocalColorAt(patternPoint);
        }
    }
}
