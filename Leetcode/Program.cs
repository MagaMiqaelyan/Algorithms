using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
            children = new List<Node>();
        }

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Examples
            //var n1 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9)))))));
            //var n2 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))));
            //Console.WriteLine(AddTwoNumbers(n1, n2));
            //Console.WriteLine(GetSum(-16, -115));
            //LetterCasePermutation("a1b2");
            //TwoSum(new[] { 3, 3 }, 6);
            //TwoSumSecond(new[] {2, 7, 11, 15}, 9);
            //LengthOfLongestSubstring("pwwkew");
            //Convert("AB", 1);
            //Console.WriteLine(MyAtoi(" -"));
            //Reverse(-125);
            //ThreeSum(new[] { -2, 0, 0, 2, 2 });

            //var n1 = new ListNode(1, new ListNode(2, new ListNode(4)));
            //var n2 = new ListNode(1, new ListNode(3, new ListNode(4)));
            //MergeTwoLists(n1, n2);
            //var d = MergeTwoListsRec(n1, n2);

            //Console.WriteLine(IntToRoman(3));
            //Console.WriteLine(IntToRoman(4));
            //Console.WriteLine(IntToRoman(9));
            //Console.WriteLine(IntToRoman(58));
            //Console.WriteLine(IntToRoman(1994));
            //Console.WriteLine(NumPairsDivisibleBy60(new int[] { 30, 20, 150, 100, 40 }));
            //Console.WriteLine(ThreeSumClosest(new int[] { 1,1,1,0 }, -100));
            // Console.WriteLine(MaxArea(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }));
            //var n1 = new ListNode(1);
            //var removed = RemoveNthFromEnd(n1, 1);
            //Console.WriteLine(removed);
            //var r = SearchRange(new int[] { 5}, 5);
            //Console.WriteLine($"{r[0]}, {r[1]}");
            // Console.WriteLine(Divide(-2147483648, -1));
            // Console.WriteLine(CountAndSay(10));
            //Console.WriteLine(string.Join(" ", AvoidFlood(new[] { 1, 0, 2, 0, 3, 0, 2, 0, 0, 0, 1, 2, 3 })));

            // Console.WriteLine(CountPairs(new[] { 2160, 1936, 3, 29, 27, 5, 2503, 1593, 2, 0, 16, 0, 3860, 28908, 6, 2, 15, 49, 6246, 1946, 23, 105, 7996, 196, 0, 2, 55, 457, 5, 3, 924, 7268, 16, 48, 4, 0, 12, 116, 2628, 1468 }));
            // Console.WriteLine(CountPairs(new[] { 1, 1, 1, 3, 3, 3, 7 }));

            //var n1 = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            //Console.WriteLine(SwapNodes(n1, 2));
            //SortArrayByParity(new int[] { 3, 1, 2, 4 });
            //KnightProbability(8, 30, 6, 4);
            //Console.WriteLine(IsPalindrome(1000000001));
            //AddStrings("9", "99");

            //Node root = new Node(1);

            //root.children.Add(new Node(3));
            //root.children.Add(new Node(2));
            //root.children.Add(new Node(4));

            //root.children[0].children.Add(new Node(5));
            //root.children[0].children.Add(new Node(6));            
            //Console.WriteLine(string.Join(" ", Postorder(root)));
            //Console.WriteLine(SingleNumber(new int[] { 1, 2, 1, 17, 2, 5 }));           
            //var treeNode = new TreeNode(5, new TreeNode(3, new TreeNode(2), new TreeNode(4)),
            //    new TreeNode(6, null, new TreeNode(6, null, new TreeNode(7))));
            //FindTarget(treeNode, 100);
            //LetterCombinations("234");
            //  Trap(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 });
            // Console.WriteLine(BuddyStrings("ab","ca"));
            //
            //int[][] jaggedArray = new int[][]
            //{
            //    new int[] { 3,3,1,1},
            //    new int[] {2,2,1,2},
            //    new int[] { 1,1,1,2}
            //};
            //var m = DiagonalSort(jaggedArray);
            //for (int i = 0; i < m.Length; i++)
            //{
            //    Console.WriteLine(string.Join(" ", m[i]));
            //}
            // Console.WriteLine(CountCharacters(new string[] { "hello", "world", "leetcode" }, "welldonehoneyr"));

            Console.WriteLine(NumRollsToTarget(30, 30, 500));
            #endregion
        }

        //public static int NumRollsToTarget(int n, int k, int target)
        //{
        //    int test;

        //    var mod = (int)Math.Pow(10, 9) + 7;
        //    var res = new int[n + 1, target + 1];
        //    for (int i = 0; i < n+1; i++)
        //    {
        //        for (int j = 0; j < target+1; j++)
        //        {
        //            res[i, j] = -1;
        //        }
        //    }
        //    return Rolls(n, k, target, res, mod);
        //}
        public static int Rolls(int n, int k, int target, int[,] result, int mod)
        {
            if (n == 0 && target == 0) return 1;
            if (n <= 0 || target <= 0) return 0;
            if (result[n, target] != -1) return result[n, target];

            long res = 0;

            for (int i = 1; i <= k; i++)
                res = res % mod  + Rolls(n - 1, k, target - i, result, mod) % mod;

            result[n, target] = (int)res % mod;
            return result[n, target];
        }
        public static int CountCharacters(string[] words, string chars)
        {
            var dict = new Dictionary<char, int>();
            foreach (var c in chars)
            {
                if (dict.ContainsKey(c))
                    dict[c]++;
                else dict.Add(c, 1);
            }
            var res = 0;
            foreach (var word in words)
            {
                var d = new Dictionary<char, int>();
                var b = false;

                foreach (var w in word)
                {
                    if (!dict.ContainsKey(w))
                    {
                        b = true;
                        break;
                    }
                    if (d.ContainsKey(w)) d[w]++;
                    else d.Add(w, 1);
                }
                if (!b)
                {
                    var c = false;
                    foreach (var item in d)
                    {
                        if (dict[item.Key] < item.Value)
                        {
                            c = true;
                            break;
                        }
                    }
                    if (!c) res += word.Length;
                }
            }
            return res;
        }
        public static int[][] DiagonalSort(int[][] mat)
        {
            var row = mat.Length;
            var column = mat[0].Length;
            for (int i = 0; i < row; i++)
            {
                var numbers = new List<int>();
                int x = i, y = 0;
                while (x < row && y < column)
                    numbers.Add(mat[x++][y++]);
                numbers.Sort();
                x = i;
                y = 0;
                foreach (var n in numbers)
                    mat[x++][y++] = n;
            }

            for (int i = 1; i < column; i++)
            {
                var numbers = new List<int>();
                int x = 0, y = i;
                while (x < row && y < column)
                    numbers.Add(mat[x++][y++]);
                numbers.Sort();
                x = 0;
                y = i;
                foreach (var n in numbers)
                    mat[x++][y++] = n;
            }
            return mat;
        }

        public static bool BuddyStrings(string s, string goal)
        {
            if (s.Length != goal.Length) return false;
            var indecies = new List<int>();
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != goal[i])
                    indecies.Add(i);
                if (dict.ContainsKey(s[i]))
                    dict[s[i]]++;
                else dict.Add(s[i], 1);
            }

            return (indecies.Count == 2 && (s[indecies[0]] == goal[indecies[1]] && s[indecies[1]] == goal[indecies[0]]))
                ||
                (indecies.Count == 0 && dict.Values.Any(x => x >= 2)) ? true : false;
        }

        public static ListNode CopyRandomList(ListNode head)
        {
            if (head == null) return null;

            var result = new ListNode(head.val);
            var current = result;
            var temp = head;

            var dict = new Dictionary<ListNode, ListNode>()
            {
                { temp, current }
            };

            while (temp.next != null)
            {
                current.next = new ListNode(temp.next.val);
                dict.Add(temp.next, current.next);

                temp = temp.next;
                current = current.next;
            }

            temp = head;
            while (temp != null)
            {
                dict[temp].random = temp.random == null ? null : dict[temp.random];
                temp = temp.next;
            }
            return result;
        }

        public static int Trap(int[] height)
        {
            var res = 0;
            var left = new int[height.Length];
            left[0] = height[0];

            var right = new int[height.Length];
            right[height.Length - 1] = height[height.Length - 1];

            for (int j = 1; j < height.Length; j++)
                left[j] = Math.Max(height[j], left[j - 1]);

            for (int j = height.Length - 2; j >= 0; j--)
                right[j] = Math.Max(height[j], right[j + 1]);

            for (int i = 0; i < height.Length; i++)
                res += Math.Min(left[i], right[i]) - height[i];

            return res;
        }

        public static IList<string> LetterCombinations(string digits)
        {
            if (digits == string.Empty) return new List<string>();
            var dict = new Dictionary<int, List<string>>
            {
                { 2, new List<string>{"a", "b","c"} },
                { 3, new List<string>{"d", "e","f"} },
                { 4, new List<string>{"g", "h","i"} },
                { 5, new List<string>{"j", "k","l"} },
                { 6, new List<string>{"m", "n","o"} },
                { 7, new List<string>{"p", "q","r", "s"} },
                { 8, new List<string>{"t", "u","v"} },
                { 9, new List<string>{"w", "x","y", "z"} },
            };
            if (digits.Length == 1)
                return dict[int.Parse(digits)];
            var result = new List<string>();
            var queue = new Queue<string>();
            queue.Enqueue("");

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Length == digits.Length)
                {
                    result.Add(current);
                }
                else
                {
                    var chars = dict[(int)(digits[current.Length] - '0')];
                    foreach (var c in chars)
                        queue.Enqueue(current + c);
                }
            }

            return result;
        }

        public static bool FindTarget(TreeNode root, int k)
        {
            var exist = DFS(root, k, new Dictionary<int, int>());
            return exist;
        }
        public static bool DFS(TreeNode root, int sum, Dictionary<int, int> summary)
        {
            if (root == null) return false;

            if (summary.Values.Contains(root.val))
                return true;


            if (!summary.ContainsKey(root.val))
                summary.Add(root.val, sum - root.val);


            if (root.left != null)
            {
                if (DFS(root.left, sum, summary))
                    return true;
            }

            if (root.right != null)
            {
                if (DFS(root.right, sum, summary))
                    return true;
            }
            return false;
        }
        public static int[] SingleNumber(int[] nums)
        {
            var result = new int[2] { 0, 0 };
            var xor = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                xor ^= nums[i];
            }

            var b = xor - (xor & (xor - 1));
            for (int i = 0; i < nums.Length; i++)
            {
                if ((nums[i] & b) == 0) // 0010 = 2, 0011 = 3, 0101 = 5, 5 ^ 3 = 0110 = 6
                {
                    result[0] ^= nums[i];
                }
            }
            result[1] = xor ^ result[0];
            return result;
        }

        public static IList<int> Postorder(Node root)
        {
            var result = new List<int>();
            var currentIndex = 0;
            var stack = new Stack<Tuple<Node, int>>();
            while (root != null || stack.Count != 0)
            {
                if (root != null)
                {
                    stack.Push(Tuple.Create(root, currentIndex));
                    currentIndex = 0;
                    root = root.children.Count > 0 ? root.children[0] : null;
                    continue;
                }
                var temp = stack.Pop();
                result.Add(temp.Item1.val);

                while (stack.Count != 0 && temp.Item2 == stack.Peek().Item1.children.Count - 1)
                {
                    temp = stack.Pop();
                    result.Add(temp.Item1.val);
                }

                if (stack.Count != 0)
                {
                    currentIndex = temp.Item2 + 1;
                    root = stack.Peek().Item1.children[currentIndex];
                }
            }
            return result;

        }


        public static string AddStrings(string num1, string num2)
        {
            var result = new StringBuilder();
            if (num1.Length > num2.Length)
            {
                var temp = num1;
                num1 = num2;
                num2 = temp;
            }
            var isRemind = false;
            num1 = new string(num1.ToCharArray().Reverse().ToArray());
            num2 = new string(num2.ToCharArray().Reverse().ToArray());
            for (int i = 0; i < num1.Length; i++)
            {
                int c1 = num1[i] - '0';
                int c2 = num2[i] - '0';

                var c = c1 + c2 + (isRemind ? 1 : 0);
                isRemind = c >= 10 ? true : false;
                result.Append(c % 10);
            }
            for (int i = num1.Length; i < num2.Length; i++)
            {
                int c2 = num2[i] - '0';
                var c = c2 + (isRemind ? 1 : 0);
                isRemind = c >= 10 ? true : false;
                result.Append(c % 10);
            }
            if (isRemind)
            {
                result.Append("1");
            }
            var ans = new StringBuilder();
            for (int i = result.Length - 1; i >= 0; i--)
            {
                ans.Append(result[i]);
            }
            return ans.ToString();
        }

        public static bool IsPalindrome(int x)
        {
            long res = 0;
            var y = x;
            while (x > 0)
            {
                var r = x % 10;
                res = (res + r) * 10;
                x /= 10;
            }

            return y == res / 10;
        }

        private static int[] _dx = { 1, 2, 2, 1, -1, -2, -2, -1 };

        private static int[] _dy = { 2, 1, -1, -2, -2, -1, 1, 2 };
        private static double _possibleCount;
        public static double KnightProbability(int n, int k, int row, int column)
        {
            var total = Math.Pow(8, k);
            Prob(row, column, k, n);
            return _possibleCount / total;
        }

        public static void Prob(int row, int column, int k, int n)
        {
            if (k == 0)
            {
                _possibleCount++;
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                var dx = _dx[i] + row;
                var dy = _dy[i] + column;

                if (IsBoard(dx, dy, n))
                {
                    Prob(dx, dy, k - 1, n);
                }
            }
        }

        public static bool IsBoard(int i, int j, int n)
        {
            return i >= 0 && i < n && j >= 0 && j < n;
        }
        public static int[] SortArrayByParity(int[] nums)
        {
            if (nums.Length == 1) return nums;
            var result = new int[nums.Length];
            var odd = nums.Length - 1; var even = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] % 2 == 0)
                {
                    result[even++] = nums[i];
                }
                else
                {
                    result[odd--] = nums[i];
                }
            }
            return result;
        }

        public static IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
        {
            var res = new List<TreeNode>();
            Dfs(root, to_delete, res, true);
            return res;
        }
        public static void Dfs(TreeNode root, int[] to_delete, List<TreeNode> result, bool isRoot)
        {
            var isRootContainDel = to_delete.Contains(root.val);
            if (!isRootContainDel && isRoot)
                result.Add(root);

            isRoot = isRootContainDel;

            if (root.left != null)
            {
                Dfs(root.left, to_delete, result, isRoot);
                if (to_delete.Contains(root.left.val))
                    root.left = null;
            }

            if (root.right != null)
            {
                Dfs(root.right, to_delete, result, isRoot);
                if (to_delete.Contains(root.right.val))
                    root.right = null;
            }
        }

        public static ListNode SwapNodes(ListNode head, int k)
        {
            ListNode temp = head;
            var length = 0;
            while (temp != null)
            {
                length++;
                temp = temp.next;
            }
            if (length < k) return null;
            if (2 * k - 1 == length) return head;

            var i = 1;
            ListNode firstNode = head, firstPrev = null;
            while (i < k)
            {
                i++;
                firstPrev = firstNode;
                firstNode = firstNode.next;
            }

            i = 1;
            ListNode secondNode = head, secondPrev = null;
            while (i < length - k + 1)
            {
                i++;
                secondPrev = secondNode;
                secondNode = secondNode.next;
            }

            if (firstPrev != null)
                firstPrev.next = secondNode;

            if (secondPrev != null)
                secondPrev.next = firstNode;

            temp = firstNode.next;
            firstNode.next = secondNode.next;
            secondNode.next = temp;

            if (k == 1) head = secondNode;
            if (k == length) head = firstNode;

            return head;
        }

        public static ListNode SwapPairs(ListNode head)
        {
            if (head == null) return null;
            var swapped = new ListNode();
            var current = swapped;

            while (head != null)
            {
                if (head.next != null)
                {
                    current.val = head.next.val;
                    current.next = new ListNode(head.val);
                }
                else
                {
                    current.val = head.val;
                }

                if (head.next.next != null)
                    current.next.next = new ListNode();

                head = head.next.next;
                current = current.next.next;
            }
            return swapped;
        }

        public static int CountPairs(int[] deliciousness)
        {
            long mod = (long)Math.Pow(10, 9) + 7;
            var res = 0L;
            var dict = new Dictionary<long, long>();
            for (int i = 0; i < deliciousness.Length; i++)
            {
                if (dict.ContainsKey(deliciousness[i]))
                    dict[deliciousness[i]]++;
                else
                    dict.Add(deliciousness[i], 1);
            }
            for (int i = 0; i < 22; i++)
            {
                var powerTwo = 1 << i;

                foreach (var current in dict.Keys)
                {
                    var second = powerTwo - current;
                    if (dict.ContainsKey(second))
                    {
                        if (second == current)
                        {
                            res += dict[current] * (dict[current] - 1);
                        }
                        else
                        {
                            res += dict[second] * dict[current];
                        }
                    }
                }
            }
            return (int)(res / 2 % mod);
        }

        public static bool IsPowerOfTwo(long x)
        {
            return x != 0 && (x & (x - 1)) == 0;
        }

        public static int[] AvoidFlood(int[] rains)
        {
            var dry = new List<int>();
            var full = new Dictionary<int, int>();
            var ans = new int[rains.Length];

            for (int i = 0; i < rains.Length; i++)
            {
                if (rains[i] == 0)
                {
                    dry.Add(i);
                    ans[i] = 1;
                }
                else
                {
                    if (full.ContainsKey(rains[i]))
                    {
                        var last = full[rains[i]];
                        var canDryLake = false;
                        foreach (var d in dry)
                        {
                            if (d > last)
                            {
                                ans[d] = rains[i];
                                dry.Remove(d);
                                canDryLake = true;
                                break;
                            }
                        }

                        if (!canDryLake)
                        {
                            return new int[] { };
                        }
                        full[rains[i]] = i;
                    }
                    else
                    {
                        full.Add(rains[i], i);
                    }
                    ans[i] = -1;
                }
            }
            return ans;
        }

        public static string CountAndSay(int n)
        {
            if (n == 1) return "1";
            var str = CountAndSay(n - 1);
            var res = new StringBuilder();
            var currentDigit = str[0];
            var currentDigitCount = 1;
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1])
                    currentDigitCount++;
                else
                {
                    res.Append($"{currentDigitCount}{currentDigit}");
                    currentDigitCount = 1;
                    currentDigit = str[i + 1];
                }
            }
            res.Append($"{currentDigitCount}{currentDigit}");
            return res.ToString();
        }
        public static int Divide(int dividend, int divisor)
        {
            var quotient = 0;
            var sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
            long absDividend = Math.Abs((long)dividend);
            long absDivisor = Math.Abs((long)divisor);
            var res = absDividend * sign;
            if (absDivisor == 1) return res >= int.MaxValue ? int.MaxValue : (res <= int.MinValue ? int.MinValue : (int)res);
            while (absDividend >= absDivisor)
            {
                absDividend -= absDivisor;
                quotient++;
            }
            res = sign * quotient;
            return res >= int.MaxValue ? int.MaxValue : (res <= int.MinValue ? int.MinValue : (int)res);
        }

        public static int[] SearchRange(int[] nums, int target)
        {
            var result = new[] { -1, -1 };
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > target) break;
                if (i == 0 && nums[i] == target)
                    result[0] = 0;
                else if (nums[i] == target && i > 0 && nums[i - 1] != nums[i])
                    result[0] = i;

                if (i == nums.Length - 1 && nums[i] == target)
                {
                    result[1] = nums.Length - 1;
                }
                else if (nums[i] == target && i < nums.Length - 1 && nums[i + 1] != nums[i])
                {
                    result[1] = i;
                    break;
                }
            }
            return result;
        }

        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode result = new ListNode();
            ListNode current = result;
            var length = 0;
            var cnt = 0;
            while (head != null)
            {
                length++;
                current.val = head.val;
                if (head.next != null) current.next = new ListNode();
                current = current.next;
                head = head.next;

            }
            if (length == n) return result.next;
            current = result;
            while (current != null)
            {
                cnt++;
                if (length - n == cnt)
                {
                    current.next = current.next?.next;
                }
                else
                {
                    current = current.next;
                }
            }
            return result;
        }

        public static int MaxArea(int[] height)
        {
            var res = 0;
            var start = 0;
            var end = height.Length - 1;
            while (start < end)
            {
                var min = Math.Min(height[start], height[end]);
                var current = (end - start) * min;
                res = Math.Max(res, current);
                while (height[start] <= min && start < end) start++;
                while (height[end] <= min && start < end) end--;
            }
            return res;
        }

        public static int ThreeSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            var closestSum = nums[0] + nums[1] + nums[2];

            for (int i = 0; i < nums.Length - 2; i++)
            {
                var second = i + 1;
                var third = nums.Length - 1;
                while (second < third)
                {
                    var currentSum = nums[i] + nums[second] + nums[third];
                    if (Math.Abs(target - currentSum) < Math.Abs(target - closestSum))
                        closestSum = currentSum;
                    if (currentSum > target)
                        third--;
                    else
                        second++;
                }
            }
            return closestSum;
        }

        public static int NumPairsDivisibleBy60(int[] time)
        {
            if (time.Length == 1) return 0;
            if (time.Length == 2) return (time[0] + time[1]) % 60 == 0 ? 1 : 0;
            var result = 0;
            var arr = new int[60];
            for (int i = 0; i < time.Length; i++)
            {
                var x = time[i] % 60;
                var y = (60 - x) % 60;

                result += arr[y];
                arr[x]++;
            }
            return result;
        }

        public static string IntToRoman(int num)
        {
            var res = new StringBuilder();
            while (num >= 1000)
            {
                res.Append("M");
                num -= 1000;
            }
            while (num >= 900)
            {
                res.Append("CM");
                num -= 900;
            }
            while (num >= 500)
            {
                res.Append("D");
                num -= 500;
            }
            while (num >= 400)
            {
                res.Append("CD");
                num -= 400;
            }
            while (num >= 100)
            {
                res.Append("C");
                num -= 100;
            }
            while (num >= 90)
            {
                res.Append("XC");
                num -= 90;
            }
            while (num >= 50)
            {
                res.Append("L");
                num -= 50;
            }
            while (num >= 40)
            {
                res.Append("XL");
                num -= 40;
            }
            while (num >= 10)
            {
                res.Append("X");
                num -= 10;
            }
            while (num >= 9)
            {
                res.Append("IX");
                num -= 9;
            }
            while (num >= 5)
            {
                res.Append("V");
                num -= 5;
            }
            while (num >= 4)
            {
                res.Append("IV");
                num -= 4;
            }
            while (num >= 1)
            {
                res.Append("I");
                num -= 1;
            }

            return res.ToString();
        }

        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var merged = new ListNode();
            var current = merged;
            if (l2 == null && l1 == null)
                return null;
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;
            while (l1 != null || l2 != null)
            {
                if (l1 != null && l2 != null)
                {
                    if (l1.val < l2.val)
                    {
                        current.val = l1.val;
                        if (l1.next != null || l2 != null)
                        {
                            current.next = new ListNode();
                            current = current.next;
                        }
                        l1 = l1.next;
                    }
                    else
                    {
                        current.val = l2.val;
                        if (l2.next != null || l1 != null)
                        {
                            current.next = new ListNode();
                            current = current.next;
                        }
                        l2 = l2.next;
                    }
                }
                else if (l1 != null)
                {
                    current.val = l1.val;
                    if (l1.next != null)
                    {
                        current.next = new ListNode();
                        current = current.next;
                    }
                    l1 = l1.next;

                }
                else if (l2 != null)
                {
                    current.val = l2.val;
                    if (l2.next != null)
                    {
                        current.next = new ListNode();
                        current = current.next;
                    }
                    l2 = l2.next;

                }
            }

            return merged;
        }

        public static ListNode MergeTwoListsRec(ListNode l1, ListNode l2)
        {
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            if (l1.val < l2.val)
            {
                l1.next = MergeTwoListsRec(l1.next, l2);
                return l1;
            }

            l2.next = MergeTwoListsRec(l1, l2.next);
            return l2;
        }

        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var numbersList = new List<IList<int>>();
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0 || nums[i] > nums[i - 1])
                    ThreeSumRec(nums, numbersList, i, i + 1, nums.Length - 1);
            }
            return numbersList;
        }

        public static void ThreeSumRec(int[] nums, List<IList<int>> result, int i, int start, int end)
        {
            if (start >= end)
                return;

            if (nums[i] + nums[start] + nums[end] == 0)
            {
                result.Add(new List<int> { nums[i], nums[start], nums[end] });
                start++;
                end--;
                while (start < end && nums[start] == nums[start - 1])
                    start++;
                while (start < end && nums[end] == nums[end + 1])
                    end--;
                ThreeSumRec(nums, result, i, start, end);
            }
            else if (nums[i] + nums[start] + nums[end] < 0)
            {
                ThreeSumRec(nums, result, i, start + 1, end);
            }
            else
            {
                ThreeSumRec(nums, result, i, start, end - 1);
            }
        }
        public static int Reverse(int x)
        {
            if (x >= 0 && x <= 9)
                return x;
            var number = x.ToString();
            var s = "";
            var end = 0;
            if (number[0] == '-' || number[0] == '+')
            {
                s += number[0];
                end = 1;
            }

            for (int i = number.Length - 1; i >= end; i--)
            {
                s += number[i];
            }

            if (int.TryParse(s, out var result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static int MyAtoi(string str)
        {
            str = str.Trim();
            if (string.IsNullOrEmpty(str) ||
                !(char.IsDigit(str[0]) || str[0] == '+' || str[0] == '-'))
                return 0;

            if ((str[0] == '+' || str[0] == '-')
                && (str.Length > 1 && !char.IsDigit(str[1])
                || str.Length == 1))
                return 0;

            var number = new StringBuilder(str[0].ToString());
            for (var i = 1; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    number.Append(str[i]);
                else
                    break;
            }

            if (int.TryParse(number.ToString(), out var result))
            {
                return result;
            }
            else if (number[0] == '-')
            {
                return int.MinValue;
            }
            else
            {
                return int.MaxValue;
            }
        }

        public static string Convert(string s, int numRows)
        {
            if (numRows == 1)
                return s;
            var matrix = new string[Math.Min(s.Length, numRows)];
            var row = 0;
            var isChangedRow = false;
            foreach (var c in s)
            {
                matrix[row] += c;
                if (row == 0 || row == numRows - 1)
                    isChangedRow = !isChangedRow;
                row += isChangedRow ? 1 : -1;
            }

            return matrix.Aggregate("", (result, current) => result + current);
        }

        public static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            var charsAndIndices = new Dictionary<char, int>();
            var startIndex = 0;
            var longLength = 0;
            for (int endIndex = 0; endIndex < s.Length; endIndex++)
            {
                if (charsAndIndices.ContainsKey(s[endIndex]))
                {
                    startIndex = Math.Max(startIndex, charsAndIndices[s[endIndex]] + 1);
                }

                charsAndIndices[s[endIndex]] = endIndex;
                longLength = Math.Max(longLength, endIndex - startIndex + 1);
            }

            return longLength;
        }

        public int[] twoSum(int[] numbers, int target)
        {
            int l = 0, h = numbers.Length - 1, sum;

            while ((sum = numbers[l] + numbers[h]) != target && h != l)
            {
                if (sum > target)
                    h = BinarySearch(numbers, l + 1, h - 1, target - numbers[l]);
                else if (sum < target)
                    l = BinarySearch(numbers, l + 1, h - 1, target - numbers[h]);
            }
            return new[] { l + 1, h + 1 };
        }

        private static int BinarySearch(int[] numbers, int low, int high, int target)
        {
            while (low < high)
            {
                var mid = (low + high) / 2;
                if (target == numbers[mid])
                    return mid;
                else if (target < numbers[mid])
                    high = mid;
                else
                    low = mid + 1;
            }

            return high;
        }

        public static int[] TwoSumSecond(int[] nums, int target)
        {
            var result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        result[0] = i + 1;
                        result[1] = j + 1;
                        return result;
                    }
                }
            }

            return result;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            var result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        result[0] = i;
                        result[1] = j;
                        return result;
                    }
                }
            }

            return result;
        }

        // "a1b24c"
        // "A1b24c"
        // "A1B24c"
        // "A1b24C" ...
        public static IList<string> LetterCasePermutation(string s)
        {
            Permutation(s);
            return res;
        }

        private static List<string> res = new List<string>();
        static void Permutation(string s, int i = 0, string temp = "")
        {
            if (i == s.Length)
            {
                res.Add(temp);
                return;
            }

            Permutation(s, i + 1, temp + s[i]);
            if (char.IsLower(s[i]))
                Permutation(s, i + 1, temp + char.ToUpper(s[i]));
            else if (char.IsUpper(s[i]))
                Permutation(s, i + 1, temp + char.ToLower(s[i]));
        }

        public static int GetSum(int a, int b)
        {
            return b == 0 ? a : GetSum(a ^ b, (a & b) * 2);
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var current = new ListNode();
            var res = current;
            var rem = 0;
            while (l1 != null || l2 != null)
            {
                var n1 = l1?.val ?? 0;
                var n2 = l2?.val ?? 0;
                var sum = n1 + n2 + rem;
                rem = sum / 10;
                current.val = sum % 10;
                if (l1?.next == null && l2?.next == null)
                    break;
                current.next = new ListNode();
                current = current.next;

                l1 = l1?.next;
                l2 = l2?.next;
            }

            if (rem == 1)
                current.next = new ListNode(1);
            return res;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode random;
        public ListNode(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
