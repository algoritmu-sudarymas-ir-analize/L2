using System;
using System.Diagnostics;
using System.Threading;

namespace LD2_0
{
    internal static class Program
    {
        private static int[] s;
        private static int[] p;

        public static void Main(string[] args)
        {
            // 2CPU -> 4VCPU
            ThreadPool.SetMaxThreads(2, 2);

            s = GetArray(50_000, 1);
            p = GetArray(50_000, 1);

            Stopwatch time = new Stopwatch();
            Console.WriteLine("Metodas;Dydis;Reiksme;Laikas");
            for (int i = 1, w = 1; i <= 30_000; i++, w++)
            {
                time.Restart();
                Console.Write($"Ga;{i};{Ga(i, w)};");
                time.Stop();
                Console.WriteLine($"{time.Elapsed}");

                time.Restart();
                Console.Write($"Gb;{i};{Gb(i, w)};");
                time.Stop();
                Console.WriteLine($"{time.Elapsed}");
            }
        }

        static int Ga(int k, int r)
        {
            if (r == 0 || k == 0) return 0;

            if (s[k - 1] > r) return Ga(k - 1, r);

            else return Math.Max(Ga(k - 1, r), p[k - 1] + Ga(k - 1, r - s[k - 1]));
        }

        static int Gb(int k, int r)
        {
            if (r == 0 || k == 0) return 0;

            if (s[k - 1] > r) return Ga(k - 1, r);

            int res1 = -1, res2 = -1;

            using (var countdown = new CountdownEvent(2))
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    res1 = Ga(k - 1, r);
                    countdown.Signal();
                });

                ThreadPool.QueueUserWorkItem(_ =>
                {
                    res2 = p[k - 1] + Ga(k - 1, r - s[k - 1]);
                    countdown.Signal();
                });

                countdown.Wait();
            }

            return Math.Max(res1, res2);
        }

        private static int[] GetArray(int length, int fill)
        {
            var arr = new int[length];

            for (int i = 0; i < length; i++)
                arr[i] = fill;

            return arr;
        }

    }
}
