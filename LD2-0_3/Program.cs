using System;
using System.Diagnostics;
using System.Linq;

namespace LD2_0_3
{
    internal class Program
    {
        private static int[] s;
        private static int[] p;

        private static int[,] cache;

        public static void Main(string[] args)
        {
            //  int max = int.Parse(args[0]);

            int max = 2000;

            var rand = new Random();
            s = GetRandomArray(500, max);
            p = GetRandomArray(500, max);
            int n = s.Length;
            int w = (int) s.Average();
            Stopwatch st = Stopwatch.StartNew();

            Console.WriteLine("Metodas;Dydis;Reiksme;Laikas");
            for (int i = 1; i <= 300; i++)
            {
                Console.Write($"Ga;{i};{Ga(i, w)};");
                st.Stop();
                Console.WriteLine($"{st.Elapsed}");
                st.Reset();
            }
        }


        static int Ga(int k, int r)
        {
            if (r == 0 || k == 0) return 0;

            if (s[k - 1] > r) return Ga(k - 1, r);

            else return Math.Max(Ga(k - 1, r), p[k - 1] + Ga(k - 1, r - s[k - 1]));
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