using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            //FindTheRunningMedian();
            // List<List<long>> listOne = new List<List<long>>();
            //long lengthOne = Convert.ToInt32(Console.ReadLine());
            //string inputOne = Console.ReadLine();
            //long[] listOne = new long[lengthOne];

            //for (int i = 0; i < lengthOne; i++)
            //{
            //    var size = Convert.ToInt32(Console.ReadLine());
            //    string line = Console.ReadLine();
            //    var numbers = line.Split(' ');
            //    List<long> temp = new List<long>();
            //    for (long j = 0; j < size; j++)
            //    {
            //        temp.Add(Int64.Parse(numbers[j]));
            //    }
            //    listOne.Add(temp);
            //}

            //for (int i = 0; i < lengthOne; i++)
            //{
            //    Console.WriteLine(KingdomDreams(listOne[i]));
            //}

            //long sum = 0;
            //for (int i = 0; i < listOne.Length / 2; i++)
            //{
            //    sum += (listOne[i] + listOne[listOne.Length - i - 1]) * (listOne[i] + listOne[listOne.Length - i - 1]);
            //}
            //Console.WriteLine(sum);

            //long sum1 = 0, sum2 = 0;
            //for (int i = 0; i <lengthOne; i++)
            //{
            //    if (i < lengthOne / 2)
            //    {
            //        sum1 += list[i];
            //    }

            //    else
            //    {
            //        sum2 += list[i];
            //    }
            //}


            //Console.WriteLine(sum1 * sum1 + sum2 * sum2);
            // Console.WriteLine(lengthOne % 2 == 0 ? arr[lengthOne / 2 -1] : arr[lengthOne / 2]);

            //int n = Convert.ToInt32(Console.ReadLine());
            //string input = Console.ReadLine();
            //Counting(input.ToCharArray());
            //List<int> listTwo = new List<int>();
            //int lengthTwo = Convert.ToInt32(Console.ReadLine());
            //string inputTwo = Console.ReadLine();
            //var numbersTwo = inputTwo.Split(' ');

            //for (int i = 0; i < lengthTwo; i++)
            //{
            //    listTwo.Add(Int32.Parse(numbersTwo[i]));
            //}


            // var sortedOne = Insertion(listOne);
            // Console.WriteLine(sortedOne[lengthOne - 1] - sortedOne[0] + 1 - lengthOne);

            // var sortedTwo = Selection(listTwo);
            // Console.WriteLine(sortedOne[0] + " " + sortedTwo[0]);


            //var sorted = SortMerge(list);
            //int res = 0;
            //for (int i = 0; i < sorted.Count; i = i + 2)
            //{
            //    res += sorted[i + 1] - sorted[i];
            //}
            //Console.WriteLine(res);
            //long lengthOne = Convert.ToInt32(Console.ReadLine());
            //List<string> Letters = new List<string>();

            //for (int i = 0; i < lengthOne; i++)
            //{
            //    Letters.Add(Console.ReadLine());
            //}

            //for (int i = 0; i < Letters.Count; i++)
            //{
            //    Console.WriteLine(IsPalindromeGood(Letters[i]));

            //}

            //string sn = Console.ReadLine();
            //int s = Int32.Parse(sn.Split(' ')[0]);
            //int n = Int32.Parse(sn.Split(' ')[1]);
            //List<string> dragons = new List<string>();
            //for (int i = 0; i < n; i++)
            //{
            //    dragons.Add(Console.ReadLine());
            //}
            //Console.WriteLine(Dragons(dragons,s));

        }

        static long KingdomDreams(List<long> list)
        {
            Bubble(list);
            int countPeople = list.Count;
            long sum = 0;
            while (countPeople >= 4)
            {
                sum += Math.Min(list[0] + list[1] + list[1] + list[countPeople - 1], list[0] + list[0] + list[countPeople - 2] + list[countPeople - 1]);
                countPeople -= 2;
            }

            if (countPeople == 1) sum += list[0];
            if (countPeople == 2) sum += list[1];
            if (countPeople == 3) sum += list[0] + list[1] + list[2];

            return sum;
        }
        static void Bubble(List<long> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                bool isSorted = true;
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        isSorted = false;
                        list[j] = list[j] + list[j + 1];
                        list[j + 1] = list[j] - list[j + 1];
                        list[j] = list[j] - list[j + 1];
                    }

                }

                if (isSorted) return;

            }

        }
        static string Dragons(List<string> dragons, int s)
        {
            Bubble(dragons);
            for (int i = 0; i < dragons.Count; i++)
            {
                if (s <= Int32.Parse(dragons[i].Split(' ')[0]))
                {
                    return "NO";
                }
                else
                {
                    s += Int32.Parse(dragons[i].Split(' ')[1]);
                }
            }
            return "YES";

        }
        static void Bubble(List<string> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var isSorted = true;
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (int.Parse(list[j].Split(' ')[0]) > int.Parse(list[j + 1].Split(' ')[0]))
                    {
                        isSorted = false;
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
                if (isSorted) return;
            }
        }
        static string IsPalindromeGood(string str)
        {
            if (str.Length == 1) return "-1";
            var letters = CountingSort(str);
            return letters[letters.Length - 1] == letters[0] ? "-1" : new string(letters);
        }
        static char[] CountingSort(string str)
        {
            char[] count = new char[256];
            for (int i = 0; i < str.Length; i++)
                ++count[str[i]];

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            char[] result = new char[str.Length];

            for (int i = str.Length - 1; i >= 0; i--)
            {
                result[count[str[i]] - 1] = str[i];
                --count[str[i]];
            }
            return result;
        }
        static void Quick(long[] arr, long first, long end)
        {
            if (first == end) return;
            long pivot = Partition(arr, first, end);
            Quick(arr, first, pivot);
            Quick(arr, pivot + 1, end);
        }
        private static long Partition(long[] arr, long min, long max)
        {
            long pivot = min;
            long min_index = min;
            for (long i = min + 1; i < max; i++)
            {
                if (arr[i] >= arr[pivot])
                {
                    ++min_index;
                    var temp1 = arr[min_index];
                    arr[min_index] = arr[i];
                    arr[i] = temp1;
                }
            }

            var temp2 = arr[min_index];
            arr[min_index] = arr[pivot];
            arr[pivot] = temp2;
            return min_index;
        }
        static void Counting(char[] list)
        {
            int z = 0, o = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == 'z')
                    z++;

                else if (list[i] == 'n')
                    o++;
            }

            while (o > 0)
            {
                Console.Write(1);
                o--;
            }

            while (z > 0)
            {
                Console.Write(0);
                z--;
            }
        }
        public static int[] InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                var currentIdx = i;
                while (currentIdx > 0 && array[currentIdx] < array[currentIdx - 1])
                {
                    var temp = array[currentIdx - 1];
                    array[currentIdx - 1] = array[currentIdx];
                    array[currentIdx] = temp;
                    currentIdx--;
                }
            }
            return array;
        }
        static int[] Selection(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var minIdx = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIdx])
                        minIdx = j;
                }
                var temp = array[i];
                array[i] = array[minIdx];
                array[minIdx] = temp;
            }
            return array;
        }
        static List<long> SortMerge(List<long> list)
        {
            if (list.Count <= 1) return list;
            var left = new List<long>();
            var right = new List<long>();

            for (int i = 0; i < list.Count / 2; i++)
                left.Add(list[i]);

            for (int i = list.Count / 2; i < list.Count; i++)
                right.Add(list[i]);

            left = SortMerge(left);
            right = SortMerge(right);
            return Merge(left, right);
        }
        static List<long> Merge(List<long> left, List<long> right)
        {
            var merged = new List<long>();
            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())
                    {
                        merged.Add(left.First());
                        left.Remove(left.First());
                    }

                    else
                    {
                        merged.Add(right.First());
                        right.Remove(right.First());
                    }

                }

                else if (left.Count > 0)
                {
                    merged.Add(left.First());
                    left.Remove(left.First());
                }

                else if (right.Count > 0)
                {
                    merged.Add(right.First());
                    right.Remove(right.First());
                }

            }
            return merged;
        }
        static void Radix(long[] arr)
        {
            long max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                max = Math.Max(max, arr[i]);
            }

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(arr, exp, 10);
            }
        }
        private static void CountingSort(long[] arr, int exp, long range)
        {
            var count = new long[range];
            var res = new long[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ++count[(arr[i] / exp) % range];

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                res[count[(arr[i] / exp) % range] - 1] = arr[i];
                --count[(arr[i] / exp) % range];
            }

            for (int i = 0; i < res.Length; i++)
            {
                arr[i] = res[i];
            }
        }
        private static void MonkSorting(long[] arr)
        {
            long max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                max = Math.Max(max, arr[i]);
            }

            int mul = 1;
            while (max > 0)
            {
                CountingSort(arr, mul, 100000);
                Print(arr);
                mul *= 100000;
                max /= 100000;
            }
        }
        private static void Print(long[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        //private static long GivenNumber(long first, long end, long num)
        //{
        //    if (end == 1) return (long)(num % Math.Pow(10, first));
        //    return (long)((num / Math.Pow(10, end - 1)) % 100000);
        //}

        static void FindTheRunningMedian()
        {
            var list = new List<int>();
            int n = Convert.ToInt32(Console.ReadLine());
            int temp = Convert.ToInt32(Console.ReadLine());
            list.Add(temp);
            Console.WriteLine(temp.ToString("F01", new CultureInfo("en-us")));
            for (int i = 1; i < n; i++)
            {
                temp = Convert.ToInt32(Console.ReadLine());
                list.Add(temp);
                if (list[i] > list[i - 1])
                {
                    list[i] = list[i] + list[i - 1];
                    list[i - 1] = list[i] - list[i - 1];
                    list[i] = list[i] - list[i - 1];
                }
                Median(list);
            }
        }

        static void Median(List<int> list)
        {
            if (list.Count % 2 == 0)
            {
                Console.WriteLine(((float)(list[list.Count / 2] + list[(list.Count / 2) - 1]) / 2).ToString("F01", new CultureInfo("en-us")));
            }
            else
            {
                Console.WriteLine(((float)list[list.Count / 2]).ToString("F01", new CultureInfo("en-us")));
            }
        }
    }

    public class MinHeap
    {
        public List<int> heap = new List<int>();

        public MinHeap(List<int> array)
        {
            heap = buildHeap(array);
        }

        public List<int> buildHeap(List<int> array)
        {
            var first = array.Count / 2 - 1;
            for (int i = first; i >= 0; i--)
                siftDown(i, array.Count - 1, array);
            return array;
        }

        public void siftDown(int currentIdx, int endIdx, List<int> heap)
        {
            var largest = currentIdx;
            var l = currentIdx * 2 + 1;
            var r = currentIdx * 2 + 2;

            if (l <= endIdx && heap[l] < heap[largest])
                largest = l;
            if (r <= endIdx && heap[r] < heap[largest])
                largest = r;

            if (largest != currentIdx)
            {
                var temp = heap[largest];
                heap[largest] = heap[currentIdx];
                heap[currentIdx] = temp;
                siftDown(largest, endIdx, heap);
            }
        }

        public void siftUp(int currentIdx, List<int> heap)
        {
            var parentIdx = (currentIdx - 1) / 2;
            while (currentIdx > 0 && heap[parentIdx] > heap[currentIdx])
            {
                Swap(parentIdx, currentIdx);
                currentIdx = parentIdx;
                parentIdx = (currentIdx - 1) / 2;
            }
        }

        public int Peek()
        {
            return heap[0];
        }

        public int Remove()
        {
            Swap(0, heap.Count - 1);
            var value = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            siftDown(0, heap.Count - 1, heap);
            return value;
        }

        public void Insert(int value)
        {
            heap.Add(value);
            siftUp(heap.Count - 1, heap);
        }

        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
