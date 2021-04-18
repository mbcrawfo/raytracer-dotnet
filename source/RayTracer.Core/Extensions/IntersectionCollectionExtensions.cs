using System;
using System.Collections.Immutable;

namespace RayTracer.Core.Extensions
{
    public static class IntersectionCollectionExtensions
    {
        public static Intersection? Hit(this IImmutableList<Intersection> intersections) =>
            intersections switch
            {
                { Count: 0 } => null,
                { Count: 1 } l when l[0].Time < 0f => null,
                { Count: 1 } l => l[0],
                { Count: 2 } l => (l[0], l[1]) switch
                {
                    ({ Time: < 0f }, { Time: < 0f }) => null,
                    (var hit, { Time: < 0f }) => hit,
                    ({ Time: < 0f }, var hit) => hit,
                    ({ Time: var t1 } hit, { Time: var t2 }) when t1 <= t2 => hit,
                    var (_, hit) => hit
                },
                ImmutableArray<Intersection> l => l.Sort(Intersection.TimeComparer).HitSorted(),
                ImmutableList<Intersection> l => l.Sort(Intersection.TimeComparer).HitSorted(),
                _ => throw new InvalidOperationException(
                    $"Unexpected collection type {intersections.GetType().FullName}"
                )
            };

        private static Intersection? HitSorted(this IImmutableList<Intersection> intersections)
        {
            for (var i = 0; i < intersections.Count; i += 1)
            {
                var current = intersections[i];

                if (current.Time >= 0f)
                {
                    return current;
                }
            }

            return null;
        }
    }
}
