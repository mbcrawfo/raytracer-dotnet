using System;
using System.Text;
using RayTracer.Core.Extensions;

namespace RayTracer.Core.Math
{
    public abstract class Matrix : IEquatable<Matrix>
    {
        protected Matrix(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), rows, "Value must be positive");
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(columns),
                    columns,
                    "Value must be positive"
                );
            }

            Rows = rows;
            Columns = columns;
            Elements = new float[Rows, Columns];
        }

        protected Matrix(float[,] elements)
            : this(elements.GetLength(0), elements.GetLength(1))
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Elements[i, j] = elements[i, j];
                }
            }
        }

        protected Matrix(Matrix other)
            : this(other.Elements)
        {
        }

        public int Columns { get; }

        public float this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Rows || j < 0 || j > Columns)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{i}, {j}] is not valid for a {Rows}x{Columns} matrix",
                        (Exception?) null
                    );
                }

                return Elements[i, j];
            }
            init
            {
                if (i < 0 || i >= Rows || j < 0 || j > Columns)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{i}, {j}] is not valid for a {Rows}x{Columns} matrix",
                        (Exception?) null
                    );
                }

                Elements[i, j] = value;
            }
        }

        public int Rows { get; }

        protected float[,] Elements { get; }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Matrix);

        /// <inheritdoc />
        public bool Equals(Matrix? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Rows != other.Rows || Columns != other.Columns)
            {
                return false;
            }

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (!Elements[i, j].ApproximatelyEquals(other.Elements[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            throw new NotSupportedException(
                nameof(Matrix) +
                " is not suitable for use as a key because it relies on approximate equality"
            );

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder(32).Append("Matrix")
                .Append(Rows)
                .Append('x')
                .Append(Columns)
                .Append("[ ");

            for (var i = 0; i < Rows; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append('[');

                for (var j = 0; j < Columns; j++)
                {
                    if (j > 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append(Elements[i, j]);
                }

                sb.Append("] ");
            }

            sb.Append(']');
            return sb.ToString();
        }

        public static bool operator ==(Matrix lhs, Matrix rhs) => lhs.Equals(rhs);

        public static bool operator !=(Matrix lhs, Matrix rhs) => !lhs.Equals(rhs);
    }
}
