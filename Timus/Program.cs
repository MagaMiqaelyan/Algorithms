using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Timus
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void Timus1727()
        {
            var n = int.Parse(Console.ReadLine());
            var temp = n;
            var count = 0;
            var numbers = new List<int>();
            for (int i = 9; i >= 0 && temp > 0; --i)
            {
                for (int j = 9; j >= 0 && temp > 0; --j)
                {
                    for (int k = 9; k >= 0 && temp > 0; --k)
                    {
                        for (int l = 9; l >= 0 && temp > 0; --l)
                        {
                            var s = i + j + k + l;
                            if (temp >= s)
                            {
                                temp -= s;
                                count++;
                                numbers.Add(1000 * i + 100 * j + 10 * k + l);
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count);
            foreach (var num in numbers)
            {
                Console.Write(num + " ");
            }

        }

        static void Timus1193()
        {
            var n = int.Parse(Console.ReadLine());
            var sortedList = new List<Tuple<int, int, int>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                sortedList.Add(Tuple.Create(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2])));
            }
            sortedList = sortedList.OrderBy(x => x.Item1).ToList();
            var neededT = new List<int> { 0 };
            var sum = sortedList[0].Item1 + sortedList[0].Item2;
            if (sum > sortedList[0].Item3)
                neededT.Add(sum - sortedList[0].Item3);
            for (var i = 1; i < sortedList.Count; i++)
            {
                sum += sortedList[i].Item2;
                if (sum > sortedList[i].Item3)
                    neededT.Add(sum - sortedList[i].Item3);
            }

            Console.WriteLine(neededT.Max());
        }

        static void Timus1048()
        {
            var n = int.Parse(Console.ReadLine());
            var num1 = new char[n + 1];
            num1[0] = '0';
            var num2 = new char[n + 1];
            num2[0] = '0';
            for (int i = 1; i <= n; i++)
            {
                var input = Console.ReadLine();
                num1[i] = input[0];
                num2[i] = input[2];
            }

            for (int i = n; i > 0; --i)
            {
                var temp = num1[i] - '0' + (num2[i] - '0');
                num1[i] = (char)(temp % 10 + '0');
                num1[i - 1] = (char)(num1[i - 1] - '0' + temp / 10 + '0');
            }

            var start = 1;
            if (num1[0] != '0')
                start = 0;
            for (int i = start; i <= n; i++)
            {
                Console.Write(num1[i]);
            }
        }

        static void Timus1821()
        {
            var n = int.Parse(Console.ReadLine());
            var biathletes = new Dictionary<int, Tuple<string, int>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(' ');
                var name = input[0];
                var time = ConvertTime(input[1]);
                var finish = time + 300 * i;
                biathletes.Add(finish, Tuple.Create(name, time));
            }

            var sortedTimes = biathletes.Keys.OrderBy(x => x).ToList();
            var min = int.MaxValue;
            var res = new List<string>();
            foreach (var t in sortedTimes.Where(t => biathletes[t].Item2 < min))
            {
                min = biathletes[t].Item2;
                res.Add(biathletes[t].Item1);
            }

            res.Sort();
            Console.WriteLine(res.Count);
            foreach (var t in res)
                Console.WriteLine(t);

        }

        static int ConvertTime(string str)
        {
            var split = str.Split(new[] { ':', '.' });
            var mm = int.Parse(split[0]);
            var ss = int.Parse(split[1]);
            var d = int.Parse(split[2]);

            return (mm * 60 + ss) * 10 + d;
        }

        static void Timus1366()
        {
            var n = int.Parse(Console.ReadLine());
            var dp = new BigInteger[1002];
            dp[0] = BigInteger.One;
            dp[1] = BigInteger.Zero;
            for (int i = 2; i <= n; i++)
                dp[i] = BigInteger.Multiply(BigInteger.Add(dp[i - 1], dp[i - 2]), (i - 1));

            Console.WriteLine(dp[n]);
        }

        static void Timus1413()
        {
            var input = Console.In.ReadToEnd();
            var x = 0d;
            var y = 0d;

            var s = Math.Sqrt(2) / 2;
            foreach (var c in input)
            {
                if (c == '2')
                    y--;
                if (c == '8')
                    y++;
                if (c == '4')
                    x--;
                if (c == '6')
                    x++;
                if (c == '1' || c == '7')
                    x -= s;
                if (c == '7' || c == '9')
                    y += s;
                if (c == '3' || c == '9')
                    x += s;
                if (c == '1' || c == '3')
                    y -= s;
                if (c == '0')
                    break;
            }
            Console.Write($"{x:0.0000000000} " + $"{y:0.0000000000}");
        }

        static void Timus1993()
        {
            var input = Console.In.ReadToEnd();
            var _subject = "";
            var _object = "";
            var _verb = "";

            var _startSubject = false;
            var _startObject = false;
            var _startVerb = false;
            int j = 0;
            foreach (var c in input)
            {
                if (c == '\n')
                    break;

                if (c == '(')
                {
                    _startSubject = true;
                    j = 0;
                }
                else if (c == '{')
                {
                    _startObject = true;
                    j = 0;
                }
                else if (c == '[')
                {
                    _startVerb = true;
                    j = 0;
                }

                else if (c == ')')
                {
                    _startSubject = false;
                }
                else if (c == '}')
                {
                    _startObject = false;
                }
                else if (c == ']')
                {
                    _startVerb = false;
                }

                else if (_startSubject)
                {
                    _subject += char.ToLower(c);
                }
                else if (_startObject)
                {
                    _object += char.ToLower(c);
                }
                else if (_startVerb)
                {
                    _verb += char.ToLower(c);
                }

                else if (c == ',')
                {
                    PrintSentence(_object, _subject, _verb);
                    _subject = "";
                    _object = "";
                    _verb = "";
                    j = 1;
                    Console.Write(c);
                }
                else if (j == 1)
                    Console.Write(c);

            }

            if (_object[0] != ' ')
            {
                PrintSentence(_object, _subject, _verb);
            }

        }
        static bool first = true;
        static void PrintSentence(string obj, string subj, string verb)
        {
            if (first && obj.Length > 0)
            {
                obj = char.ToUpper(obj[0]) + obj.Remove(0, 1);
                first = false;
            }
            Console.Write("{0} {1} {2}", obj, subj, verb);
        }

        static void Timus1273()
        {
            var k = int.Parse(Console.ReadLine());
            var ans = k;
            var points = new List<Tuple<int, int>>();
            for (int i = 0; i < k; i++)
            {
                var xy = Console.ReadLine().Split();
                points.Add(Tuple.Create(int.Parse(xy[0]), int.Parse(xy[1])));
            }

            points = points.OrderBy(a => a.Item2).ToList();
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = Tuple.Create(points[i].Item1, i);
            }

            points = points.OrderBy(a => a.Item1).ToList();
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = Tuple.Create(i, points[i].Item2);
            }

        }

        static void Timus1010()
        {
            var n = int.Parse(Console.ReadLine());
            var points = new List<long>();
            for (int i = 1; i <= n; i++)
            {
                var y = long.Parse(Console.ReadLine());
                points.Add(y);
            }

            var maxX1 = long.MinValue;
            var max = long.MinValue;
            for (int i = 1; i < points.Count; i++)
            {
                if (Math.Abs(points[i] - points[i - 1]) > max)
                {
                    max = Math.Abs(points[i] - points[i - 1]);
                    maxX1 = i;
                }
            }

            Console.WriteLine(maxX1 + " " + (maxX1 + 1));

        }

        static void Timus1135()
        {
            var n = int.Parse(Console.ReadLine());
            var str = Console.In.ReadToEnd();

            var index = 0;
            var ans = 0;
            var current = 0;

            while (index < str.Length)
            {
                if (str[index] == '>')
                    current++;
                else if (str[index] == '<')
                    ans += current;
                index++;
            }

            Console.WriteLine(ans);
        }

        static void Timus1576()
        {
            var n1c1 = Console.ReadLine().Split();
            var n1 = int.Parse(n1c1[0]);
            var c1 = int.Parse(n1c1[1]);

            var n2tc2 = Console.ReadLine().Split();
            var n2 = int.Parse(n2tc2[0]);
            var t = int.Parse(n2tc2[1]);
            var c2 = int.Parse(n2tc2[2]);

            var n3 = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            double time = 0;
            for (int i = 0; i < k; i++)
            {
                var call = Console.ReadLine().Split(':');
                var mm = int.Parse(call[0]);
                var ss = int.Parse(call[1]);
                if (mm == 0 && ss <= 6)
                    continue;
                time += mm;
                if (ss != 0)
                    time++;
            }

            Console.WriteLine("Basic:     {0}", n1 + c1 * time);
            Console.WriteLine("Combined:  {0}", n2 + Math.Max(0, time - t) * c2);
            Console.WriteLine("Unlimited: {0}", n3);

        }

        static void Timus1178()
        {
            var n = int.Parse(Console.ReadLine());
            var list = new List<Tuple<int, int, int>>();
            for (int i = 0; i < n; i++)
            {
                var xy = Console.ReadLine().Split();
                var x = int.Parse(xy[0]);
                var y = int.Parse(xy[1]);
                list.Add(Tuple.Create(x, y, i + 1));
            }
            list.Sort();
            for (int i = 0; i < n - 1; i += 2)
            {
                Console.WriteLine(list[i].Item3 + " " + list[i + 1].Item3);
            }
        }

        static void Timus1026()
        {
            var n = int.Parse(Console.ReadLine());
            var database = new List<int>();
            for (int i = 0; i < n; i++)
                database.Add(int.Parse(Console.ReadLine()));

            database.Sort();

            var f = Console.ReadLine();
            var k = int.Parse(Console.ReadLine());
            for (int i = 0; i < k; i++)
            {
                var q = int.Parse(Console.ReadLine());
                Console.WriteLine(database[q - 1]);
            }
        }

        private static void Timus1788()
        {
            var nm = Console.ReadLine().Split();
            var n = int.Parse(nm[0]);
            var m = int.Parse(nm[1]);
            var girls = Console.ReadLine().Split().Select(int.Parse).ToList();
            var boys = Console.ReadLine().Split().Select(int.Parse).ToList();

            girls.Sort();
            boys.Sort();

            var min = int.MaxValue;
            for (int k = 0; k <= Math.Min(n, m); k++)
            {
                var current = 0;
                for (int i = 0; i < m - k; i++)
                    current += boys[i];
                current *= k;
                for (int i = 0; i < n - k; i++)
                    current += girls[i];

                min = Math.Min(min, current);
            }

            Console.WriteLine(min);
        }

        private static void Timus1964()
        {
            var nk = Console.ReadLine().Split();
            var n = int.Parse(nk[0]);
            var k = int.Parse(nk[1]);
            var p = Console.ReadLine().Split().Select(int.Parse).Sum();
            var ans = p - n * (k - 1);
            Console.WriteLine(ans < 0 ? 0 : ans);
        }

        private static void Timus1931()
        {
            var n = Int32.Parse(Console.ReadLine());
            var arr = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var current = 0;
            var pirates = new int[n];
            for (int i = 1; i < arr.Length; i++)
            {
                pirates[i]++;
                pirates[current]++;
                if (arr[current] > arr[i])
                    current = i;
            }

            var max = pirates.Max();
            var index = pirates.ToList().IndexOf(max);
            Console.WriteLine(index + 1);
        }

        static void Timus1868()
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < 4; i++)
            {
                dict.Add(Console.ReadLine(), "gold");
            }
            for (int i = 0; i < 4; i++)
            {
                dict.Add(Console.ReadLine(), "silver");
            }
            for (int i = 0; i < 4; i++)
            {
                dict.Add(Console.ReadLine(), "bronze");
            }
            var n = Convert.ToInt32(Console.ReadLine());
            var answer = new int[n];
            for (int i = 0; i < n; i++)
            {
                var count = 0;
                var m = Convert.ToInt32(Console.ReadLine());
                for (int j = 0; j < m; j++)
                {
                    var rp = Console.ReadLine().Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (dict.ContainsKey(rp[0]))
                        if (dict[rp[0]] == rp[1])
                            count++;
                }
                answer[i] = count;
            }
            var max = answer.Max();
            var counts = answer.Count(x => x == max);
            Console.WriteLine(counts * 5);
        }

        static void Timus2065()
        {
            var nk = Console.ReadLine().Split();
            var n = Int32.Parse(nk[0]);
            var k = Int32.Parse(nk[1]);
            for (int i = 0; i <= n - k; i++)
                Console.Write("0 ");
            var num = 0;
            while (k > 1)
            {
                k--;
                num = num <= 0 ? 1 - num : -num;
                Console.Write(num + " ");
            }
        }

        static void Timus1688()
        {
            var nm = Console.ReadLine().Split();
            var n = Int32.Parse(nm[0]);
            var m = Int32.Parse(nm[1]);
            n *= 3;
            for (int i = 0; i < m; i++)
            {
                var money = Int32.Parse(Console.ReadLine());
                n -= money;
                if (n >= 0)
                    continue;
                Console.WriteLine("Free after {0} times.", i + 1);
                return;
            }

            Console.WriteLine("Team.GOV!");

        }

        static void Timus1711()
        {
            var n = Int32.Parse(Console.ReadLine());
            var codeNames = new string[n, 3];
            for (int i = 0; i < n; i++)
            {
                var names = Console.ReadLine().Split().ToList();
                names.Sort();
                codeNames[i, 0] = names[0];
                codeNames[i, 1] = names[1];
                codeNames[i, 2] = names[2];
            }
            var queue = new Queue<int>();
            var input = Console.ReadLine().Split();
            foreach (var q in input)
            {
                queue.Enqueue(Int32.Parse(q) - 1);
            }

            var lastName = codeNames[Int32.Parse(input[0]) - 1, 0];
            var impossible = true;
            var ans = new List<string>();
            while (queue.Count > 0 && impossible)
            {
                impossible = false;
                var current = queue.Dequeue();
                for (int i = 0; i < 3; i++)
                {
                    if (String.CompareOrdinal(lastName, codeNames[current, i]) <= 0)
                    {
                        impossible = true;
                        lastName = codeNames[current, i];
                        ans.Add(lastName);
                        break;
                    }
                }
            }

            if (impossible)
            {
                ans.Sort();
                foreach (var a in ans)
                {
                    Console.WriteLine(a);
                }
            }
            else
            {
                Console.WriteLine("IMPOSSIBLE");
            }
        }

        static void Timus1228()
        {
            var ns = Console.ReadLine().Split();
            var n = Int32.Parse(ns[0]);
            var s = Int32.Parse(ns[1]);

            for (int i = 0; i < n; i++)
            {
                var d = Int32.Parse(Console.ReadLine());
                Console.WriteLine(s / d - 1);
                s = d;
            }
        }

        static void Timus1573()
        {
            var color = Console.ReadLine().Split();
            var colors = new Dictionary<string, int>
            {
                {"Blue", Int32.Parse(color[0])},
                {"Red", Int32.Parse(color[1])},
                {"Yellow", Int32.Parse(color[2])}
            };

            var k = Int32.Parse(Console.ReadLine());
            var w = 1;
            for (int i = 0; i < k; i++)
            {
                var c = Console.ReadLine();
                w *= colors[c];
            }

            Console.WriteLine(w);
        }

        static void Timus1446()
        {
            var n = Int32.Parse(Console.ReadLine());
            var l1 = new List<string>();
            var l2 = new List<string>();
            var l3 = new List<string>();
            var l4 = new List<string>();
            for (int i = 0; i < n; i++)
            {
                var w = Console.ReadLine();
                var l = Console.ReadLine();
                if (l == "Slytherin")
                    l1.Add(w);
                else if (l == "Hufflepuff")
                    l2.Add(w);
                else if (l == "Gryffindor")
                    l3.Add(w);
                else
                    l4.Add(w);
            }

            Console.WriteLine("Slytherin:");
            foreach (var l in l1)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine();
            Console.WriteLine("Hufflepuff:");
            foreach (var l in l2)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine();
            Console.WriteLine("Gryffindor:");
            foreach (var l in l3)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine();
            Console.WriteLine("Ravenclaw:");
            foreach (var l in l4)
            {
                Console.WriteLine(l);
            }
        }

        static void LeftRotate(int[] arr, int d)
        {
            var n = arr.Length;
            d %= n;
            var g = GCD(d, n);
            for (var i = 0; i < g; i++)
            {
                var temp = arr[i];
                var j = i;
                while (true)
                {
                    var k = j + d;
                    if (k >= n)
                        k -= n;
                    if (k == i)
                        break;
                    arr[j] = arr[k];
                    j = k;
                }
                arr[j] = temp;
            }
        }
        static void Timus1370()
        {
            var nm = Console.ReadLine().Split();
            var n = Int32.Parse(nm[0]);
            var m = Int32.Parse(nm[1]);
            var arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = Int32.Parse(Console.ReadLine());
            }
            LeftRotate(arr, m);
            for (int i = 0; i < 10; i++)
                Console.Write(arr[i]);
        }


        static void DieDie()
        {
            var t = Convert.ToInt32(Console.ReadLine());
            var mod = (long)(Math.Pow(10, 9) + 7);
            for (int i = 0; i < t; i++)
            {
                var n = Convert.ToInt64(Console.ReadLine());
                var b = BinPowerMod(2, n - 1, mod);
                Console.WriteLine(BinPowerMod(b, mod - 2, mod));

            }
        }
        public static void Timus1506()
        {
            var nk = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nk[0];
            var k = nk[1];
            var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            k = (n + k - 1) / k;
            for (int i = 0; i < k; i++)
            {
                for (int j = i; j < input.Length; j += k)
                {
                    if (j > input.Length)
                        break;
                    if (input[j] < 10)
                        Console.Write("   " + input[j]);
                    else if (input[j] < 100)
                        Console.Write("  " + input[j]);
                    else if (input[j] < 1000)
                        Console.Write(" " + input[j]);
                }
                Console.WriteLine();
            }
        }

        static void Polyphemus()
        {
            var c = Convert.ToInt64(Console.ReadLine());
            var sqrtC = Math.Sqrt(c);
            if (sqrtC % 1 != 0)
            {
                Console.WriteLine(1);
                return;
            }

            sqrts = new List<long> { 0, 1 };
            PerfectSquares(4, c);
            int count = 0;

            for (int i = 0; i < sqrts.Count; i++)
            {
                for (int j = i; j < sqrts.Count; j++)
                {
                    if (SqrtSum(sqrts[i], sqrts[j]) == sqrtC)
                        count++;
                }
            }

            Console.WriteLine(count);
        }

        static void PerfectSquares(long l, long r)
        {
            var number = (long)Math.Ceiling(Math.Sqrt(l));
            var n2 = number * number;
            number = number * 2 + 1;

            while (n2 >= l && n2 <= r)
            {
                sqrts.Add(n2);
                n2 += number;
                number += 2;
            }
        }

        private static List<long> sqrts;
        static void SqrtRec(long a, long c)
        {
            if (a > c)
                return;
            var sqrtA = Math.Sqrt(a);
            if (sqrtA % 1 == 0)
            {
                sqrts.Add(a);
                SqrtRec(a + 1, c);
            }
        }

        static double SqrtSum(long a, long b)
        {
            var sqrtA = Math.Sqrt(a);
            var sqrtB = Math.Sqrt(b);
            if (sqrtA % 1 != 0 || sqrtB % 1 != 0)
                return -1;
            return sqrtA + sqrtB;
        }

        static void Cover()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var list = new List<Job>();
            for (int i = 0; i < n; i++)
            {
                var se = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                list.Add(new Job(Math.Min(se[0], se[1]), Math.Max(se[1], se[0]), 1));

            }
            list = list.OrderBy(j => j.start).ToList();
            list = list.OrderBy(j => j.finish).ToList();
            var last = list[0].finish;
            var res = new List<Tuple<int, int>> { Tuple.Create(list[0].start, list[0].finish) };
            for (int i = 1; i < n; i++)
            {
                if (list[i].start < last)
                    continue;
                last = list[i].finish;
                res.Add(Tuple.Create(list[i].start, list[i].finish));
            }
            Console.WriteLine(res.Count);
            foreach (var (item1, item2) in res)
            {
                Console.WriteLine(item1 + " " + item2);
            }

        }
        public static void LittleChu()
        {
            var primes = MakeSieve(65536);
            //var dict = new Dictionary<long, long>();
            //var arr = new long[primes.Length];
            //arr[0] = 1;
            //arr[1] = 2;
            //dict.Add(primes[1], 2);
            //for (int i = 2; i < arr.Length; i++)
            //{
            //    arr[i] = arr[i - 1] + arr[i - 2];
            //    dict.Add(primes[i], arr[i]);
            //}
            var t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }
        }

        private static KeyValuePair<long, long> Count;
        public static void Solve(int n, int index, List<int> comb, HashSet<int> visitedDays)
        {
            if (visitedDays.Contains(index))
            {
                if (Count.Key < comb.Count)
                    Count = new KeyValuePair<long, long>(comb.Count, comb.First());
                return;
            }


        }

        static void diagonalOrder(int[,] matrix, int row, int col)
        {
            for (int line = 1; line <= (row + col - 1); line++)
            {

                int start_col = Math.Max(0, line - row);
                int count = Math.Min(line, Math.Min(
                    (col - start_col), row));
                for (int j = 0; j < count; j++)
                    Console.Write(matrix[Math.Min(row, line)
                                         - j - 1, start_col + j] + " ");
                Console.WriteLine();
            }
        }

        public static void UncleScroogesGold()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var arr = new BigInteger[n + 1];
            arr[0] = 4; // 3
            arr[1] = 7; // 4
            for (int i = 2; i <= n; i++)
            {
                arr[i] = BigInteger.Add(arr[i - 1], arr[i - 2]);
                if (i > 5)
                {
                    arr[i - 5] = 0;
                }
            }

            Console.WriteLine(arr[n - 3]);
        }

        public static void Nikifor3()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var m = Console.ReadLine();
                Console.WriteLine(DivideSeven(m));
            }
        }

        public static BigInteger DivideSeven(string m)
        {
            var nums = new[]
            {
                1234,
                1243,
                1324,
                1342,
                1423,
                1432,

                2134,
                2143,
                2341,
                2314,
                2413,
                2431,

                3124,
                3142,
                3214,
                3241,
                3421,
                3412,

                4123,
                4132,
                4213,
                4231,
                4312,
                4321
            };
            var num = "";
            var zeros = "";
            var temp = "";
            foreach (var c in m)
            {
                if (c == '1' && !temp.Contains("1"))
                    temp += "1";
                else if (c == '2' && !temp.Contains("2"))
                    temp += "2";
                else if (c == '3' && !temp.Contains("3"))
                    temp += "3";
                else if (c == '4' && !temp.Contains("4"))
                    temp += "4";
                else if (c == '0')
                    zeros += c;
                else
                    num += c;
            }

            if (num == "" && zeros == "")
            {
                return 4123;
            }

            if (num == "" && zeros != "")
            {
                foreach (var s in nums)
                {
                    if (s * Math.Pow(10, zeros.Length) % 7 == 0)
                    {
                        return new BigInteger(s * Math.Pow(10, zeros.Length));
                    }
                }
            }
            var number = Int64.Parse(num + zeros);
            var res = new BigInteger(0);
            foreach (var s in nums)
            {
                res = BigInteger.Add(BigInteger.Multiply(number, 10000), s);
                BigInteger r = BigInteger.One;
                BigInteger.DivRem(res, new BigInteger(7), out r);
                if (r == 0)
                    break;
            }

            return res;
        }

        static void InterestingNumber()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            if (n == 1)
                Console.WriteLine(14);
            else if (n == 2)
                Console.WriteLine(155);
            else
            {
                Console.Write(1575);
                for (int i = 0; i < n - 3; i++)
                    Console.Write("0");
            }
        }
        static void DMaze()
        {
            var nx = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nx[0];
            var x = nx[1];
            var coordinates = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var max = -1000;
            var min = 1000;
            foreach (var c in coordinates)
            {
                if (c < 0)
                    max = Math.Max(c, max);
                if (c > 0)
                    min = Math.Min(c, min);
            }


            if (max < x && min > x)
            {
                if (x > 0)
                    Console.WriteLine(x + " " + (x - 2 * max));
                else
                    Console.WriteLine(2 * min - x + " " + (-x));
            }
            else
                Console.WriteLine("Impossible");
        }

        static void Milliard()
        {
            var s = Convert.ToInt32(Console.ReadLine());
            var VF = new long[s + 1];
            VF[0] = 1;
            if (s == 1)
                Console.WriteLine(10);
            else
            {
                for (int i = 1; i < 10; i++)
                    for (int j = s; j >= 0; j--)
                        for (int d = 1; d <= Math.Min(9, j); d++)
                            VF[j] += VF[j - d];
                Console.WriteLine(VF[s]);
            }
        }

        static void BaldSpotRevisited()
        {
            var t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var ab = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var a = ab[0];
                var b = ab[1];

                Console.WriteLine(Exp(a, b));
            }
        }
        static int Exp(int a, int b)
        {
            if (b % a != 0)
                return 0;
            int res = 0;
            int i = 2;
            int n = b / a;
            while (true)
            {
                if (n == 1)
                    break;
                if (i * i > n)
                {
                    res++;
                    break;
                }
                if (n % i == 0)
                    while (n % i == 0)
                    {
                        n /= i;
                        res++;
                    }
                i++;
            }

            return ++res;
        }
        static void RSAAttack()
        {
            var k = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < k; i++)
            {
                var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var e = input[0];
                var n = input[1];
                var c = input[2];
                var p = 0;
                var q = 0;
                for (int j = 2; j * j <= n; j++)
                {
                    if (n % j == 0 && IsPrime(n % j) && IsPrime(j))
                    {
                        p = j;
                        q = n / j;
                        break;
                    }
                }

                int x = 0, y = 0;
                int pq = (p - 1) * (q - 1);
                ExtendedGcd(e, pq, ref x, ref y);
                if (x < 0)
                    x += pq;

                int m = Pow(c, x, n);
                Console.WriteLine(m);
            }
        }
        static void ExtendedGcd(int a, int b, ref int x, ref int y) // a*x == 1,
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return;
            }

            int x1 = 1, y1 = 1;
            ExtendedGcd(b, a % b, ref x1, ref y1);

            y = x1 - (a / b) * y1;
            x = y1;
        }
        static BigInteger Ts(BigInteger n, BigInteger p)
        {
            if (BigInteger.ModPow(n, (p - 1) / 2, p) != 1)
            {
                return -1;
            }

            BigInteger q = p - 1;
            BigInteger ss = 0;
            while ((q & 1) == 0)
            {
                ss += 1;
                q >>= 1;
            }

            if (ss == 1)
            {
                BigInteger r1 = BigInteger.ModPow(n, (p + 1) / 4, p);
                return r1;
            }

            BigInteger z = 2;
            while (BigInteger.ModPow(z, (p - 1) / 2, p) != p - 1)
            {
                z += 1;
            }
            BigInteger c = BigInteger.ModPow(z, q, p);
            BigInteger r = BigInteger.ModPow(n, (q + 1) / 2, p);
            BigInteger t = BigInteger.ModPow(n, q, p);
            BigInteger m = ss;

            while (true)
            {
                if (t == 1)
                {
                    return r;
                }
                BigInteger i = 0;
                BigInteger zz = t;
                while (zz != 1 && i < (m - 1))
                {
                    zz = zz * zz % p;
                    i += 1;
                }
                BigInteger b = c;
                BigInteger e = m - i - 1;
                while (e > 0)
                {
                    b = b * b % p;
                    e -= 1;
                }
                r = r * b % p;
                c = b * b % p;
                t = t * c % p;
                m = i;
            }
        }
        static void SquareRoot()
        {
            var k = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= k; i++)
            {
                var an = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var a = an[0] % an[1];
                var n = an[1];
                if (a == 1)
                {
                    Console.WriteLine(1 + " " + (n - 1));
                }
                else
                {
                    //var x = STonelli(a, n);
                    var x = Ts(new BigInteger(a), new BigInteger(n));
                    var y1 = x;
                    var y2 = n - x;
                    if (x == -1)
                        Console.WriteLine("No root");
                    else if (y1 < y2)
                        Console.WriteLine(y1 + " " + y2);
                    else if (y1 == y2)
                        Console.WriteLine(y1);
                    else
                        Console.WriteLine(y2 + " " + y1);

                }
            }

        }
        static int STonelli(int n, int p)
        {
            if (Pow(n, (p - 1) / 2, p) != 1)
                return -1;

            int s, e;
            s = ConvertXto2e(p - 1);
            e = z;

            if (e == 1)
                return Pow(n, (p + 1) / 4, p);

            // q ^ ((p - 1) / 2) (mod p) = p - 1  
            int q;
            for (q = 2; ; q++)
            {
                if (Pow(q, (p - 1) / 2, p) == (p - 1))
                    break;
            }

            int x = Pow(n, (s + 1) / 2, p);
            int b = Pow(n, s, p);
            int g = Pow(q, s, p);
            int r = e;

            while (true)
            {
                int m;
                for (m = 0; m < r; m++)
                {
                    if (Order(p, b) == -1)
                        return -1;

                    if (Order(p, b) == Math.Pow(2, m))
                        break;
                }
                if (m == 0)
                    return x;

                x = (x * Pow(g, (int)Math.Pow(2, r - m - 1), p)) % p;
                g = Pow(g, (int)Math.Pow(2, r - m), p);
                b = (b * g) % p;

                if (b == 1)
                    return x;
                r = m;
            }
        }

        static int z = 0;
        // f(x) = 2^e
        static int ConvertXto2e(int x)
        {
            z = 0;
            while (x % 2 == 0)
            {
                x /= 2;
                z++;
            }
            return x;
        }
        // b^k = 1 (modp)
        static int Order(int p, int b)
        {
            if (GCD(p, b) != 1)
            {
                return -1;
            }

            int k = 3;
            while (true)
            {
                if (Pow(b, k, p) == 1)
                    return k;
                k++;
            }
        }
        static int Pow(int b, int exp, int mod)
        {
            int res = 1;
            b %= mod;

            while (exp > 0)
            {
                if (exp % 2 == 1)
                    res = (res * b) % mod;
                exp >>= 1;
                b = (b * b) % mod;
            }
            return res;
        }
        static void Idempotents()
        {
            var k = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < k; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                int p = 2, q = 2;
                for (int j = 2; j * j <= n; j++)
                {
                    if (n % j == 0 && IsPrime(j) && IsPrime(n / j))
                    {
                        p = j;
                        q = n / j;
                        break;
                    }
                }
                int x = 0, y = 0;
                ExtendedGCD(p, q, ref x, ref y);
                var pIdempontent = x < 0 ? n + p * x : p * x;
                ExtendedGCD(q, p, ref x, ref y);
                var qIdempontent = x < 0 ? n + q * x : q * x;

                if (pIdempontent > qIdempontent)
                    Console.WriteLine("0 1 {0} {1}", qIdempontent, pIdempontent);
                else
                    Console.WriteLine("0 1 {0} {1}", pIdempontent, qIdempontent);
            }

        }
        static int ExtendedGCD(int a, int b, ref int x, ref int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int x1 = 1, y1 = 1;
            var gcd = ExtendedGCD(b % a, a, ref x1, ref y1);

            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
        static void RichnessWords()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("{0} : {1}", i, Words(i, n));
            }
        }
        static string Words(int i, int n)
        {
            var temp = "bca";
            var res = new StringBuilder();
            var c = 0;
            if (i > 2)
            {
                for (int j = 0; j < Math.Min(i - 2, n); j++)
                    res.Append('a');
                for (int j = i - 2; j < n; j++)
                    res.Append(temp[c++ % 3]);
                return res.ToString();
            }
            else if (n == i && i < 3)
            {
                for (int j = 0; j < i; j++)
                    res.Append('a');
                return res.ToString();
            }

            return "NO";
        }
        static void FreedomChoice()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var s = Console.In.ReadToEnd().Split('\n');
            var s1 = s[0];
            var s2 = s[1];

            var letters = new int[n + 1, n + 1];
            int len = 0, end = n;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        letters[i, j] = letters[i - 1, j - 1] + 1;
                        if (len < letters[i, j])
                        {
                            len = letters[i, j];
                            end = i;
                        }
                    }
                }
            }


            Console.WriteLine(s1.Substring(end - len, len));
        }
        static void GetTogetheratDens()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var liters = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var sum = liters.Sum();
            double average = (double)sum / (n + 1);
            double drink = 0;

            for (int i = 0; i < n; i++)
                if (liters[i] > average)
                    drink += liters[i] - average;

            for (int i = 0; i < n; i++)
            {
                if (liters[i] > average)
                    Console.Write((int)(100.0 * (liters[i] - average) / drink + 0.0001) + " ");
                else
                    Console.Write("0 ");
            }
        }
        static void KeySubstrings()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var commands = new List<string>();
            for (int i = 0; i < n; i++)
            {
                commands.Add(Console.ReadLine());
            }


        }
        static void JacksLastWord()
        {
            var first = Console.ReadLine();
            var second = Console.ReadLine();
            var size = first.Length;
            first += "*" + second;
            var isFindPattern = false;
            var patterns = KMPForPattern(first);
            for (int i = size + 1; i < patterns.Length; i++)
            {
                if (patterns[i] == 0)
                    isFindPattern = true;
            }
            if (isFindPattern)
            {
                Console.WriteLine("Yes");
                return;
            }
            else
            {
                Console.WriteLine("No");
                var indexes = new List<int>();
                for (int i = patterns.Length - 1; i > size; i -= patterns[i])
                    indexes.Add(patterns[i]);

                for (int i = indexes.Count - 1; i >= 0; i--)
                {
                    Console.Write(first.Substring(0, indexes[i]) + " ");
                }
            }
        }
        static int[] KMPForPattern(string s)
        {
            var patterns = new int[s.Length];
            for (int i = 1; i < s.Length; i++)
            {
                var j = patterns[i - 1];
                while (j > 0 && s[i] != s[j])
                    j = patterns[j - 1];
                patterns[i] = j + (s[i] == s[j] ? 1 : 0);
            }
            return patterns;
        }
        static void StringTale()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var s = Console.ReadLine();
            var t = Console.ReadLine();

            if (s == t)
            {
                Console.WriteLine(0);
                return;
            }
            int dif = KMP(t + t, s);
            if (dif == -1)
                Console.WriteLine(-1);
            else
                Console.WriteLine(dif);
        }
        static int KMP(string s, string pattern)
        {
            // get pattern's array
            var patterns = new int[pattern.Length];
            for (int i = 1; i < pattern.Length; i++)
            {
                var j = 0;
                if (pattern[j] == pattern[i])
                {
                    patterns[i]++;
                    while (i + 1 < pattern.Length && pattern[++j] == pattern[++i])
                    {
                        patterns[i] = patterns[i - 1] + 1;
                    }
                }
            }

            // get substring in given string
            int m = 0;
            int n = 0;

            while (m < s.Length)
            {
                if (s[m] == pattern[n])
                {
                    m++;
                    n++;
                }

                if (n == pattern.Length)
                {
                    return m - n;
                }
                else if (m < s.Length && s[m] != pattern[n])
                {
                    if (n == 0)
                        m++;
                    else
                        n = patterns[n - 1];
                }

            }

            return -1;
        }
        static int Digit(int n)
        {
            if (n >= 1 && n <= 9)
                return 1;
            return Digit(n / 10) + 1;
        }
        static void PairsOfIntegers()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var b = Digit(n);
            var numbers = new List<int>();
            for (int i = 0; i < n; i++)
            {

            }
        }
        static void CoatTransportation()
        {
            var nr = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var arr = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nr[0];
            var r = nr[1];
            int j = 0;
            var queue = new Queue<KeyValuePair<int, int>>();
            var ans = new List<List<int>> { new List<int>() };
            queue.Enqueue(new KeyValuePair<int, int>(-1, 0));
            for (int i = 1; i <= n; i++)
            {
                if (arr[i - 1] > queue.Peek().Key)
                {
                    ans[queue.Peek().Value].Add(i);
                    queue.Enqueue(new KeyValuePair<int, int>(arr[i - 1] + r, queue.Peek().Value));
                    queue.Dequeue();
                }
                else
                {
                    ans.Add(new List<int> { i });
                    queue.Enqueue(new KeyValuePair<int, int>(arr[i - 1] + r, ++j));
                }
            }

            ans = ans.OrderBy(l => l.Count).ToList();
            Console.WriteLine(ans.Count);
            for (int i = 0; i < ans.Count; i++)
            {
                Console.Write(ans[i].Count + " ");
                for (int l = 0; l < ans[i].Count; l++)
                {
                    Console.Write(ans[i][l] + " ");
                }
                Console.WriteLine();
            }

        }
        static void FilmRating()
        {
            var input = Console.ReadLine().Split(' ');
            var x = Convert.ToDouble(input[0]);
            var y = Convert.ToDouble(input[1]);
            var n = Convert.ToDouble(input[2]);

            if (x <= y + 0.0001)
            {
                Console.WriteLine(0);
                return;
            }
            var temp = x;
            long ans = 0;
            if (x < 10.0)
            {
                x = (int)((x + 0.05) * n);
                while (x / n >= temp + 0.049999999999)
                    x--;
            }
            else
                x *= n;

            if (((ans + 60000000 + x) / (ans + 60000000 + n)) >= y + 0.0499999999999)
                ans += 60000000;

            while (((ans + x) / (ans + n)) >= y + 0.0499999999999)
                ans++;

            Console.WriteLine(ans);
        }
        static void Ivanushka()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var count = new List<int>();

            while (n > 1)
            {
                count.Add(n >> 1);
                n -= (n >> 1);
            }

            Console.WriteLine(count.Count);

            for (int i = 0; i < count.Count; i++)
            {
                Console.Write(count[i] + " ");
            }

        }
        static void FairyTale()
        {
            long k = Convert.ToInt64(Console.ReadLine());
            var s = Console.ReadLine();
            long n = 0;
            var limit = 1;

            for (int j = 0; j < k; j++)
            {
                if (!Char.IsDigit(s[j]))
                    j--;
                else
                    n = n * 10 + (s[j] - '0');
            }
            if (n == 0)
            {
                n++;
                k++;
            }
            for (limit = 1; k < 12; k++, limit *= 10)
                n *= 10;

            for (int j = 0; j < limit; j++)
            {
                if (IsPrime(n + j))
                {
                    n += j;
                    break;
                }
            }
            Console.WriteLine($"{n:000000000000}");
        }
        static void FibonacciSequence()
        {
            var F = new BigInteger[2001];
            F[0] = BigInteger.Zero;
            F[1] = BigInteger.One;
            for (int k = 2; k <= 2000; k++)
            {
                F[k] = BigInteger.Add(F[k - 1], F[k - 2]);
            }

            var input = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            var i = input[0];
            BigInteger fi = new BigInteger(input[1]);
            var j = input[2];
            var fj = new BigInteger(input[3]);
            var n = input[4];
            var fn = new BigInteger();

            if (i == n)
            {
                Console.WriteLine(fi);
                return;
            }
            if (j == n)
            {
                Console.WriteLine(fj);
                return;
            }
            if (j < i)
            {
                var te = i;
                i = j;
                j = te;

                var t = fi;
                fi = fj;
                fj = t;
            }

            var mul = BigInteger.Multiply(F[j - i - 1], fi);
            var sub = BigInteger.Subtract(fj, mul);
            BigInteger FI = BigInteger.Divide(sub, F[j - i]);

            if (n < i)
            {
                while (--i != n)
                {
                    fn = BigInteger.Subtract(FI, fi);
                    FI = fi;
                    fi = fn;
                }
            }
            else
            {
                while (++i != n)
                {
                    fn = BigInteger.Add(FI, fi);
                    fi = FI;
                    FI = fn;
                }
            }

            Console.WriteLine(fn);
        }
        static long Pollard_Rho(long n)
        {
            if (n % 2 == 0)
                return 2;

            long x = 2;
            long y = x;
            long factor = 1;

            while (factor == 1)
            {
                x = ((x * x) % n - 1) % n;
                y = ((y * y) % n + 1) % n;
                y = ((y * y) % n + 1) % n;
                factor = GCD(Math.Abs(x - y), n);
            }
            return factor;
        }
        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        static bool IsPrime(long n)
        {
            if (n == 1)
                return false;
            for (long i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }
        static void MichaelAndCryptography()
        {
            var n = Convert.ToInt64(Console.ReadLine());
            var sum = 0;
            for (int i = 2; i < 1953126 && n > 1; i++)
            {
                while (n % i == 0)
                {
                    sum++;
                    n /= i;
                    if (sum > 20)
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }
                if (Math.Pow(i + 1, 20 - sum) > n)
                {
                    Console.WriteLine("No");
                    return;
                }
            }
            if (n > 1)
            {
                if (sum == 19 && IsPrime(n))
                    Console.WriteLine("Yes");
                else
                    Console.WriteLine("No");
            }
            else if (sum == 20)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }
        static void MersennePrimes()
        {
            int[] primes = { 2, 3, 5, 7, 13, 17, 19, 31, 61, 89, 107, 127, 521,
                607, 1279, 2203, 2281, 3217, 4253, 4423, 9689, 9941, 11213, 19937, 21701,
                23209, 44497, 86243, 110503, 132049, 216091, 756839, 859433, 1257787,
                1398269, 2976221, 3021377, 6972593, 13466917, 20996011, 24036583,
                25964951, 30402457, 32582657, 37156667, 42643801, 43112609, 57885161 };
            var t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(primes[n - 1]);
            }
        }
        static void Cryptography()
        {
            var numbers = MakeSieve(170000);
            var k = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < k; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(numbers[n - 1]);
            }
        }
        static void DontAskWomanAboutHerAge()
        {
            int maxDig = 0;
            int res = 0;
            var number = new char[1000001];
            var str = Console.ReadLine();
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] <= '9')
                    number[i] = (char)(str[i] - '0');
                else
                    number[i] = (char)(str[i] - 'A' + 10);
                maxDig = Math.Max(maxDig, number[i]);
            }

            if (maxDig == 0)
                res = 2;
            else
            {
                for (int k = maxDig + 1; k <= 36; k++)
                {
                    var rem = 0;
                    var div = k - 1;
                    var exp = 1;
                    for (int i = str.Length - 1; i >= 0; --i)
                    {
                        rem = (rem + number[i] * exp) % div;
                        exp = (exp * k) % div;
                    }
                    if (rem == 0)
                    {
                        res = k;
                        break;
                    }
                }
            }

            if (res == 0)
                Console.WriteLine("No solution.");
            else
                Console.WriteLine(res);
        }
        static void Stripies()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var weights = new List<int>();
            for (int i = 0; i < n; i++)
            {
                weights.Add(Convert.ToInt32(Console.ReadLine()));
            }
            weights = weights.OrderByDescending(x => x).ToList();
            double min = weights[0];
            for (int i = 1; i < weights.Count; i++)
            {
                min = 2 * Math.Sqrt(weights[i] * min);
            }

            Console.WriteLine($"{min:0.00}");
        }

        static long HashString(string str)
        {
            const int p = 301;
            const int m = 1000000009;
            long hash = 0;
            long prime_pow = 1;
            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash + (str[i] - 'a' + 1) * prime_pow) % m;
                prime_pow = (prime_pow * p) % m;
            }
            return hash;
        }
        static void SoldOut()
        {
            var nk = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nk[0];
            var k = nk[1];
            if (Math.Max(k - 1, n - k) >= 2)
                Console.WriteLine(Math.Max(k - 1, n - k) - 2);
            else
                Console.WriteLine(0);
        }
        static void HammingCode()
        {
            var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            bool[] petal = new bool[3];
            if ((input[0] + input[1] + input[3]) % 2 == input[6])
                petal[0] = true;
            if ((input[1] + input[2] + input[3]) % 2 == input[4])
                petal[1] = true;
            if ((input[0] + input[2] + input[3]) % 2 == input[5])
                petal[2] = true;
            if (petal.All(x => x == true))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    Console.Write(input[i] + " ");
                }
                return;
            }
            else if (petal.All(x => x == false))
            {
                input[3] = input[3] == 1 ? 0 : 1;
                for (int i = 0; i < input.Length; i++)
                {
                    Console.Write(input[i] + " ");
                }
                return;
            }
            else
            {
                if (!petal[0] && !petal[1])
                {
                    input[1] = input[1] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }
                if (!petal[0] && !petal[2])
                {
                    input[0] = input[0] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }
                if (!petal[1] && !petal[2])
                {
                    input[2] = input[2] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }
                if (!petal[0])
                {
                    input[6] = input[6] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }
                if (!petal[1])
                {
                    input[4] = input[4] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }
                if (!petal[2])
                {
                    input[5] = input[5] == 1 ? 0 : 1;
                    for (int i = 0; i < input.Length; i++)
                    {
                        Console.Write(input[i] + " ");
                    }
                    return;
                }

            }
        }
        static void Maximum()
        {
            int[] dp = new int[100000];
            int[] ans = new int[100000];
            dp[0] = ans[0] = 0;
            dp[1] = ans[1] = 1;
            for (int i = 2; i < 100000; i++)
            {
                if (i % 2 != 0)
                {
                    dp[i] = dp[i / 2] + dp[i / 2 + 1];
                    ans[i] = Math.Max(dp[i], ans[i - 1]);
                }
                else
                {
                    dp[i] = dp[i / 2];
                    ans[i] = ans[i - 1];
                }
            }
            while (true)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                if (n == 0)
                    break;
                Console.WriteLine(ans[n]);
            }
        }
        static void CityBlocks()
        {
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];

            if (n == m)
                Console.WriteLine(n - 1);
            else
            {
                --n;
                --m;
                Console.WriteLine(n + m - GCD(n, m));
            }
        }
        static void SquareCountry()
        {
            var squares = new int[60001];
            for (int i = 1; i < squares.Length; i++)
            {
                squares[i] = i;
                for (int j = 1; j * j <= i; j++)
                {
                    squares[i] = Math.Min(squares[i], squares[i - j * j] + 1);
                }
            }
            var n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(squares[n]);
        }
        static List<int> GetSquares(int n)
        {
            var ans = new List<int>();
            for (int i = 4; i <= n; i++)
            {
                if (Math.Sqrt(i) % 1 == 0)
                    ans.Add(i);
            }
            return ans;
        }
        static void MnemonicsandPalindromes()
        {
            var word = Console.ReadLine();
            if (IsPolindrome(word))
            {
                Console.WriteLine(1);
                Console.WriteLine(word);
                return;
            }
            var palindromes = new bool[word.Length + 1, word.Length + 1];
            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < word.Length - i; j++)
                {
                    if (i == 0)
                        palindromes[j, j] = true;
                    else if (i == 1)
                        palindromes[j, j + 1] = (word[j] == word[j + 1]);
                    else
                        palindromes[j, j + i] = (palindromes[j + 1, j + i - 1] && word[j] == word[j + i]);

                }
            }

            var lengths = new int[word.Length + 1];
            for (int i = 0; i < word.Length; i++)
                lengths[i] = 4000;
            var res = new int[word.Length + 1];
            for (int i = word.Length - 1; i >= 0; i--)
            {
                for (int j = i + 1; j <= word.Length; j++)
                {
                    if (palindromes[i, j - 1])
                    {
                        if (lengths[i] > lengths[j] + 1)
                        {
                            lengths[i] = lengths[j] + 1;
                            res[i] = j;
                        }
                    }
                }
            }

            Console.WriteLine(lengths[0]);
            for (int i = 0; i < word.Length; i = res[i])
            {
                for (int j = i; j < res[i]; j++)
                    Console.Write(word[j]);
                Console.Write(" ");
            }
        }
        static bool IsPolindrome(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != str[str.Length - i - 1])
                    return false;
            }
            return true;
        }
        static void Relations()
        {
            var ans = new int[11];
            var dp = new int[11, 11];
            dp[1, 1] = 1;
            for (int i = 2; i <= 10; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    dp[i, j] = (dp[i - 1, j] + dp[i - 1, j - 1]) * j;
                    ans[i] += dp[i, j];
                }
            }
            while (true)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                if (n == -1)
                    break;
                Console.WriteLine(ans[n]);
            }
        }

        static void Salary()
        {
            var salary = Console.ReadLine().ToArray();

            int j = salary.Length - 1;
            for (int i = 0; i <= salary.Length / 2; i++, j--)
            {
                if (salary[i] >= salary[j])
                    salary[j] = salary[i];

                else
                {
                    int l = j - 1;
                    while (salary[l] == '9')
                        salary[l--] = '0';
                    salary[l]++;
                    salary[j] = salary[i];
                }
            }
            Console.WriteLine(salary);
        }
        static void Dwarf()
        {
            var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var dwarfspot = (double)input[0];
            var dwarfslife = input[1];
            var pay = input[2];
            var ans = 0;
            double percent = (double)(100 - pay) / 100;
            while (dwarfspot > dwarfslife)
            {
                dwarfspot = dwarfspot * percent;
                ans++;
            }
            Console.WriteLine(ans);
        }
        static void CipherGrille()
        {
            var grille = new char[4, 4];
            var password = new char[4, 4];
            for (int i = 0; i < 4; i++)
            {
                var temp = Console.ReadLine();
                for (int j = 0; j < 4; j++)
                {
                    grille[i, j] = temp[j];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                var temp = Console.ReadLine();
                for (int j = 0; j < 4; j++)
                {
                    password[i, j] = temp[j];
                }
            }

            var ans = "";
            var l = 0;
            var k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (grille[i, j] == 'X')
                        ans += password[i, j];
                }
            }

            k = 0;
            for (int i = 0; i < 4; i++)
            {
                k = 0;
                for (int j = 3; j >= 0; j--)
                {
                    if (grille[j, i] == 'X')
                        ans += password[i, k];
                    k++;
                }
            }

            k = 0;
            for (int i = 3; i >= 0; i--)
            {
                l = 0;
                for (int j = 3; j >= 0; j--)
                {
                    if (grille[i, j] == 'X')
                        ans += password[k, l];
                    l++;
                }
                k++;
            }

            l = 0;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (grille[j, i] == 'X')
                        ans += password[l, j];
                }
                l++;
            }

            Console.WriteLine(ans);
        }
        static void NegotiationswithParthians()
        {
            BigInteger n = Convert.ToInt64(Console.ReadLine());
            BigInteger res = 1;
            for (long i = 1; i * i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (n / i % i == 0)
                        res = BigInteger.Max(res, i * i);
                    BigInteger m = n / i;
                    BigInteger mid = 0;
                    BigInteger l = 1;
                    BigInteger h = 1000000000;
                    while (l < h)
                    {
                        mid = (l + h) / 2;
                        if (mid * mid < m)
                            l = mid + 1;
                        else
                            h = mid;
                    }
                    if (l * l == m)
                        res = BigInteger.Max(res, m);
                }
            }
            Console.WriteLine(res);
        }
        static void Bookmakers()
        {
            var k = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();
            var ans = Math.Round(1000 / ((k[0] / k[1]) + (k[0] / k[2]) + 1) * k[0]);
            Console.WriteLine(ans);
        }
        static void SymbolicSequence()
        {
            var random = new Random();
            var j = 1;
            while (j <= 1000000)
            {
                Console.Write((char)('a' + (char)random.Next(26)));
                j++;
            }
        }
        static void Anindilyakwa()
        {
            var x = Console.ReadLine().Split(' ').Select(BigInteger.Parse).ToList();

            BigInteger ans = BigInteger.One;
            while (true)
            {
                BigInteger dif = FindDif(x);
                if (dif == 0)
                    break;
                ans++;
                x.Add(dif);
            }
            Console.WriteLine(ans);
        }
        static BigInteger FindDif(List<BigInteger> x)
        {
            BigInteger res = BigInteger.Abs(x[0] - x[1]);
            for (int i = 0; i < x.Count - 1; i++)
            {
                for (int j = i + 1; j < x.Count; j++)
                {
                    res = BigInteger.Min(res, BigInteger.Abs(x[i] - x[j]));
                }
            }
            return res;
        }
        static void ReverseOrder()
        {
            var text = Console.In.ReadToEnd().ToArray();
            var stack = new Stack<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                    stack.Push(text[i]);
                else
                {
                    while (stack.Count > 0)
                    {
                        Console.Write(stack.Pop());
                    }
                    Console.Write(text[i]);
                }
            }

            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
            }
        }
        static void FalseMirrors()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var nums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var dp = new int[n + 1, n + 1];
            int min = Int32.MaxValue;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < i + 3; j++)
                {

                }
            }
        }

        static void Hyperjump()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            int sum = 0;
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                var a = Convert.ToInt32(Console.ReadLine());
                if (sum + a <= 0)
                    sum = 0;
                else
                {
                    sum += a;
                    if (sum > max)
                        max = sum;
                }
            }
            Console.WriteLine(max);
        }
        static void Plato()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var d = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();

            d.Sort();
            long sum = d.Sum();
            long ans = 0;
            for (int i = 0; i < d.Count; i++)
            {
                ans += (sum + sum - d[i]) * d[i];
                sum -= d[i];
            }
            Console.WriteLine(ans);
        }
        static void StrangeDialog()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var s = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                s.Append(Console.In.ReadToEnd());
                int j = 0;
                while (j < s.Length)
                {
                    if (j + 5 < s.Length && s[j] == 'o' && s[j + 1] == 'u' && s[j + 2] == 't' && s[j + 3] == 'p' && s[j + 4] == 'u' && s[j + 5] == 't')
                    {
                        j += 6;
                        continue;
                    }
                    if (j + 4 < s.Length && (s[j] == 'p' && s[j + 1] == 'u' && s[j + 2] == 't' && s[j + 3] == 'o' && s[j + 4] == 'n') ||
                        (s[j] == 'i' && s[j + 1] == 'n' && s[j + 2] == 'p' && s[j + 3] == 'u' && s[j + 4] == 't'))
                    {
                        j += 5;
                        continue;
                    }
                    if (j + 3 < s.Length && (s[j] == 'o' && s[j + 1] == 'u' && s[j + 2] == 't') ||
                        (s[j] == 'o' && s[j + 1] == 'n' && s[j + 2] == 'e'))
                    {
                        j += 3;
                        continue;
                    }
                    if (j + 2 < s.Length && s[j] == 'i' && s[j + 1] == 'n')
                    {
                        j += 2;
                        continue;
                    }
                    break;
                }
                Console.WriteLine(j == s.Length ? "YES" : "NO");
                s = s.Clear();
            }

        }
        static void Cards()
        {
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            if (m > n)
            {
                Console.WriteLine("NO");
                return;
            }
            var cards = Console.In.ReadToEnd().Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

            cards.Sort();

            var count = new int[1010];
            count[0] = 1;
            count[n] = 1;
            for (int i = 1; i <= n - 1; i++)
                count[i] = 2;

            for (int i = 0; i < m; i++)
            {
                if (cards[i] == 0)
                {
                    if (count[0] <= 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    else
                    {
                        --count[0];
                        --count[1];
                    }
                }

                else if (cards[i] == n)
                {
                    if (count[n - 1] <= 0 || count[n] <= 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    else
                    {
                        --count[n];
                        --count[n - 1];
                    }
                }

                else if (count[cards[i]] > 0 && (count[cards[i] + 1] > 0 || count[cards[i] - 1] > 0))
                {
                    --count[cards[i]];
                    if (count[cards[i] - 1] > 0)
                        --count[cards[i] - 1];
                    else if (count[cards[i] + 1] > 0)
                        --count[cards[i] + 1];
                }

                else
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
            Console.WriteLine("YES");
        }
        static void JediRiddle3()
        {
            var nxy = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            var n = nxy[0];
            var x = nxy[1];
            var y = nxy[2];

            var k = Console.ReadLine().Split(' ').Select(Int64.Parse).Reverse().ToArray();
            var c = Console.ReadLine().Split(' ').Select(Int64.Parse).Reverse().ToArray();

            var matrix = new long[n, n];
            for (int i = 1; i < n; i++)
            {
                matrix[i, i - 1] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                matrix[0, i] = c[i];
            }
            var f = FindPow(matrix, x - n, y, n);
            long ans = 0;
            for (int i = 0; i < n; i++)
            {
                ans += f[0, i] * k[i];
                ans %= y;
            }
            Console.WriteLine(ans);
        }
        static long[,] FindPow(long[,] matrix, long a, long mod, long n)
        {
            var res = new long[n, n];
            for (int i = 0; i < n; i++)
                res[i, i] = 1;
            while (a > 0)
            {
                if ((a & 1) == 1)
                    res = MatrixMul(res, matrix, mod, n);
                matrix = MatrixMul(matrix, matrix, mod, n);
                a /= 2;
            }
            return res;
        }
        static long[,] MatrixMul(long[,] a, long[,] b, long mod, long n)
        {
            var res = new long[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    long current = 0;
                    for (int k = 0; k < n; k++)
                    {
                        current += a[i, k] * b[k, j];
                        current %= mod;
                    }
                    res[i, j] = current;
                }
            }
            return res;
        }
        static void JediRiddle2()
        {
            var an = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var a = an[0];
            var n = an[1];

            if (GCD(a, n) != 1)
                Console.WriteLine(0);
            else
            {
                int phi = GetPhi(n);
                int min = phi;
                for (int i = 2; i * i <= phi; i++)
                {
                    if (phi % i == 0)
                    {
                        if (BinPowerMod(a, i, n) == 1)
                            min = Math.Min(min, i);
                        if (BinPowerMod(a, phi / i, n) == 1)
                            min = Math.Min(min, phi / i);
                    }
                }
                Console.WriteLine(min);
            }
        }
        //φ(n) = n *(1 - 1/p1)* ... (1 - 1/pn).
        static int GetPhi(int n)
        {
            int phi = 1;
            int limit = n;
            for (int i = 2; i * i <= limit; i++)
            {
                if (n % i == 0)
                {
                    var current = 1;
                    while (n % i == 0)
                    {
                        n /= i;
                        current *= i;
                    }
                    phi *= current - current / i;
                }
            }
            if (n > 1)
                phi *= (n - 1);
            return phi;
        }
        static long BinPowerMod(long x, long y, long mod)
        {
            long res = 1;
            x %= mod;

            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = (res * x) % mod;
                y >>= 1;
                x = (x * x) % mod;
            }
            return res;
        }
        static void JediRiddle()
        {
            var abc = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var a = abc[0];
            var b = abc[1];
            var c = abc[2];

            Console.WriteLine(new BigInteger(Math.Pow(2, (c - 1) / a)));
            Console.WriteLine(new BigInteger(Math.Pow(2, (c - 1) / b)));
            Console.WriteLine(2);
        }
        static void SashaGrandMaster()
        {
            var n = Convert.ToInt64(Console.ReadLine());
            var xy = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            long x = xy[0];
            long y = xy[1];

            long ans = 0;
            if (x > 1)
            {
                ans++;
                if (y > 1)
                    ans++;
                if (y < n)
                    ans++;
            }

            if (x < n)
            {
                ans++;
                if (y > 1)
                    ans++;
                if (y < n)
                    ans++;
            }

            if (y > 1)
                ans++;
            if (y < n)
                ans++;
            Console.WriteLine("King: {0}", ans);

            ans = 0;
            if (x > 1)
            {
                if (y > 2)
                    ans++;
                if (y < n - 1)
                    ans++;
            }

            if (x > 2)
            {
                if (y > 1)
                    ans++;
                if (y < n)
                    ans++;
            }

            if (x < n)
            {
                if (y > 2)
                    ans++;
                if (y < n - 1)
                    ans++;
            }

            if (x < n - 1)
            {
                if (y > 1)
                    ans++;
                if (y < n)
                    ans++;
            }

            Console.WriteLine("Knight: {0}", ans);

            var bishop = Math.Min(x - 1, y - 1) + Math.Min(x - 1, n - y) + Math.Min(n - x, y - 1) + Math.Min(n - x, n - y);
            Console.WriteLine("Bishop: {0}", bishop);

            ans = 2 * (n - 1);
            Console.WriteLine("Rook: {0}", ans);

            Console.WriteLine("Queen: {0}", ans + bishop);

        }
        static void StrangeProcedure()
        {
            var xy = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            if (xy[0] < 0 || xy[1] < 0 || ((xy[0] + xy[1]) % 2 == 0))
                Console.WriteLine(xy[0] + " " + xy[1]);
            else
                Console.WriteLine(xy[1] + " " + xy[0]);
        }
        static void P(long x, long y)
        {
            int i, j;
            if (x > 0 && y > 0)
            {
                for (i = 1; i <= x + y; i++)
                {
                    y = x * x + y;
                    x = x * x + y;
                    y = (long)Math.Sqrt(x + (y / Math.Abs(y)) * (-Math.Abs(y)));
                    for (j = 1; j <= 2 * y; j++)
                        x = x - y;
                }
            }
            Console.WriteLine(x + " " + y);
        }
        static void NeoVenice()
        {
            var nts = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int n = nts[0];
            int t = nts[1];
            int s = nts[2];
            var arr = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();

            for (int i = 0; i < n; i++)
            {

                Console.WriteLine(((t + s + arr[i]) / 2).ToString("F6", new CultureInfo("en-US")));
            }

        }
        static void SinusDances()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            S(1, n);
        }
        static void S(int pos, int n)
        {
            if (pos < n)
            {
                Console.Write("(");
                S(pos + 1, n);
                Console.Write(")");
            }
            A(1, n + 1 - pos);
            Console.Write("+{0}", pos);
        }
        static void A(int pos, int n)
        {
            Console.Write("sin({0}", pos);
            if (pos < n)
            {
                if ((pos & 1) == 1)
                    Console.Write("-");
                else
                    Console.Write("+");
                A(pos + 1, n);
            }
            Console.Write(")");
        }
        static void SandroBiography()
        {
            var text = Console.ReadLine();
            var sandro = "Sandro";
            int ans = Int32.MaxValue;
            for (int i = 0; i <= text.Length - 6; i++)
            {
                int res = 0;
                var subString = text.Substring(i, 6).ToCharArray();
                for (int j = 0; j < 6; j++)
                {
                    if (subString[j] == sandro[j])
                        continue;
                    if (!((subString[j] >= 'A' && subString[j] <= 'Z' && sandro[j] >= 'A' && sandro[j] <= 'Z') ||
                          (subString[j] >= 'a' && subString[j] <= 'z' && sandro[j] >= 'a' && sandro[j] <= 'z')))
                    {
                        res += 5;
                        if (subString[j] >= 'A' && subString[j] <= 'Z')
                        {
                            subString[j] = Char.ToLower(subString[j]);
                        }
                        else
                        {
                            subString[j] = Char.ToUpper(subString[j]);
                        }
                    }
                    if (subString[j] != sandro[j])
                        res += 5;
                }

                ans = Math.Min(ans, res);
            }
            Console.WriteLine(ans);
        }
        static void ScientificConference()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var list = new List<Job>();
            for (int i = 0; i < n; i++)
            {
                var se = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                list.Add(new Job(se[0], se[1], 1));
            }

            list = list.OrderBy(j => j.finish).ToList();
            int ans = 0;
            int last = 0;
            for (int i = 0; i < n; i++)
            {
                if (list[i].start > last)
                {
                    ++ans;
                    last = list[i].finish;
                }
            }
            Console.WriteLine(ans);
        }
        static void InterestingNumbers()
        {
            long[] PrimeNumbers = MakeSieve(10000000);
            var LR = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            long l = LR[0];
            long r = LR[1];
            long ans = r - l + 1;

            for (long i = 0; PrimeNumbers[i] * PrimeNumbers[i] <= r && i < PrimeNumbers.Length; i++)
            {
                long exp = 0;
                long mul = 1;
                long j = 1;
                while (mul < r)
                {
                    while (j < PrimeNumbers.Length && exp++ < PrimeNumbers[j] - 1)
                    {
                        mul *= PrimeNumbers[i];
                        if (mul > r)
                            break;
                    }
                    if (mul >= l && mul <= r)
                        ans--;
                    else if (mul > r)
                        break;
                    j++;
                    mul *= PrimeNumbers[i];
                }
            }
            Console.WriteLine(ans);
        }
        static long[] MakeSieve(long max)
        {
            long N = 0;
            var is_prime = new Dictionary<long, bool>();
            var primes = new long[867933982];
            for (long i = 1; i <= max; i++)
                is_prime[i] = true;

            for (long i = 2; i <= max; i++)
            {
                if (is_prime[i])
                {
                    primes[N++] = i;
                    for (long j = i * i; j <= max; j += i)
                    {
                        is_prime[j] = false;
                    }
                }
            }
            return primes;
        }
        static void Cocktails()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            BigInteger res = BigInteger.Zero;
            for (int i = 2; i <= n; i++)
            {
                res += Fact(n) / Fact(n - i);
            }
            Console.WriteLine(res);
        }
        static void Devices()
        {
            var dictCount = new Dictionary<string, int>();
            var dictPrice = new Dictionary<string, int>();
            var maxCount = 0;
            for (int i = 0; i < 6; i++)
            {
                var friend = Console.ReadLine();
                var device = Console.ReadLine();
                var price = Convert.ToInt32(Console.ReadLine());

                if (dictCount.ContainsKey(device))
                    ++dictCount[device];
                else
                    dictCount.Add(device, 1);
                if (dictPrice.ContainsKey(device))
                    dictPrice[device] = Math.Min(price, dictPrice[device]);
                else
                    dictPrice.Add(device, price);
                maxCount = Math.Max(maxCount, dictCount[device]);
            }

            string ans = "";
            var min = 1000000;
            foreach (var item in dictCount)
            {
                if (item.Value == maxCount)
                {
                    var name = item.Key;
                    if (dictPrice[name] < min)
                    {
                        min = dictPrice[name];
                        ans = name;
                    }
                }
            }
            Console.WriteLine(ans);
        }
        static void Walnuts()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int hungry = 2, satisfied = 10;
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine().Split(' ');
                string outcome = temp[1];
                int number = Convert.ToInt32(temp[0]);
                if (outcome == "satisfied" && number < satisfied)
                    satisfied = number;
                else if (outcome == "hungry" && number > hungry)
                    hungry = number;
            }

            if (hungry >= satisfied)
                Console.WriteLine("Inconsistent");
            else
                Console.WriteLine(satisfied);
        }
        static void Handshakes()
        {
            var nk = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            string[] s = Console.In.ReadToEnd().Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int n = nk[0];
            int k = nk[1];
            var ans = n * (n - 1) / 2;
            Console.WriteLine(ans - k);
        }
        static void SumofDigitsoftheSumofNumbers()
        {
            var k = Convert.ToInt32(Console.ReadLine());
            BigInteger ans = new BigInteger(36);
            BigInteger current = new BigInteger(55);

            for (int i = 1; i < k; i++)
            {
                ans = BigInteger.Multiply(ans, current);
            }

            Console.WriteLine(ans);
        }
        static void Intervals()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var listSum = new List<long>();
            var currentSum = 0;
            for (int i = 0; i < n; i++)
            {
                var k = Convert.ToInt32(Console.ReadLine());
                currentSum += k;
                listSum.Add(currentSum);
            }

            var q = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < q; i++)
            {
                var ij = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                ij[0]--;
                ij[1]--;
                if (ij[0] == 0)
                    Console.WriteLine(listSum[ij[1]]);
                else
                    Console.WriteLine(listSum[ij[1]] - listSum[ij[0] - 1]);
            }
        }
        static void DummyGuy()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            if (n == 1)
                Console.WriteLine(1);
            else
            {
                Console.WriteLine((1 + 1 / (Math.Sin(Math.PI / n))).ToString("F9", new CultureInfo("en-US")));
            }
        }
        static void Boxes()
        {
            var nab = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int n = nab[0];
            int a = nab[1];
            int b = nab[2];
            Console.WriteLine(BoxBall(n, a) * BoxBall(n, b));
        }
        static BigInteger BoxBall(int box, int ball)
        {
            BigInteger res = BigInteger.Zero;
            for (int i = 0; i < ball + 1; i++)
            {
                res += NChooseK(box - 1 + i, i);
            }
            return res;
        }
        static BigInteger NChooseK(int n, int k)
        {
            return Fact(n) / (Fact(k) * Fact(n - k));
        }
        static BigInteger Fact(int n)
        {
            BigInteger res = 1;
            for (int i = 2; i <= n; i++)
                res *= i;
            return res;
        }
        static void Magic()
        {
            var lands = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            var mana = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

            long a = lands[0];
            long b = lands[1];
            long c = lands[2];

            long x = mana[0];
            long y = mana[1];
            long z = mana[2];

            if (x >= a)
            {
                if (x > a + c)
                {
                    Console.WriteLine("There are no miracles in life");
                    return;
                }
                else
                {
                    var diff = x - a;
                    a += diff;
                    c -= diff;
                }
            }
            if (y >= b)
            {
                if (y > b + c)
                {
                    Console.WriteLine("There are no miracles in life");
                    return;
                }
                else
                {
                    var diff = y - b;
                    b += diff;
                    c -= diff;
                }
            }

            if (c < z)
            {
                if (a >= x)
                {
                    z -= a - x;
                }
                if (b >= y)
                {
                    z -= b - y;
                }
            }
            if (c >= z)
            {
                Console.WriteLine("It is a kind of magic");
                return;
            }

            Console.WriteLine("There are no miracles in life");

        }
        static void Parliament()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            FindMembers(0, n - 1, arr);
        }
        static void FindMembers(int i, int j, int[] members)
        {
            if (i > j)
                return;
            if (i == j)
            {
                Console.WriteLine(members[i]);
                return;
            }
            int root = members[j];
            for (int k = i; k <= j; k++)
            {
                if (members[k] >= members[j])
                {
                    FindMembers(k, j - 1, members);
                    FindMembers(i, k - 1, members);
                    Console.WriteLine(root);
                    return;
                }
            }
        }
        static void GameOfNuts()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i] / 2;
            }
            if (sum % 2 == 0)
                Console.WriteLine("Stannis");
            else
                Console.WriteLine("Daenerys");
        }
        static void StoneGame()
        {
            var s = Console.In.ReadToEnd().Trim();
            long ans = 0;
            for (int i = 0; i < s.Length; i++)
            {
                ans += s[i] - '0';
            }
            if (ans % 3 == 0)
                Console.WriteLine("2");
            else
                Console.WriteLine("1" + "\n" + ans % 3);
        }
        static void BicoloredHorses()
        {
            var nk = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(nk[0]);
            int k = Convert.ToInt32(nk[1]);
            int[] arr = new int[n + 1];
            for (int i = 0; i < n; i++)
                arr[i] = Convert.ToInt32(Console.ReadLine());

            int[,] dp = new int[n + 1, k + 1];
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < k + 1; j++)
                {
                    dp[i, j] = 100000000;
                }
            }
            dp[0, 0] = 0;
            int black = 0, white = 0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= k; j++)
                {
                    black = 0;
                    white = 0;
                    for (int l = i - 1; l >= 0; l--)
                    {
                        if (arr[l] == 0)
                            white++;
                        else
                            black++;
                        dp[i, j] = Math.Min(dp[i, j], dp[l, j - 1] + black * white);
                    }
                }
            }

            Console.WriteLine(dp[n, k]);
        }
        static void TaxiforProgrammers()
        {
            int[,] table = new int[6, 6];
            int current = 0;
            int min = Int32.MaxValue;
            for (int i = 1; i < 6; i++)
            {
                var temp = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                for (int j = 1; j < 6; j++)
                {
                    table[i, j] = temp[j - 1];
                }
            }
            int[] ans = { 1, 2, 3, 4, 5 };
            min = table[1, 2] + table[2, 3] + table[3, 4] + table[4, 5];
            current = table[1, 3] + table[3, 2] + table[2, 4] + table[4, 5];
            if (min > current)
            {
                min = current;
                ans[1] = 3;
                ans[2] = 2;
                ans[3] = 4;
            }
            current = table[1, 3] + table[3, 4] + table[4, 2] + table[2, 5];
            if (min > current)
            {
                min = current;
                ans[1] = 3;
                ans[2] = 4;
                ans[3] = 2;
            }
            current = table[1, 4] + table[4, 3] + table[3, 2] + table[2, 5];
            if (min > current)
            {
                min = current;
                ans[1] = 4;
                ans[2] = 3;
                ans[3] = 2;
            }
            Console.WriteLine(min);
            for (int i = 0; i < 5; i++)
            {
                Console.Write(ans[i] + " ");
            }
        }
        static void GCD2010()
        {
            int q = Convert.ToInt32(Console.ReadLine());
            var temp = Console.ReadLine().Split(' ');
            var x = Convert.ToInt32(temp[1]);
            int ans = x;
            Console.WriteLine(ans);

            List<int> res = new List<int>();
            for (int i = 0; i < q - 1; i++)
            {
                temp = Console.ReadLine().Split(' ');
                x = Convert.ToInt32(temp[1]);
                var op = temp[0];

                if (op == "+")
                {
                    res.Add(x);
                    ans = GCD(ans, x);
                    Console.WriteLine(ans);
                }
                else
                {
                    if (res.Count == 0)
                    {
                        Console.WriteLine(1);
                        return;
                    }
                    res.Remove(x);
                    ans = FindGCDCollection(res);
                    Console.WriteLine(ans);
                }
            }
        }
        static int FindGCDCollection(List<int> list)
        {
            int res = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                res = GCD(res, list[i]);
                if (res == 1)
                    return 1;
            }
            return res;
        }
        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        static int LCM(int a, int b)
        {
            return a / GCD(a, b) * b;
        }
        static void NudnikPhotographer()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Nundik(n));
        }
        static int Nundik(int n)
        {
            if (n == 1 || n == 2)
                return 1;
            if (n == 3)
                return 2;
            else
            {
                int[] res = new int[n + 1];
                res[1] = 1;
                res[2] = 1;
                res[3] = 2;
                for (int i = 4; i < res.Length; i++)
                {
                    res[i] = res[i - 1] + res[i - 3] + 1;
                }
                return res[n];
            }
        }
        static void Dill()
        {
            var nm = Console.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            long n = nm[0];
            long m = nm[1];

            for (long i = 1; i <= n; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            for (long i = n + 1; i <= m * n + 1; i += n)
            {
                Console.Write(i + " ");
            }
        }
        static void PassengerComfort()
        {
            var htvx = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();
            double h = htvx[0];
            double t = htvx[1];
            double v = htvx[2];
            double x = htvx[3];
            double min, max;

            if (t * x >= h)
            {
                min = 0;
                max = h / x;
            }
            else
            {
                min = (h - t * x) / (v - x);
                max = t;
            }

            Console.WriteLine(min.ToString("F7", new CultureInfo("en-US")) + " " + max.ToString("F7", new CultureInfo("en-US")));
        }
        static void GrandTheft()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var players = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int playerOne = --players[0];
            int playerTwo = --players[1];
            long totalSum = arr.Sum();

            if (playerOne == playerTwo)
            {
                var sum = Sum(arr, 0, playerOne);
                var max = Math.Max(sum, totalSum - sum - arr[playerOne]);
                Console.WriteLine(max + arr[playerOne] + " " + (totalSum - max - arr[playerOne]));
            }
            else
            {
                if (playerOne < playerTwo)
                {
                    var d = playerTwo - playerOne - 1;
                    var mid = playerOne + d / 2 + d % 2;
                    var sum = Sum(arr, 0, mid + 1);
                    Console.WriteLine(sum + " " + (totalSum - sum));
                }
                else
                {
                    var d = playerOne - playerTwo - 1;
                    var mid = playerTwo + d / 2;
                    var sum = Sum(arr, mid + 1, n);
                    Console.WriteLine(sum + " " + (totalSum - sum));
                }
            }
        }
        static long Sum(int[] arr, int s, int e)
        {
            long sum = 0;
            for (int i = s; i < e; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        static void PalindromeAgainPalindrome()
        {
            var str = Console.ReadLine();
            if (str.Length == 0)
                Console.WriteLine('a');
            else if (str.Length == 1)
                Console.WriteLine(str + str);
            else
            {
                for (int i = 1; i < str.Length; i++)
                {
                    var ispol = true;
                    for (int j = 0; j < str.Length - i && ispol; j++)
                        if (str[i + j] != str[str.Length - j - 1])
                            ispol = false;
                    if (ispol)
                    {
                        Console.Write(str);
                        for (int j = i - 1; j >= 0; j--)
                        {
                            Console.Write(str[j]);
                        }
                        return;
                    }
                }
            }
        }
        static int MaxArraySum(int[] a)
        {
            int max_so_far = a[0];
            int curr_max = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                curr_max = Math.Max(a[i], curr_max + a[i]);
                max_so_far = Math.Max(max_so_far, curr_max);
            }

            return max_so_far;
        }
        static void MaximumSum()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] Rec = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()?.
                    Split(' ').Select(Int32.Parse).ToArray();
                for (int j = 0; j < n; j++)
                {
                    Rec[i, j] = input[j];
                }
            }

            int ans = Int32.MinValue;
            for (int l = 0; l < n; l++)
            {
                int[] Sum = new int[n];
                for (int r = l; r < n; r++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Sum[i] += Rec[i, r];
                    }

                    var temp = MaxArraySum(Sum);
                    if (temp > ans)
                        ans = temp;

                }
            }

            Console.WriteLine(ans);
        }
        static void YoungTiler()
        {
            var mnk = Console.ReadLine()
                ?.Split(' ')
                .Select(Int32.Parse).ToArray();
            int l = 0;

        }

        static void SumOfSequentialNumbers()
        {
            // n = (2*A + P - 1)/2 * P; P=count of numbers
            var n = Convert.ToInt64(Console.ReadLine());

            n *= 2;
            var div = new List<long>();
            div.Add(1);
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    div.Add(i);
                    if (i != n / i)
                        div.Add(n / i);
                }
            }

            var sorted = div.OrderByDescending(a => a).ToList();
            for (int i = 0; i < sorted.Count(); i++)
            {
                var r = n / sorted[i];
                r = r - sorted[i] + 1;
                if (r > 0 && r % 2 == 0)
                {
                    Console.WriteLine("{0} {1}", r / 2, sorted[i]);
                    break;
                }
            }
        }
        static void IntegerPercentage()
        {
            var ns = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(ns[0]);
            int s = Convert.ToInt32(ns[1]);

            int[] arr = new int[n + 1];
            arr[s] = 1;
            for (int i = s + 1; i <= n; ++i)
            {
                for (int j = 101; j <= 201; j++)
                {
                    if ((i * 100) % j == 0)
                    {
                        var temp = i * 100 / j;
                        if (arr[temp] != 0 && arr[temp] + 1 > arr[i])
                        {
                            arr[i] = arr[temp] + 1;
                        }
                    }

                }
            }

            Console.WriteLine(arr.Max());
        }
        // F(i,j) - i cub , j height
        static void Staircases()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            long[,] F = new long[n + 1, n + 1];
            F[0, 0] = 1;
            for (int i = 1; i <= n; i++)
            {
                F[0, i] = F[1, i] = 1;
            }
            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= n - 1; j++)
                {
                    F[i, j] = F[i, j - 1];
                    if (i >= j)
                        F[i, j] += F[i - j, j - 1];
                }
            }

            Console.WriteLine(F[n, n - 1]);
        }
        static void BritishScientistsSaveTheWorld()
        {
            var nk = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(nk[0]);
            int sum1 = Convert.ToInt32(nk[1]);
            int sum2 = 0;
            for (int i = 0; i < n; i++)
            {
                var bg = Console.ReadLine().Split(' ');
                sum1 += Convert.ToInt32(bg[0]);
                sum2 += Convert.ToInt32(bg[1]);
            }

            int res = sum1 - (2 * (n + 1)) - sum2;
            if (res >= 0)
                Console.WriteLine(res);
            else
                Console.WriteLine("Big Bang!");
        }

        static void PenaltyTime()
        {
            var tt = Console.ReadLine().Split(' ');
            int q = Convert.ToInt32(tt[0]);
            int z = Convert.ToInt32(tt[1]);
            var sum = Console.ReadLine().Split(' ').
                Select(n => Int32.Parse(n)).Sum();
            if ((z - sum * 20) < q)
                Console.WriteLine("Dirty debug :(");
            else
                Console.WriteLine("No chance.");
        }
        static void Bayan()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            HashSet<string> hs = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                var str = Console.ReadLine();
                if (hs.Contains(str))
                    count++;
                else
                    hs.Add(str);
            }

            Console.WriteLine(count);
        }
        static void StonePile()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ');
            int[] w = new int[20];
            for (int i = 0; i < n; i++)
            {
                w[i] = Convert.ToInt32(arr[i]);
            }

            Console.WriteLine(FindMin(0, n, 0, 0, w, Int32.MaxValue));
        }
        static int FindMin(int k, int n, int pile1, int pile2, int[] weight, int dif)
        {
            if (n == k)
            {
                if (Math.Abs(pile1 - pile2) < dif)
                    dif = Math.Abs(pile1 - pile2);
            }
            else
            {
                pile1 += weight[k];
                dif = FindMin(k + 1, n, pile1, pile2, weight, dif);
                pile1 -= weight[k];
                pile2 += weight[k];
                dif = FindMin(k + 1, n, pile1, pile2, weight, dif);
                pile2 -= weight[k];
            }

            return dif;
        }
        static void A380()
        {
            int num = 0;
            char letter = ' ';
            var input = Console.ReadLine();
            if (input.Length == 3)
            {
                num = 10 * (input[0] - '0') + (input[1] - '0');
                letter = input[2];
            }
            else
            {
                num = input[0] - '0';
                letter = input[1];
            }

            if (num == 1 || num == 2)
            {
                if (letter == 'A' || letter == 'D')
                    Console.WriteLine("window");
                else
                    Console.WriteLine("aisle");
            }
            else if (num >= 3 && num <= 20)
            {
                if (letter == 'A' || letter == 'F')
                    Console.WriteLine("window");
                else if (letter == 'B' || letter == 'C' || letter == 'D' || letter == 'E')
                    Console.WriteLine("aisle");
                else
                    Console.WriteLine("neither");
            }
            else
            {
                if (letter == 'A' || letter == 'K')
                    Console.WriteLine("window");
                else if (letter == 'C' || letter == 'D' || letter == 'G' || letter == 'H')
                    Console.WriteLine("aisle");
                else
                    Console.WriteLine("neither");
            }
        }
        static void SandroBook()
        {
            var input = Console.ReadLine();

            Console.WriteLine(input.GroupBy(c => c).
                OrderByDescending(g => g.Count()).First().Key);
        }
        static void Sabotage()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            List<long> list = new List<long>();
            for (int i = 0; i < n; i++)
            {
                list.Add(Convert.ToInt64(Console.ReadLine()));
            }

            var l = list.OrderByDescending(x => x);
            foreach (var el in l)
            {
                Console.WriteLine(el);
            }
        }
        static void TheBattleSwamp()
        {
            var nk = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(nk[0]);
            int k = Convert.ToInt32(nk[1]);
            var arr = Console.ReadLine().Split(' ');
            int unused = 0, survived = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (k > Convert.ToInt32(arr[i]))
                    survived += k - Convert.ToInt32(arr[i]);
                else
                    unused += Convert.ToInt32(arr[i]) - k;
            }

            Console.WriteLine(unused + " " + survived);
        }

        static void AntiCAPS()
        {
            var input = Console.In.ReadToEnd();
            bool first = true;
            for (int i = 0; i < input.Length; i++)
            {
                var s = input[i];
                if (Char.IsLetter(s))
                {
                    if (first)
                    {
                        Console.Write(Char.ToUpper(s));
                        first = false;
                    }
                    else
                    {
                        Console.Write(Char.ToLower(s));
                    }
                }
                else if (s == '?' || s == '.' || s == '!')
                {
                    Console.Write(s);
                    first = true;
                }
                else
                {
                    Console.Write(s);
                }
            }
            Console.WriteLine();

        }
        static void TheDebutAlbum()
        {
            var input = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(input[0]);
            int a = Convert.ToInt32(input[1]);
            int b = Convert.ToInt32(input[2]);
            int mod = 1000000007;
            var end1 = new int[n + 1];
            var end2 = new int[n + 1];
            end1[0] = end2[0] = 1;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= Math.Min(i, a); j++)
                {
                    end1[i] += end2[i - j];
                    end1[i] %= mod;
                }
                for (int j = 1; j <= Math.Min(i, b); j++)
                {
                    end2[i] += end1[i - j];
                    end2[i] %= mod;
                }
            }

            Console.WriteLine((end1[n] + end2[n]) % mod);
        }
        static void EasyToHack()
        {
            int x = 0, y = 0;
            var str = Console.ReadLine();
            char[] res = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                x = str[i] - 'a';
                if (i == 0)
                    x = (x + 21) % 26;
                else
                {
                    y = str[i - 1] - 'a';
                    x = (x - y + 26) % 26;
                }

                res[i] = (char)(x + 'a');
            }

            Console.WriteLine(new string(res));
        }
        static void TestTask()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int n = Convert.ToInt32(Console.ReadLine());
            HashSet<string> loginUsers = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                var user = Console.ReadLine().Split(' ');
                if (user[0] == "register")
                {
                    if (dict.Keys.Contains(user[1]))
                        Console.WriteLine("fail: user already exists");
                    else
                    {
                        Console.WriteLine("success: new user added");
                        dict.Add(user[1], user[2]);
                    }
                }
                else if (user[0] == "login")
                {
                    if (dict.Keys.Contains(user[1]))
                    {
                        if (dict[user[1]] != user[2])
                            Console.WriteLine("fail: incorrect password");
                        else if (loginUsers.Contains(user[1]))
                            Console.WriteLine("fail: already logged in");
                        else
                        {
                            Console.WriteLine("success: user logged in");
                            loginUsers.Add(user[1]);
                        }
                    }
                    else
                        Console.WriteLine("fail: no such user");
                }
                else if (user[0] == "logout")
                {
                    if (dict.Keys.Contains(user[1]))
                    {
                        if (loginUsers.Contains(user[1]))
                        {
                            Console.WriteLine("success: user logged out");
                            loginUsers.Remove(user[1]);
                        }

                        else
                            Console.WriteLine("fail: already logged out");
                    }
                    else
                        Console.WriteLine("fail: no such user");
                }
            }
        }
        static void KbasedNumbers2()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long k = Convert.ToInt64(Console.ReadLine());
            BigInteger[] arr = new BigInteger[n + 1];
            arr[0] = 1;
            arr[1] = k - 1;
            for (int i = 2; i <= n; i++)
            {
                arr[i] = (k - 1) * (arr[i - 2] + arr[i - 1]);
            }
            Console.WriteLine(arr[n]);

        }
        static void KbasedNumbers3()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long k = Convert.ToInt64(Console.ReadLine());
            long m = Convert.ToInt64(Console.ReadLine());
            BigInteger[,] matrix = new BigInteger[2, 2];
            matrix[0, 0] = (k - 1);
            matrix[0, 1] = (k - 1);
            matrix[1, 0] = 1;
            matrix[1, 1] = 0;
            var res = Solve(matrix, n - 1, m);
            k = (k - 1) % m;
            Console.WriteLine((res[0, 0] % m * k + res[1, 0] % m * k) % m);

        }
        static BigInteger[,] Solve(BigInteger[,] matrix, long n, long mod)
        {
            if (n == 1)
                return matrix;
            var temp = Solve(matrix, n / 2, mod);
            temp = Mul(temp, temp, mod);
            if (n % 2 == 0)
                return temp;
            else
                return Mul(temp, matrix, mod);
        }

        static BigInteger[,] Mul(BigInteger[,] left, BigInteger[,] right, long mod)
        {
            var res = new BigInteger[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    BigInteger current = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        left[i, k] %= mod;
                        right[k, j] %= mod;
                        current += ((left[i, k] * right[k, j]) % mod);
                    }
                    res[i, j] = current % mod;
                }
            }
            return res;
        }

        static string Add(string a, string b)
        {
            if (a.Length > b.Length)
            {
                var t = a;
                a = b;
                b = t;
            }
            var str = "";
            int c = 0;
            int diff = b.Length - a.Length;

            for (int i = a.Length - 1; i >= 0; i--)
            {
                var sum = (a[i] - '0') + (b[i + diff] - '0') + c;
                str += (char)(sum % 10 + '0');
                c = sum / 10;
            }

            for (int i = b.Length - a.Length - 1; i >= 0; i--)
            {
                int sum = (b[i] - '0') + c;
                str += (char)(sum % 10 + '0');
                c = sum / 10;
            }

            if (c > 0)
                str += (char)(c + '0');

            var res = str.ToCharArray();
            Array.Reverse(res);
            str = new string(res);
            return str;
        }
        static string Mul(long a, string b)
        {
            if (a == 1)
                return b;

            long c = 0;
            string res = "";
            for (int i = b.Length - 1; i >= 0; i--)
            {
                long temp = a * (b[i] - '0') + c;
                if (temp > 9)
                {
                    res += temp % 10 + '0';
                    c = temp / 10;
                }
                else
                {
                    res += temp.ToString();
                    c = 0;
                }
            }
            if (c > 0)
                b += c.ToString();

            var str = res.ToCharArray();
            Array.Reverse(str);
            res = new string(str);
            return res;
        }

        static void FlatSpots()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Dictionary<int, int> dict = new Dictionary<int, int>();
            var res = 0;
            for (int i = 0; i < n; i++)
            {
                var wheel = Convert.ToInt32(Console.ReadLine());
                if (dict.Keys.Contains(wheel))
                    dict[wheel]++;
                else
                    dict.Add(wheel, 1);
            }

            foreach (var d in dict)
            {
                res += d.Value / 4;

            }

            Console.WriteLine(res);
        }
        static void Hieroglyphs()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            HashSet<string> hs = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                hs.Add(Console.ReadLine());
            }

            var letter = Convert.ToChar(Console.ReadLine());
            foreach (var h in hs)
            {
                if (h[0] == letter)
                    Console.WriteLine(h);
            }
        }
        static void TearsofDrowned()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var num = Console.ReadLine().Split(' ');
            int sum = 0;
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                var temp = Convert.ToInt32(num[i]);
                max = Math.Max(max, temp);
                sum += temp;
            }

            Console.WriteLine(sum + max);
        }
        static void Spammer()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                var s = Console.ReadLine();
                if (dict.Keys.Contains(s))
                    dict[s]++;
                else
                    dict.Add(s, 1);
            }

            foreach (var d in dict)
            {
                if (d.Value >= 2)
                    Console.WriteLine(d.Key);
            }
        }
        static void OverturnedNumbers()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            if (n > 4)
                Console.WriteLine("Glupenky Pierre");
            else
            {
                string[] nums = { "11", "01", "60", "80" };
                for (int i = 0; i < n; i++)
                {
                    Console.Write(nums[i] + " ");
                }
            }
        }
        static void TramForum()
        {
            var input = Console.In.ReadToEnd()
                .Split(
                    new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', '-', ':' }
                    , StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = new String(input[i].Where(Char.IsLetter).ToArray());
            }

            int count = 0;
            foreach (var s in input)
            {
                if (s == "tram")
                    count++;
                else if (s == "trolleybus")
                    count--;
            }
            if (count > 0)
                Console.WriteLine("Tram driver");
            else if (count < 0)
                Console.WriteLine("Trolleybus driver");
            else
                Console.WriteLine("Bus driver");
        }
        static void SacramentSum()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var list1 = new HashSet<long>();
            for (int i = 0; i < n; i++)
            {
                list1.Add(Convert.ToInt64(Console.ReadLine()));
            }

            var m = Convert.ToInt32(Console.ReadLine());
            long res = 0;
            for (int i = 0; i < m; i++)
            {
                var temp = Convert.ToInt64(Console.ReadLine());
                if (list1.Contains(10000 - temp))
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            Console.WriteLine("NO");
        }
        static void NonTrivialNumbers()
        {
            var IJ = Console.ReadLine().Split(' ');
            long first = Convert.ToInt64(IJ[0]);
            long end = Convert.ToInt64(IJ[1]);

            if (first == 1)
            {
                Console.WriteLine(1);
                return;
            }

            double min = Double.MaxValue;
            long res = 0;
            while (end >= first)
            {
                var trv = NonTrivial(end);
                if (trv == -1)
                {
                    Console.WriteLine(end);
                    return;
                }
                if (min > trv)
                {
                    min = trv;
                    res = end;
                }

                end--;
            }

            Console.WriteLine(res);
        }
        static double NonTrivial(long a)
        {
            long sum = 1;
            for (int i = 2; i * i < a; i++)
            {
                if (a % i == 0)
                {
                    sum += (i + a / i);
                }
            }

            if (sum == 1)
                return -1;
            return (double)sum / a;
        }
        static void BenBetsalel()
        {
            var n = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(n * n + "\n" + n);
        }

        static void Copying()
        {
            var input = Console.ReadLine().Split(' ');
            long n = Convert.ToInt64(input[0]);
            long k = Convert.ToInt64(input[1]);
            long count = 1;
            long ans = 0;
            while (count < n && count < k)
            {
                count <<= 1;
                ans++;
            }

            if (ans < n)
                ans += (n - count + k - 1) / k;
            Console.WriteLine(ans);
        }
        static void HistoryExam()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var professors = new HashSet<long>();
            for (int i = 0; i < n; i++)
            {
                professors.Add(Convert.ToInt64(Console.ReadLine()));
            }

            var m = Convert.ToInt32(Console.ReadLine());
            long res = 0;
            for (int i = 0; i < m; i++)
            {
                var temp = Convert.ToInt64(Console.ReadLine());
                if (professors.Contains(temp))
                    res++;
            }

            Console.WriteLine(res);
        }
        static void LongProblemStatement()
        {
            var input = Console.ReadLine().Split(' ').ToArray();
            var w = Convert.ToInt32(input[1]);
            var h = Convert.ToInt32(input[0]);

            var chars = 0;
            var line = 1;
            for (int i = 0; i < Convert.ToInt32(input[2]); i++)
            {
                var word = Console.ReadLine();
                if (chars + word.Length + 1 <= w)
                {
                    chars += word.Length + 1;
                }
                else
                {
                    line++;
                    chars = word.Length + 1;
                }
            }

            Console.WriteLine((line + h - 1) / h);
        }
        static void DominoDots()
        {
            var n = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(n * (n + 1) / 2 * (n + 2));
        }
        static void Order()
        {
            var n = Console.ReadLine();
            long current = 0;
            int count = 0;
            for (int i = 0; i < Convert.ToInt32(n); i++)
            {
                var temp = Convert.ToInt64(Console.ReadLine());
                if (temp == current)
                {
                    ++count;
                }
                else
                {
                    --count;
                }

                if (count < 0)
                {
                    current = temp;
                    count = 1;
                }
            }

            Console.WriteLine(current);
        }
        static void DemocracyInDanger()
        {
            int k = Int32.Parse(Console.ReadLine());
            int sum = 0;
            var col = Console.ReadLine().Split(' ').
                Select(n => Int32.Parse(n)).OrderBy(i => i).ToArray();
            for (int i = 0; i < (k + 1) / 2; i++)
            {
                sum += (col[i] + 1) / 2;
            }

            Console.WriteLine(sum);
        }
        static void HeatingMain()
        {
            var n = Console.ReadLine();
            var sum = Console.ReadLine().Split(' ').Select(t => Int32.Parse(t)).ToArray().Sum();
            Console.WriteLine(String.Format("{0:0.000000}", (double)sum / Convert.ToInt32(n)));
        }
        static void Taxi()
        {
            var input = Console.ReadLine().Split(' ');
            int a = Convert.ToInt32(input[0]);
            int b = Convert.ToInt32(input[1]);
            int c = Convert.ToInt32(input[2]);
            int d = Convert.ToInt32(input[3]);
            int res = 0;

            while (a <= c)
            {
                if (a + b > c)
                {
                    a = c;
                    break;
                }

                a += b;
                if (c - d < a)
                    break;
                c -= d;
            }

            Console.WriteLine(a);
        }
        static void Fuses()
        {
            var a = Console.ReadLine();
            var b = Console.ReadLine();
            int res = 0;
            for (int i = Convert.ToInt32(a); i <= Convert.ToInt32(b); i++)
            {
                if (i % 2 != 0)
                    res++;
            }

            Console.WriteLine(res);
        }
        static void AmusementPark()
        {
            var nominals = new[] { 10, 50, 100, 500, 1000, 5000 };

            var money = Console.ReadLine().Split(' ').Select(t => Int32.Parse(t)).ToArray();
            var price = Int32.Parse(Console.ReadLine());

            var total = 0;
            var min = -1;
            for (var i = 0; i < nominals.Length; ++i)
            {
                total += money[i] * nominals[i];
                if (money[i] != 0 && min == -1)
                    min = nominals[i];
            }

            var maxCount = total / price;
            var minCount = 1 + (total - min) / price;
            var count = maxCount - minCount + 1;
            Console.WriteLine(count);
            for (int c = 0; c < count; ++c)
            {
                Console.Write(minCount + c + " ");
            }
        }
        static void LongStatement()
        {
            var n = Console.ReadLine();
            var str = Console.ReadLine().Split(' ');
            var list = new List<int>() { 0, 0, 0 };
            for (int i = 0; i < Convert.ToInt32(n); i++)
            {
                ++list[Convert.ToInt32(str[i]) - 1];
            }

            if ((list[0] != 0 && list[1] != 0 && list[2] != 0))
                Console.WriteLine("Yes");
            else if (list[0] + list[1] + list[2] == 1)
                Console.WriteLine("No");
            else
            {
                list.Sort();
                if (Convert.ToInt32(n) <= 5 && list[1] >= 2 && list[2] >= 2)
                    Console.WriteLine("Yes");
                else if (Convert.ToInt32(n) > 5 && list[1] != 0 && list[2] != 0)
                    Console.WriteLine("Yes");
                else
                    Console.WriteLine("No");
            }

        }

        static void HeapSort(List<KeyValuePair<long, int>> arr)
        {
            int n = arr.Count;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                Heapify(arr, i, 0);
            }
        }
        static void Heapify(List<KeyValuePair<long, int>> arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && arr[l].Value < arr[largest].Value)
                largest = l;

            if (r < n && arr[r].Value < arr[largest].Value)
                largest = r;

            if (largest != i && arr[i].Value != arr[largest].Value)
            {
                var swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                Heapify(arr, n, largest);
            }
        }
        static void CentipedeMorning()
        {
            var ab = Console.ReadLine().Split(' ');
            int left = Convert.ToInt32(ab[0]);
            int right = Convert.ToInt32(ab[1]);

            int time1 = 40 * 2 + (right - 40) * 2 + 40;
            int time2 = 39 * 2 + 40 + (left - 40) * 2 + 1;

            Console.WriteLine(Math.Max(time1, time2));
        }
        static void Spiral()
        {
            var input = Console.ReadLine().Split(' ');
            var n = Convert.ToInt64(input[0]);
            var m = Convert.ToInt64(input[1]);

            if (n <= m)
                Console.WriteLine(2 * n - 2);
            else
                Console.WriteLine(2 * m - 1);
        }
        static void DivorceOfTheSeven()
        {
            var n = Console.ReadLine();
            double temp = 0;
            for (int i = 0; i < n.Length; i++)
            {
                temp *= 10;
                temp += (n[i] - '0');
                temp %= 7;
            }

            Console.WriteLine(temp);
        }
        static void Scholarship()
        {
            var input = Console.ReadLine();
            var exam = Convert.ToInt32(input);
            int five = 0;
            int four = 0;
            int three = 0;
            for (int i = 0; i < exam; i++)
            {
                var value = Console.ReadLine();
                if (value == "5")
                    five++;
                if (value == "4")
                    four++;
                if (value == "3")
                    three++;
            }

            if (three != 0)
                Console.WriteLine("None");
            else if (four == 0)
                Console.WriteLine("Named");
            else if (four > five)
                Console.WriteLine("Common");
            else if (four <= five)
                Console.WriteLine("High");
        }
        static void AnotherDressRehearsal()
        {
            var input = Console.ReadLine().Split(' ');
            var x = Convert.ToInt64(input[0]);
            var y = Convert.ToInt64(input[1]);
            var c = Convert.ToInt64(input[2]);

            if (x + y < c)
                Console.WriteLine("Impossible");
            else
            {
                x = Math.Min(x, c);
                Console.WriteLine(x + " " + (c - x));
            }
        }
        static void OneStepFromHappiness()
        {
            var input = Console.ReadLine();
            var sumLeft = Convert.ToInt32(input[0]) + Convert.ToInt32(input[1]) + Convert.ToInt32(input[2]);
            var sumRight = Convert.ToInt32(input[3]) + Convert.ToInt32(input[4]) + Convert.ToInt32(input[5]);
            if (Math.Abs(sumRight - sumLeft) != 1 || (sumLeft > sumRight && input[5] == '9') || (sumLeft < sumRight && input[5] == '0'))
            {
                Console.WriteLine("No");
            }
            else
            {
                Console.WriteLine("Yes");
            }
        }
        static void LineFighting()
        {
            var t = Console.ReadLine();
            for (int i = 0; i < Convert.ToInt32(t); i++)
            {
                var nk = Console.ReadLine().Split(' ');
                int n = Convert.ToInt32(nk[0]);
                int k = Convert.ToInt32(nk[1]);
                int ans = 0;

                var arr = new int[k];
                for (int l = 0; l < n; l++)
                    ++arr[l % k];


                for (int j = 0; j < k - 1; j++)
                {
                    ans += arr[j] * (n - arr[j]);
                    n -= arr[j];
                }

                Console.WriteLine(ans);

            }
        }
        static void Bookworm()
        {
            var input = Console.ReadLine().Split(' ');
            int volume = Convert.ToInt32(input[0]);
            int cover = Convert.ToInt32(input[1]);
            int start = Convert.ToInt32(input[2]);
            int end = Convert.ToInt32(input[3]);

            if (end >= start)
            {
                Console.WriteLine((end - start) * 2 * cover + (end - start - 1) * volume);
            }
            else
            {
                Console.WriteLine((start - end) * 2 * cover + (start - end + 1) * volume);
            }
        }
        static void Farm()
        {
            var n = Console.ReadLine();
            if (n == "1")
                Console.WriteLine("1 2 3");
            else if (n == "2")
                Console.WriteLine("3 4 5");
            else
                Console.WriteLine(-1);
        }
        static void CrazyNotions()
        {
            var input = Console.ReadLine();
            int n = Convert.ToInt32(input);

            if (n % 4 == 0)
            {
                Console.WriteLine(0);
            }
            else if (n % 4 == 3 || (n % 5 == 0 && n % 4 == 1))
            {
                Console.WriteLine(2);
            }
            else
                Console.WriteLine(1);
        }
        static void ChernobylEagles()
        {
            var n = Console.ReadLine();
            int count = Convert.ToInt32(n);
            BigInteger ans = BigInteger.One;

            if (count <= 3)
            {
                Console.WriteLine(count);
                return;
            }

            while (count > 4)
            {
                ans = BigInteger.Multiply(ans, 3);
                count -= 3;
            }

            if (count == 4)
            {
                ans = BigInteger.Multiply(ans, 4);
            }
            else if (count == 3)
            {
                ans = BigInteger.Multiply(ans, 3);
            }
            else if (count == 2)
            {
                ans = BigInteger.Multiply(ans, 2);
            }
            Console.WriteLine(ans);

        }
        static void Lonesome()
        {
            var n = Console.ReadLine();
            int ans = 0;
            for (int i = 0; i < Convert.ToInt32(n); i++)
            {
                ans = 0;
                var input = Console.ReadLine();
                var file = input[0];
                var rank = Convert.ToInt32(input[1].ToString());

                if (file + 2 <= 'h')
                {
                    if (rank + 1 <= 8)
                        ans++;
                    if (rank - 1 >= 1)
                        ans++;
                }

                if (file - 2 >= 'a')
                {
                    if (rank + 1 <= 8)
                        ans++;
                    if (rank - 1 >= 1)
                        ans++;
                }

                if (rank + 2 <= 8)
                {
                    if (file + 1 <= 'h')
                        ans++;
                    if (file - 1 >= 'a')
                        ans++;
                }

                if (rank - 2 >= 1)
                {
                    if (file + 1 <= 'h')
                        ans++;
                    if (file - 1 >= 'a')
                        ans++;
                }

                Console.WriteLine(ans);
            }

        }
        static void ProductofDigits()
        {
            var input = Console.ReadLine();
            var n = Convert.ToInt64(input);
            if (n >= 0 && n <= 9)
            {
                Console.WriteLine(n);
                return;
            }

            var dig = new Stack<long>();
            long ans = 0;
            for (int i = 9; i >= 2; i--)
            {
                while (n > 1 && n % i == 0)
                {
                    dig.Push(i);
                    n /= i;
                }
            }

            if (n != 1)
            {
                Console.WriteLine(-1);
                return;
            }

            while (dig.Count > 0)
            {
                ans = 10 * ans + dig.Pop();
            }

            Console.WriteLine(ans);
        }
        static int Digits(int n)
        {
            int count = 0;
            int i = 1;
            while (n > 0)
            {
                count++;
                n /= (int)Math.Pow(10, i);
            }

            return count;
        }
        static void LongestPalindrome()
        {
            var res = "";
            var input = Console.ReadLine();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i; j < input.Length; j++)
                {
                    var temp = input.Substring(i, j - i + 1);
                    if (ReverseString(temp) == temp && temp.Length > res.Length)
                        res = temp;
                }
            }

            Console.WriteLine(res);
        }
        static void Elections()
        {
            var nm = Console.ReadLine().Split(' ');
            var n = Convert.ToInt32(nm[0]);
            var m = Convert.ToInt32(nm[1]);
            int[] arr = new int[n + 1];
            for (int i = 1; i <= m; i++)
            {
                var temp = Console.ReadLine();
                ++arr[Convert.ToInt32(temp)];
            }

            for (int i = 1; i < arr.Length; i++)
            {
                double temp = (double)arr[i] / m * 100;
                Console.WriteLine(String.Format("{0:0.00}", temp) + "%");

            }
        }
        static void Penguins()
        {
            int e = 0, m = 0, l = 0;
            var n = Console.ReadLine();
            for (int i = 0; i < Convert.ToInt32(n); i++)
            {
                var current = Console.ReadLine();
                if (current == "Emperor Penguin")
                    e++;
                else if (current == "Macaroni Penguin")
                    m++;
                else
                    l++;
            }
            if (e > m && e > l)
                Console.WriteLine("Emperor Penguin");
            else if (m > e && m > l)
                Console.WriteLine("Macaroni Penguin");
            else
                Console.WriteLine("Little Penguin");
        }
        static void TeamWork(int[] arr)
        {
            int temp = arr[0];
            int j = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == temp)
                    j++;
                else
                {
                    Console.Write(j + " " + arr[i - 1] + " ");
                    j = 1;
                }
                temp = arr[i];
            }

            Console.Write(j + " " + temp);
        }
        static int SMS(char[] sms)
        {
            int ans = 0;
            foreach (char c in sms)
            {
                if (c == 'a' || c == 'd' || c == 'g' || c == 'j' || c == 'm' || c == 'p' || c == 's' || c == 'v' || c == 'y'
                    || c == '.' || c == ' ')
                    ans += 1;
                else if (c == 'b' || c == 'e' || c == 'h' || c == 'k' || c == 'n' || c == 'q' || c == 't' || c == 'w' || c == 'z' || c == ',')
                    ans += 2;
                else
                    ans += 3;
            }

            return ans;
        }
        static BigInteger Fib(long n)
        {
            var arr = new BigInteger[n + 1];
            arr[0] = BigInteger.Zero;
            arr[1] = BigInteger.One;
            for (int i = 2; i <= n; i++)
            {
                arr[i] = BigInteger.Add(arr[i - 1], arr[i - 2]);
            }

            return 2 * arr[n];
        }

        static void DonaldPostman(string[] names)
        {
            int res = 0;
            int current = 1;
            char[] one = { 'A', 'P', 'R', 'O' };
            char[] two = { 'B', 'M', 'S' };
            for (int i = 0; i < names.Length; i++)
            {
                if (one.Contains(names[i].First()))
                {
                    res += Math.Abs(1 - current);
                    current = 1;
                }
                else if (two.Contains(names[i].First()))
                {
                    res += Math.Abs(2 - current);
                    current = 2;
                }
                else
                {
                    res += Math.Abs(3 - current);
                    current = 3;
                }

            }

            Console.WriteLine(res);

        }
        static void FourImps(int n)
        {
            int sum = 1;
            for (int i = 2; i <= n; i++)
            {
                sum += i % 2 == 0 ? i : -i;
            }

            Console.WriteLine(sum % 2 == 0 ? "black" : "grimy");
        }
        static void TitanRuins(int[] arr)
        {
            int sum = arr[0] + arr[1] + arr[2];
            int mid = 2;
            for (int i = 1; i < arr.Length - 2; i++)
            {
                if (arr[i] + arr[i + 1] + arr[i + 2] > sum)
                {
                    sum = arr[i] + arr[i + 1] + arr[i + 2];
                    mid = i + 2;
                }
            }

            Console.WriteLine(sum + " " + mid);
        }
        static void Eigenvalues(long[] arrOne, long[] arrTwo, long[] arrThree)
        {
            int res = 0;
            int k = 0;
            int j = 0;
            int i = 0;
            while (i < arrOne.Length && j < arrTwo.Length && k < arrThree.Length)
            {
                if (arrOne[i] == arrTwo[j] && arrTwo[j] == arrThree[k])
                {
                    res++;
                    i++;
                    j++;
                    k++;
                }
                else if (arrOne[i] < arrTwo[j])
                    i++;
                else if (arrTwo[j] < arrThree[k])
                    j++;
                else
                    k++;
            }

            Console.WriteLine(res);
        }
        static void Polindrome(string str)
        {

            if (str.Length == 1)
            {
                Console.WriteLine(str + str);
                return;
            }
            bool isSame = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != str[str.Length - 1 - i])
                {
                    isSame = false;
                    var temp = str + ReverseString(str.Substring(0, i - 1));
                    if (temp == ReverseString(temp))
                    {
                        Console.WriteLine(temp);
                        break;
                    }
                }
            }

            if (isSame)
            {
                str += str[1];
                for (int i = 0; i < str.Length; i++)
                {
                    var temp = str + ReverseString(str.Substring(0, i));
                    if (temp == ReverseString(temp))
                    {
                        Console.WriteLine(temp);
                        break;
                    }
                }
            }

        }
        static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static int DistinctSubstring(string str)
        {
            string temp = "";
            HashSet<string> result = new HashSet<string>();
            for (int i = 0; i < str.Length; i++)
            {
                temp = "";
                for (int j = i; j < str.Length; j++)
                {
                    temp += str[j];
                    result.Add(temp);
                }
            }

            return result.Count;
        }
        static double SequenceMedian(int[] arr)
        {
            long median = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                median += arr[i];
            }

            return (double)median / arr.Length;
        }
        static long GoodBadUgly(long n)
        {
            long res = 0;
            int i = 0;
            while (n > 0)
            {
                res += (long)((n % 256) * Math.Pow(256, 3 - i));
                n /= 256;
                i++;
            }

            return res;
        }
        static long Factorials(int n, int count)
        {
            if (n <= count)
                return n;

            long res;

            if (n % count == 0)
                res = n * count;
            else
                res = n * (n % count);

            while (n > 2 * count)
            {
                n -= count;
                res *= n;
            }
            return res;
        }
        static void Power(int N, int M, int Y)
        {
            var interval = M - 1;
            var res = new List<int>();
            for (int i = 0; i <= interval; i++)
            {
                if (BinPowerMod(i, N, M) == Y)
                {
                    res.Add(i);
                }
            }

            if (res.Count == 0)
            {
                Console.Write(-1);
            }
            else
            {
                foreach (var r in res)
                {
                    Console.Write(r + " ");
                }
            }
        }
        static int BinPowerMod(int x, int y, int mod)
        {
            int res = 1;
            x %= mod;

            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = (res * x) % mod;
                y >>= 1;
                x = (x * x) % mod;
            }
            return res;
        }
        static long BinPow(long value, long exp)
        {
            if (exp == 0)
                return 1;
            if ((exp & 1) == 0)
            {
                return (BinPow(value, exp >> 1) * BinPow(value, exp >> 1));
            }
            else
            {
                return value * BinPow(value, exp - 1);
            }
        }
        static void LostinLocalization(int x)
        {
            if (x < 5)
                Console.WriteLine("few");
            else if (x <= 9)
                Console.WriteLine("several");
            else if (x <= 19)
                Console.WriteLine("pack");
            else if (x <= 49)
                Console.WriteLine("lots");
            else if (x <= 99)
                Console.WriteLine("horde");
            else if (x <= 249)
                Console.WriteLine("throng");
            else if (x <= 499)
                Console.WriteLine("swarm");
            else if (x <= 999)
                Console.WriteLine("zounds");
            else
                Console.WriteLine("legion");
        }
        static void ReverseRoot(string input)
        {
            string[] s = Console.In.ReadToEnd().Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = s.Length - 1; i >= 0; i--)
            {
                Console.WriteLine("{0:0.0000}", Math.Sqrt(Double.Parse(s[i])));
            }
        }
    }

    public class Job
    {
        public int start;
        public int finish;
        public int profit;

        public Job(int start, int finish, int profit)
        {
            this.start = start;
            this.finish = finish;
            this.profit = profit;
        }
    }
}
