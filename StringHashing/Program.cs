using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace StringHashing
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Timus1685()
        {
            word = Console.ReadLine();
            answer = new char[word.Length];
            FindRiddle(0, word.Length - 1);
            foreach (var a in answer)
                Console.Write(a);
        }

        public static string word;
        public static int index = 0;
        public static char[] answer;
        public static void FindRiddle(int start, int end)
        {
            if (start > end) return;
            answer[(start + end) / 2] = word[index++];
            FindRiddle(start, (start + end) / 2 - 1);
            FindRiddle((start + end) / 2 + 1, end);
        }

        static void CostofDataHK()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var trie = new Trie();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                trie.InsertWord(input[0]);
            }

            Console.WriteLine(trie.CountNode);
        }

        static void Timus1542()
        {
            var trie = new Trie();
            var N = Convert.ToInt32(Console.ReadLine());
            var dict = new Dictionary<string, int>();
            for (int i = 0; i < N; i++)
            {
                var input = Console.ReadLine().Split();
                var str = input[0];
                var n = int.Parse(input[1]);
                dict.Add(str, n);
                trie.InsertWord(str);
            }
            var words = dict.OrderByDescending(x => x.Value).ThenBy(x => x.Key);

            var m = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < m; i++)
            {
                var s = Console.ReadLine();
                var list = trie.GetWordsForPrefix(s);
                var count = 0;
                var all = words.Where(x => list.Contains(x.Key));
                foreach (var w in all)
                {
                    count++;
                    Console.WriteLine(w.Key);
                    if (count == 10)
                        break;
                }
                if (i != m - 1)
                    Console.WriteLine();
            }
        }

        static void TriesHK()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var trie = new Trie();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                if (input[0] == "add")
                {
                    trie.InsertWord(input[1]);
                }
                else if (input[0] == "find")
                {
                    Console.WriteLine(trie.GetWordsForPrefix(input[1]).Count);
                }
            }
        }

        static void Autocompletion()
        {
            var N = int.Parse(Console.ReadLine());
            var dict = new Dictionary<string, int>();
            for (var i = 0; i < N; i++)
            {
                var input = Console.ReadLine().Split();
                var str = input[0];
                var n = int.Parse(input[1]);
                dict.Add(str, n);
            }

            var trie = new Trie();
            var m = Convert.ToInt32(Console.ReadLine());
            var prefix = new string[m];

            for (int i = 0; i < m; i++)
            {
                var s = Console.ReadLine();
                trie.InsertWord(s);
                prefix[i] = s;
            }

            //foreach (var d in dict)
            //    trie.SearchPrefix(d.Key, d.Value);

            for (var i = 0; i < m; i++)
            {
                Print(prefix[i], trie);
                if (i != m - 1)
                    Console.WriteLine();
            }
        }

        public static int count;
        public static void Print(string str, Trie trie)
        {
            //var node = trie.root;
            //foreach (var index in str.Select(s => s - 'a'))
            //{
            //    if (node.Children[index] == null)
            //    {
            //        count = 0;
            //        return;
            //    }
            //    node = node.Children[index];
            //}

            //count = node.words.Count >= 10 ? 10 : node.words.Count;
            //int t = 0;
            //foreach (var word in node.words)
            //{
            //    t++;
            //    Console.WriteLine(word.Item2);
            //    if (t == count) break;
            //}
        }

        static void PhoneNumbers()
        {
            Dictionary<char, string> dict = new Dictionary<char, string>();
            List<string> answer = new List<string>();
            dict.Add('1', "ij");
            dict.Add('2', "abc");
            dict.Add('3', "def");
            dict.Add('4', "gh");
            dict.Add('5', "kl");
            dict.Add('6', "mn");
            dict.Add('7', "prs");
            dict.Add('8', "tuv");
            dict.Add('9', "wxy");
            dict.Add('0', "oqz");

            var phoneNumber = "";
            while ((phoneNumber = Console.ReadLine()) != "-1")
            {
                var n = int.Parse(Console.ReadLine());
                var words = new List<string>();
                for (var k = 0; k < n; k++)
                {
                    words.Add(Console.ReadLine());
                }

            }
        }

        static void TwoStrings()
        {
            var s1 = Console.ReadLine();
            var s2 = Console.ReadLine();
            var h = new int[256];
            h[0] = 0;
            for (int j = 1; j < s1.Length; j++)
                h[s1[j]]++;

            for (int i = 0; i < s2.Length; i++)
            {
                if (h[s2[i]] != 0)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            Console.WriteLine("NO");

        }

        static void PalindromePairs()
        {
            var dict = new Dictionary<string, int>();
            var count = new int[28];
            var words = new string[100010];
            var t = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= t; i++)
            {
                var s = Console.ReadLine();

                for (int j = 0; j < s.Length; j++)
                    count[j - 'a']++;

                var temp = "";
                for (int j = 0; j < 26; j++)
                    temp += (count[j] % 2) + '0';

                words[i] = s;
                if (dict.ContainsKey(temp))
                    dict[temp]++;
                else dict.Add(temp, 1);
            }

            BigInteger ans = BigInteger.Zero;
            for (int i = 1; i <= t; i++)
            {
                char[] temp = words[i].ToCharArray();
                BigInteger.Add(ans, dict[new string(temp)] - 1);
                for (int j = 0; j < 26; j++)
                {
                    temp = words[i].ToCharArray();
                    if (temp[j] == '0')
                        temp[j] = '1';
                    else temp[j] = '0';
                    ans += dict[new string(temp)];
                }
            }

            Console.WriteLine(ans >> 1);
        }

        static void CommonDivisors()
        {
            var s = Console.ReadLine();
            var t = Console.ReadLine();

            var hashS = new Hashing(s + s);
            var hashT = new Hashing(t + t);

            var ans = 0;
            var len = Math.Min(s.Length, t.Length);
            for (var i = 1; i <= len; i++)
            {
                if (IsEqual(hashS, i) && IsEqual(hashT, i) &&
                    hashS.Hash(0, i - 1) == hashT.Hash(0, i - 1))
                    ans++;
            }

            Console.WriteLine(ans);
        }

        static bool IsEqual(Hashing hash, int len)
        {
            var n = hash.Size() / 2;
            if (len == n) return true;
            if (n % len != 0) return false;
            return hash.Hash(0, n - 1) == hash.Hash(len, len + n - 1);
        }

        static void ORInMatrix()
        {
            var mn = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int m = mn[0];
            int n = mn[1];
            int[,] matrix = new int[m, n];
            int[,] A = new int[m, n];
            int[,] B = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                var temp = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                for (int j = 0; j < n; j++)
                {
                    B[i, j] = temp[j];
                    A[i, j] = 1;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (B[i, j] == 0)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            A[i, k] = 0;
                        }

                        for (int k = 0; k < m; k++)
                        {
                            A[k, j] = 0;
                        }
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (A[i, j] == 1)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            matrix[i, k] = 1;
                        }

                        for (int k = 0; k < m; k++)
                        {
                            matrix[k, j] = 1;
                        }
                    }
                }
            }

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (matrix[i, j] != B[i, j])
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
            }

            Console.WriteLine("YES");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(A[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        static void VitaliyAndPie()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            n = 2 * n - 2;
            var str = Console.ReadLine();
            int ans = 0;
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < n; i++)
            {
                if (char.IsLower(str[i]))
                {
                    if (dict.ContainsKey(str[i]))
                        dict[str[i]]++;
                    else dict.Add(str[i], 1);
                }
                else
                {
                    var a = char.ToLower(str[i]);
                    if (dict.ContainsKey(a) && dict[a] > 0)
                        --dict[a];
                    else
                        ++ans;
                }
            }

            Console.WriteLine(ans);
        }

        static void Winner()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, long> dict = new Dictionary<string, long>();
            long[] score = new long[n];
            string[] name = new string[n];
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine().Split(' ');
                name[i] = temp[0];
                score[i] = Convert.ToInt64(temp[1]);

                if (dict.Keys.Contains(name[i]))
                    dict[name[i]] += score[i];
                else
                    dict.Add(name[i], score[i]);
            }

            var max = dict.Max(a => a.Value);
            var current = new Dictionary<string, long>();
            for (int i = 0; i < n; i++)
            {
                if (current.Keys.Contains(name[i]))
                    current[name[i]] += score[i];
                else
                    current.Add(name[i], score[i]);
                if (current[name[i]] >= max && dict[name[i]] == max)
                {
                    Console.WriteLine(name[i]);
                    break;
                }
            }
        }

        static void Registration()
        {
            long n = Convert.ToInt64(Console.ReadLine());
            Dictionary<long, long> dict = new Dictionary<long, long>();
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine();
                var hash = HashString(temp);
                if (dict.Keys.Contains(hash))
                {
                    Console.WriteLine(temp + dict[hash]);
                    dict[hash]++;
                }
                else
                {
                    dict.Add(hash, 1);
                    Console.WriteLine("OK");
                }
            }
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
    }
    readonly struct Hashing
    {
        private readonly string s;
        private readonly long[] hash;
        private readonly long[] power;

        public Hashing(string str)
        {
            this.s = str;
            power = new long[200000];
            hash = new long[s.Length];
            hash[0] = s[0];
            power[0] = 1;
            for (var j = 1; j < power.Length; j++)
                power[j] = power[j - 1] * 33;
            for (var i = 1; i < s.Length; i++)
                hash[i] = hash[i - 1] * 33 + s[i];
        }

        public long Hash(int i, int j)
        {
            var ans = hash[j];
            if (i - 1 >= 0)
                ans -= hash[i - 1] * power[j - i + 1];
            return ans;
        }

        public int Size()
        {
            return s.Length;
        }
    }
}
