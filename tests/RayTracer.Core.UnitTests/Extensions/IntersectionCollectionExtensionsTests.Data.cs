using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using RayTracer.Core.Shapes;

namespace RayTracer.Core.UnitTests.Extensions
{
    public partial class IntersectionCollectionExtensionsTests
    {
        public static IEnumerable<object> TestCasesWhenListContainsHit =>
            new object[]
            {
                new object[] { ImmutableArray.Create(new Intersection(new Sphere(), 0f)), 0f },
                new object[] { ImmutableArray.Create(new Intersection(new Sphere(), 1f)), 1f },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), -1f),
                        new Intersection(new Sphere(), 1f)
                    ),
                    1f
                },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), 1f),
                        new Intersection(new Sphere(), -1f)
                    ),
                    1f
                },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), 2f),
                        new Intersection(new Sphere(), 1f)
                    ),
                    1f
                },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), 1f),
                        new Intersection(new Sphere(), 2f)
                    ),
                    1f
                },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), 3f),
                        new Intersection(new Sphere(), 2f),
                        new Intersection(new Sphere(), 1f)
                    ),
                    1f
                },
                new object[]
                {
                    ImmutableList.Create(
                        new Intersection(new Sphere(), 3f),
                        new Intersection(new Sphere(), 2f),
                        new Intersection(new Sphere(), 1f)
                    ),
                    1f
                },
            };

        public static IEnumerable<object> TestCasesWhenListDoesNotContainHit =>
            new object[]
            {
                new object[] { ImmutableArray<Intersection>.Empty },
                new object[] { ImmutableArray.Create(new Intersection(new Sphere(), -1f)) },
                new object[]
                {
                    ImmutableArray.Create(
                        new Intersection(new Sphere(), -1f),
                        new Intersection(new Sphere(), -1f)
                    )
                },
            };

        private sealed class TestImmutableList : IImmutableList<Intersection>
        {
            /// <inheritdoc />
            public int Count { get; set; }

            /// <inheritdoc />
            public Intersection this[int index] => throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> Add(Intersection value) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> AddRange(IEnumerable<Intersection> items) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> Clear() => throw new NotImplementedException();

            /// <inheritdoc />
            public IEnumerator<Intersection> GetEnumerator() => throw new NotImplementedException();

            /// <inheritdoc />
            public int IndexOf(
                Intersection item,
                int index,
                int count,
                IEqualityComparer<Intersection>? equalityComparer
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> Insert(int index, Intersection element) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> InsertRange(
                int index,
                IEnumerable<Intersection> items
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public int LastIndexOf(
                Intersection item,
                int index,
                int count,
                IEqualityComparer<Intersection>? equalityComparer
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> Remove(
                Intersection value,
                IEqualityComparer<Intersection>? equalityComparer
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> RemoveAll(Predicate<Intersection> match) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> RemoveAt(int index) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> RemoveRange(
                IEnumerable<Intersection> items,
                IEqualityComparer<Intersection>? equalityComparer
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> RemoveRange(int index, int count) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> Replace(
                Intersection oldValue,
                Intersection newValue,
                IEqualityComparer<Intersection>? equalityComparer
            ) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            public IImmutableList<Intersection> SetItem(int index, Intersection value) =>
                throw new NotImplementedException();

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
