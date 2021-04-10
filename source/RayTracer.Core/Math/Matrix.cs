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
        {
            Rows = elements.GetLength(0);
            Columns = elements.GetLength(1);
            Elements = elements;
        }

        protected Matrix(Matrix other)
            : this(other.Rows, other.Columns)
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Elements[i, j] = other.Elements[i, j];
                }
            }
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

        public abstract float Determinant();

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

        public abstract Matrix SubMatrix(int row, int column);

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

        public abstract Matrix Transpose();

        protected float[,] SubMatrixElements(int rowToRemove, int columnToRemove)
        {
            var result = new float[Rows - 1, Columns - 1];

            var readI = rowToRemove == 0 ? 1 : 0;
            var writeI = 0;
            while (readI < Rows)
            {
                var readJ = columnToRemove == 0 ? 1 : 0;
                var writeJ = 0;

                while (readJ < Columns)
                {
                    result[writeI, writeJ] = Elements[readI, readJ];

                    readJ += readJ == columnToRemove - 1 ? 2 : 1;
                    writeJ += 1;
                }

                readI += readI == rowToRemove - 1 ? 2 : 1;
                writeI += 1;
            }

            return result;
        }

        protected float[,] TransposeElements()
        {
            var result = new float[Rows, Columns];

            for (int readI = 0, writeJ = 0; readI < Rows; readI += 1, writeJ += 1)
            {
                for (int readJ = 0, writeI = 0; readJ < Columns; readJ += 1, writeI += 1)
                {
                    result[writeI, writeJ] = Elements[readI, readJ];
                }
            }

            return result;
        }

        public static bool operator ==(Matrix lhs, Matrix rhs) => lhs.Equals(rhs);

        public static bool operator !=(Matrix lhs, Matrix rhs) => !lhs.Equals(rhs);
    }
}
