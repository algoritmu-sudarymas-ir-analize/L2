using System;
using System.Diagnostics;

namespace LD2_0_2
{
    internal class Program
    {
        private static int count = 0;

        public static void Main(string[] args)
        {
            var time = new Stopwatch();
            Console.WriteLine("Size;Count;Time");
            for (int i = 1; i <= 1000000; i*=10)
            {
                int[,] arr = RandomSquareMatrix(i);

                count = 0;
                time.Restart();
                int[] answer = MaxEqualRectangle(arr);
                time.Stop();
                Console.WriteLine($"{i*i};{count};{time.Elapsed}");

                // Console.WriteLine("K = " + answer[0]);
                // Console.WriteLine($"Start = N[{answer[1] - answer[0] + 1},{answer[2] - answer[0] + 1}]");
                // Console.WriteLine($"End = N[{answer[1]},{answer[2]}]");
            }
        }

        /// <returns>
        /// [0] - K
        /// [1] - eilute
        /// [2] - stulpelis
        /// </returns>
        static int[] MaxEqualRectangle(int[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            int[,] ats = new int [rows, cols];
            int[] result = {0, 0, 0};

            for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
            {
                count++; // performance test
                
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
                if (ats[row, col] > result[0])
                {
                    result[0] = ats[row, col];
                    result[1] = row;
                    result[2] = col;
                }
            }

            return result;
        }

        static int[,] RandomSquareMatrix(int height)
        {
            int[,] arr = new int[height, height];
            var rand = new Random();
            for (int i = 0; i < height; i++)
            for (int j = 0; j < height; j++)
                arr[i, j] = rand.Next();

            return arr;
        }
    }
}