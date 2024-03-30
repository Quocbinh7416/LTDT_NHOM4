using System;
using System.Collections.Generic;

namespace GraphTheory
{
    class Logger
    {
        public static void Log2DArray(int[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public static void LogArray(int[] array)
        {
            int Length = array.Length;

            for (int i = 0; i < Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }

        public static void LogList(List<int> list)
        {
            foreach (int item in list)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }

    }
}