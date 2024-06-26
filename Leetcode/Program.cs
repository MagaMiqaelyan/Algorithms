﻿using HeapStackQueue;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Leetcode
{
    public class MyCalendarTwo
    {

        private Dictionary<int, int> _calendar;
        public MyCalendarTwo()
        {
            _calendar = new Dictionary<int, int>();
        }

        public bool Book(int start, int end)
        {
            var booked = 0;
            if (_calendar.ContainsKey(start))
                _calendar[start]++;
            else
                _calendar.Add(start, 1);

            if (_calendar.ContainsKey(end))
                _calendar[end]--;
            else
                _calendar.Add(end, -1);

            foreach (var (time, count) in _calendar)
            {
                booked += count;
                if (booked == 3)
                {
                    _calendar[start]--;
                    _calendar[end]++;
                    return false;
                }

            }
            return true;
        }
    }

    public class File
    {
        public bool IsDir { get; set; }
        public string Content { get; set; }
        public List<File> Children { get; set; }
        public string Name { get; set; }

        public File()
        {

        }

        public File(bool isDir, string name)
        {
            IsDir = isDir;
            Name = name;
            Children = new List<File>();
            Content = "";
        }
    }

    public class FileSystem
    {
        private File root;

        public FileSystem()
        {
            root = new File(true, "/");
        }

        /// <summary>
        /// Given a path in string format. If it is a file path, return a list that only contains this file’s name.
        /// If it is a directory path, return the list of file and directory names in this directory. 
        /// Your output (file and directory names together) should in lexicographic order.
        public List<string> ls(string path)
        {
            var file = GetLast(path);

            var res = new List<string>();
            if (file.IsDir)
            {
                foreach (var f in file.Children)
                    res.Add(f.Name);
            }
            else
                res.Add(file.Name);
            return res;
        }

        //Given a directory path that does not exist, you should make a new directory according to the path.
        // If the middle directories in the path don’t exist either, you should create them as well.
        public void mkdir(string path)
        {
            GetAndCreate(path, true);
        }

        //Given a file path and file content in string format.If the file doesn’t exist, you need to create that file containing given content.
        //If the file already exists, you need to append given content to original content.
        public void addContentToFile(string filePath, string content)
        {
            var file = GetAndCreate(filePath, false);
            file.Content += content;
        }

        // read content
        public string readContentFromFile(string filePath)
        {
            var file = GetLast(filePath);
            return file.Content;
        }

        private File GetAndCreate(string path, bool isDir)
        {
            var current = root;
            path = path.Substring(1);
            var index = path.IndexOf('/');

            while (index >= 0)
            {
                var pathName = path.Substring(0, index);
                if (current.Children.Count == 0)
                {
                    var dir = new File(true, pathName);
                    current.Children.Add(dir);
                    current = dir;

                    path = path.Substring(index + 1);
                    index = path.IndexOf('/');
                    continue;
                }

                var found = SearchFile(current.Children, pathName);
                var temp = current.Children[found];
                if (temp.Name != pathName)
                {
                    var dir = new File(true, pathName);
                    current.Children.Insert(found + 1, dir);
                    current = dir;

                    path = path.Substring(index + 1);
                    index = path.IndexOf('/');
                    continue;
                }

                current = temp;
                path = path.Substring(index + 1);
                index = path.IndexOf('/');
            }

            var file = SearchFile(current.Children, path);
            if (file == -1 || current.Children[file].Name != path)
            {
                var f = new File(isDir, path);
                if (current.Children.Count == 0)
                    current.Children.Add(f);
                else
                    current.Children.Insert(file + 1, f);
                current = f;
            }
            else
                current = current.Children[file];

            return current;
        }

        private File GetLast(string path)
        {
            var current = root;
            path = path.Substring(1);
            var index = path.IndexOf('/');

            while (index >= 0)
            {
                var pathName = path.Substring(0, index);
                var found = SearchFile(current.Children, pathName);
                current = current.Children[found];
                path = path.Substring(index + 1);
                index = path.IndexOf('/');
            }

            var file = SearchFile(current.Children, path);
            if (file < 0) return root;
            current = current.Children[file];
            return current;
        }

        private int SearchFile(List<File> list, string name)
        {
            if (list.Count < 1) return -1;
            var idx = -1;
            var l = 0;
            var r = list.Count - 1;
            while (l <= r)
            {
                var mid = l + (r - l) / 2;
                if (list[mid].Name == name) return mid;

                if (list[mid].Name.CompareTo(name) < 0)
                {
                    l = mid + 1;
                    idx = mid;
                }
                else
                    r = mid - 1;
            }
            return idx;
        }
    }

    public class SnakeGame
    {
        private bool[,] board;
        private Queue<int> snake;
        private int[][] food;
        private int foodIndex;
        private int row;
        private int col;
        private int score;
        private int width;
        private int height;

        public SnakeGame(int width, int height, int[][] food)
        {
            this.width = width;
            this.height = height;
            this.food = food;

            board = new bool[height, width];
            board[0, 0] = true;
            snake = new Queue<int>();
            snake.Enqueue(0);
        }

        public int Move(string direction)
        {
            if (score == -1) return score;

            if (direction == "L") col--;
            else if (direction == "R") col++;
            else if (direction == "U") row--;
            else row++;

            if (row < 0 || col < 0 || row >= height || col >= width)
            {
                score = -1;
                return score;
            }

            if (foodIndex == food.Length || row != food[foodIndex][0] || col != food[foodIndex][1])
            {
                var tail = snake.Dequeue();
                board[tail / width, tail % width] = false;
            }
            else
            {
                score++;
                foodIndex++;
            }

            if (board[row, col])
            {
                score = -1;
                return score;
            }
            else
            {
                snake.Enqueue(row * width + col);
                board[row, col] = true;
            }
            return score;
        }
    }
    public class TicTacToe
    {
        private int[] rows;
        private int[] columns;
        private int diagonal;
        private int antiDiagonal;
        private int n;
        public TicTacToe(int n)
        {
            rows = new int[n];
            columns = new int[n];
            this.n = n;
        }

        public int Move(int x, int y, int player)
        {
            var add = player == 1 ? 1 : -1;

            rows[x] += add;
            columns[y] += add;

            if (x == y)
                diagonal += add;
            if (x + y == n - 1)
                antiDiagonal += add;

            if (Math.Abs(rows[x]) == n || Math.Abs(columns[y]) == n || Math.Abs(diagonal) == n || Math.Abs(antiDiagonal) == n)
                return player;

            return 0;
        }
    }

    public class SnapshotArray
    {

        Dictionary<int, int[]> map;
        int snap_id = 0;
        int length = 0;
        public SnapshotArray(int length)
        {
            map = new Dictionary<int, int[]>();
            snap_id = 0;
            this.length = length;
        }

        public void Set(int index, int val)
        {
            if (!map.ContainsKey(index))
                map.Add(index, new int[length]);

            map[index][snap_id] = val;

        }

        public int Snap()
        {
            return snap_id++;
        }

        public int Get(int index, int snap_id)
        {
            if (map.ContainsKey(index))
                return map[index] == null ? 0 : map[index][snap_id];
            return 0;
        }
    }

    /**
     * Your SnapshotArray object will be instantiated and called as such:
     * SnapshotArray obj = new SnapshotArray(length);
     * obj.Set(index,val);
     * int param_2 = obj.Snap();
     * int param_3 = obj.Get(index,snap_id);
     */
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
            //Console.WriteLine(NumRollsToTarget(30, 30, 500));
            //Console.WriteLine(RotateString("abcde", "abced"));

            //Console.WriteLine(string.Join(" ", MajorityElement(new int[] { 2, 1, 1, 3, 1, 4, 5, 6})));

            //var treeNode = new TreeNode(5, new TreeNode(3, new TreeNode(2), new TreeNode(4)),
            //    new TreeNode(6, null, new TreeNode(6, null, new TreeNode(7))));
            //var r = TrimBST(treeNode, 1, 3);

            //var n1 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))));
            //var n2 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))));
            //var r = AddTwoNumbers2(n1, n2);

            ///[5,3,6,2,4,null,null,1]
            //var treeNode = new TreeNode(5, new TreeNode(3, new TreeNode(2, new TreeNode(1)),
            //    new TreeNode(4)), new TreeNode(6, new TreeNode(7), new TreeNode(9)));
            //KthSmallest(treeNode, 3);
            //GenerateTrees(3);
            //points = [[1, 3],[3,3],[5,3],[2,2]], queries = [[2, 3, 1],[4,3,1],[1,1,2]]
            //CountPoints(new int[][] { new int[] {1,3}, new int[] {3,3}, new int[] {5,3}, new int[] {2,2}},
            //    new int[][] { new int[] { 2, 3, 1 }, new int[] { 4, 3, 1 }, new int[] { 1, 1, 2 } });
            //MinOperations("001011");
            //MaxWidthOfVerticalArea(new int[][] { new int[] { 3, 1 }, new int[] { 9, 0 }, new int[] { 1,0 },
            //    new int[] { 1, 4 }, new int[] {5, 3 }, new int[] { 8, 8 } });
            // CombinationIterator("gkosu", 3);
            //FirstUniqChar("loveleetcode"); [[2],[3,4],[6,5,7],[4,1,8,3]]
            //MinimumTotal(new List<IList<int>>
            //{
            //    new List<int>
            //    {
            //        2
            //    },
            //    new List<int>
            //    {
            //        3,4
            //    },
            //    new List<int>
            //    {
            //        6,5,7
            //    },
            //    new List<int>
            //    {
            //        4,1,8,3
            //    },
            //});
            //GuessNumber(2126753390);
            //StrStr("mississippi", "issip");
            //SwapPairs(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4)))));

            //BraceExpansionII("{{a,z},a{b,c},{ab,z}}");

            //IsPalindrome(new ListNode(1, new ListNode(2, new ListNode
            //(2, new ListNode(1)))));
            //LengthOfLIS(new int[] { 7, 7, 7, 7, 7, 7, 7 });
            //NumberToWords(1234567891);
            //var streamReader = new StreamReader(new FileStream("filepath", FileMode.Open, FileAccess.Read), Encoding.UTF8);
            //var nums = new int[] { 0,1 };
            //QuickSort(nums, 0, 1);

            //RestoreIpAddresses("25525511135");

            //MinInterval(new int[][]
            //{
            //    new int[] { 1,4},
            //    new int[] {2,4},
            //    new int[] { 3,6},
            //    new int[] { 4,4},
            //}, new int[] { 2, 3, 4, 5 });

            //var arr = new int[5] { 1, 2, 3, 4, 5 };
            //var rnd = new Random();
            //for(int i = 4; i > 1; i--)
            //{
            //    var idx = rnd.Next(i - 1);

            //    Console.WriteLine($"{arr[i]}, {arr[idx]}");
            //    var t = arr[i];
            //    arr[i] = arr[idx];
            //    arr[idx] = t;
            //}


            //Console.WriteLine(string.Join(", ", arr));
            //Console.WriteLine(CuttingMethod(F, 1, -3));
            //     MostVisitedPatterns(new string[] { "joe", "joe", "joe", "james", "james", "james", "james", "mary", "mary", "mary" },
            //    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new string[] { "home", "about", "career", "home", "cart", "maps", "home", "home", "about", "career" });

            //var snake = new SnakeGame(3, 2, new int[][] { new int[] { 1, 2 }, new int[] { 0, 1 } });
            //Console.WriteLine(snake.Move("R"));
            //Console.WriteLine(snake.Move("D"));
            //Console.WriteLine(snake.Move("R"));
            //Console.WriteLine(snake.Move("U"));
            //Console.WriteLine(snake.Move("L"));
            //Console.WriteLine(snake.Move("U"));
            //IsMatch("aacb", "a**b");
            // CanCross(new int[] { 0, 1, 3, 5, 6, 8, 12, 17 });
            //UniqueLetterString("LEETCODE");
            //FirstMissingPositive(new int[] { 3, 4, -1, 1 });
            //SumScores("azbazbzaz");
            //LargestInteger(247);
            //MaximumProduct(new int[] { 24, 5, 64, 53, 26, 38 }, 54);
            //MinimizeResult("99+99");
            //MoveZeroes(new int[] { 0, 1, 0, 3, 12 });
            //var n = new ListNode(1, new ListNode(2, new ListNode(4, new ListNode(5))));
            //DeleteNode(n.next);
            //RemoveDuplicates(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 });
            //DeleteDuplicates(new ListNode(1, new ListNode(1, new ListNode(1))));
            //IsHappy(2);
            //MaxSumTwoNoOverlap(new int[] { 0, 6, 5, 2, 2, 5, 1, 9, 4 }, 1, 2);
            //FurthestBuilding(new[] { 4, 12, 2, 7, 3, 18, 20, 3, 19 }, 10, 2);
            //Console.WriteLine(CanMeasureWater(3, 5, 4));

            //Console.WriteLine(DateTime.Parse("October, 12 2022"));
            //Console.WriteLine(DaysBetweenDates("2019-06-30", "2019-06-29"));

            //FindRestaurant(new[] { "Shogun", "Tapioca Express", "Burger King", "KFC" }, new[] { "KFC", "Shogun", "Burger King" });
            //NumSplits("aacaba");

            //var treeNode = new TreeNode(-100, new TreeNode(-200, new TreeNode(-20), new TreeNode(-5)),
            //    new TreeNode(-300, null, new TreeNode(-10, null, null)));
            //MaxLevelSum(treeNode);

            //CanFinish(2, new int[][] { new int[] { 1, 0 } });
            //KnightDialer(2);
            //FindMinHeightTrees(6, new int[][] 
            //{
            //    new int[] { 3, 0 },
            //    new int[] { 3, 1 },
            //    new int[] { 3, 2 }, 
            //    new int[] { 3, 4 }, 
            //    new int[] { 5, 4 }, 
            //});

            //ShipWithinDays(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5);
            //ShortestPathBinaryMatrix(new int[][]
            //{
            //    new int[] { 0, 0, 0 },
            //    new int[] { 1, 1, 0 },
            //    new int[] { 1, 1, 1 },
            //});
            //var s = new string[] { "xbc", "pcxbcf", "xb", "cxbc", "pcxbc" };
            //HorizontalFlip(new int[] { 1, 1, 1, 0 });
            //Array.Sort(s, (string x, string y) => x.Length - y.Length);

            //NumWaterBottles(15, 4);
            //MinSteps("aba", "bab");
            //GetMaximumGold(new int[][]
            //{
            //    new int[]{0,6,0},
            //    new int[]{5,8,7},
            //    new int[]{0,9,0},
            //});

            //ReorderLogFiles(new string[] { "a1 9 2 3 1", "g1 act car", "zo4 4 7", "ab1 off key dog", "a8 act zoo" });


            //MinDominoRotations(new int[] { 2, 1, 2, 4, 2, 2 }, new int[] { 5, 2, 6, 2, 3, 2 });
            //StrongPasswordChecker("aaa123"); 
            //FirstBadVersion(5);
            //var a = new SnapshotArray(1);
            //a.Set(0, 15);
            //a.Snap();
            //a.Snap();
            //a.Snap();
            //a.Set(0, 2);
            //a.Snap();
            //a.Snap();
            //var b = a.Get(0, 0);
            //UniqueOccurrences(new int[] { 1, 2, 2, 1, 1, 3 });
            // MinOperations("001011");
            //RemoveDuplicates2(new int[] { 0 });
            //Rotate(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3);


            //NumSubmat(new int[][]
            //{
            //    new int[] { 1,0,1},
            //    new int[] { 1,1,0},
            //    new int[] { 1,1,0}
            //});

            //var myCalendarTwo = new MyCalendarTwo();
            //myCalendarTwo.Book(10, 20); // return True, The event can be booked. 
            //myCalendarTwo.Book(50, 60); // return True, The event can be booked. 
            //myCalendarTwo.Book(10, 40); // return True, The event can be double booked. 
            //myCalendarTwo.Book(5, 15);  // return False, The event cannot be booked, because it would result in a triple booking.
            //myCalendarTwo.Book(5, 10); // return True, The event can be booked, as it does not use time 10 which is already double booked.
            //myCalendarTwo.Book(25, 55);
            //CombinationSum3(3, 9);
            //var s = "2-5g-3-J";
            // LicenseKeyFormatting(s, 2);
            //LengthLongestPath("dir\n\tsubdir1\n\tsubdir2\n\t\tfile.ext");
            //IsLongPressedName("saed","ssaaedd");
            //GardenNoAdj(3, new int[][]
            //{
            //    new int[] { 1, 2},
            //    new int[] { 2, 3 },
            //    new int[] { 3, 1 }
            //});
            //FindReplaceString("abcd", new int[] { 0, 2 }, new string[] { "ab", "ec" }, new string[] { "eee", "ffff" });
            //DuplicateZeros(new int[] { 1, 0, 2, 3, 0, 4, 5, 0 });
            //WordsTyping(new string[] { "hello", "world" }, 2, 8);
            #endregion

            //MaxSubarrayLength(new int[] { 7, 6, 5, 4, 3, 2, 1, 6, 10, 11 });
            //ValidateStackSequences(new int[] { 2, 1, 0 }, new int[] { 1,2,0 });
            //NumEnclaves(new int[][]
            //{
            //    new int[] { 0,1,1,0 },
            //    new int[] { 0,0,1,0 },
            //    new int[] { 0,0,1,0 },
            //    new int[] { 0,0,0,0 }
            //});

            //RemoveKdigits("1432219", 3);
            SortArray(new int[] { 4, 2, 0, 3, 1 });
        }
        public bool IsStrobogrammatic(string num)
        {
            var digits = new Dictionary<char, char>
            {
               {'0', '0'},
               {'1', '1'},
               {'6', '9'},
               {'8', '8' },
               {'9', '6'}
            };

            var rotated = new StringBuilder();
            foreach (var n in num)
            {
                if (!digits.ContainsKey(n)) return false;
                rotated.Append(digits[n]);
            }
            return rotated.ToString().Equals(num);
        }

        // 2 4 1 3 0   4 2 0 3 1
        // 3 4 1 2 0   4 2 3 0 1
        // 0 4 1 2 3   0 2 3 4 1
        // 4 0 1 2 3   1 2 3 4 0
        // 1 4 2 3 0
        // 4 1 2 3 0
        // 0 1 2 3 4
        public static int SortArray(int[] nums)
        {
            var n = nums.Length;
            var indices = new int[n];
            for (int i = 0; i < nums.Length; i++)
                indices[nums[i]] = i;
            var swapsZeroIndexed = CountSwaps(indices, 0);
            for (int i = 0; i < nums.Length; i++)
                indices[nums[i]] = i;
            var swapsOneIndexed = CountSwaps(indices, 1);
            return Math.Min(swapsOneIndexed, swapsZeroIndexed);
        }
        private static int CountSwaps(int[] indices, int start)
        {
            var count = 0;
            var nextIndex = 1;
            var currentIndex = 0;

            while (nextIndex < indices.Length)
            {
                if (indices[0] == (1 - start) * (indices.Length - 1))
                {
                    while (indices[nextIndex] == nextIndex + (start - 1))
                    {
                        if (++nextIndex == indices.Length) return count;
                    }
                    currentIndex = nextIndex;
                }
                else
                {
                    currentIndex = indices[0] + 1 - start;
                }

                var temp = indices[0];
                indices[0] = indices[currentIndex];
                indices[currentIndex] = temp;
                count++;
            }
            return count;
        }
        public int[][] MinScore(int[][] grid)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;

            var pq = new PriorityQueue<int[], int>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    pq.Enqueue(new int[] { i, j }, grid[i][j]);
                }
            }

            var maxCols = new int[cols];
            var maxRows = new int[rows];
            var result = new int[rows][];
            for (int i = 0; i < rows; i++)
                result[i] = new int[cols];

            while (pq.Count > 0)
            {
                var item = pq.Dequeue();
                var x = item[0];
                var y = item[1];
                var max = Math.Max(maxRows[x], maxCols[y]) + 1;
                result[x][y] = max;
                maxRows[x] = max;
                maxCols[y] = max;
            }
            return result;
        }

        public static string RemoveKdigits(string num, int k)
        {
            var stack = new Stack<char>();

            foreach (var n in num.ToCharArray())
            {
                while (stack.Count() > 0 && k > 0 && stack.Peek() > n)
                {
                    stack.Pop();
                    k--;
                }
                stack.Push(n);

                if (stack.Count() == 1 && n == '0')
                    stack.Pop();
            }

            while (stack.Count() > 0 && k > 0)
            {
                stack.Pop();
                k--;
            }

            var res = new StringBuilder();
            while (stack.Count() > 0)
            {
                res.Append(stack.Pop());
            }
            if (res.Length == 0) return "0";
            return string.Join("", res.ToString().ToCharArray().Reverse());

        }

        public static int NumEnclaves(int[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (i == 0 || j == 0 || i == grid.Length - 1 || j == grid[0].Length - 1)
                        DFS(grid, i, j);
                }
            }
            var result = 0;

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 1) result++;
                }
            }
            return result;
        }

        private static void DFS(int[][] grid, int i, int j)
        {
            if (i >= grid.Length || j >= grid[0].Length || i < 0 || j < 0 || grid[i][j] == 0)
                return;

            grid[i][j] = 0;
            DFS(grid, i + 1, j);
            DFS(grid, i - 1, j);
            DFS(grid, i, j + 1);
            DFS(grid, i, j - 1);
        }

        public static bool IsThereAPath(int[][] grid)
        {
            var n = grid.Length;
            var m = grid[0].Length;
            if ((n + m - 1) % 2 != 0) return false;
            return Solve(grid, 0, 0, 0);
        }

        private static bool Solve(int[][] grid, int i, int j, int sum)
        {
            if (i >= grid.Length || j >= grid[0].Length) return false;
            sum += grid[i][j] == 1 ? 1 : -1;
            if (i == grid.Length - 1 && j == grid[0].Length - 1) return sum == 0;
            return Solve(grid, i + 1, j, sum) || Solve(grid, i, j + 1, sum);
        }

        public static bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            var i = 0;
            var j = 0;
            var stack = new Stack<int>();
            while (i < pushed.Length && j < popped.Length)
            {
                while (stack.Count() > 0 && stack.Peek() == popped[j])
                {
                    j++;
                    stack.Pop();
                }

                stack.Push(pushed[i]);
                i++;
            }

            while (j < popped.Length)
            {
                if (stack.Peek() == popped[j])
                {
                    stack.Pop();
                }
                j++;
            }
            return stack.Count() == 0;
        }

        public static int MaxSubarrayLength(int[] nums)
        {
            Stack<int> stack = new Stack<int>();
            int n = nums.Length;

            for (int i = n - 1; i >= 0; i--)
            {
                if (stack.Count() == 0 || nums[i] < nums[stack.Peek()])
                {
                    stack.Push(i);
                }
            }

            int r = 0;
            int m = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                while (stack.Count() > 0 && stack.Peek() <= i)
                {
                    stack.Pop();
                }

                if (nums[i] > m)
                {
                    m = nums[i];

                    while (stack.Count() > 0 && nums[stack.Peek()] < m)
                    {
                        r = Math.Max(r, stack.Peek() - i + 1);
                        stack.Pop();
                    }
                }
            }

            return r;
        }


        public static int WordsTyping(string[] sentence, int rows, int cols)
        {
            var str = new StringBuilder();
            foreach (var s in sentence)
                str.Append(s).Append(' ');

            var cursor = 0;
            var n = str.Length;
            for (int i = 0; i < rows; i++)
            {
                cursor += cols;
                while (cursor % n >= 0 && str[cursor % n] != ' ')
                    cursor--;

                cursor++;
            }
            return cursor / n;
        }

        public static void DuplicateZeros(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] != 0) continue;
                for (int j = arr.Length - 2; j > i; j--)
                {
                    arr[j + 1] = arr[j];
                }
                arr[i + 1] = 0;
                i++;
            }
        }

        //        "vmokgggqzp"
        //[3,5,1]
        //        ["kg","ggq","mo"]
        //        ["s","so","bfr"]

        public static string FindReplaceString(string s, int[] indices, string[] sources, string[] targets)
        {
            var ans = new StringBuilder();
            var j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (!indices.Contains(i))
                {
                    ans.Append(s[i]);
                    continue;
                }

                var len = sources[j].Length;
                if (len > 0)
                {
                    var sub = s.Substring(indices[j], len);
                    if (sub == sources[j])
                    {
                        ans.Append(targets[j]);
                        i += sources[j].Length - 1;
                        j++;
                    }
                    else
                        ans.Append(s[i]);
                }
            }
            return ans.ToString();
        }

        public static int[] GardenNoAdj(int n, int[][] paths)
        {
            var map = new Dictionary<int, HashSet<int>>();

            foreach (var node in paths)
            {
                if (!map.ContainsKey(node[0]))
                    map.Add(node[0], new HashSet<int>());
                map[node[0]].Add(node[1]);
            }

            var result = new int[n];
            foreach (var (x1, y1) in map)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (y1.Contains(i) || (x1 >= 2 && result[x1 - 2] == i)) continue;
                    result[x1 - 1] = i;
                    break;
                }
            }
            return result;
        }

        public static bool IsLongPressedName(string name, string typed)
        {
            var target = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (target >= typed.Length || name[i] != typed[target]) return false;
                var temp = 1;
                while (i < name.Length - 1 && name[i] == name[i + 1])
                {
                    temp++;
                    i++;
                }

                while (target < typed.Length && typed[target] == name[i])
                {
                    target++;
                    temp--;
                }

                if (temp >= 1) return false;
            }
            return target == typed.Length;
        }

        public static int LengthLongestPath(string input)
        {
            var tokens = input.Split("\n");
            var result = 0;
            var curLen = 0;
            var stack = new Stack<int>();

            foreach (var t in tokens)
            {
                var level = t.LastIndexOf('\t') + 1;

                while (stack.Count() > level)
                    curLen -= stack.Pop();

                var l = t.Replace("\t", "").Length + 1;
                curLen += l;

                if (t.Contains('.'))
                {
                    result = Math.Max(curLen - 1, result);
                }
                stack.Push(l);
            }

            return result;

        }

        public static string LicenseKeyFormatting(string s, int k)
        {
            s = s.Replace("-", "");
            var i = 0;
            var n = s.Length;
            var result = "";
            // 25g3j 5
            while (i < n)
            {
                if (n - k - i < 0)
                    result = s.Substring(0, n - i) + result;
                else
                    result = s.Substring(n - k - i, k).ToUpper() + result;
                i += k;
                if (i < n) result = '-' + result;
            }
            return result;
        }

        public static IList<IList<int>> CombinationSum3(int k, int n)
        {
            var numbers = new int[9];
            sum3 = new List<IList<int>>();
            for (int i = 1; i <= 9; i++)
                numbers[i - 1] = i;

            Helper(numbers, 1, 0, k, n, new List<int>());
            return sum3;
        }

        private static IList<IList<int>> sum3;
        public static void Helper(int[] numbers, int start, int sum, int k, int n, List<int> current)
        {
            if (sum == n && current.Count == k)
            {
                sum3.Add(new List<int>(current));
                return;
            }
            if (sum >= n || current.Count >= k) return;

            for (int i = start; i <= 9; i++)
            {
                current.Add(numbers[i - 1]);
                Helper(numbers, i + 1, sum + numbers[i - 1], k, n, current);
                current.RemoveAt(current.Count - 1);
            }
        }

        public static int NumSubmat(int[][] mat)
        {
            var m = mat.Length;
            var n = mat[0].Length;
            var helper = new int[n];
            var res = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    helper[j] = (mat[i][j] == 0 ? 0 : helper[j] + 1);
                }
                res += Helper(helper);
            }
            return res;
        }

        public static int Helper(int[] arr)
        {
            var stack = new Stack<int>();
            var result = new int[arr.Length];
            var i = 0;
            while (i < arr.Length)
            {
                while (stack.Count > 0 && arr[stack.Peek()] >= arr[i])
                    stack.Pop();

                if (stack.Count > 0)
                {
                    var preIndex = stack.Peek();
                    result[i] = result[preIndex] + arr[i] * (i - preIndex);
                }
                else
                {
                    result[i] = arr[i] * (i + 1);
                }
                stack.Push(i);
                i++;
            }
            return result.Sum();
        }

        public static void Rotate(int[] nums, int k)
        {
            var result = new int[k];
            var n = nums.Length;
            for (int i = n - k, j = 0; i < n; i++, j++)
            {
                result[j] = nums[i];
            }

            for (int i = 0; i < n - k; i++)
            {
                nums[n - i - 1] = nums[n - k - i - 1];
            }

            for (int i = 0; i < k; i++)
            {
                nums[i] = result[i];
            }
        }

        public static int RemoveDuplicates2(int[] nums)
        {
            if (nums.Length <= 2) return 2;

            var count = 2;
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] != nums[count - 2])
                {
                    nums[count] = nums[i];
                    count++;
                }
            }
            return count;
        }

        public static bool UniqueOccurrences(int[] arr)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (map.ContainsKey(arr[i]))
                {
                    map[arr[i]]++;
                }
                else
                {
                    map[arr[i]] = 1;
                }
            }
            var values = map.Select(x => x.Value).Distinct();
            return values.Count() == map.Count();
        }


        public static int FirstBadVersion(int n)
        {
            var start = 1;
            var end = n;
            while (start < end)
            {
                var mid = start + (end - start) / 2;
                var isBadMid = IsBadVersion(mid);
                var isBadMidLeft = IsBadVersion(mid - 1);
                if (!isBadMidLeft && isBadMid)
                    return mid;
                if (!isBadMidLeft && !isBadMid)
                    start = mid + 1;
                else
                    end = mid - 1;

            }
            return start;
        }

        private static bool IsBadVersion(int n)
        {
            if (n == 5) return true;
            if (n == 4) return true;
            if (n == 3) return false;
            return false;
        }

        public static int StrongPasswordChecker(string password)
        {
            int missingCharacters = GetMissingCharacters(password);
            int missingTypes = GetMissingTypes(password);
            int repeatingChars = GetRepeatingCharacters(password);

            if (password.Length < 6)
            {
                return Math.Max(6 - password.Length, Math.Max(missingTypes, repeatingChars));
            }

            int totalSteps = 0;
            if (password.Length > 20)
            {
                int deleteCount = password.Length - 20;
                totalSteps += deleteCount;
                repeatingChars = Math.Max(0, repeatingChars - 3 * deleteCount); // Corrected this line
                int index = 0;
                while (deleteCount > 0 && index < password.Length)
                {
                    if (index + 2 < password.Length && password[index] == password[index + 1] && password[index] == password[index + 2])
                    {
                        password = password.Remove(index + 2, 1);
                        deleteCount--;
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            totalSteps += Math.Max(missingCharacters, missingTypes);
            totalSteps += repeatingChars;

            return totalSteps;
        }

        private static int GetMissingCharacters(string password)
        {
            int missingChars = 0;
            if (!ContainsLowercase(password)) missingChars++;
            if (!ContainsUppercase(password)) missingChars++;
            if (!ContainsDigit(password)) missingChars++;
            return missingChars;
        }

        private static int GetMissingTypes(string password)
        {
            int missingTypes = 0;
            if (!ContainsLowercase(password)) missingTypes++;
            if (!ContainsUppercase(password)) missingTypes++;
            if (!ContainsDigit(password)) missingTypes++;
            return missingTypes;
        }

        private static int GetRepeatingCharacters(string password)
        {
            int repeatingChars = 0;
            for (int i = 0; i < password.Length; i++)
            {
                int repeatLength = 1;
                while (i + 1 < password.Length && password[i] == password[i + 1])
                {
                    repeatLength++;
                    i++;
                }
                repeatingChars += repeatLength / 3;
            }
            return repeatingChars;
        }

        private static bool ContainsLowercase(string password)
        {
            return password.Any(char.IsLower);
        }

        private static bool ContainsUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        private static bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        public static int MinDominoRotations(int[] tops, int[] bottoms)
        {
            var topCounts = new int[7];
            var bottomCounts = new int[7];
            var same = new int[7];
            var n = tops.Length;
            for (int i = 0; i < n; i++)
            {
                topCounts[tops[i]]++;
                bottomCounts[bottoms[i]]++;
                if (tops[i] == bottoms[i])
                    same[tops[i]]++;
            }

            for (int i = 1; i < 7; i++)
            {
                if (topCounts[i] + bottomCounts[i] - same[i] == n)
                    return n - Math.Max(topCounts[i], bottomCounts[i]);
            }

            return -1;
        }

        public static string[] ReorderLogFiles(string[] logs)
        {
            var mapLetterLogs = new Dictionary<string, string>();
            var digitlogs = new List<string>();

            foreach (var log in logs)
            {
                var split = log.Split(' ');
                if (int.TryParse(split[1], out var res))
                    digitlogs.Add(log);
                else
                {
                    var value = string.Join(' ', split.Skip(1));
                    mapLetterLogs.Add(log, value);
                }
            }

            var letterlogs = mapLetterLogs.OrderBy(x => x.Value).ThenBy(x => x.Key);
            var answer = new List<string>();

            foreach (var log in letterlogs)
            {
                answer.Add(log.Key);
            }

            foreach (var log in digitlogs)
            {
                answer.Add(log);
            }
            return answer.ToArray();
        }


        static int maxGold = 0;
        static List<int[]> directions1 = new List<int[]>
                {
                   new int[] { 1, 0},
                   new int[]  { 0, 1},
                   new int[]  { -1, 0},
                    new int[] { 0, -1},
                };
        public static int GetMaximumGold(int[][] grid)
        {
            maxGold = 0;
            for (int i = 0; i < grid.Length; i++)
                for (int j = 0; j < grid[0].Length; j++)
                {
                    maxGold = Math.Max(maxGold, GetGold(grid, new byte[grid.Length, grid[0].Length], i, j));
                }
            return maxGold;
        }

        public static int GetGold(int[][] grid, byte[,] visited, int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length || visited[i, j] == 1 || grid[i][j] == 0)
                return 0;

            var maxPath = 0;
            visited[i, j] = 1;
            foreach (var dir in directions1)
            {
                maxPath = Math.Max(maxPath, GetGold(grid, visited, i + dir[0], j + dir[1]));
            }
            visited[i, j] = 0;
            return maxPath + grid[i][j];
        }

        public int MaxSum(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int maxSum = int.MinValue;

            for (int colIndex = 0; colIndex < cols; colIndex++)
            {
                int sum = matrix[0, colIndex] - colIndex;
                int rowIndex = 1;
                int currColIndex = colIndex;

                while (rowIndex < rows && currColIndex < cols)
                {
                    sum += matrix[rowIndex, currColIndex] - currColIndex;
                    rowIndex++;
                    currColIndex++;
                }

                maxSum = Math.Max(maxSum, sum);
            }

            return maxSum;
        }


        public void HorizontalFlip(int width, byte[] image)
        {
            int height = image.Length / width;

            for (int y = 0; y < height; y++)
            {
                int rowStart = y * width;
                int rowEnd = rowStart + width - 1;

                while (rowStart < rowEnd)
                {
                    image[rowStart] ^= image[rowEnd];
                    image[rowEnd] ^= image[rowStart];
                    image[rowStart] ^= image[rowEnd];

                    rowStart++;
                    rowEnd--;
                }
            }
        }

        public static int MinSteps(string s, string t)
        {
            var firstArr = new char[26];
            var secondArr = new char[26];

            for (int i = 0; i < s.Length; i++)
            {
                firstArr[s[i] - 'a']++;
                secondArr[t[i] - 'a']++;
            }

            var minChars = 0;
            for (int i = 0; i < 26; i++)
            {
                minChars += (Math.Abs(firstArr[i] - secondArr[i]));
            }
            return minChars / 2;
        }

        public static int NumWaterBottles(int numBottles, int numExchange)
        {
            var result = numBottles;
            while (numBottles >= numExchange)
            {
                result += (numBottles / numExchange);
                numBottles = numBottles / numExchange + (numBottles % numExchange);
            }
            return result;
        }


        public static int ShortestPathBinaryMatrix(int[][] grid)
        {
            return BFS(grid);
            //var res = DFS(0, 0, grid);
            //return res == int.MaxValue ? -1 : res;
        }


        public static int BFS(int[][] grid)
        {
            if (grid[0][0] == 1) return -1;
            Queue<(int row, int col, int val)> queue = new Queue<(int, int, int)>();
            queue.Enqueue((0, 0, 1));
            var cols = grid[0].Length;
            var rows = grid.Length;

            var min = int.MaxValue;
            grid[0][0] = 1;

            while (queue.Count() > 0)
            {
                var current = queue.Dequeue();
                if (current.row == rows - 1 && current.col == cols - 1)
                    return current.val;

                foreach (var dir in directions)
                {
                    var r = current.row + dir[0];
                    var c = current.col + dir[1];

                    if (r < 0 || c < 0 || r >= rows || c >= cols || grid[r][c] == 1)
                        continue;
                    grid[r][c] = 1;
                    queue.Enqueue((r, c, current.val + 1));
                }

            }

            return min == int.MaxValue ? -1 : min;

        }

        static int[][] directions = new int[][]
                {
                   new int[]{ -1, 0},
                   new int[]{ 0, -1},
                   new int[]{ 1, 0},
                   new int[]{ 0, 1},
                   new int[]{ 1, 1},
                   new int[]{ -1, -1},
                   new int[]{ 1, -1},
                   new int[]{ -1, 1},
                };
        public static int DFS(int row, int col, int[][] grid)
        {
            if (row < 0 || col < 0 || row >= grid.Length || col >= grid[0].Length ||
               grid[row][col] == 1)
                return int.MaxValue;

            if (row == (grid.Length - 1) && col == (grid[0].Length - 1)) return 1;

            grid[row][col] = 1;

            var min = int.MaxValue;
            foreach (var dir in directions)
                min = Math.Min(min, DFS(row + dir[0], col + dir[1], grid));

            grid[row][col] = 0;
            return min != int.MaxValue ? min + 1 : int.MaxValue;
        }

        public static int ShipWithinDays(int[] weights, int days)
        {
            var minCapacity = weights.Max();
            var maxCapacity = weights.Sum();

            var currCap = -1;
            while (minCapacity <= maxCapacity)
            {
                var mid = minCapacity + (maxCapacity - minCapacity) / 2;
                if (CanShip(mid, weights, days))
                {
                    maxCapacity = mid - 1;
                    currCap = mid;
                }
                else
                {
                    minCapacity = mid + 1;
                }
            }
            return currCap;
        }

        public static bool CanShip(int capacity, int[] weights, int days)
        {
            int currCap = capacity;

            foreach (int weight in weights)
            {
                if (currCap - weight >= 0)
                {
                    currCap -= weight;
                }
                else
                {
                    days--;
                    currCap = capacity - weight;
                }
            }
            return days > 0;
        }

        public int[] SortByBits(int[] arr)
        {
            List<(int num, int count)> map = new List<(int, int)>();
            for (int i = 0; i < arr.Length; i++)
            {
                var count = CountsOnes(arr[i]);
                map.Add((arr[i], count));
            }

            return map.OrderBy(x => x.count).Select(x => x.num).ToArray();
        }

        public int CountsOnes(int number)
        {
            var countOnes = 0;
            while (number != 0)
            {
                if ((number & 1) == 1)
                    countOnes++;

                number >>= 1;
            }
            return countOnes;
        }

        public static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            var graph = CreateGraph(edges);

            var leaves = new Queue<int>();
            foreach (var node in graph)
            {
                if (node.Value.Count() <= 1)
                    leaves.Enqueue(node.Key);
            }

            while (n > 2)
            {
                n -= leaves.Count();
                for (int i = leaves.Count(); i > 0; i--)
                {
                    var leaf = leaves.Dequeue();
                    foreach (var node in graph[leaf])
                    {
                        graph[node].Remove(leaf);
                        if (graph[node].Count() == 1)
                            leaves.Enqueue(node);
                    }
                }
            }
            return leaves.ToList();
        }

        public static Dictionary<int, List<int>> CreateGraph(int[][] edges)
        {
            var adjacencyList = new Dictionary<int, List<int>>();

            for (int i = 0; i < edges.Length; i++)
            {
                if (adjacencyList.ContainsKey(edges[i][0]))
                    adjacencyList[edges[i][0]].Add(edges[i][1]);
                else
                    adjacencyList[edges[i][0]] = new List<int> { edges[i][1] };

                if (adjacencyList.ContainsKey(edges[i][1]))
                    adjacencyList[edges[i][1]].Add(edges[i][0]);
                else
                    adjacencyList[edges[i][1]] = new List<int> { edges[i][0] };
            }
            return adjacencyList;
        }


        static int modulo = (int)Math.Pow(10, 9) + 7;
        public static int KnightDialer(int n)
        {
            long modulo = 1000000007;
            long output = 0;
            long[,,] field = new long[4, 3, n + 1];
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 3; c++)
                    output = (output + Jump(r, c, n, field)) % modulo;
            }

            return (int)output;
        }


        public static long Jump(int row, int col, int count, long[,,] field)
        {
            if (row >= 4 || row < 0 || col >= 3 || col < 0) return 0;
            if ((row == 3 && col == 2) || (row == 3 && col == 0)) return 0;
            if (count == 1) return 1;
            if (field[row, col, count] > 0) return field[row, col, count];
            field[row, col, count] =
            Jump(row - 2, col - 1, count - 1, field) % modulo +
            Jump(row - 1, col - 2, count - 1, field) % modulo +
            Jump(row - 2, col + 1, count - 1, field) % modulo +
            Jump(row - 1, col + 2, count - 1, field) % modulo +
            Jump(row + 1, col - 2, count - 1, field) % modulo +
            Jump(row + 2, col - 1, count - 1, field) % modulo +
            Jump(row + 2, col + 1, count - 1, field) % modulo +
            Jump(row + 1, col + 2, count - 1, field) % modulo;

            return field[row, col, count];
        }


        public static bool CanFinish(int numCourses, int[][] prerequisites)
        {
            List<int>[] adjencyList = new List<int>[numCourses];
            Queue<int> queue = new Queue<int>();
            int[] indegree = new int[numCourses];

            for (int i = 0; i < numCourses; i++)
                adjencyList[i] = new List<int>();

            for (int i = 0; i < prerequisites.Length; i++)
                adjencyList[prerequisites[i][1]].Add(prerequisites[i][0]);

            for (int i = 0; i < numCourses; i++)
                foreach (int j in adjencyList[i])
                    indegree[j]++;

            for (int i = 0; i < indegree.Length; i++)
                if (indegree[i] == 0)
                    queue.Enqueue(i);

            int nodeCount = queue.Count;
            while (queue.Count != 0)
            {
                int temp = queue.Dequeue();
                foreach (int i in adjencyList[temp])
                {
                    if (--indegree[i] == 0)
                    {
                        queue.Enqueue(i);
                        nodeCount++;
                    }
                }
            }

            return nodeCount == numCourses;
        }

        public int ShortestPath(int[][] grid, int k)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;

            if (k >= rows - 1 + cols - 1)
                return rows + cols - 2;

            Queue<(int row, int col, int rem, int dist)> queue = new Queue<(int, int, int, int)>();
            HashSet<(int row, int col, int rem)> map = new HashSet<(int row, int col, int rem)>();

            queue.Enqueue((0, 0, k, 0));

            var directions = new List<Point> { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1) };

            while (queue.Count() > 0)
            {
                var currentCell = queue.Dequeue();

                if (currentCell.row == rows - 1 && currentCell.col == cols - 1) return currentCell.dist;
                foreach (var direction in directions)
                {
                    var newCell = new Point(currentCell.row + direction.X, currentCell.col + direction.Y);
                    if (newCell.X >= 0 && newCell.X < rows && newCell.Y >= 0 && newCell.Y < cols)
                    {
                        var newRemoved = currentCell.rem - grid[newCell.X][newCell.Y];
                        if (newRemoved >= 0 && !map.Contains((newCell.X, newCell.Y, newRemoved)))
                        {
                            map.Add((newCell.X, newCell.Y, newRemoved));
                            queue.Enqueue(((newCell.X, newCell.Y, newRemoved, currentCell.dist + 1)));
                        }
                    }
                }
            }

            return -1;
        }


        public static int MaxLevelSum(TreeNode root)
        {
            if (root.left == null && root.right == null) return 1;
            var maxLevel = 1;
            var currentLevel = 1;
            var maxSum = root.val;
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);


            while (queue.Count() > 0)
            {
                var size = queue.Count();
                var currentSum = 0;
                for (int i = 0; i < size; i++)
                {
                    var current = queue.Dequeue();
                    if (current.left != null)
                    {
                        queue.Enqueue(current.left);
                        currentSum += current.left.val;
                    }
                    if (current.right != null)
                    {
                        queue.Enqueue(current.right);
                        currentSum += current.right.val;
                    }

                }
                if (queue.Count() == 0) break;
                currentLevel++;
                if (maxSum < currentSum)
                {
                    maxSum = currentSum;
                    maxLevel = currentLevel;
                }


            }

            return maxLevel;

        }


        public static int NumSplits(string s)
        {
            var good = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsSame(s.Substring(0, i + 1), s.Substring(i + 1, s.Length - i - 1)))
                    good++;
            }
            return good;
        }

        public static bool IsSame(string s1, string s2)
        {
            var letters1 = new char[26];
            var letters2 = new char[26];

            foreach (var s in s1)
            {
                letters1[s - 'a']++;
            }

            foreach (var s in s2)
            {
                letters2[s - 'a']++;
            }

            var l1 = 0;
            var l2 = 0;
            for (int i = 0; i < 26; i++)
            {
                if (letters1[i] > 0) l1++;
                if (letters2[i] > 0) l2++;
            }
            return l1 == l2;
        }

        public static string[] FindRestaurant(string[] list1, string[] list2)
        {
            var commons = new List<string>();

            var set1 = new Dictionary<string, int>();


            for (int i = 0; i < list1.Count(); i++)
                set1.Add(list1[i], i);

            var minIndex = int.MaxValue;
            for (int i = 0; i < list2.Count(); i++)
            {
                var s2 = list2[i];
                if (set1.ContainsKey(s2))
                {
                    var j = set1[s2];
                    if (minIndex == i + j)
                    {
                        commons.Add(s2);
                    }
                    else if (minIndex > i + j)
                    {
                        minIndex = i + j;
                        commons.Clear();
                        commons.Add(s2);
                    }
                }
            }

            return commons.ToArray();
        }

        public int MinScoreTriangulation(int[] values)
        {
            int[,] map = new int[values.Length, values.Length];
            int ans = MinScoreTriangulationRec(values, 1, values.Length - 1, map);

            return ans;
        }

        private int MinScoreTriangulationRec(int[] arr, int i, int j, int[,] map)
        {
            if (i >= j) return 0;
            if (map[i, j] != 0) return map[i, j];

            int maxn = int.MaxValue;
            for (int k = i; k < j; k++)
            {
                int temp = MinScoreTriangulationRec(arr, i, k, map) + MinScoreTriangulationRec(arr, k + 1, j, map) + arr[i - 1] * arr[k] * arr[j];

                if (temp < maxn)
                {
                    maxn = temp;
                }
            }
            map[i, j] = maxn;
            return maxn;
        }


        public static int DaysBetweenDates(string date1, string date2)
        {
            var s1 = date1.Split('-').Select(x => int.Parse(x)).ToArray();
            var s2 = date2.Split('-').Select(x => int.Parse(x)).ToArray();
            var d1 = new DateTime(s1[0], s1[1], s1[2]);
            var d2 = new DateTime(s2[0], s2[1], s2[2]);
            return Math.Abs(d1.Subtract(d1).Days);
        }

        public static bool CanMeasureWater(int jug1Capacity, int jug2Capacity, int targetCapacity)
        {
            if (jug1Capacity + jug2Capacity < targetCapacity) return false;
            var gcd = GCD(Math.Max(jug1Capacity, jug2Capacity), Math.Min(jug1Capacity, jug2Capacity));
            return targetCapacity % gcd == 0;
        }


        public static int GCD(int a, int b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        public static int FurthestBuilding(int[] heights, int bricks, int ladders)
        {
            var queue = new PriorityQueue<int, int>();
            var n = heights.Count() - 1;
            for (int i = 0; i < n; i++)
            {
                var climb = heights[i + 1] - heights[i];
                if (climb <= 0) continue;

                queue.Enqueue(climb, climb);

                if (ladders >= queue.Count) continue;

                var min = queue.Dequeue();
                bricks -= min;
                if (bricks < 0) return i;
            }

            return n;
        }

        public static int MaxSumTwoNoOverlap(int[] A, int L, int M)
        {
            return Math.Max(MaxSum(A, L, M), MaxSum(A, M, L));
        }

        private static int MaxSum(int[] A, int L, int M)
        {
            int sumL = 0, sumM = 0;
            for (int i = 0; i < L + M; i++)
            {
                if (i < L)
                {
                    sumL += A[i];
                }
                else
                {
                    sumM += A[i];
                }
            }
            int ans = sumM + sumL;
            int maxL = sumL;
            for (int i = L + M; i < A.Length; i++)
            {
                sumM += A[i] - A[i - M];
                sumL += A[i - M] - A[i - L - M];
                maxL = Math.Max(maxL, sumL);
                ans = Math.Max(ans, maxL + sumM);
            }

            return ans;
        }

        public static bool IsHappy(int n)
        {
            if (n == 1) return true;

            while (n > 0)
            {
                n = Squares(n);
                if (n == 1)
                    return true;
            }
            return false;
        }

        public static int Squares(int a)
        {
            if (a >= 1 && a <= 9)
                return a * a;
            var count = 0d;
            while (a > 0)
            {
                count += Math.Pow((a % 10), 2);
                a = a / 10;
            }
            return (int)count;
        }

        public static ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) return null;
            ListNode node = new ListNode(head.val);
            ListNode temp = node;
            while (head != null && head.next != null)
            {
                if (head.val != head.next.val)
                {
                    temp.next = new ListNode(head.next.val);
                    temp = temp.next;
                }
                head = head.next;
            }
            return node;
        }

        public static int RemoveDuplicates(int[] nums)
        {
            var current = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[current] == nums[i]) continue;

                current++;
                nums[current] = nums[i];

            }
            return current;
        }

        public static int HammingWeight(uint n)
        {
            var count = 0;
            while (n > 0)
            {
                count += (int)(n & 1);
                n >>= 1;
            }
            return count;
        }

        public static void DeleteNode(ListNode node)
        {
            while (node != null && node.next != null)
            {
                node.val = node.next.val;
                if (node.next.next == null)
                    node.next = null;
                node = node.next;
            }
        }


        public static void MoveZeroes(int[] nums)
        {
            var j = 0;
            var i = 0;
            while (i < nums.Length)
            {
                if (nums[j] == 0)
                {
                    if (nums[i] != 0)
                    {
                        nums[j++] = nums[i];
                        nums[i] = 0;
                    }
                }
                else
                {
                    j++;
                }
                i++;
            }
        }

        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            var prod = products.Where(p => p.StartsWith(searchWord[0])).OrderBy(x => x).ToList();

            var result = new List<IList<string>>();
            for (int i = 0; i < searchWord.Length; i++)
            {
                result.Add(prod.Where(w => w.StartsWith(searchWord.Substring(0, i + 1))).Take(3).ToList());
            }
            return result;
        }

        public static string MinimizeResult(string expression)
        {
            var eq = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+')
                {
                    eq = i;
                    break;
                }
            }

            var res = "";
            var min = int.MaxValue;
            for (int i = 0; i < eq; i++)
            {
                var a = GetValue(expression, 0, i);
                if (a == 0) a = 1;
                var b = GetValue(expression, i, eq);
                for (int j = eq + 2; j <= expression.Length; j++)
                {
                    var c = GetValue(expression, eq + 1, j);
                    var d = GetValue(expression, j, expression.Length);
                    if (d == 0) d = 1;
                    var val = a * (b + c) * d;
                    if (val < min)
                    {
                        min = val;
                        res = string.Concat(expression.Substring(0, i), '(', expression.Substring(i, j - i), ')', expression.Substring(j));
                    }
                }
            }
            return res;
        }

        private static int GetValue(string s, int start, int end)
        {
            var res = 0;
            while (start < end)
            {
                res = res * 10 + (s[start++] - '0');
            }
            return res;
        }
        public static int MaximumProduct(int[] nums, int k)
        {
            var heap = new Heap<int>();
            foreach (var n in nums)
            {
                heap.Add(n);
            }

            for (int i = 0; i < k; i++)
            {
                var first = heap.Remove();
                heap.Add(first + 1);
            }
            var prod = 1;
            foreach (var item in heap.GetElements)
            {
                prod *= item % (int)Math.Pow(10, 7);
            }
            return prod;
        }
        public static int LargestInteger(int num)
        {
            var odds = new List<int>();
            var evens = new List<int>();

            var n = num.ToString();
            for (int k = 0; k < n.Length; k++)
            {
                var el = n[k] - '0';
                if (el % 2 == 0)
                    evens.Add(el);
                else
                    odds.Add(el);
            }

            odds.Sort();
            evens.Sort();
            var res = 0;
            int i = evens.Count - 1, j = odds.Count - 1;
            for (int k = 0; k < n.Length; k++)
            {
                var el = n[k] - '0';
                if (el % 2 == 0)
                {
                    res = res * 10 + evens[i--];
                }
                else
                    res = res * 10 + odds[j--];
            }

            return res;
        }

        //azbzaz
        public static long SumScores(string s)
        {
            var cnt = s.Length;
            var temp = s[s.Length - 1].ToString();
            var j = s.Length - 2;
            var i = 0;
            while (j >= 0)
            {
                if (i < s.Length && s.Substring(0, i + 1) == temp)
                {
                    i++;
                }
                else
                {
                    cnt += (temp.Length - 1);
                }

                temp = s[j] + temp;
                j--;

            }
            return cnt;
        }
        public static int CommonPrefixUtil(string str1, string str2)
        {
            var result = 0;
            int n1 = str1.Length,
                n2 = str2.Length;

            for (int i = 0, j = 0; i <= n1 - 1 && j <= n2 - 1; i++, j++)
            {
                if (str1[i] != str2[j])
                {
                    break;
                }
                result++;
            }

            return result;
        }
        public static int FirstMissingPositive(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                while (nums[i] > 0 && nums[i] <= nums.Length && nums[nums[i] - 1] != nums[i])
                    Swap(i, nums[i] - 1, nums);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != i + 1) return i + 1;
            }

            return nums.Length + 1;
        }

        private static void Swap(int i, int j, int[] nums)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public static int UniqueLetterString(string s)
        {
            var indices = new List<int>[26];
            for (int i = 0; i < 26; i++)
            {
                indices[i] = new List<int>();
            }

            for (int i = 0; i < s.Length; i++)
            {
                indices[s[i] - 'A'].Add(i);
            }

            long res = 0;

            foreach (var list in indices)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int prev = i > 0 ? list[i - 1] : -1;
                    int next = i < list.Count - 1 ? list[i + 1] : s.Length;
                    long prod = (long)(list[i] - prev) * (next - list[i]);
                    res += prod;
                }
            }

            return (int)res;
        }
        public static bool CanCross(int[] stones)
        {
            return FrogJump(stones, 0, 0, stones[0], new int[stones.Length, stones.Length]);
        }

        // 0 not visit, 1 - true, 2 -false
        private static bool FrogJump(int[] stones, int i, int currentStone, int k, int[,] dp)
        {
            while (i < stones.Length && currentStone > stones[i])
                i++;

            if (currentStone == stones[stones.Length - 1]) return true;
            if (i >= stones.Length || currentStone != stones[i]) return false;

            if (dp[i, k] > 0)
                return dp[i, k] == 1;

            var res = FrogJump(stones, i + 1, currentStone + k - 1, k - 1, dp) ||
                      FrogJump(stones, i + 1, currentStone + k, k, dp) ||
                      FrogJump(stones, i + 1, currentStone + k + 1, k + 1, dp);
            dp[i, k] = res ? 1 : 2;
            return res;
        }

        public static bool IsMatch(string str, string pattern)
        {
            var s = 0;
            var p = 0;
            var match = 0;
            var starIdx = -1;
            while (s < str.Length)
            {
                if (p < pattern.Length && (pattern[p] == '?' || str[s] == pattern[p]))
                {
                    s++;
                    p++;
                }
                else if (p < pattern.Length && pattern[p] == '*')
                {
                    starIdx = p;
                    match = s;
                    p++;
                }
                else if (starIdx != -1)
                {
                    p = starIdx + 1;
                    match++;
                    s = match;
                }
                else
                    return false;
            }


            while (p < pattern.Length && pattern[p] == '*')
                p++;

            return p == pattern.Length;
        }

        // Analyze User Website Visit Pattern
        public static List<string> MostVisitedPatterns(string[] names, int[] timestamp, string[] website)
        {
            var n = timestamp.Length;
            var sessions = new List<List<string>>();
            for (int i = 0; i < n; i++)
            {
                sessions.Add(new List<string>());
                sessions[i].Add(names[i]);
                sessions[i].Add(timestamp[i].ToString());
                sessions[i].Add(website[i]);
            }

            sessions = sessions.OrderBy(x => int.Parse(x[1])).ToList();

            var visited = new Dictionary<string, List<string>>();
            for (int i = 0; i < n; i++)
            {
                if (!visited.ContainsKey(sessions[i][0]))
                    visited.Add(sessions[i][0], new List<string>());
                visited[sessions[i][0]].Add(sessions[i][2]);
            }

            var maxCount = 0;
            var maxSeq = "";
            var sequence = new Dictionary<string, int>();
            foreach (var visit in visited)
            {
                if (visit.Value.Count < 3) continue;

                var subSequences = GetSubsequences(visit.Value);
                foreach (var sub in subSequences)
                {
                    var count = 1;
                    if (sequence.ContainsKey(sub))
                        count = ++sequence[sub];
                    else
                        sequence.Add(sub, 1);

                    if (count > maxCount)
                    {
                        maxCount = count;
                        maxSeq = sub;
                    }
                    else if (count == maxCount && string.Compare(sub, maxSeq) < 0)
                        maxSeq = sub;
                }
            }

            return maxSeq.Split(',').ToList();
        }

        public static HashSet<string> GetSubsequences(List<string> list)
        {
            var res = new HashSet<string>();
            for (int i = 0; i < list.Count - 2; i++)
                for (int j = i + 1; j < list.Count - 1; j++)
                    for (int k = j + 1; k < list.Count; k++)
                        res.Add(list[i] + "," + list[j] + "," + list[k]);
            return res;
        }

        public static double F(double x) => Math.Pow(Math.E, x) - Math.Tan(x);

        public static double CuttingMethod(Func<double, double> f, double xn, double xn_1)
        {
            var n = 100;
            if (f(xn) * f(xn_1) >= 0)
                return 0;
            while (n > 1)
            {
                var fxn = f(xn);
                var fxn_1 = f(xn_1);

                var mn = xn - (fxn * (xn - xn_1) / (fxn - fxn_1));
                var fmn = f(mn);
                if (fxn * fmn < 0)
                    xn_1 = mn;
                else if (fxn_1 * fmn < 0)
                    xn = mn;
                else if (fmn == 0)
                    return mn;
                else
                    return 0;
                n--;
            }

            return xn - (f(xn) * (xn - xn_1) / (f(xn) - f(xn_1)));
        }

        public static int[] MinInterval(int[][] intervals, int[] queries)
        {
            var query = new int[queries.Length][];
            for (int i = 0; i < queries.Length; i++)
            {
                query[i] = new int[2];
                query[i][0] = i;
                query[i][1] = queries[i];
            }

            var orderedQueries = query.OrderBy(x => x[1]).ToArray();
            var sort = intervals.OrderBy(x => x[0]).ToArray();
            var ans = new int[queries.Length];
            var sortedSet = new Heap<int[]>(Comparer<int[]>.Create((x, y) => (x[1] - x[0]).CompareTo(y[1] - y[0])));
            var j = 0;
            foreach (var q in orderedQueries)
            {
                while (j < sort.Length && sort[j][0] <= q[1])
                {
                    sortedSet.Add(sort[j++]);
                }
                while (sortedSet.Count > 0)
                {
                    if (sortedSet.Peek()[1] < q[1])
                        sortedSet.Remove();
                    else
                        break;
                }

                ans[q[0]] = sortedSet.Count <= 0 ? -1 : sortedSet.Peek()[1] - sortedSet.Peek()[0] + 1;
            }

            return ans;
        }

        public static IList<string> RestoreIpAddresses(string s)
        {
            var res = new List<string>();
            Restore(s, 0, res, new List<string>());
            return res;
        }

        private static void Restore(string s, int i, List<string> result, List<string> current)
        {
            if (i == s.Length)
            {
                if (current.Count > 4) return;
                result.Add(string.Join('.', current));
                return;
            }
            for (int j = i; j < i + 3 && j < s.Length; j++)
            {
                var sub = s.Substring(i, j - i + 1);
                if (!IsValid(sub)) continue;
                current.Add(sub);
                Restore(s, j + 1, result, current);
                current.RemoveAt(current.Count - 1);

            }
        }

        private static bool IsValid(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            var ipInt = int.Parse(ip);
            if (ipInt > 255) return false;
            return ip.Length == ipInt.ToString().Length;
        }
        private static void QuickSort(int[] nums, int left, int right)
        {
            if (left >= right) return;
            var p = Partion(nums, left, right);
            QuickSort(nums, left, p - 1);
            QuickSort(nums, p + 1, right);
        }

        private static int Partion(int[] nums, int left, int right)
        {
            var pivot = left;
            left++;
            while (left <= right)
            {
                if (nums[pivot] <= nums[left] && nums[pivot] >= nums[right])
                {
                    Swap(nums, left, right);
                }
                if (nums[right] >= nums[pivot])
                    right--;
                if (nums[left] <= nums[pivot])
                    left++;
            }
            Swap(nums, right, pivot);
            return right;
        }

        private static void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        public static List<int> GetConsecutiveCustomer(string filepath)
        {
            var customeDateMap = new Dictionary<int, List<DateTime>>();
            var streamReader = new StreamReader(new FileStream(filepath, FileMode.Open, FileAccess.Read), Encoding.UTF8);
            var consecutiveCustomers = new HashSet<int>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var customerId = int.Parse(line.Split('\t')[1]);
                var date = DateTime.Parse(line.Split('\t')[0]);
                if (customeDateMap.ContainsKey(customerId))
                {
                    if (customeDateMap[customerId].Count == 3)
                    {
                        customeDateMap[customerId].RemoveAt(2);
                    }
                    customeDateMap[customerId].Add(date);
                    if (!consecutiveCustomers.Contains(customerId))
                    {
                        if (customeDateMap[customerId].Count == 3
                        && customeDateMap[customerId][0] == customeDateMap[customerId][1].AddDays(1)
                        && customeDateMap[customerId][0] == customeDateMap[customerId][2].AddDays(2))
                        {
                            consecutiveCustomers.Add(customerId);
                        }
                    }
                }
                else
                {
                    customeDateMap.Add(customerId, new List<DateTime>() { date });
                }
            }
            return consecutiveCustomers.ToList();
        }

        private static string[] untillTwenty = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fiveteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty" };
        private static string[] tens = new string[] { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

        public static string NumberToWords(int num)
        {
            var result = new StringBuilder();
            if (num <= 20)
                result.Append(untillTwenty[num]);
            else if (num < 100)
                result.Append(tens[num / 10]).Append(" ").Append(NumberToWords(num % 10));
            else if (num < 1000)
                result.Append(NumberToWords(num / 100)).Append(" Hundred ").Append(NumberToWords(num % 100));
            else if (num < 1000000)
                result.Append(NumberToWords(num / 1000)).Append(" Thousand ").Append(NumberToWords(num % 1000));
            else if (num < 1000000000)
                result.Append(NumberToWords(num / 1000000)).Append(" Million ").Append(NumberToWords(num % 1000000));
            else
                result.Append(NumberToWords(num / 1000000000)).Append(" Billion ").Append(NumberToWords(num % 1000000000));

            return result.ToString();
        }

        public static int LengthOfLIS(int[] nums)
        {
            var tails = new int[nums.Length];
            var size = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                var l = 0;
                var r = size;
                while (l < r)
                {
                    var mid = l + (r - l) / 2;
                    if (tails[mid] >= nums[i])
                        r = mid;
                    else
                        l = mid + 1;
                }
                tails[l] = nums[i];
                if (l == size) size++;
            }
            return size;
        }

        public static bool IsPalindrome(ListNode head)
        {
            ListNode nodeForReverse = head;
            ListNode temp = new ListNode(head.val);
            ListNode current = temp;
            head = head.next;
            while (head != null)
            {
                current.next = new ListNode(head.val);
                current = current.next;
                head = head.next;
            }

            var reverse = Reverse(head, null);
            while (reverse != null && temp != null)
            {
                Console.WriteLine(temp.val);
                if (reverse.val != temp.val) return false;
                reverse = reverse.next;
                temp = temp.next;
            }
            return reverse == null && temp == null;

        }

        private static ListNode Reverse(ListNode head, ListNode previous)
        {
            if (head == null) return previous;
            var temp = head.next;
            head.next = previous;
            return Reverse(temp, head);
        }

        public static ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            while (head != null)
            {
                var newHead = head.next;
                head.next = prev;
                prev = head;
                head = newHead;
            }
            return prev;
        }

        public static IList<string> BraceExpansionII(string expression)
        {
            var res = new HashSet<string>();
            Dfs(expression, 0, "", res);
            return res.OrderBy(x => x).ToList();
        }

        private static void Dfs(string exp, int pos, string current, HashSet<string> result)
        {
            if (pos == exp.Length)
            {
                result.Add(current);
                return;
            }

            if (exp[pos] >= 'a' && exp[pos] <= 'z')
            {
                Dfs(exp, pos + 1, current + exp[pos], result);
            }
            else if (exp[pos] == ',')
            {
                result.Add(current);
                Dfs(exp, pos + 1, "", result);
            }
            else if (exp[pos] == '{')
            {
                var count = 1;
                var i = pos + 1;
                while (count != 0)
                {
                    if (exp[i] == '{')
                        count++;
                    else if (exp[i] == '}')
                    {
                        count--;
                        if (count == 0) break;
                    }
                    i++;
                }

                var temp = new HashSet<string>();
                Dfs(exp.Substring(pos + 1, i - pos - 1), 0, "", temp);
                foreach (var t in temp)
                    Dfs(exp, i + 1, current + t, result);

            }
        }

        public static List<string> generateParenthesis(int n)
        {
            List<string> list = new List<string>();
            backtrack(list, "", 0, 0, n);
            return list;
        }

        public static void backtrack(List<string> list, string str, int open, int close, int max)
        {

            if (str.Length == max * 2)
            {
                list.Add(str);
                return;
            }

            if (open < max)
                backtrack(list, str + "(", open + 1, close, max);
            if (close < open)
                backtrack(list, str + ")", open, close + 1, max);
        }

        public static int[][] MatrixBlockSum(int[][] mat, int k)
        {
            var ans = new int[mat.Length][];

            for (int i = 0; i < mat.Length; i++)
            {
                ans[i] = new int[mat[i].Length];
                for (int j = 0; j < mat[i].Length; j++)
                {
                    var rStart = Math.Max(0, i - k);
                    var rEnd = Math.Min(mat.Length - 1, i + k);

                    var cStart = Math.Max(0, j - k);
                    var cEnd = Math.Min(mat[i].Length - 1, j + k);

                    for (int m = rStart; m <= rEnd; m++)
                        for (int n = cStart; n <= cEnd; n++)
                            ans[i][j] += mat[m][n];
                }
            }
            return ans;
        }

        public static int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
        {
            Fill(image, sr, sc, newColor, image[sr][sc]);
            return image;
        }
        public static void Fill(int[][] image, int sr, int sc, int newColor, int oldColor)
        {
            if (sr >= image.Length || sr < 0 || sc >= image[0].Length || sc < 0) return;
            if (image[sr][sc] != oldColor) return;

            image[sr][sc] = newColor;

            Fill(image, sr + 1, sc, newColor, oldColor);
            Fill(image, sr - 1, sc, newColor, oldColor);
            Fill(image, sr, sc - 1, newColor, oldColor);
            Fill(image, sr, sc + 1, newColor, oldColor);
        }
        public static int CoinChange(int[] coins, int amount)
        {
            int[] d = new int[amount + 1];

            for (int i = 1; i <= amount; i++)
            {
                d[i] = int.MaxValue;
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i >= coins[j] && d[i - coins[j]] != int.MaxValue)
                    {
                        d[i] = Math.Min(d[i], 1 + d[i - coins[j]]);
                    }
                }
            }
            return d[amount] == int.MaxValue ? -1 : d[amount];
        }
        public static string ReverseParentheses(string s)
        {
            var stack = new Stack<char>();

            foreach (var ch in s)
            {
                if (ch == ')')
                {
                    var current = new StringBuilder();
                    while (stack.Count > 0 && stack.Peek() != ')')
                    {
                        current.Append(stack.Pop());
                    }
                    stack.Pop();
                    for (int j = 0; j < current.Length; j++)
                        stack.Push(current[j]);
                }
                else
                    stack.Push(ch);
            }

            var res = new char[stack.Count];
            var i = stack.Count - 1;
            while (stack.Count > 0)
            {
                res[i--] = stack.Pop();
            }
            return new string(res.Reverse().ToArray());
        }
        public static IList<IList<int>> KSum(int[] nums, int start, int k, int target)
        {
            var len = nums.Length;
            var res = new List<IList<int>>();
            if (k == 2)
            {
                var left = start;
                var right = len - 1;
                while (left < right)
                {
                    var sum = nums[left] + nums[right];
                    if (sum == target)
                    {
                        res.Add(new List<int> { nums[left], nums[right] });
                        while (left < right && nums[left] == nums[left + 1])
                            left++;
                        while (left < right && nums[right] == nums[right - 1])
                            right--;
                        left++;
                        right--;
                    }
                    else
                        if (sum < target) left++;
                    else
                        right--;
                }
            }
            else
            {
                for (int i = start; i <= len - k; i++)
                {
                    while (i > start && i < len - 1 && nums[i] == nums[i - 1])
                        i++;
                    var temp = KSum(nums, i + 1, k - 1, target - nums[i]);
                    foreach (var element in temp)
                    {
                        element.Add(nums[i]);
                    }
                    foreach (var val in temp)
                    {
                        res.Add(val);
                    }
                }
            }

            return res;
        }

        public static int StrStr(string haystack, string needle)
        {
            if (needle.Length == 0) return 0;
            for (int i = 0; i + needle.Length <= haystack.Length; i++)
            {
                if (haystack[i] == needle[0])
                {
                    var x = haystack.Substring(i, needle.Length);
                    if (x == needle)
                        return i;
                }
            }
            return -1;
        }

        //2126753390
        //1702766719
        public static int GuessNumber(int n)
        {
            var left = 1;
            var right = n;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                var g = 1702766719;
                if (g == 0)
                    return mid;
                else if (g == -1) right = mid - 1;
                else left = mid + 1;

            }
            return 0;
        }

        public static int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle.Count == 1) return triangle[0][0];
            var dp = new int[triangle.Count + 1];

            for (int i = triangle.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < triangle[i].Count; j++)
                {
                    dp[j] = Math.Min(dp[j], dp[j + 1]) + triangle[i][j];
                }
            }
            return dp[0];
        }
        public static int FirstUniqChar(string s)
        {
            var uniqIndex = int.MaxValue;
            var letters = new int[26];
            for (int i = 0; i < s.Length; i++)
            {
                letters[s[i] - 'a']++;
                if (uniqIndex != int.MaxValue && letters[s[uniqIndex] - 'a'] > 1) uniqIndex = int.MaxValue;
                if (letters[s[i] - 'a'] == 1)
                    uniqIndex = Math.Min(uniqIndex, i);
            }

            return uniqIndex == int.MaxValue ? -1 : uniqIndex;
        }

        IList<string> result = new List<string>();
        private void DFS(string num, int target, StringBuilder expression, decimal prevNum, decimal prevSum, int idx)
        {
            if (idx == num.Length && prevSum == target)
            {
                result.Add(expression.ToString());
                return;
            }

            for (var i = idx; i < num.Length; i++)
            {
                if (num[idx] == '0' && i != idx) break; // 0 ok, but 01 isn't

                var curNumStr = num.Substring(idx, i - idx + 1);
                var curNum = System.Convert.ToDecimal(curNumStr);
                var expressionLen = expression.Length;
                if (idx == 0)
                {
                    DFS(num, target, expression.Append(curNum), curNum, curNum, i + 1);
                    expression.Length = expressionLen;
                }
                else
                {
                    DFS(num, target, expression.Append('+').Append(curNum), curNum, prevSum + curNum, i + 1);
                    expression.Length = expressionLen;
                    DFS(num, target, expression.Append('-').Append(curNum), -curNum, prevSum - curNum, i + 1);
                    expression.Length = expressionLen;
                    DFS(num, target, expression.Append('*').Append(curNum), prevNum * curNum, (prevNum * curNum) + (prevSum - prevNum), i + 1);
                    expression.Length = expressionLen;
                }
            }
        }

        private static string _characters;
        private static int _combinationLenght;
        private static Queue<string> queue = new Queue<string>();

        //abc 2 -> ab ac bc
        //gkosu 3 -> gko gks gku gos gou gsu kos kou ksu osu
        public static void CombinationIterator(string characters, int combinationLength)
        {
            _characters = characters;
            _combinationLenght = combinationLength;
            GetCombination(0, new List<char>());

        }

        public static void GetCombination(int index, List<char> result)
        {
            if (_combinationLenght == result.Count)
            {
                queue.Enqueue(new string(result.ToArray()));
                return;
            }

            for (int i = index; i < _characters.Length; i++)
            {
                result.Add(_characters[i]);
                GetCombination(i + 1, result);
                result.RemoveAt(result.Count - 1);
            }
        }

        public static int MaxWidthOfVerticalArea(int[][] points)
        {
            var horizontal = points.Select(x => x.FirstOrDefault()).OrderBy(x => x).ToArray();
            var res = 0;
            for (int i = 1; i < horizontal.Count(); i++)
            {
                res = Math.Max(res, horizontal[i] - horizontal[i - 1]);
            }
            return res;
        }

        public static int[] MinOperations(string boxes)
        {
            var result = new int[boxes.Length];
            for (int i = 0; i < boxes.Length; i++)
            {
                var left = i - 1;
                var right = i + 1;
                while (left >= 0)
                {
                    if (boxes[left] == '1')
                    {
                        result[i] += Math.Abs(i - left);
                    }
                    left--;
                }
                while (right < boxes.Length)
                {
                    if (boxes[right] == '1')
                    {
                        result[i] += Math.Abs(i - right);
                    }
                    right++;
                }
            }
            return result;
        }

        public static int[] CountPoints(int[][] points, int[][] queries)
        {
            var result = new int[queries.Length];
            var q = 0;
            for (int i = 0; i < queries.Length; i++)
            {
                var x = queries[i][0];
                var y = queries[i][1];
                var r = queries[i][2];
                for (int j = 0; j < points.Length; j++)
                {
                    var x0 = points[j][0];
                    var y0 = points[j][1];
                    var eq = Math.Pow(x - x0, 2) + Math.Pow(y - y0, 2);
                    if (eq <= Math.Pow(r, 2))
                        result[q]++;
                }
                q++;
            }
            return result;
        }
        public static IList<TreeNode> GenerateTrees(int n)
        {
            var result = GenerateBST(1, n);
            return result;
        }

        public static IList<TreeNode> GenerateBST(int s, int e)
        {
            var list = new List<TreeNode>();
            if (s > e) list.Add(null);
            if (s == e) list.Add(new TreeNode(s));
            else
            {
                for (int i = s; i <= e; i++)
                {
                    var lefts = GenerateBST(s, i - 1);
                    var rights = GenerateBST(i + 1, e);
                    foreach (var l in lefts)
                        foreach (var r in rights)
                            list.Add(new TreeNode(i, l, r));
                }
            }
            return list;
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null) return null;
            if (root == p || root == q)
                return root;
            var left = LowestCommonAncestor(root.left, p, q);
            var right = LowestCommonAncestor(root.right, p, q);
            if (left != null && right != null) return root;
            if (left != null) return left;
            if (right != null) return right;
            return null;
        }

        public static void BFSTree(TreeNode root)
        {
            var q = new Queue<TreeNode>();
            var l = new List<List<TreeNode>>() { new List<TreeNode> { root } };
            q.Enqueue(root);
            var c = new List<TreeNode>();
            while (q.Count > 0)
            {

                if (q.Count == c.Count)
                {
                    l.Add(c);
                    c = new List<TreeNode>();
                }

                var curr = q.Dequeue();
                if (curr.left != null)
                {
                    q.Enqueue(curr.left);
                    c.Add(curr.left);
                }
                if (curr.right != null)
                {
                    q.Enqueue(curr.right);
                    c.Add(curr.right);
                }

            }
        }

        public static int KthSmallest(TreeNode root, int k)
        {
            k0 = k;
            var d = Helper(root);
            return d;
        }

        private static int k0;
        public static int Helper(TreeNode node)
        {
            if (node == null) return -1;
            var leftVal = Helper(node.left);
            if (leftVal != -1) return leftVal;
            if (k0 == 1) return node.val;
            k0--;
            return Helper(node.right);
        }

        public static ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            BigInteger num1 = l1.val;
            BigInteger num2 = l2.val;
            var result = new ListNode();
            var pathName = result;

            while (l1?.next != null || l2?.next != null)
            {
                if (l1?.next != null)
                {
                    num1 *= 10;
                    num1 += l1.next.val;
                    l1 = l1?.next;
                }
                if (l2?.next != null)
                {
                    num2 *= 10;
                    num2 += l2.next.val;
                    l2 = l2?.next;
                }
            }

            var sum = num1 + num2;
            var s = sum.ToString().Length;
            BigInteger cnt = BigInteger.Pow(10, (s - 1));// Math.Pow(10, (s - 1));
            while (cnt > 0)
            {
                var r = sum / cnt;
                sum %= cnt;
                cnt /= 10;
                pathName.val = (int)r;
                if (cnt > 0)
                {
                    pathName.next = new ListNode();
                    pathName = pathName.next;
                }
            }
            return result;
        }

        private static TreeNode TrimBST(TreeNode root, int low, int high)
        {
            if (root == null) return null;

            root.left = TrimBST(root.left, low, high);
            root.right = TrimBST(root.right, low, high);

            if (root.val < low)
                return root.right;

            if (root.val > high)
                return root.left;

            return root;
        }

        public static IList<int> MajorityElement(int[] nums)
        {
            if (nums.Length == 1) return nums;
            if (nums.Length == 2 && nums[1] == nums[0]) return new List<int> { nums[0] };
            if (nums.Length == 2) return nums;
            var res = new List<int>();
            var n = nums.Length;
            var times = Math.Round(n / 3d, MidpointRounding.ToZero);
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                if (dict.ContainsKey(nums[i]))
                    dict[nums[i]]++;
                else dict.Add(nums[i], 1);

                if (dict[nums[i]] > times && (!res.Contains(nums[i])))
                    res.Add(nums[i]);
            }
            return res;
        }

        //s = "abcde", goal = "cdeab"
        public static bool RotateString(string s, string goal)
        {
            if (s.Length != goal.Length) return false;
            for (int i = 0; i < s.Length; i++)
            {
                var temp = s.Substring(i, s.Length - i) + s.Substring(0, i);
                if (temp == goal)
                    return true;
            }
            return false;
        }

        public static int NumRollsToTarget(int n, int k, int target)
        {
            var mod = (int)Math.Pow(10, 9) + 7;
            var res = new int[n + 1, target + 1];
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < target + 1; j++)
                {
                    res[i, j] = -1;
                }
            }
            return Rolls(n, k, target, res, mod);
        }
        public static int Rolls(int n, int k, int target, int[,] result, int mod)
        {
            if (n == 0 && target == 0) return 1;
            if (n <= 0 || target <= 0) return 0;
            if (result[n, target] != -1) return result[n, target];

            long res = 0;

            for (int i = 1; i <= k; i++)
                res = res % mod + Rolls(n - 1, k, target - i, result, mod) % mod;

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
    public class CombinationIterator
    {
        string _characters;
        int _combinationLenght;
        Queue<string> stack = new Queue<string>();

        public CombinationIterator(string characters, int combinationLength)
        {
            _characters = characters;
            _combinationLenght = combinationLength;
            if (characters.Length >= combinationLength)
            {
                for (int i = 0; i < characters.Length; i++)
                {

                }
            }

        }

        public string Next()
        {
            if (stack.Count != 0)
            {
                return stack.Dequeue();
            }
            return "";
        }

        public bool HasNext()
        {
            if (stack.Count == 0) return false;
            return true;
        }
    }

}
