using System.Collections.Generic;
using System.Linq;

namespace RayTracer.Core.Extensions
{
    public static class IntersectionCollectionExtensions
    {
        public static Intersection? Hit(this IEnumerable<Intersection> intersections) =>
            intersections.OrderBy(i => i.Time).FirstOrDefault(i => i.Time >= 0f);
    }
}
