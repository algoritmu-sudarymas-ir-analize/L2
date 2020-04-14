using System;

namespace LD2_0
{
    internal static class Program
    {
        private static int[] s;
        private static int[] p;

        public static void Main()
        {
            s = new[] {1, 2, 3};
            p = new[] {1, 2, 3};

            int n = s.Length;
            int w = 3;

            Console.WriteLine(G(n, w));
        }

        static int G(int k, int r)
        {
            if (r == 0 || k == 0) return 0;

            if (s[k-1] > r) return G(k - 1, r);

            else return Math.Max(G(k - 1, r), p[k-1] + G(k - 1, r - s[k-1]));
        }
        
    }
}