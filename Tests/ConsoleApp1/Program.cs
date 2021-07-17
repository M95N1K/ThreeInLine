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
            int x = 3;
            int y = 2;
            TIL_Logics matrix = new TIL_Logics(x, y);


            for (int i = 0; i < y; i++)
            {
                for (int k = 0; k < x; k++)
                {
                    Console.Write($"{matrix[k, i]} ");
                }
                Console.WriteLine();
            }
        }

        static void TestMatrix()
        {
            int x = 3;
            int y = 2;
            Matrix matrix = new Matrix(x, y);

            matrix[0, 0] = 5;
            matrix[1, 0] = 4;
            matrix[2, 0] = 3;

            for (int i = 0; i < y; i++)
            {
                for (int k = 0; k < x; k++)
                {
                    Console.Write($"{matrix[k, i]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
