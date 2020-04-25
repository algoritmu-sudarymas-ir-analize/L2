using System;

namespace LD2_0_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[,] a =
            {
                {1, 3, 3},
                {2, 3, 3},
                {1, 2, 4},
            };

            Console.WriteLine("K = " + MaxEqualRectangle(a));
        }

        static int MaxEqualRectangle(int[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            int[,] ats = new int [rows, cols];
            int result = 0;

            for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
            {
                // pirmojo stulpelio ar eilutes elementai kvadrato nesudarys, tai jie lieka kaip 1x1 
                if (row == 0 || col == 0) ats[row, col] = 1;

                else
                {
                    // jei kvadrata sudaro 4 vienodi elementai, tada ats = Ats minimumas + 1 
                    if (arr[row, col] == arr[row - 1, col] &&
                        arr[row, col] == arr[row, col - 1] &&
                        arr[row, col] == arr[row - 1, col - 1])

                        ats[row, col] = Math.Min(
                                            ats[row - 1, col], Math.Min(
                                                ats[row, col - 1],
                                                ats[row - 1, col - 1])
                                        ) + 1;

                    // jei elementai ne vienodi, tai bazinis atvejis
                    else ats[row, col] = 1;
                }

                // isimenam didziausia ats reiksme
                result = Math.Max(result, ats[row, col]);
            }

            return result;
        }
    }
}