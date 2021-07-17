using System;
using ThreeInLine;
using ThreeInLine.Models;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMatrix();

            Console.ReadKey();
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
