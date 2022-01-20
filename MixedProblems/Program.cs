using System;
using System.Collections.Generic;

namespace MixedProblems
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void Init()
        {
            var mod = 1000000007;
            var primes = MakeSieve(1000000);
            var pascalTriangle = PacscalTriangle(mod);
            var answer = new long[pascalTriangle.Count];
            for (int i = 0; i < pascalTriangle.Count; i++)
            {
                answer[i] = 1;
                for (int j = 0; j < pascalTriangle[i].Count; j++)
                {
                    answer[i] *= ExponentMod(j + 1, pascalTriangle[i][j], mod);
                    answer[i] %= mod;
                }
            }
            var T = Convert.ToInt32(Console.ReadLine());
            for (int j = 0; j < T; j++)
            {
                var n = Console.ReadLine();
                var numbers = Console.ReadLine().Split(' ');
                long PrimesCount = 0;
                long mul = 1;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (primes[Convert.ToInt64(numbers[i])])
                        PrimesCount++;
                }

                mul = answer[PrimesCount];
                var c = Convert.ToInt64(n) - PrimesCount;
                while (c-- > 0) mul = (mul * mul % mod);
                Console.WriteLine(mul);
            }
        }
        static long TheFirstMeeting(long[] arr)
        {
            bool isPrime = true;
            long max = 0, min = Int64.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsPrime(arr[i]))
                {
                    isPrime = false;
                    max = Math.Max(max, arr[i]);
                    min = Math.Min(min, arr[i]);
                }
            }

            if (isPrime) return 1;
            return Math.Abs(max - min);
        }

        static bool IsPrime(long n)
        {
            if (n == 1) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static double GreatArjitAndNumbers(long[] arr)
        {
            double F = 0;
            long[] A = new long[arr.Length - 1];
            var mod = Math.Pow(10, 9) + 7;
            for (int i = 1; i < arr.Length; i++)
            {
                A[i - 1] = arr[i] / arr[i - 1];
                F = (F % mod + ((A[i - 1] + 1) % mod) / 2 * (A[i - 1] % mod) % mod) % mod;
            }

            var a = ((F % mod * (F + 1) % mod) % mod) % mod;
            return a;
        }

        /// (A^B)modC
        static long ExponentMod(long value, long exponent, long modulus)
        {
            long res = 1;
            value %= modulus;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    res = (res * value) % modulus;
                exponent >>= 1;
                value = (value * value) % modulus;
            }
            return res;
        }

        static long PandaAndChainReaction(long n, long x)
        {
            long mod = (long)(Math.Pow(10, 6) + 3);
            var chain = new long[n + 1];
            chain[0] = x;
            for (int i = 1; i < chain.Length; i++)
            {
                chain[i] = (chain[i - 1] % mod * i % mod) % mod;
            }

            return chain[n];
        }

        static long FindMe(string numbers)
        {
            var input = numbers.Split(' ');
            var res = Convert.ToInt64(input[0]);
            for (int i = 1; i < input.Length; ++i)
            {
                res -= Convert.ToInt64(input[i]);
            }

            return res;
        }

        static long SearchPassword(string numbers)
        {
            var input = numbers.Split(' ');
            var mod = 1000000007;
            return Factorial(Convert.ToInt64(input[0]), Convert.ToInt64(input[1]), mod) % mod;
        }

        static long Factorial(long end, long n, long mod)
        {
            if (end == 1) return n;
            if (n == 1) return 1;
            return (n % mod * Factorial(end - 1, n - 1, mod) % mod) % mod;
        }

        static List<List<long>> PacscalTriangle(long mod)
        {
            var triangle = new List<List<long>>();
            var temp = new List<long>() { 0, 1, 0 };
            triangle.Add(temp);
            for (int i = 1; i <= 1000; i++)
            {
                temp = new List<long>() { 0 };
                for (int j = 0; j < triangle[i - 1].Count - 1; j++)
                {
                    temp.Add((triangle[i - 1][j] + triangle[i - 1][j + 1]) % (mod - 1));
                }
                temp.Add(0);
                triangle.Add(temp);
            }
            return triangle;
        }

        static Dictionary<long, bool> MakeSieve(int max)
        {
            var is_prime = new Dictionary<long, bool>();
            for (long i = 1; i <= max; i++) is_prime[i] = true;

            for (long i = 2; i <= max; i++)
            {
                if (is_prime[i])
                {
                    for (long j = i * i; j <= max; j += i)
                        is_prime[j] = false;
                }
            }
            return is_prime;
        }

        static long SumDivisors(long n)
        {
            if (IsPrime(n) || n == 1) return 1;
            long res = 1;
            for (int i = 2; i <= n / 2; i++)
            {
                if (n % i == 0)
                    res += i;
            }

            return res;
        }

        static long[] Manner(long[] A)
        {
            var N = A.Length - 1;
            var B = new long[N + 1];
            for (int i = 0; i <= N; i++)
            {
                B[i] = 1;
                for (int j = 0; j <= N; j++)
                {
                    if (i != j)
                    {
                        B[i] = B[i] * A[j];
                    }
                }
            }

            return B;
        }

        static void AToB(long[] A, long[] B)
        {
            var mod = 1000000007;
            double x = 1;
            long index = 0;
            var Q = Console.ReadLine();

            for (int i = 0; i < Convert.ToInt64(Q); i++)
            {
                var temp = Console.ReadLine().Split(' ');
                if (temp[0] == "1")
                {
                    Console.WriteLine(B[Convert.ToInt64(temp[1]) - 1] % mod);
                }
                else if (temp[0] == "0")
                {
                    index = Convert.ToInt64(temp[1]) - 1;
                    if (A[index] != 0)
                    {
                        x = (double)Convert.ToInt64(temp[2]) / A[index];
                        A[index] = Convert.ToInt64(temp[2]);
                        if (x != 1)
                            for (int j = 0; j < B.Length; j++)
                            {
                                if (index != j)
                                {
                                    B[j] = (long)(B[j] * x);
                                    B[j] %= mod;
                                }
                            }
                    }
                    else if (Convert.ToInt64(temp[2]) == 0)
                    {
                        A[index] = 0;
                        var t = B[index];
                        B = new long[A.Length];
                        B[index] = t;
                    }
                    else
                    {
                        A[index] = Convert.ToInt64(temp[2]);
                        B = Manner(A);
                    }
                }
            }
        }

        static long DivorceOfTheSeven(string n)
        {
            double temp = 0;
            for (int i = 0; i < n.Length; i++)
            {
                temp *= 10;
                temp += (n[i] - '0');
                temp %= 7;
            }

            return (long)temp;
        }

        static int Fib(int n)
        {
            int[] arr = new int[n];
            arr[0] = arr[1] = 2;
            for (int i = 2; i < n; i++)
            {
                arr[i] = arr[i - 1] + arr[i - 2];
            }

            return arr[n - 1];
        }

        static void PowersOfTwo(long n, long k)
        {
            if (n == k)
            {
                Console.WriteLine("YES");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(1 + " ");
                }
                return;
            }

            long sum = k;
            var arr = new long[k];
            for (int i = 0; i < k; i++) arr[i] = 1;

            for (long i = k - 1; i >= 0; i--)
            {
                while (arr[i] + sum <= n)
                {
                    sum += arr[i];
                    arr[i] *= 2;
                }
            }

            if (sum != n)
            {
                Console.WriteLine("NO");
                return;
            }
            else
            {
                Console.WriteLine("YES");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                return;
            }
        }

        static void Powers(long n, int k)
        {
            if (n < k)
            {
                Console.WriteLine("NO");
                return;
            }

            if (n == k)
            {
                Console.WriteLine("YES");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(1 + " ");
                }
                return;
            }

            int j = 0;
            var list = new List<long>();
            long count = 0;
            var a = 0;
            while (n > 0)
            {
                if ((n & 1) == 1)
                {
                    list.Add(a);
                    count++;
                }
                a++;
                n >>= 1;
            }

            if (count > k)
            {
                Console.WriteLine("NO");
            }

            else
            {
                j = 0;
                var arr = new long[k];
                long temp = k - list.Count;
                while (temp >= 0)
                {
                    temp--;
                    var index = j++;
                    if (list[index] != 0)
                    {
                        list.Add(list[index] - 1);
                        list.Add(list[index] - 1);
                        list.Remove(list[index]);
                    }

                }
            }
        }
    }
}
