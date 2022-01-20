using System;
using System.Collections.Generic;
using System.Linq;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            // PairSums();
            int[] arr = { 1, 1, 4, 2, 2 };
            SingleNumber(arr);
        }

        static void MaxOccurence()
        {
            var str = Console.ReadLine();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (dict.Keys.Contains(str[i].ToString()))
                    ++dict[str[i].ToString()];
                else dict.Add(str[i].ToString(), 1);
            }

            var max = dict.Aggregate((x, y) => x.Value > y.Value ? x : y);
            Console.WriteLine(max.Key + " " + max.Value);
        }

        static void PairSums()
        {
            var nk = Console.ReadLine().Split(' ');
            long n = Convert.ToInt64(nk[0]);
            long k = Convert.ToInt64(nk[1]);
            var input = Console.ReadLine().Split(' ');
            long[] arr = new long[n];
            HashSet<long> hs = new HashSet<long>();

            for (int i = 0; i < n; i++)
            {
                arr[i] = Convert.ToInt64(input[i]);
            }

            foreach (var a in arr)
            {
                var temp = k - a;
                if (hs.Contains(temp))
                {
                    Console.WriteLine("YES");
                    return;
                }

                hs.Add(a);
            }
            Console.WriteLine("NO");
        }

        static void SingleNumber(int[] arr)
        {
            var res = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                res ^= arr[i];
            }

            Console.WriteLine(res);
        }
    }
}
