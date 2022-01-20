using Graph;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void Timus1494()
        {
            var n = int.Parse(Console.ReadLine());
            var arr = new int[n];
            var s = new Stack<int>();
            for (int i = 0; i < n; i++)
                arr[i] = int.Parse(Console.ReadLine());
             
            if (n == 1)
            {
                Console.WriteLine("Not a proof");
                return;
            }
            var isCheater = false;
            var current = 0;
            foreach (var a in arr)
            {
                if (a > current)
                {
                    for (var i = current + 1; i < a; i++)
                    {
                        s.Push(i);
                    }
                    current = a;
                }
                else
                {
                    if (a == s.Peek())
                        s.Pop();
                    else
                    {
                        isCheater = true;
                        break;
                    }
                }
            }

            Console.WriteLine(isCheater ? "Cheater" : "Not a proof");
        }

        private static void Timus1028()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var st = new SegmentTree(n);
            var level = new int[n];
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine().Split(' ');
                var x = int.Parse(temp[0]);
                level[st.GetSum(0, 40000, 0, x, 0)]++;
                st.UpdateValue(0, 40000, x, 1, 0);
            }

            foreach (var t in level)
            {
                Console.WriteLine(t);
            }
        }

        public static void Timus1471()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            if (n == 1)
            {
                Console.WriteLine(0);
                return;
            }

            var tree = new Tree(n);
            for (int i = 1; i < n; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                tree.AddEdge(input[0], input[1], input[2]);
            }

            tree.PreCalculate();
            var m = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                var x = input[0];
                var y = input[1];
                if (x == y)
                    Console.WriteLine(0);
                else
                {
                    var node = tree.LCA(x, y);
                    var xw = tree._weighted[x];
                    var yw = tree._weighted[y];
                    var nw = tree._weighted[node];
                    Console.WriteLine(xw + yw - 2 * nw);
                }
            }
        }

        public static void Timus1613()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            int[] a = new int[n];
            int[] b = new int[n];
            var ss = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            for (int i = 0; i < n; i++)
            {
                a[i] = ss[i];
                b[i] = i;
            }
            sort(a, 0, a.Length - 1, b);
            int q = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < q; i++)
            {
                var line = Console.ReadLine().Split(' ');
                int l = 0;
                var from = Convert.ToInt32(line[0]) - 1;
                var to = Convert.ToInt32(line[1]) - 1;
                var val = Convert.ToInt32(line[2]);
                if (@from == to)
                {
                    if (ss[@from] == val)
                    {
                        Console.Write(1);
                        continue;
                    }

                    Console.Write(0);
                    continue;
                }

                if (ss[@from] == val)
                {
                    Console.Write(1);
                    continue;
                }

                if (ss[to] == val)
                {
                    Console.Write(1);
                    continue;
                }
                var index = search(a, Convert.ToInt32(line[2]));
                var forward = index;
                while (forward >= 0 && forward < a.Length && (a[forward] == a[index]))
                {
                    int realPlace = b[forward++];
                    if (realPlace > to || realPlace < @from) continue;
                    l = 1; break;
                }
                Console.Write(l);
            }
        }
        public static int search(int[] a, int find)
        {
            int low = 0;
            int high = a.Length - 1;
            if (a.Length == 0) return -1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (find > a[mid])
                {
                    low = mid + 1;
                }
                else if (find < a[mid])
                {
                    high = mid - 1;
                }
                else if (low != mid)
                {
                    high = mid;
                }
                else
                {
                    return mid;
                }
            }
            return -1;
        }
        public static void sort(int[] a, int left, int right, int[] b)
        {
            int i = left;
            int j = right;
            int tmp;
            int pivot = a[(left + right) / 2];
            do
            {
                while (i < right && a[i] < pivot) i++;
                while (j > left && a[j] > pivot) j--;
                if (i <= j)
                {
                    tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;
                    int bb = b[i];
                    b[i] = b[j];
                    b[j] = bb;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (left < j) sort(a, left, j, b);
            if (i < right) sort(a, i, right, b);
        }

        static void Timus1126()
        {
            var m = Convert.ToInt32(Console.ReadLine());
            var nums = new List<int>();
            while (true)
            {
                var n = Console.ReadLine();
                if (n == "-1")
                    break;
                nums.Add(Convert.ToInt32(n));
            }

            for (int i = 0; i <= nums.Count - m; i++)
            {
                var max = 0;
                for (int j = i; j < i + m; j++)
                {
                    max = Math.Max(nums[j], max);
                }
                Console.WriteLine(max);
            }
        }

        private static void Timus1654()
        {
            var str = Console.ReadLine();
            var st = new Stack<char>();
            foreach (var t in str)
            {
                char c = st.Count == 0 ? '0' : st.Peek();
                if (c == t) st.Pop();
                else st.Push(t);
            }

            var res = new char[st.Count];
            for (int i = 0; i < res.Length; i++)
            {
                res[res.Length - 1 - i] = st.Pop();
            }

            Console.WriteLine(new string(res));
        }

        static void Timus1100()
        {
            var n = Console.ReadLine();
            var firstList = new List<long>();
            var secondList = new List<long>();

            for (int k = 0; k < Convert.ToInt32(n); k++)
            {
                var temp = Console.ReadLine().Split(' ');
                firstList[k] = Convert.ToInt64(temp[0]);
                secondList[k] = Convert.ToInt32(temp[1]);
            }
            for (int i = 100; i >= 0; i--)
            {
                for (int j = 0; j < Convert.ToInt32(n); j++)
                {
                    if (secondList[j] == i)
                        Console.WriteLine(firstList[j] + " " + secondList[j]);

                }
            }
        }
    }
}
