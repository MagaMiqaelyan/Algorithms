using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AlgoExpert
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreeNumberSum(new int[] { 12, 3, 1, 2, -6, 5, -8, 6 }, 0);
        }

        public static List<int[]> ThreeNumberSum(int[] array, int targetSum)
        {
            Array.Sort(array);
            var list = new List<int[]>();
            for (int i = 0; i < array.Length; i++)
            {
                var current = array[i];
                var l = i + 1;
                var r = array.Length - 1;
                while (l < r)
                {
                    var curentSum = current + array[l] + array[r];
                    if (curentSum == targetSum)
                    {
                        list.Add(new int[] { current, array[l], array[r] });
                        l++;
                        r--;
                    }
                    else if (curentSum < targetSum)
                        l++;
                    else
                        r--;

                }
            }
            return list;
        }
    }
}
