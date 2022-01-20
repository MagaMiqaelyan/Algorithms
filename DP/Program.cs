using System;
using System.Collections.Generic;
using System.Linq;

namespace DP
{
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
    class Program
    {
        static void Main(string[] args)
        {
            //KefaAndFirstSteps();
            //AlexAndRhombus();
            //MaximumIncrease();
            //FillingShapes();
            //MakeProductEqualOne();
            // ScientificConference();
            //SleepingSchedule();
            //CutRibbon();
            Boredom();
        }

        public void Flip(int n, int[] a)
        {
            int count = 0;
            int max = 0;
            int current = 0;
            int i = 0;
            while (n > 0)
            {
                n--;
                if (a[i++] == 1)
                {
                    count += 1;
                    if (current > 0)
                    {
                        current -= 1;
                    }
                }
                else
                {
                    current += 1;
                    if (current > max)
                    {
                        max = current;
                    }
                }
            }

            Console.WriteLine(count + max);
        }

        static void Boredom()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var dp = new int[n];
            int max = 0;
            for (int i = 1; i < n - 1; i++)
            {
                dp[i] = arr.Where(x => x == arr[i - 1] || x == arr[i + 1]).Sum();
                max = Math.Max(dp[i], max);
            }
            Console.WriteLine(max);
        }

        static void CutRibbon()
        {
            var nabc = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = nabc[0];
            int a = nabc[1];
            int b = nabc[2];
            int c = nabc[3];

            int[] arr = { a, b, c };
            int x, y, z;
            arr = arr.OrderBy(d => d).ToArray();
            int[] dp = new int[4001];
            dp[arr[0]] = 1;
            dp[arr[1]] = 1;
            dp[arr[2]] = 1;
            for (int i = arr[0] + 1; i <= n; i++)
            {
                x = y = z = 0;
                if (arr[0] <= i && dp[i - arr[0]] > 0)
                {
                    x = dp[i - arr[0]] + 1;
                }
                if (arr[1] <= i && dp[i - arr[1]] > 0)
                {
                    y = dp[i - arr[1]] + 1;
                }
                if (arr[2] <= i && dp[i - arr[2]] > 0)
                {
                    z = dp[i - arr[2]] + 1;
                }
                var max = Math.Max(x, y);
                dp[i] = Math.Max(max, Math.Max(z, dp[i]));
            }
            Console.WriteLine(dp[n]);
        }

        static void SleepingSchedule()
        {
            var nhlr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = nhlr[0];
            int h = nhlr[1];
            int l = nhlr[2];
            int r = nhlr[3];

            var arr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var dp = new int[h];
            dp[0] = -1;
            for (int i = 0; i < n; i++)
            {
                var temp = new int[h];
                for (int j = 0; j < h; j++)
                {
                    if (dp[(j + h - arr[i]) % h] != 0)
                        temp[j] = Math.Max(temp[j], dp[(j + h - arr[i]) % h] + j >= l && j <= r ? 1 : 0);
                    if (dp[(j + h - arr[i] + 1) % h] != 0)
                        temp[j] = Math.Max(temp[j], dp[(j + h - arr[i] + 1) % h] + j >= l && j <= r ? 1 : 0);
                }
                dp = temp;
            }
            int ans = 0;
            for (int i = 0; i < h; i++)
            {
                ans = Math.Max(ans, dp[i]);
            }
            Console.WriteLine(ans);
        }

        static void ScientificConference()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var list = new List<Job>();
            for (int i = 0; i < n; i++)
            {
                var sew = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                list.Add(new Job(sew[0], sew[1], sew[2]));
            }

            Console.WriteLine(WeightedActivity(list.OrderBy(j => j.finish).ToList()));
        }
        static int WeightedActivity(List<Job> jobs)
        {
            if (jobs.Count == 1)
                return jobs[0].profit;
            int[] dp = new int[jobs.Count];
            dp[0] = jobs[0].profit;

            int j = 0;
            for (int i = 1; i < jobs.Count; i++)
            {
                j = i - 1;
                var temp = jobs[i].profit;
                while (j >= 0 && jobs[i].start >= jobs[j].finish)
                    j--;

                if (j != -1)
                    temp += dp[j];

                dp[i] = Math.Max(temp, dp[i - 1]);
            }
            return dp[jobs.Count - 1];
        }
        static void MakeProductEqualOne()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ').
                Select(long.Parse).ToArray();
            long ans = 0;
            long neg = 0;
            long zero = 0;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] == 0)
                    zero++;
                else if (arr[i] > 0)
                    ans += (arr[i] - 1);
                else
                {
                    ans += (-1 - arr[i]);
                    neg++;
                }
            }
            if ((neg & 1) == 1 && zero == 0)
                ans += 2;
            ans += zero;
            Console.WriteLine(ans);
        }
        static void FillingShapes()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            if ((n & 1) == 1)
                Console.WriteLine(0);
            else Console.WriteLine(Math.Pow(2, n / 2));
        }
        static void MaximumIncrease()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').
                Select(a => long.Parse(a)).ToArray();
            long max = 1;
            long current = 1;

            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                    if (input[i - 1] < input[i])
                    {
                        current++;
                        max = Math.Max(current, max);
                    }
                    else current = 1;
            }

            Console.WriteLine(max);
        }
        static void AlexAndRhombus()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Nthrhombus(n));
        }
        static int Nthrhombus(int n)
        {
            if (n == 1)
                return 1;
            return 4 * (n - 1) + Nthrhombus(n - 1);
        }
        static void KefaAndFirstSteps()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').
                Select(a => int.Parse(a)).ToArray();
            int max = 1;
            int current = 1;

            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                    if (input[i] >= input[i - 1])
                    {
                        current++;
                        max = Math.Max(current, max);
                    }
                    else current = 1;
            }

            Console.WriteLine(max);
        }
    }
}
