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
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    Elements[row, col] = other.Elements[row, col];
                }
            }
        }

        public int Columns { get; }

        public bool IsInvertible => Determinant() != 0f;

        public float this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows || col < 0 || col > Columns)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{row}, {col}] is not valid for a {Rows}x{Columns} matrix",
                        (Exception?) null
                    );
                }

                return Elements[row, col];
            }
            init
            {
                if (row < 0 || row >= Rows || col < 0 || col > Columns)
                {
                    throw new ArgumentOutOfRangeException(
                        $"[{row}, {col}] is not valid for a {Rows}x{Columns} matrix",
                        (Exception?) null
                    );
                }

                Elements[row, col] = value;
            }
        }

        public int Rows { get; }

        protected float[,] Elements { get; }

        public float Cofactor(int row, int column)
        {
            var multiplier = (row + column) % 2 == 1 ? -1f : 1f;
            return Minor(row, column) * multiplier;
        }

        public virtual float Determinant()
        {
            var result = 0f;

            for (var col = 0; col < Columns; col += 1)
            {
                result += Elements[0, col] * Cofactor(0, col);
            }

            return result;
        }

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

            for (var col = 0; col < Rows; col++)
            {
                for (var row = 0; row < Columns; row++)
                {
                    if (!Elements[col, row].ApproximatelyEquals(other.Elements[col, row]))
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

        public abstract Matrix Inverse();

        public float Minor(int row, int column) => SubMatrix(row, column).Determinant();

        public abstract Matrix SubMatrix(int row, int column);

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder(32).Append("Matrix")
                .Append(Rows)
                .Append('x')
                .Append(Columns)
                .Append("[ ");

            for (var row = 0; row < Rows; row++)
            {
                if (row > 0)
                {
                    sb.Append(", ");
                }

                sb.Append('[');

                for (var col = 0; col < Columns; col++)
                {
                    if (col > 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append(Elements[row, col]);
                }

                sb.Append("] ");
            }

            sb.Append(']');
            return sb.ToString();
        }

        public abstract Matrix Transpose();

        protected float[,] InverseElements()
        {
            var determinant = Determinant();
            if (determinant == 0f)
            {
                throw new InvalidOperationException("Matrix is not invertible");
            }

            var result = new float[Rows, Columns];
            for (var row = 0; row < Rows; row += 1)
            {
                for (var col = 0; col < Columns; col += 1)
                {
                    result[col, row] = Cofactor(row, col) / determinant;
                }
            }

            return result;
        }

        protected float[,] SubMatrixElements(int rowToRemove, int columnToRemove)
        {
            var result = new float[Rows - 1, Columns - 1];

            var readRow = rowToRemove == 0 ? 1 : 0;
            var writeRow = 0;
            while (readRow < Rows)
            {
                var readCol = columnToRemove == 0 ? 1 : 0;
                var writeCol = 0;

                while (readCol < Columns)
                {
                    result[writeRow, writeCol] = Elements[readRow, readCol];

                    readCol += readCol == columnToRemove - 1 ? 2 : 1;
                    writeCol += 1;
                }

                readRow += readRow == rowToRemove - 1 ? 2 : 1;
                writeRow += 1;
            }

            return result;
        }

        protected float[,] TransposeElements()
        {
            var result = new float[Rows, Columns];

            for (var row = 0; row < Rows; row += 1)
            {
                for (var col = 0; col < Columns; col += 1)
                {
                    result[col, row] = Elements[row, col];
                }
            }

            return result;
        }

        public static bool operator ==(Matrix lhs, Matrix rhs) => lhs.Equals(rhs);

        public static bool operator !=(Matrix lhs, Matrix rhs) => !lhs.Equals(rhs);
    }
}
