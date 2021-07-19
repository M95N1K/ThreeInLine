using System;
using ThreeInLine;
using ThreeInLine.Models;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestMatrix();
            TestTIL_Logics();

            Console.ReadKey();
        }

        static void TestTIL_Logics()
        {
            int x = 10;
            int y = 20;
            int elements = 3;
            TIL_Logics matrix = new TIL_Logics(x, y,elements);
            matrix.OnDestruction += Matrix_OnDestruction;

            PrintMatrix(matrix);

            Console.WriteLine();
            matrix.DestructionAllScreen();

            PrintMatrix(matrix);

            Console.WriteLine();
            matrix.Fall();

            PrintMatrix(matrix);
        }

        private static void Matrix_OnDestruction(int destructionElement)
        {
            Console.WriteLine(destructionElement);
        }

        static void PrintMatrix(Matrix matrix)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int k = 0; k < matrix.Columns; k++)
                {
                    Console.ForegroundColor = (ConsoleColor)(matrix[k, i] / 10)+2;
                    Console.Write($"{matrix[k, i],2} ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = tmp;
        }

        static void TestMatrix()
        {
            int x = 3;
            int y = 2;
            Matrix matrix = new Matrix(x, y);

            matrix[0, 0] = 5;
            matrix[1, 0] = 4;
            matrix[2, 0] = 3;

            PrintMatrix(matrix);
        }
    }
}
