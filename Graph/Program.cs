using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Graph
{
    public struct Node
    {
        public int pairs;
        public int open;
        public int closed;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Timus2062();
        }


        private static void Timus2062()
        {
            var n = int.Parse(Console.ReadLine());
            var divisors = GetDivisors(n);
            var particles = Console.ReadLine().Split().Select(long.Parse).ToList();
            var q = long.Parse(Console.ReadLine());
            var fenwickTree = new FenwickTree(n);
            for (int i = 0; i < q; i++)
            {
                var action = Console.ReadLine().Split().Select(int.Parse).ToArray();
                if (action[0] == 1)
                {
                    var ans = particles[action[1] - 1];
                    foreach (var d in divisors[action[1]])
                        ans += fenwickTree.Sum(d);
                    Console.WriteLine(ans);
                }
                else
                {
                    var l = action[1];
                    var r = action[2];
                    var val = action[3];
                    fenwickTree.Update(l, r, val);
                }
            }
        }

        private static List<int>[] GetDivisors(int n)
        {
            var divisors = new List<int>[300010];
            for (int i = 1; i <= n; i++)
                for (int j = 1; i * j <= n; j++)
                {
                    divisors[i * j] ??= new List<int>();
                    divisors[i * j].Add(i);
                }
            return divisors;
        }

        // to be continued
        static void Timus1930()
        {
            var nm = Console.ReadLine().Split();
            var n = Int32.Parse(nm[0]);
            var m = Int32.Parse(nm[1]);
            var graph = new Graph<int>();
            for (int i = 0; i < n; i++)
            {
                graph.AddVertex(i);
            }

            for (int i = 0; i < m; i++)
            {
                var c = Console.ReadLine().Split();
                var v1 = Int32.Parse(c[0]) - 1;
                var v2 = Int32.Parse(c[1]) - 1;
                graph.AddEdge(v1, v2, v1 < v2 ? 1f : 0f);
            }
            var ivanOrlov = Console.ReadLine().Split();
            var ivan = Int32.Parse(ivanOrlov[0]) - 1;
            var orlov = Int32.Parse(ivanOrlov[1]) - 1;
            var d = graph.ShortestPath(ivan, orlov).Count - 2;
            Console.WriteLine(d);
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        static void SerejaAndBrackets()
        {
            var brackets = Console.ReadLine();
            var m = brackets.Length;
            var bTree = new SegmentTree(m);
            bTree.BuildBracketTree(brackets, 0, m - 1, 1);
            var n = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                var l = Int32.Parse(input[0]) - 1;
                var r = Int32.Parse(input[1]) - 1;

                var pairs = bTree.GetBracket(0, m - 1, l, r, 1).pairs * 2;
                Console.WriteLine(pairs);
            }

        }

        static void AntColony()
        {
            var n = Int32.Parse(Console.ReadLine());
            var segmentTree = new SegmentTree(n);
            var tupleSegmentTree = new SegmentTree(n);
            var arr = Console.ReadLine().Split().Select(Int64.Parse).ToArray();
            var tupleArr = new Tuple<long, long>[n];
            for (int i = 0; i < n; i++)
                tupleArr[i] = Tuple.Create(arr[i], 1L);

            segmentTree.BuildGcdTree(arr, 0, n - 1, 1);
            tupleSegmentTree.BuildAdditionalMinTree(tupleArr, 0, n - 1, 1);

            var q = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < q; i++)
            {
                var input = Console.ReadLine().Split();
                var l = Int64.Parse(input[0]) - 1;
                var r = Int64.Parse(input[1]) - 1;
                long ans = r - l + 1;
                var gcd = segmentTree.GetGcd(0, n - 1, l, r, 1);
                var min = tupleSegmentTree.GetAdditionalMin(0, n - 1, l, r, 1);
                if (gcd == min.Item1)
                    ans -= min.Item2;
                Console.WriteLine(ans);
            }
        }

        static void TeamBuilding()
        {
            var graph = new Graph<int>();
            var n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                graph.AddVertex(input[0]);
                graph.AddVertex(input[1]);
                graph.AddEdge(input[0], input[1], 1);
            }

            var count = 0;
            foreach (var adj in graph.GetAdjacencyList())
            {
                var visited = new HashSet<int>();
                graph.Dfs(adj.Key, visited);
                count++;
                // graph.Dfs();

            }
        }

        static void GraphDec()
        {
            var graph = new Graph<int>();
            string[] s = Console.In.ReadToEnd().Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Length; i += 2)
            {
                var first = Int32.Parse(s[i]);
                var second = Int32.Parse(s[i + 1]);
                graph.AddVertex(first);
                graph.AddVertex(second);
                graph.AddEdge(first, second, 1);
            }

            var c = graph.CountComponents();
            if (c % 2 == 0)
                Console.WriteLine(0);
            else Console.WriteLine(1);
        }
        static void BearAndFriendshipCondition()
        {
            var nm = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            var dj = new DisjointSet(n);
            var graph = new Dictionary<int, List<int>>();

            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                dj.Union(input[0], input[1]);
                try
                {
                    graph[input[0]].Add(input[1]);
                }
                catch (Exception e)
                {
                    graph.Add(input[0], new List<int> { input[1] });
                }

                try
                {
                    graph[input[1]].Add(input[0]);
                }
                catch (Exception e)
                {
                    graph.Add(input[1], new List<int> { input[0] });
                }
                //if (graph.ContainsKey(input[0]))
                //    graph[input[0]].Add(input[1]);
                //else graph.Add(input[0], new List<int> { input[1] });
                //if (graph.ContainsKey(input[1]))
                //    graph[input[1]].Add(input[0]);
                //else graph.Add(input[1], new List<int> { input[0] });
            }

            var parents = new Dictionary<int, List<int>>();
            for (int i = 1; i <= n; i++)
            {
                var p = dj.FindRepresentative(i);
                try
                {
                    parents[p].Add(i);
                }
                catch (Exception e)
                {
                    parents.Add(p, new List<int> { i });
                }
                //if (parents.ContainsKey(p))
                //    parents[p].Add(i);
                //else parents.Add(p, new List<int> { i });
            }

            foreach (var g in graph)
            {
                if (!parents.ContainsKey(g.Key) || parents[g.Key].Count <= 0) continue;
                var degree = parents[g.Key].Where(p => graph.ContainsKey(p)).Sum(p => graph[p].Count);
                var k = parents[g.Key].Count;
                k = k * (k - 1) / 2;
                if (k == degree / 2) continue;
                Console.WriteLine("NO");
                return;
            }

            Console.WriteLine("YES");
        }

        static void AliceAndHairdresser()
        {
            var nml = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nml[0];
            var m = nml[1];
            var l = nml[2];

            var hairs = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            for (int i = 0; i < m; i++)
            {
                var request = Console.ReadLine().Split();
                if (request[0] == "0")
                {
                    var ans = hairs.Where(x => x > l).Max() % l;
                    Console.WriteLine(ans);
                }
                else
                {
                    var p = Int32.Parse(request[1]);
                    var d = Int32.Parse(request[2]);

                    hairs[p - 1] += d;
                }
            }
        }

        public static void LearningLanguages()
        {
            var nm = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nm[0];
            var k = nm[1];
            var dj = new DisjointSet(n + 1);
            var languages = new Dictionary<int, List<int>>();
            var noLang = 0;
            for (int i = 0; i < n; i++)
            {
                var employee = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                if (employee[0] == 0)
                    noLang++;

                for (int j = 1; j < employee.Length; j++)
                {
                    if (languages.ContainsKey(employee[j]))
                        languages[employee[j]].Add(i);
                    else
                    {
                        languages.Add(employee[j], new List<int> { i });
                    }
                }
            }

            var amount = n;
            for (int i = 1; i <= k; i++)
            {
                if (!languages.ContainsKey(i)) continue;
                for (int j = 0; j < languages[i].Count && j + 1 < languages[i].Count; j++)
                {
                    if (dj.Union(languages[i][j], languages[i][j + 1]))
                        amount--;
                }
            }

            if (noLang == n)
                amount++;
            Console.WriteLine(amount - 1);
        }

        public static void EdgyTrees()
        {
            var nm = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nm[0];
            var k = nm[1];
            var graph = new Graph<double>();

            for (int i = 1; i <= n; i++)
                graph.AddVertex(i);

            for (int i = 1; i <= n - 1; i++)
            {
                var input = Console.ReadLine().Split().Select(Double.Parse).ToArray();
                graph.AddEdge(input[0], input[1], input[2]);
            }

            var visited = new HashSet<double>();
            var degreeList = new List<long>();
            for (int i = 1; i <= n; i++)
            {
                if (!visited.Contains(i))
                {
                    graph.Count = 0;
                    graph.Dfs(i, visited);
                    degreeList.Add(graph.Count);
                }
            }

            var mod = new BigInteger(Math.Pow(10, 9) + 7);
            var ans = BigInteger.Zero;
            foreach (var c in degreeList)
            {
                BigInteger.DivRem(BigInteger.Add(ans, BigInteger.ModPow(c, k, mod)), mod, out ans);
            }

            var all = BigInteger.ModPow(n, k, mod);
            BigInteger.DivRem(BigInteger.Add(BigInteger.Subtract(all, ans), mod), mod, out var rem);
            Console.WriteLine(rem);
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
        public static void CyclicComponents()
        {
            var nm = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            var graph = new Graph<int>();

            for (int i = 1; i <= n; i++)
                graph.AddVertex(i);

            for (int i = 1; i <= m; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                graph.AddEdge(input[0], input[1], 1);
            }

            Console.WriteLine(graph.CountSingleCycles());
        }

        static void HE_ZerosAndOnes()
        {
            int n = Int32.Parse(Console.ReadLine());
            int q = Int32.Parse(Console.ReadLine());
            SegmentTree segmentTree = new SegmentTree(n);
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = 1;
            }
            segmentTree.BuildSumTree(arr, 1, 1, n);
            for (int i = 0; i < q; i++)
            {
                var input = Console.ReadLine().Split();
                if (Int32.Parse(input[0]) == 0)
                {
                    segmentTree.UpdateValue(1, n, Int32.Parse(input[1]), 0, 1);
                }
                else
                {
                    //Console.WriteLine(segmentTree.GetSum(1, 1, n, int.Parse(input[1])));
                }
            }

        }

        static Dictionary<string, int> map = new Dictionary<string, int>();
        static List<List<string>> groups = new List<List<string>>();
        public static void LegendaryTeams()
        {
            var n = Int32.Parse(Console.ReadLine());
            var array = new int[n + 1];
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                groups.Add(new List<string>());
                for (int l = 0; l < 3; l++)
                {
                    groups[i].Add(input[l]);
                    map[groups[i][l]] = 0;
                }
            }
            for (int i = 0; i < groups.Count; i++)
            {
                map[groups[i][0]] = 1;
                map[groups[i][1]] = 1;
                map[groups[i][2]] = 1;
                Recursion(i + 1, 1);
                map[groups[i][0]] = 0;
                map[groups[i][1]] = 0;
                map[groups[i][2]] = 0;
            }
            Console.WriteLine(max);
        }
        static int max = 0;
        private static void Recursion(int index, int teamNum)
        {
            max = Math.Max(max, teamNum);
            if (index >= groups.Count) return;
            if (map[groups[index][0]] == 0 && map[groups[index][1]] == 0 && map[groups[index][2]] == 0)
            {
                map[groups[index][0]] = 1;
                map[groups[index][1]] = 1;
                map[groups[index][2]] = 1;
                Recursion(index + 1, teamNum + 1);
                map[groups[index][0]] = 0;
                map[groups[index][1]] = 0;
                map[groups[index][2]] = 0;
            }
            Recursion(index + 1, teamNum);
        }

        public static void NewYearTransportation()
        {
            var nt = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nt[0];
            var t = nt[1];
            var graph = new Graph<int>(true);
            var cells = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            for (int i = 1; i <= n; i++)
                graph.AddVertex(i);

            for (int i = 0; i < cells.Length; i++)
            {
                graph.AddEdge(i + 1, cells[i] + i + 1, 1.0f);
            }
            //  var dfs = graph.Dfs(1);
            // Console.WriteLine(dfs.Contains(t) ? "YES" : "NO");
        }

        public static void BytheUndergroundorbyFoot()
        {
            {
                var v1v2 = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();
                var v1 = v1v2[0];
                var v2 = v1v2[1];
                var graph = new Graph<KeyValuePair<double, double>>();
                var n = Convert.ToInt32(Console.ReadLine());
                var points = new List<KeyValuePair<double, double>>();
                var stationEdges = new List<Tuple<KeyValuePair<double, double>, KeyValuePair<double, double>>>();
                for (int i = 0; i < n; i++)
                {
                    var xy = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();
                    points.Add(new KeyValuePair<double, double>(xy[0], xy[1]));
                    graph.AddVertex(points[i]);
                }

                while (true)
                {
                    var xy = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                    if (xy[0] == 0 && xy[1] == 0)
                        break;
                    var point1 = points[xy[0] - 1];
                    var point2 = points[xy[1] - 1];
                    stationEdges.Add(Tuple.Create(point1, point2));
                    graph.AddEdge(point1, point2, Math.Round(Time(point1, point2, v2), 13));
                }

                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        graph.AddEdge(points[i], points[j], Math.Round(Time(points[i], points[j], v1), 13));
                    }
                }

                var a = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var b = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var ap = new KeyValuePair<double, double>(a[0], a[1]);
                var bp = new KeyValuePair<double, double>(b[0], b[1]);
                graph.AddVertex(ap);
                graph.AddVertex(bp);
                graph.AddEdge(ap, bp, Math.Round(Time(ap, bp, v1), 13));
                for (int i = 0; i < n; i++)
                {
                    graph.AddEdge(ap, points[i], Math.Round(Time(ap, points[i], v1), 13));
                    graph.AddEdge(bp, points[i], Math.Round(Time(bp, points[i], v1), 13));
                }

                var dijk = graph.OrderedListShortesDistance(ap, bp);
                if (dijk.Count == 1)
                {
                    Console.WriteLine("0.000000");
                    Console.WriteLine(0);
                    return;
                }

                List<int> pathList = new List<int>();

                for (int i = 0; i < dijk.Count - 1; i++)
                {
                    if (stationEdges.Contains(Tuple.Create(dijk[i].Item1, dijk[i + 1].Item1)) ||
                        stationEdges.Contains(Tuple.Create(dijk[i + 1].Item1, dijk[i].Item1)))
                    {
                        var h = points.IndexOf(dijk[i].Item1) + 1;
                        var f = points.IndexOf(dijk[i + 1].Item1) + 1;
                        if (!pathList.Contains(h))
                        {
                            pathList.Add(h);
                        }

                        if (!pathList.Contains(f))
                        {
                            pathList.Add(f);
                        }
                    }
                }

                Console.WriteLine($"{dijk.Last().Item2}");
                Console.Write(pathList.Count + " ");
                foreach (var item in pathList)
                {
                    Console.Write(item + " ");
                }
            }
        }
        public static double Time(KeyValuePair<double, double> a, KeyValuePair<double, double> b, double v)
        {
            var y0 = Math.Abs(b.Value - a.Value);
            var x0 = Math.Abs(b.Key - a.Key);
            var s = Math.Sqrt(x0 * x0 + y0 * y0);
            return (double)(s / v);
        }

        public static void Labyrinth()
        {
            n = Convert.ToInt32(Console.ReadLine());
            matrix = new int[n, n];
            fill = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                for (var j = 0; j < input.Length; j++)
                {
                    if (input[j] == '#')
                        matrix[i, j] = 1;
                }
            }
            FloodFill(0, 0);
            FloodFill(n - 1, n - 1);

            var ans = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (fill[i, j] != 1) continue;
                    if (i + 1 > n - 1 || fill[i + 1, j] != 1)
                        ans++;
                    if (j + 1 > n - 1 || fill[i, j + 1] != 1)
                        ans++;
                    if (i - 1 < 0 || fill[i - 1, j] != 1)
                        ans++;
                    if (j - 1 < 0 || fill[i, j - 1] != 1)
                        ans++;
                }
            }

            Console.WriteLine((ans - 4) * 9);
        }

        private static int[,] fill;
        private static int[,] matrix;
        public static void FloodFill(int x, int y)
        {
            fill[x, y] = 1;
            if (x + 1 < n && matrix[x + 1, y] != 1 && fill[x + 1, y] != 1)
                FloodFill(x + 1, y);
            if (y + 1 < n && matrix[x, y + 1] != 1 && fill[x, y + 1] != 1)
                FloodFill(x, y + 1);
            if (x - 1 >= 0 && matrix[x - 1, y] != 1 && fill[x - 1, y] != 1)
                FloodFill(x - 1, y);
            if (y - 1 >= 0 && matrix[x, y - 1] != 1 && fill[x, y - 1] != 1)
                FloodFill(x, y - 1);
        }

        public static void Network()
        {
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            var edges = new List<Edge<int>>();
            var dj = new DisjointSet(n);

            for (int i = 1; i <= m; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                edges.Add(new Edge<int>(input[0], input[1], input[2]));
            }

            var k = dj.KruskalMTS(edges);
            Console.WriteLine(k.Max(x => x.Weight));
            Console.WriteLine(k.Count);
            foreach (var k0 in k)
            {
                Console.WriteLine(k0.From + " " + k0.To);
            }
        }

        public static void GenealogicalTree()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var graph = new Graph<int>(true);

            for (int i = 1; i <= n; i++)
                graph.AddVertex(i);

            for (int i = 1; i <= n; i++)
            {
                var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                for (int j = 0; j < input.Length - 1; j++)
                {
                    graph.AddEdge(i, input[j], 1.0f);
                }
            }

            Console.WriteLine(String.Join(" ", graph.Kahnsalgorithm()));
        }

        public static void KindSpirits()
        {
            var n = Int32.Parse(Console.ReadLine());
            var graph = new Graph<int>(true);
            var levels = new List<List<int>>();
            var vertexCount = 0;
            levels.Add(new List<int> { 0 });
            graph.AddVertex(vertexCount++);
            for (int i = 0; i < n; i++)
            {
                var q = Console.ReadLine();
                if (q == "*")
                {
                    i--;
                    continue;
                }

                var k = Int32.Parse(q);
                levels.Add(new List<int>());
                for (int m = 0; m < k; m++)
                {
                    graph.AddVertex(vertexCount);
                    levels.Last().Add(vertexCount);
                    vertexCount++;
                }

                for (int j = 0; j < k; j++)
                {
                    var input = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                    for (int l = 0; l < input.Length - 2; l += 2)
                    {
                        var firstVertex = levels[i][input[l] - 1];
                        graph.AddEdge(firstVertex, levels[i + 1][j], input[l + 1]);
                    }
                }
            }


            var distance = graph.DijkstraAlgorithm(0, levels.Last()[0]);
            var answer = (from item in levels.Last() where distance.ContainsKey(item) select distance[item]).ToList().Min();
            Console.WriteLine(answer);

        }

        public static void NonYekaterinburgSubway()
        {
            var nkm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nkm[0];
            var k = nkm[1];
            var m = nkm[2];

            var dj = new DisjointSet(n);
            for (int i = 0; i < k; i++)
            {
                var xy = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                dj.Union(xy[0], xy[1]);
            }

            for (int i = 0; i < m; i++)
            {
                var xy = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            }

            Console.WriteLine(dj.SetCount - 1);
        }

        public static void TopoSort()
        {
            var nm = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            if (m == 0)
            {
                Console.WriteLine("YES");
                return;
            }
            var vertices = new int[n + 1];
            for (int i = 1; i <= n; i++)
                vertices[i] = i;
            var edges = new List<Tuple<int, int, double>>();
            for (int i = 0; i < m; i++)
            {
                var xy = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
                edges.Add(Tuple.Create(xy[0], xy[1], 1.0));
            }

            var sort = Console.ReadLine().Trim().Split().Select(Int32.Parse).ToHashSet();
            var graph = new Graph<int>(vertices, edges, true);
            var res = graph.IsTopologicalSortValid(sort);
            Console.WriteLine(res ? "YES" : "NO");
        }

        public static void ElectrificationPlan()
        {
            var input = Console.ReadLine().Split();
            var n = Int32.Parse(input[0]);
            var k = Int32.Parse(input[1]);
            var dj = new DisjointSet(n);
            var edges = new List<Edge<int>>();
            var matrix = new double[n, n];
            var builtStations = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();
            for (int i = 0; i < n; i++)
            {
                var inp = Console.ReadLine().Split();
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = Int32.Parse(inp[j]);
                }
            }

            foreach (var item in builtStations)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (item == j || builtStations.Contains(j)) continue;
                    edges.Add(new Edge<int>(item, j, matrix[item - 1, j - 1]));
                }
            }

            for (int i = 1; i <= n; i++)
            {
                if (builtStations.Contains(i)) continue;
                for (int j = 1; j <= n; j++)
                {
                    if (edges.Any(x => x.To == i && x.From == j) || i == j) continue;
                    edges.Add(new Edge<int>(i, j, matrix[i - 1, j - 1]));

                }
            }
            var a = dj.KruskalMTS(edges, builtStations);

            double sum = a.Sum(t => t.Weight);

            Console.WriteLine(sum);
        }
        private static int n;
        private static int m;
        private static int[] DX;
        private static int[] DY;
        private static int[,] grid;
        private static bool isPath;
        private static bool[,,,] vis;
        public static void IgorAndHisWayToWork()
        {
            DX = new int[] { 0, 1, -1, 0 };
            DY = new int[] { 1, 0, 0, -1 };
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            n = nm[0];
            m = nm[1];
            var startX = 0;
            var startY = 0;
            grid = new int[n + 1, m + 1];
            vis = new bool[n + 1, m + 1, 3, 4];
            for (var i = 0; i < n; i++)
            {
                var row = Console.ReadLine();
                for (var j = 0; j < row.Length; j++)
                {
                    if (row[j] == 'S')
                    {
                        startX = i;
                        startY = j;
                    }

                    grid[i, j] = row[j];
                }
            }

            isPath = false;
            for (var i = 0; i < 4; i++)
            {
                Array.Clear(vis, 0, vis.Length);
                DFS(startX, startY, 0, i);
                if (isPath)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            Console.WriteLine("NO");
        }

        public static void DFS(int x, int y, int turns, int d)
        {
            if (turns > 2) return;

            if (grid[x, y] == 'T')
            {
                isPath = true;
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                var x0 = DX[i] + x;
                var y0 = DY[i] + y;

                if (x0 >= 0 && x0 < n && y0 >= 0 && y0 < m && !vis[x0, y0, turns, i] && grid[x0, y0] != '*')
                {
                    vis[x0, y0, turns, i] = true;
                    DFS(x0, y0, (i != d ? turns + 1 : turns), i);
                }
            }

        }

        public static void Party()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var employee = new int[n + 1];
            var maxpath = 0;
            for (int i = 1; i <= n; i++)
            {
                employee[i] = Convert.ToInt32(Console.ReadLine());
            }
            for (int i = 1; i <= n; i++)
            {
                var path = GetPath(i, employee);
                maxpath = Math.Max(path, maxpath);
            }

            Console.WriteLine(maxpath + 1);
        }

        private static int GetPath(int e, int[] employee)
        {
            if (employee[e] == -1)
                return 0;
            return 1 + GetPath(employee[e], employee);
        }

        public static void KingEscape()
        {
            DX = new[] { 1, 1, 1, -1, -1, -1, 0, 0 };
            DY = new[] { 0, 1, -1, 0, 1, -1, 1, -1 };
            n = Convert.ToInt32(Console.ReadLine());
            var a = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var b = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var c = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var ax = a[0];
            var ay = a[1];
            var bx = b[0];
            var by = b[1];
            var cx = c[0];
            var cy = c[1];

            grid = new int[n + 5, n + 5];
            var x = ax;
            var y = ay;
            while (x < n && y < n)
                grid[x++, y++] = -1;

            x = ax;
            y = ay;
            while (x > 0 && y > 0)
                grid[x--, y--] = -1;

            x = ax;
            y = ay;
            while (x > 0 && y < n)
                grid[x--, y++] = -1;

            x = ax;
            y = ay;
            while (x < n && y > 0)
                grid[x++, y--] = -1;

            for (var i = 1; i <= n; i++)
            {
                grid[ax, i] = -1;
                grid[i, ay] = -1;
            }

            Dfs(bx, @by, cx, cy);
            Console.WriteLine(grid[cx, cy] == 1 ? "YES" : "NO");
        }

        public static void Dfs(int x0, int y0, int cx, int cy)
        {
            if (x0 == cx && y0 == cy)
            {
                grid[cx, cy] = 1;
                return;
            }

            for (int i = 0; i < 8; i++)
            {
                var x = DX[i] + x0;
                var y = DY[i] + y0;
                if (x > 0 && y > 0 && x <= n && y <= n && grid[x, y] != -1)
                {
                    grid[x, y] = -1;
                    Dfs(x, y, cx, cy);
                }
            }
        }

        public static void NetworkTopology()
        {
            var graph = new Graph<int>();
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];
            for (int i = 1; i <= n; i++)
            {
                graph.AddVertex(i);
            }
            for (int i = 1; i <= m; i++)
            {
                var edge = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                graph.AddEdge(edge[0], edge[1], 1);
            }

            var isStar = false;
            var sum1 = 0;
            var sum2 = 0;
            foreach (var adj in graph.GetAdjacencyList())
            {
                if (adj.Value.Count == m)
                    isStar = true;
                else if (adj.Value.Count == 1)
                    sum1++;
                else if (adj.Value.Count == 2)
                    sum2++;
            }

            if (isStar)
                Console.WriteLine("star topology");
            else if (sum1 == 2 && sum2 == n - 2)
                Console.WriteLine("bus topology");
            else if (sum2 == n)
                Console.WriteLine("ring topology");
            else
                Console.WriteLine("unknown topology");
        }

        public static void Badge()
        {
            var graph = new Graph<int>(true);
            var n = Convert.ToInt32(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            for (int i = 1; i <= n; i++)
            {
                graph.AddVertex(i);
                graph.AddVertex(input[i - 1]);
                graph.AddEdge(i, input[i - 1], 1);
            }
            for (int i = 1; i <= n; i++)
            {
                var res = new HashSet<int>();
                graph.DfsWithRecursion(i, res);

            }
        }

        public static void PolandBallandHypothesis()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i < 1000; i++)
            {
                if (i != n)
                    if (!IsPrime(n * i + 1))
                    {
                        Console.WriteLine(i);
                        return;
                    }
            }
        }

        public static bool IsPrime(long n)
        {
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        public static void LoveTriangle()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var planes = new int[n + 1];
            var input = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            for (int i = 1; i <= n; i++)
                planes[i] = input[i - 1];

            var isTriangle = false;
            for (int i = 1; i <= n; i++)
            {
                var a = i;
                var b = planes[a];
                var c = planes[b];

                if (a == planes[c])
                    isTriangle = true;
            }

            Console.WriteLine(isTriangle ? "YES" : "NO");
        }

        public static void TwoTeams()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            visited = new bool[n];
            used = new int[n];
            for (int i = 0; i < n; i++)
            {
                var members = Console.ReadLine().Split(' ').Select(x => Int32.Parse(x) - 1).ToList();
                members.Remove(members.Last());
                AdjacencyList.Add(members);
            }

            for (int i = 0; i < n; i++)
                if (!visited[i])
                    BFS(i);
            var teamFirst = new List<int>();
            for (int i = 0; i < used.Length; i++)
                if (used[i] % 2 == 0)
                    teamFirst.Add(i + 1);

            Console.WriteLine(teamFirst.Count);
            Console.WriteLine(String.Join(" ", teamFirst));

        }

        public static bool[] visited;
        public static int[] used;
        public static List<List<int>> AdjacencyList = new List<List<int>>();
        public static void BFS(int source)
        {
            var queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;
            while (queue.Count != 0)
            {
                var vertex = queue.Dequeue();
                for (int i = 0; i < AdjacencyList[vertex].Count; i++)
                {
                    var neighbor = AdjacencyList[vertex][i];
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        used[neighbor] = used[vertex] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }

        }

        public static void IsenbaevsNumber()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var graph = new Graph<string>();
            for (int i = 0; i < n; i++)
            {
                var names = Console.ReadLine().Split(' ');
                graph.AddVertex(names[0]);
                graph.AddVertex(names[1]);
                graph.AddVertex(names[2]);
                graph.AddEdge(names[0], names[1], 1);
                graph.AddEdge(names[0], names[2], 1);
                graph.AddEdge(names[1], names[2], 1);
            }

            var adj = graph.GetAdjacencyList().OrderBy(c => c.Key);

            foreach (var a in adj)
            {
                var shortesPath = graph.ShortestPath("Isenbaev", a.Key);
                if (shortesPath != null)
                    Console.WriteLine(a.Key + " " + (shortesPath.Count - 1));
                else
                    Console.WriteLine(a.Key + " undefined");

            }
        }

        public static void Metro()
        {
            var nm = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];

            var k = Convert.ToInt32(Console.ReadLine());
            var diagonal = new Point[k];
            for (int i = 0; i < k; i++)
            {
                var xy = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                diagonal[i] = new Point(xy[0], xy[1]);
            }

        }
    }
}

  //var n = int.Parse(Console.ReadLine());
            //var tree = new Tree(n);
            //for (int i = 0; i < n - 1; i++)
            //{
            //    var ab = Console.ReadLine().Split();
            //    var a = int.Parse(ab[0]) - 1;
            //    var b = int.Parse(ab[1]) - 1;
            //    tree.AddEdge(a, b, 1);
            //}
            //tree.PreCalculate();
            //var vert = tree._tree.Where(x => x.Value.Count == 1).ToList();
            //var min = int.MaxValue;
            //for (int i = 0; i < vert.Count - 1; i++)
            //{
            //    for (int j = i + 1; j < vert.Count; j++)
            //    {
            //        var l = tree.LCA(vert[i].Key, vert[j].Key);
            //        var d = tree._depth[l];
            //        var a = tree._depth[vert[i].Key] - d;
            //        var b = tree._depth[vert[j].Key] - d;
            //        min = Math.Min(a + b, min);
            //    }
            //}

            //Console.WriteLine(min);
