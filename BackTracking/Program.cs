using System;
using System.Collections.Generic;
using System.Linq;

namespace BackTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            //MaximumInTable();
            //NQueen();
            // LongestValue();
            // HackTheMoney();
            //HackTheMoney();
            //DivideNumber();
            //ItsConfidential();
            // Watermelon();
            //Team();
            // Grammar();
            //PartitionToKEqualSum();
            // Partition();
            //Letters();
            //NumTilePossibilities();
            VasyaAndSocks();
        }

        static void VasyaAndSocks()
        {
            var nm = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            if (nm != null) Console.WriteLine((nm[0] + (nm[0] - 1) / (nm[1] - 1)));
        }
        public static void NumTilePossibilities()
        {
            int r = -1;
            var s = "CDC";
            var arr = s.ToCharArray();
            arr = arr.OrderBy(x => x).ToArray();
            NumTile(arr, new bool[s.Length], ref r);
        }
        static void NumTile(char[] s, bool[] used, ref int count)
        {
            count++;
            for (int i = 0; i < s.Length; i++)
            {
                if (used[i] || (i > 0 && s[i] == s[i - 1] && !used[i - 1]))
                    continue;
                used[i] = true;
                NumTile(s, used, ref count);
                used[i] = false;
            }
        }
        static void Letters()
        {
            var l = LetterCasePermutation("a1b2");
        }
        public static IList<string> LetterCasePermutation(string S)
        {
            var str = S.ToCharArray();
            var indexes = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetter(str[i]))
                {
                    indexes.Add(i);
                    str[i] = char.ToLower(str[i]);
                }
            }
            if (indexes.Count == 0) return new List<string> { S };
            var res = new List<string>();
            LetterBackTrack(str, 0, indexes, ref res);
            return res;
        }
        public static void LetterBackTrack(char[] s, int i, List<int> indexes, ref List<string> words)
        {
            if (i == indexes.Count)
            {
                words.Add(new string(s));
                return;
            }
            LetterBackTrack(s, i + 1, indexes, ref words);
            s[indexes[i]] = char.ToUpper(s[indexes[i]]);
            LetterBackTrack(s, i + 1, indexes, ref words);
            s[indexes[i]] = char.ToLower(s[indexes[i]]);

        }
        static int CountOfStair(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            if (n == 2)
                return 2;
            return CountOfStair(n - 1) + CountOfStair(n - 2) + CountOfStair(n - 3);
        }
        static void Partition()
        {
            int[] nums = { 2, 2, 3, 5 };
            Console.WriteLine(CanPartition(nums));
        }
        static bool CanPartition(int[] nums)
        {
            if (nums == null || nums.Length < 2) return false;
            int sum = nums.Sum();
            if (sum % 2 != 0 || nums.Max() > sum / 2) return false;
            sum /= 2;
            int row = nums.Length + 1;
            int col = sum + 1;
            var used = new bool[row, col];
            used[0, 0] = true;
            for (int i = 1; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (used[i - 1, j] || (j - nums[i - 1] >= 0 && used[i - 1, j - nums[i - 1]]))
                        used[i, j] = true;
                }
            }
            return used[row - 1, col - 1];
        }

        static void PartitionToKEqualSum()
        {
            int[] nums = { 2, 2, 2, 2, 3, 4, 5 };
            int k = 4;
            Console.WriteLine(CanPartitionKSubsets(nums, k));
        }
        static public bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (nums == null || k > nums.Length) return false;
            int sum = nums.Sum();
            if (sum % k != 0 || nums.Max() > sum / k) return false;
            if (k == 1) return true;
            return Search(nums, sum / k, 0, 0, k, new bool[nums.Length], 0);
        }
        static bool Search(int[] nums, int totalSum, int currentSum, int count, int k, bool[] used, int start)
        {
            if (totalSum == currentSum)
            {
                if (++count == k)
                    return true;
                else
                    return Search(nums, totalSum, 0, count, k, used, 0);
            }
            else if (currentSum < totalSum)
            {
                for (int i = start; i < nums.Length; i++)
                {
                    if (used[i]) continue;
                    used[i] = true;
                    if (Search(nums, totalSum, currentSum + nums[i], count, k, used, i + 1))
                        return true;
                    used[i] = false;
                }
            }
            return false;
        }

        static void Grammar()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(KthGrammar(n, k));
        }
        static int KthGrammar(int N, int K)
        {
            if (N == 1 || N == 2 && K == 1)
                return 0;
            int parent = KthGrammar(N - 1, (K + 1) / 2);
            if (parent == 0)
                return 1 - (K % 2);
            return K % 2;
        }

        static void Team()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var count = 0;
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine().Split(' ').Count(x => x == "1");
                if (temp >= 2)
                    count++;
            }
            Console.WriteLine(count);
        }
        static void Watermelon()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            if (n == 1 || n == 2)
                Console.WriteLine("NO");
            else
                Console.WriteLine((n % 2 == 1) ? "NO" : "YES");
        }
        static void ItsConfidential()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                string str = Console.ReadLine();
                RecStr(str, 0, n - 1);
                Console.WriteLine();
            }
        }

        static void RecStr(string s, int start, int end)
        {
            if (start <= end)
            {
                int mid = (start + end) / 2;
                Console.Write(s[mid]);
                RecStr(s, start, mid - 1);
                RecStr(s, mid + 1, end);
            }
        }
        static void DivideNumber()
        {
            long t = Convert.ToInt64(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var list = new List<int>();
                int n = Convert.ToInt32(Console.ReadLine());
                for (int j = 1; j * j <= n; j++)
                {
                    if (n % j == 0)
                    {
                        list.Add(j);
                        if (n / j != j) list.Add(n / j);
                    }
                }
                Console.WriteLine(MaxNumber(list, 4, n));
            }
        }
        static long MaxNumber(List<int> list, int count, int n)
        {
            if (n == 0 && count == 0) return 1;
            if ((n == 0 && count != 0) || (n != 0 && count == 0)) return -1;
            long ans = -1;
            foreach (var l in list)
            {
                if (l <= n)
                {
                    var temp = MaxNumber(list, count - 1, n - l);
                    if (temp != -1)
                        ans = Math.Max(ans, temp * l);
                }
            }

            return ans;
        }
        static void HackTheMoney()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                long n = Convert.ToInt64(Console.ReadLine());

                if (Divisor10or20(n) == 1)
                    Console.WriteLine("Yes");
                else Console.WriteLine("No");

            }
        }
        static long Divisor10or20(long n)
        {
            if (n == 1)
                return 1;
            if (n % 20 == 0)
                return Math.Max(Divisor10or20(n / 10), Divisor10or20(n / 20));
            if (n % 20 != 0 && n % 10 == 0)
                return Divisor10or20(n / 10);
            return 0;
        }
        static void MovementInArrays()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                var array = Console.ReadLine().Split(' ');
                var m = Convert.ToInt32(Console.ReadLine());
                int[] arr = new int[n];
                for (int j = 0; j < n; j++)
                {
                    arr[i] = Convert.ToInt32(array[i]);
                }
            }


        }

        static void LongestValue()
        {
            TreeNode root = null;
            root = new TreeNode(1);
            // root.left = new TreeNode(1);
            //root.right = new TreeNode(5);
            //root.left.left = new TreeNode(4);
            //root.left.right = new TreeNode(4);
            //root.right.right = new TreeNode(5);
            if (root.left == root.right == null)
            {
                Console.WriteLine(0);
                return;
            }
            LongestUnivaluePath(root);
            Console.WriteLine(ans);
        }
        static int LongestUnivaluePath(TreeNode root)
        {
            if (root == null)
                return 0;
            int left = LongestUnivaluePath(root.left);
            int right = LongestUnivaluePath(root.right);

            int maxLeft = 0, maxRight = 0;

            if (root.left != null && root.left.val == root.val)
                maxLeft += left + 1;
            if (root.right != null && root.right.val == root.val)
                maxRight += right + 1;

            ans = Math.Max(maxLeft + maxRight, ans);
            return Math.Max(maxRight, maxLeft);
        }

        private static int ans = 0;
        static void NQueen()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] board = new int[n, n];
            if (!TheBoarderSolver(board, 0, n))
                Console.WriteLine("Not possible");
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.WriteLine(board[i, j] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }
        static bool TheBoarderSolver(int[,] board, int col, int n)
        {
            if (col >= n) return true;
            for (int i = 0; i < n; i++)
            {
                if (TopPlaceOrNotToPlace(board, i, col, n))
                {
                    board[i, col] = 1;
                    if (TheBoarderSolver(board, col + 1, n))
                        return true;
                    board[i, col] = 0;
                }
            }

            return false;
        }
        static bool TopPlaceOrNotToPlace(int[,] board, int row, int col, int n)
        {
            for (int i = 0; i < col; i++)
            {
                if (board[row, i] == 1) return false;
            }
            for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 1) return false;
            }
            for (int i = row, j = col; i < n && j >= 0; i++, j--)
            {
                if (board[i, j] == 1) return false;
            }
            return true;
        }
        static void MaximumInTable()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            if (n == 1)
            {
                Console.WriteLine(1);
                return;
            }
            int[,] table = new int[n, n];
            Console.WriteLine(MaxValue(table, 0, 0, n));
        }
        static int MaxValue(int[,] table, int i, int j, int n)
        {
            if (i == n)
                return table[i - 1, i - 1];
            if (j == n)
                return MaxValue(table, i + 1, 0, n);
            if (i == 0 || j == 0)
            {
                table[i, j] = 1;
                return MaxValue(table, i, j + 1, n);
            }

            table[i, j] = table[i - 1, j] + table[i, j - 1];
            return MaxValue(table, i, j + 1, n);
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
}
