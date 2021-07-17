using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInLine.Models;

namespace ThreeInLine
{
    public class TIL_Logics : Matrix
    {
        
        public TIL_Logics(int columns, int rows) : base(columns, rows)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int k = 0; k < Columns; k++)
                {
                    this[k, i] = 10;
                }
            }
        }
    }
}
