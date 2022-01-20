using System;
using System.Collections.Generic;

namespace NumberTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Timus1756()
        {
            var md1d2 = Console.ReadLine().Split();
            var m1 = int.Parse(md1d2[0]);
            var d1 = int.Parse(md1d2[1]);
            var d2 = int.Parse(md1d2[2]);

            var total = m1 * d1;
            int m2;
            if (total % d2 == 0)
                m2 = total / d2;
            else 
                m2 = total / d2 + 1;

            for (int i = 0; i < d2; i++)
            {
                if(total >= m2)
                {
                    Console.Write(m2 + " ");
                    total -= m2;
                }
                else
                {
                    Console.Write(total + " ");
                    total = Math.Max(total - m2, 0);
                }
            }
        }

        /// <summary>
        /// a^2 + b^2 / c
        /// c = n^2
        /// n^2 <= a,b,c <= (n+1)^2 = n^2 + 2n + 1
        /// a = n^2 + k
        /// b = n ^ 2 + d
        /// a^2 + b^2 = 2*n^4 + 2*n^2(k + d) + k^2 + d^2
        ///                      k = n && d = 2n =>/c
        /// a = n^2 + n
        /// b = n^2 + 2n
        /// c = n^2
        /// </summary>
        static void Timus1335()
        {
            var n = long.Parse(Console.ReadLine());
            var a = n * n + n;
            var b = n * n + 2 * n;
            var c = n * n;

            Console.WriteLine(a + " " + b + " " + c);
        }

        static void Timus1430()
        {
            var abn = Console.ReadLine().Split();
            var a = long.Parse(abn[0]);
            var b = long.Parse(abn[1]);
            var n = long.Parse(abn[2]);
            if (a < b)
            {
                var t = FindMinDuration(b, a, n);
                Console.WriteLine(t.Item2 + " " + t.Item1);
            }
            else
            {
                var t = FindMinDuration(a, b, n);
                Console.WriteLine(t.Item1 + " " + t.Item2);
            }
        }
        static Tuple<long, long> FindMinDuration(long a, long b, long n)
        {
            var min = long.MaxValue;
            var x = 0l;
            for (int i = 0; i * a <= n; i++)
            {
                if ((n - i * a) % b < min)
                {
                    min = (n - i * a) % b;
                    x = i;
                }
                else if (i * a % b == a % b && i > 1)
                    break;
            }

            return Tuple.Create(x, (n - a * x) / b);
        }


        static void Timus1049()
        {
            for (int i = 0; i < 10; i++)
            {
                var n = int.Parse(Console.ReadLine());
                DivCount(n);
            }

            long ans = 1;
            for (int i = 0; i < 10000; i++)
            {
                ans *= divisors[i] + 1;
                ans %= 10;
            }

            Console.WriteLine(ans);
        }

        static int[] divisors = new int[10000];
        static void DivCount(int n)
        {
            for (int i = 2; i * i <= n; i++)
            {
                while (n % i == 0)
                {
                    divisors[i]++;
                    n /= i;
                }
            }
            if (n != 1)
                divisors[n]++;
        }

        static void Timus2047()
        {
            var sum = 1568617;
            var divisors = new int[1568618];
            var ans = new int[100005];

            // Calculates the number of divisors
            for (var i = 1; i <= sum; i++)
                for (var j = i; j <= sum; j += i)
                    divisors[j]++;

            for (int i = 100000; i >= 0; i--)
            {
                ans[i] = divisors[sum];
                sum -= ans[i];
            }

            var n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.Write(ans[i] + " ");
            }
        }

        static void Timus1356()
        {
            var t = int.Parse(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var n = int.Parse(Console.ReadLine());
                if (IsPrime(n))
                    Console.WriteLine(n);
                else
                {
                    if (n % 2 == 0)
                    {
                        for (int j = 2; j < n; j++)
                        {
                            if (IsPrime(n - j) && IsPrime(j))
                            {
                                Console.WriteLine(j + " " + (n - j));
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (IsPrime(n - 2))
                        {
                            Console.WriteLine(2 + " " + (n - 2));
                        }
                        else
                        {
                            var k = n - 3;
                            for (int j = 2; j < n; j++)
                            {
                                if (IsPrime(k - j) && IsPrime(j))
                                {
                                    Console.WriteLine("3 " + (k - j) + " " + j);
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
        static bool IsPrime(int n)
        {
            if (n == 1)
                return false;
            for (var i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        static void Timus1740()
        {
            var input = Console.ReadLine().Split();
            var l = int.Parse(input[0]);
            var k = int.Parse(input[1]);
            var h = int.Parse(input[2]);

            if (l % k == 0)
            {
                Console.WriteLine($"{l / k * h:0.00000000000}" + " " + $"{l / k * h:0.00000000000}");
            }
            else
            {
                Console.WriteLine($"{l / k * h:0.00000000000}" + " " + $"{(l / k + 1) * h:0.00000000000}");
            }
        }

        static void Timus1044()
        {
            N = int.Parse(Console.ReadLine());
            GetChoice(0, 0);

            int ans = 0;
            for (int i = 0; i <= 36; ++i)
                ans += count[i] * count[i];
            Console.WriteLine(ans);
        }
        private static int N;
        private static int[] count = new int[37];
        static void GetChoice(int n, int sum)
        {
            if (n == N / 2)
                ++count[sum];
            else
            {
                for (int i = 0; i < 10; ++i)
                    GetChoice(n + 1, sum + i);
            }
        }

        static void Timus1940()
        {
            var input = Console.ReadLine().Split();
            var A = int.Parse(input[0]);
            var B = int.Parse(input[1]);
            var k = int.Parse(input[2]);
            Sieve(k + 1);
            var ans = B - Query(A, A + B, primes.Count - 1);
            foreach (var p in primes)
            {
                if (p > A && p <= A + B)
                    ans++;
            }

            Console.WriteLine(ans);
        }
        static int Query(int a, int b, int k)
        {
            if (b <= a || k == -1)
                return 0;
            return b / primes[k] - a / primes[k] +
                Query(a, b, k - 1) - Query(a / primes[k], b / primes[k], k - 1);
        }

        static List<int> primes;
        static void Sieve(int max)
        {
            var is_prime = new bool[305];
            primes = new List<int>();
            for (int p = 2; p <= max; p++)
            {
                if (is_prime[p] == false)
                {
                    primes.Add(p);
                    for (int i = p * 2; i <= max; i += p)
                        is_prime[i] = true;
                }
            }
        }

    }
}
