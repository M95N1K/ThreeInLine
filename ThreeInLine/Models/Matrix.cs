using System;

namespace ThreeInLine.Models
{
    public class Matrix
    {
        private readonly int[,] _matrix;
        private readonly int _columns;
        private readonly int _rows;

        /// <summary>
        /// Возвращает значение ячейки по адрессу x,y
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <returns>int значение</returns>
        /// <exception cref="NullReferenceException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public int this[int x, int y]
        {
            get
            {
                if (_matrix == null || _matrix.Length == 0)
                    throw new NullReferenceException("Matrix not created");

                if (x > -1 && x < _columns)
                    if (y > -1 && y < _rows)
                        return _matrix[x, y];

                throw new IndexOutOfRangeException("Invalid index");
            }
            set
            {
                if (_matrix == null || _matrix.Length == 0)
                    throw new NullReferenceException("Matrix not created");

                if (x > -1 && x < _columns)
                    if (y > -1 && y < _rows)
                    {
                        _matrix[x, y] = value;
                        return;
                    }

                throw new IndexOutOfRangeException("Invalid index");
            }
        }

        public Matrix(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
            _matrix = new int[_columns, _rows];
        }
    }
}
