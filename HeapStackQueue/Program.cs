using System;
using System.Collections.Generic;

namespace HeapStackQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PrimeNumber(5));
            //int[][] jagged_arr =

            //{

            //   new int[] {1, 2, 100},

            //   new int[] {3, 4, 10},

            //   new int[] {2, 3, 20}

            //};

            //Console.WriteLine(arrayManipulation(4, jagged_arr));aba
            string[] strings = { "baba", "aba", "xzxb", "aba" };
            string[] queries = { "aba", "xzxb", "ab" };
            var res = matchingStrings(strings, queries);

            for (int i = 0; i < res.Length; i++)
            {
                Console.Write(res[i] + " ");
            }
            Console.ReadLine();
        }

        static void waiter(int[] number, int q)
        {
            int prime = PrimeNumber(q);
            Stack<int> A = new Stack<int>();
            Stack<int>[] B = new Stack<int>[prime];
            int j = 0;

            for (int i = number.Length - 1; i <= 0; i--)
            {
                if (number[i] % prime == 0)
                {
                    B[j].Push(number[i]);
                }
            }
        }
        static int PrimeNumber(int n)
        {
            int candidate, count;
            for (candidate = 2, count = 0; count < n; ++candidate)
            {
                if (isPrime(candidate)) ++count;
            }
            return candidate - 1;
        }
        static bool isPrime(int num)
        {
            for (int i = 2; i < num; ++i)
            {
                if (num % i != 0) continue;
                return false;
            }

            return true;
        }
        static int[] matchingStrings(string[] strings, string[] queries)
        {
            int[] res = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < strings.Length; j++)
                {
                    if (queries[i] == strings[j]) count++;

                }
                res[i] = count;
            }
            return res;
        }
        private static int[] LeftRotation(int[] arr, int d)
        {
            int[] res = new int[arr.Length];
            int j = 0;
            for (int i = d; i < arr.Length; i++)
            {
                res[j++] = arr[i];
            }

            for (int i = 0; i < d; i++)
            {
                res[j++] = arr[i];
            }
            return res;
        }
        static long arrayManipulation(int n, int[][] queries)
        {
            long max = 0, current = 0;
            int[] res = new int[n];
            for (int i = 0; i < queries.Length; i++)
            {
                int a = queries[i][0] - 1;
                int b = queries[i][1];
                int op = queries[i][2];
                res[a] += op;
                if (b + 1 < n)
                {
                    res[b] -= op;
                }
            }

            for (int i = 0; i < n; i++)
            {
                current += res[i];
                max = Math.Max(max, current);
            }
            return max;
        }
        private static Stack<char> Neutralisationofcharges(string str)
        {
            Stack<char> st = new Stack<char>();
            st.Push(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                if (st.Count > 0 && st.Peek() == str[i])
                {
                    st.Pop();
                }

                else
                {
                    st.Push(str[i]);
                }
            }

            Console.WriteLine(st.Count);
            int size = st.Count;
            var print = new Stack<char>();
            for (int i = 0; i < size; i++)
            {
                print.Push(st.Pop());
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(print.Pop());
            }
            return print;
        }


    }
}
