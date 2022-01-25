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
            // ThreeNumberSum(new int[] { 12, 3, 1, 2, -6, 5, -8, 6 }, 0);
            // ArrayOfProducts(new int[] { 5, 1, 4, 2 });
            // SmallestDifference(new int[] { -1, 5, 10, 20, 28, 3 }, new int[] { 26, 134, 135, 15, 17 });
            LongestPeak(new int[] { 1, 1, 3, 2, 1 });
        }

        public static int LongestPeak(int[] array)
        {
            var longest = 0;
            var i = 1;
            while (i < array.Length - 1)
            {
                var isPeak = array[i - 1] < array[i] && array[i] > array[i + 1];
                if (!isPeak)
                {
                    i++;
                    continue;
                }

                var left = i - 2;
                while (left >= 0 && array[left] < array[left + 1])
                {
                    left--;
                }
                var right = i + 1;
                while (right < array.Length - 1 && array[right] > array[right + 1])
                {
                    right++;
                }

                longest = Math.Max(longest, right - left);
                i = right;
            }
            return longest;
        }

        public static int[] SmallestDifference(int[] arrayOne, int[] arrayTwo)
        {
            var minDistance = int.MaxValue;
            var result = new int[2];
            Array.Sort(arrayOne);
            Array.Sort(arrayTwo);
            var i = 0;
            var j = 0;
            while (i < arrayOne.Length && j < arrayTwo.Length)
            {
                var min = Math.Max(arrayOne[i], arrayTwo[j]) - Math.Min(arrayOne[i], arrayTwo[j]);
                if (min < minDistance)
                {
                    minDistance = min;
                    result[0] = arrayOne[i];
                    result[1] = arrayTwo[j];
                }

                if (arrayOne[i] < arrayTwo[j])
                {
                    i++;
                }
                else if (arrayOne[i] > arrayTwo[j])
                {
                    j++;
                }
                else
                    return result;

            }
            return result;
            // bad solution
            //var result = new int[2];
            //var minDistance = int.MaxValue;
            //Array.Sort(arrayOne);
            //Array.Sort(arrayTwo);
            //for (int i = 0; i < arrayOne.Length; i++)
            //{
            //    var j = 0;
            //    var min = Math.Max(arrayOne[i], arrayTwo[j]) - Math.Min(arrayOne[i], arrayTwo[j]);
            //    while (j < arrayTwo.Length && arrayOne[i] > arrayTwo[j])
            //    {
            //        j++;
            //    }
            //    if (j == arrayTwo.Length) j--;
            //    var twoNum = arrayTwo[j];
            //    if (j > 0)
            //    {
            //        var previous = Math.Max(arrayTwo[j - 1], arrayOne[i]) - Math.Min(arrayTwo[j - 1], arrayOne[i]);
            //        var next = Math.Max(arrayTwo[j], arrayOne[i]) - Math.Min(arrayTwo[j], arrayOne[i]);
            //        if (previous < next)
            //        {
            //            min = previous;
            //            twoNum = arrayTwo[j - 1];
            //        }
            //        else
            //        {
            //            min = next;
            //            twoNum = arrayTwo[j];
            //        }

            //    }
            //    if (minDistance > min)
            //    {
            //        minDistance = min;
            //        result[0] = arrayOne[i];
            //        result[1] = twoNum;
            //    }
            //}
            //return result;
        }

        public static int[] ArrayOfProducts(int[] array)
        {
            var left = new int[array.Length];
            var product = 1;
            for (int i = 0; i < array.Length; i++)
            {
                left[i] = product;
                product *= array[i];
            }
            product = 1;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                left[i] *= product;
                product *= array[i];
            }
            return left;
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
