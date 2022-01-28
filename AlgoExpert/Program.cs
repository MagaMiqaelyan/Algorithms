﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            //LongestPeak(new int[] { 1, 1, 3, 2, 1 });
            //LongestPalindromicSubstring("5abbba5");
            //groupAnagrams(new List<string> { "yo", "act", "flop", "tac", "foo", "cat", "oy", "olfp" });
            //ValidIPAddresses("1921680");
            //Console.WriteLine(ReverseWordsInString("tim    4"));
            // MinimumCharactersForWords(new string[] { "this", "that", "did", "deed", "them!", "a"});
            //var xy = PatternMatcher("xxx", "sassassas");
            // LongestBalancedSubstring("())()(()())");
            // KMP("this is a man", "mamanghmanan");
            // CaesarCypherEncryptor("abc", 52);
            //GenerateDocument("     ", "     ");
            //UnderscorifySubstring("testthis is a testtest to see if testestest it works", "test");
            // LongestSubstringWithoutDuplication("abc");
            //var c = SmallestSubstringContaining("abcd$ef$axb$c$", "$$abf");
        }

        public static string SmallestSubstringContaining(string bigstring, string smallstring)
        {
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < smallstring.Length; i++)
            {
                if (dict.ContainsKey(smallstring[i]))
                    dict[smallstring[i]]++;
                else dict.Add(smallstring[i], 1);
            }

            var cnt = dict.Count;
            var left = 0;
            var right = 0;
            var mins = new int[] { 0, int.MaxValue };
            for (int i = 0; i < bigstring.Length; i++)
            {
                if (dict.ContainsKey(bigstring[right]))
                {
                    dict[bigstring[right]]--;
                    if (dict[bigstring[right]] == 0)
                        cnt--;
                }
                if (cnt == 0)
                {
                    if (right - left < mins[1] - mins[0])
                    {
                        mins[1] = right;
                        mins[0] = left;
                        cnt = dict.Count;
                    }

                }
                right++;
            }
            return bigstring.Substring(mins[0], mins[1] - mins[0]);
        }

        //clementisacap
        public static string LongestSubstringWithoutDuplication(string str)
        {
            var startIndex = 0;
            var left = 0;
            var right = 1;
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (dict.ContainsKey(c))
                {
                    startIndex = Math.Max(startIndex, dict[c] + 1);
                    dict[c] = i;
                }
                else
                {
                    dict.Add(c, i);
                }

                if (right - left < i + 1 - startIndex)
                {
                    right = i + 1;
                    left = startIndex;
                }
            }
            return str.Substring(left, right - left);
        }

        public static string UnderscorifySubstring(string str, string substring)
        {
            var list = KMP(str, substring);
            var result = new StringBuilder();
            var k = 0;
            for (int i = 0; i < list.Count; i++)
            {
                //if (k < list.Count && i == list[k])
                //{
                //    result.Append('_');

                //    var left = list[k];
                //    var right = 0;
                //    while (i < str.Length && k + 1 < list.Count
                //        && list[k + 1] - list[k] <= substring.Length)
                //    {
                //        k++;
                //    }

                //    if (left == list[k])
                //    {
                //        result.Append(substring);
                //        i += substring.Length - 1;
                //        k++;
                //    }
                //    else
                //    {
                //        right = list[k] + substring.Length;
                //        result.Append(str.Substring(left, right - left));
                //        i = right - 1;
                //        k++;

                //    }
                //    result.Append('_');
                //}
                //else
                //{
                //    result.Append(str[i]);
                //}
            }

            return result.ToString();
        }

        public static bool GenerateDocument(string characters, string document)
        {
            var arr = new int[256];
            for (int i = 0; i < characters.Length; i++)
                arr[(int)characters[i]]++;
            for (int i = 0; i < document.Length; i++)
                arr[(int)document[i]]--;

            for (int i = 0; i < 256; i++)
            {
                if (arr[i] < 0)
                    return false;
            }
            return true;
        }

        public static List<bool> MultistringSearch(string bigstring, string[] smallstrings)
        {
            //var res = new List<bool>();
            //for (int i = 0; i < smallstrings.Length; i++)
            //{
            //    if (KMP(bigstring, smallstrings[i]) != -1)
            //        res.Add(true);
            //    else
            //        res.Add(false);
            //}
            //return res;
            return null;
        }

        // this is a man,   mamanghmanan
        //                  001200012345 
        public static List<int> KMP(string s, string pattern)
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
            var list = new List<int>();
            while (m < s.Length)
            {
                if (s[m] == pattern[n])
                {
                    m++;
                    n++;
                }

                // abacd   n
                // abababacd  m
                if (n == pattern.Length)
                {
                    list.Add(m - n);
                    n = 0;
                    if (m != 0)
                        m--;
                }
                else if (m < s.Length && s[m] != pattern[n])
                {
                    if (n == 0)
                        m++;
                    else
                        n = patterns[n - 1];
                }

            }

            return list;
        }
        //(()))(
        public static int LongestBalancedSubstring(string str)
        {
            var maxCount = 0;
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = i + 1; j < str.Length; j++)
                {
                    var temp = str.Substring(i, j - i + 1);
                    if (temp.Length % 2 != 0) continue;
                    if (IsBalanced(temp))
                    {
                        maxCount = Math.Max(maxCount, temp.Length);
                    }
                }
            }
            return maxCount;
        }

        public static bool IsBalanced(string str)
        {
            var stack = new Stack<char>();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    stack.Push(str[i]);
                }
                else
                {
                    if (stack.Count == 0) return false; // ))
                    if (stack.Peek() == '(')
                    {
                        stack.Pop();
                    }
                }
            }
            return stack.Count == 0;
        }

        //xxyxxy //gogopowerrangegogopowerrange = 30
        // xxx   sassassas
        public static string[] PatternMatcher(string pattern, string str)
        {
            var xCount = PatternCount(pattern, 'x'); // 4
            var yCount = PatternCount(pattern, 'y'); // 2
            if (xCount == 0 || yCount == 0)
                return new string[] { "", str.Substring(0, str.Length / pattern.Length) };
            var xStartIndx = pattern.IndexOf('x'); // 0
            var yStartIndx = pattern.IndexOf('y');// 2
            for (int i = 1; i < str.Length; i++)
            {
                var xLength = i; // 1
                if (xCount * xLength > str.Length) return new string[] { };
                var r = (str.Length - xCount * xLength) % yCount; // 0
                if (r != 0) continue;
                var yLength = (str.Length - xCount * xLength) / yCount; // 13
                string x = "", y = "";
                if (xStartIndx == 0)
                {
                    x = str.Substring(0, xLength);
                    y = str.Substring(yStartIndx * xLength, yLength);
                }
                else
                {
                    y = str.Substring(0, yLength);
                    x = str.Substring(xStartIndx * yLength, xLength);
                }

                var result = new StringBuilder();
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (pattern[j] == 'x')
                    {
                        result.Append(x);
                    }
                    else
                    {
                        result.Append(y);
                    }
                }
                if (result.ToString() == str)
                    return new string[] { x, y };
            }

            return new string[] { };
        }

        public static int PatternCount(string pattern, char current)
        {
            return pattern.Where(p => p == current).Count();
        }

        public static string[] MinimumCharactersForWords(string[] words)
        {
            var dict = new Dictionary<char, List<char>>();
            for (int i = 0; i < words.Length; i++)
            {
                var temp = new Dictionary<char, int>();
                foreach (var w in words[i])
                {
                    if (temp.ContainsKey(w))
                        temp[w]++;
                    else
                        temp.Add(w, 1);

                }

                foreach (var t in temp)
                {
                    if (dict.ContainsKey(t.Key))
                    {
                        if (dict[t.Key].Count < t.Value)
                        {
                            var cnt = t.Value - dict[t.Key].Count;
                            while (cnt > 0)
                            {
                                dict[t.Key].Add(t.Key);
                                cnt--;
                            }
                        }
                    }
                    else
                    {
                        dict.Add(t.Key, new List<char>());
                        var cnt = t.Value;
                        while (cnt > 0)
                        {
                            dict[t.Key].Add(t.Key);
                            cnt--;
                        }
                    }

                }
            }
            var result = new List<string>();
            foreach (var item in dict.Values)
            {
                result.AddRange(item.Select(s => s.ToString()));
            }
            return result.ToArray();
        }

        public static string ReverseWordsInString(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            var temp = new StringBuilder(str[0]);
            var stack = new Stack<string>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ')
                    temp.Append(str[i]);
                else
                {
                    stack.Push(temp.ToString());
                    temp.Clear();
                }
            }
            stack.Push(temp.ToString());
            return string.Join(" ", stack);
        }

        // 1921680  107  2310
        public static List<string> ValidIPAddresses(string str)
        {
            var result = new List<string>();
            if (str.Length <= 3) return result;
            for (int i = 1; i < 4; i++)
            {
                var sub1 = str.Substring(0, i);
                if (!IsValidIp(sub1)) continue;
                for (int j = i + 1; j < Math.Min(4, str.Length - i) + i; j++)
                {
                    var sub2 = str.Substring(i, j - i);
                    if (!IsValidIp(sub2)) continue;
                    for (int k = j + 1; k < Math.Min(4, str.Length - j) + j; k++)
                    {
                        var sub3 = str.Substring(j, k - j);
                        var sub4 = str.Substring(k);
                        if (!IsValidIp(sub3) || !IsValidIp(sub4)) continue;
                        result.Add(string.Join(".", sub1, sub2, sub3, sub4));
                    }
                }
            }

            return result;
        }

        public static bool IsValidIp(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            var ipInt = int.Parse(ip);
            if (ipInt > 255) return false;
            return ip.Length == ipInt.ToString().Length;
        }

        // "yo", "act", "flop", "tac", "foo", "cat", "oy", "olfp" 
        public static List<List<string>> groupAnagrams(List<string> words)
        {
            var anagrams = new Dictionary<string, List<string>>();
            foreach (var w in words)
            {
                var sortedW = new string(w.OrderBy(x => x).ToArray());
                if (anagrams.ContainsKey(sortedW))
                    anagrams[sortedW].Add(w);
                else
                    anagrams.Add(sortedW, new List<string> { w });
            }
            return anagrams.Values.ToList();
        }

        //abaxyzzyx  5abbba5
        public static string LongestPalindromicSubstring(string str)
        {
            var res = str[0].ToString();
            for (int i = 0; i < str.Length; i++)
            {
                var odd = PalindromicSubstring(i - 1, i + 1, str);
                var even = PalindromicSubstring(i - 1, i, str);
                var temp = odd.Length > even.Length ? odd : even;
                if (res.Length < temp.Length)
                    res = temp;
            }
            return res;


            //var res = "";
            //for (int i = 0; i < str.Length; i++)
            //{
            //    for (int j = i; j < str.Length; j++)
            //    {
            //        var temp = str.Substring(i, j - i + 1);
            //        if (ReverseString(temp) == temp && temp.Length > res.Length)
            //            res = temp;
            //    }
            //}
            //return res;
        }

        private static string PalindromicSubstring(int left, int right, string str)
        {
            var res = "";
            while (left >= 0 && right < str.Length)
            {
                if (str[left] == str[right])
                {
                    var temp = str.Substring(left, right - left + 1);
                    if (res.Length < temp.Length)
                        res = temp;

                    left--;
                    right++;
                }
                else
                {
                    break;
                }
            }
            return res;
        }

        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
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
