using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace LD2_0
{
    internal static class Program
    {
        private static int[] s;
        private static int[] p;

        private static int[,] cache;

        private static int count = 0;

        public static void Main(string[] args)
        {
            //  int max = int.Parse(args[0]);

            int max = 2000;

            var rand = new Random();
            s = GetRandomArray(500,max);
            p = GetRandomArray(500,max);
            int n = s.Length;
            int w = (int)s.Average();
            
            Console.WriteLine("Metodas;Dydis;Reiksme;Laikas;Veiksmai");
            for (int i = 1; i <= 300; i ++)
            {
                cache = InitializeCache(i, w, -1);

                Stopwatch st = Stopwatch.StartNew();
                Console.Write($"Ga;{i};{Ga(i, w)};");
                st.Stop();
                Console.WriteLine($"{st.Elapsed};{count}");
                count = 0;

                st.Reset();
                st.Start();
                Console.Write($"Gb;{i};{Gb(i, w)};");
                st.Stop();
                Console.WriteLine($"{st.Elapsed};{count}");
                count = 0;
            }
        }


        static int Ga(int k, int r)
        {
            count++; //performance test
            if (r == 0 || k == 0) return 0;

            if (s[k - 1] > r) return Ga(k - 1, r);

            else return Math.Max(Ga(k - 1, r), p[k - 1] + Ga(k - 1, r - s[k - 1]));
        }

        static int Gb(int k, int r)
        {
            count++; //performance test
            if (r == 0 || k == 0) return 0;

            if (cache[k - 1, r - 1] != -1) return cache[k - 1, r - 1];

            if (s[k - 1] > r)
            {
                cache[k - 1, r - 1] = Gb(k - 1, r);
                return cache[k - 1, r - 1];
            }

            else
            {
                cache[k - 1, r - 1] = Math.Max(Gb(k - 1, r), p[k - 1] + Gb(k - 1, r - s[k - 1]));
                return cache[k - 1, r - 1];
            }
        }

        static int[,] InitializeCache(int rows, int columns, int fillValue)
        {
            var arr = new int[rows, columns];

            for (int j = 0; j < arr.GetLength(0); j++)
            for (int k = 0; k < arr.GetLength(1); k++)
                arr[j, k] = fillValue;

            return arr;
        }

        private static int[] GetRandomArray(int length, int max)
        {
            var arr = new int[length];
            var rand = new Random();

            for (int i = 0; i < length; i++)
                arr[i] = rand.Next(max);

            return arr;
        }
    }
}