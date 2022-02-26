using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T>
    {
        private Dictionary<T, Dictionary<T, double>> AdjacencyList = new Dictionary<T, Dictionary<T, double>>();
        private T[,] AdjacencyMatrix;
        private bool isDirected;
        private SortedSet<Tuple<T, T>> EdgeSortedSet = new SortedSet<Tuple<T, T>>();
        private List<T> _currentGraph;
        public long Count;
        public Graph(int n)
        {
            AdjacencyMatrix = new T[n, n];
        }

        public Graph(bool isDirected = false)
        {
            this.isDirected = isDirected;
        }

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T, double>> edges, bool isDirected = false)
        {
            this.isDirected = isDirected;
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        /// <summary>
        /// Add vertex in adjacency matrix
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="weight"></param>
        public void AddEdgeToAdjancencyMatrix(long to, long from, T weight)
        {
            AdjacencyMatrix[to, from] = weight;
        }

        /// <summary>
        /// Add vertex in graph
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public void AddVertex(T vertex)
        {
            if (!AdjacencyList.ContainsKey(vertex))
                AdjacencyList[vertex] = new Dictionary<T, double>();
        }

        /// <summary>
        /// Add edge in graph
        /// </summary>
        /// <param name="vertexFrom">From vertex</param>
        /// <param name="vertexTo">To vertex</param>
        /// <param name="weight">Weight</param>
        public void AddEdge(T vertexFrom, T vertexTo, double weight)
        {
          //  if (AdjacencyList.ContainsKey(vertexFrom) && AdjacencyList.ContainsKey(vertexTo))
           // {
                //if (AdjacencyList[vertexFrom].ContainsKey(vertexTo))
                //    return;

                AdjacencyList[vertexFrom].Add(vertexTo, weight);
                //EdgeSortedSet.Add(Tuple.Create(vertexFrom, vertexTo));

               // if (!isDirected && !AdjacencyList[vertexTo].ContainsKey(vertexFrom))
               // {
               AdjacencyList[vertexTo].Add(vertexFrom, weight);
                    //EdgeSortedSet.Add(Tuple.Create(vertexTo, vertexFrom));
                //}
            //}
        }

        /// <summary>
        /// Remove edge in graph
        /// </summary>
        /// <param name="vertexFrom">From vertex</param>
        /// <param name="vertexTo">To vertex</param>
        public void RemoveEdge(T vertexFrom, T vertexTo)
        {
            if (AdjacencyList.ContainsKey(vertexFrom) && AdjacencyList.ContainsKey(vertexTo))
            {
                if (AdjacencyList[vertexFrom].ContainsKey(vertexTo))
                    AdjacencyList[vertexFrom].Remove(vertexTo);
                if (!isDirected && AdjacencyList[vertexTo].ContainsKey(vertexFrom))
                    AdjacencyList[vertexTo].Remove(vertexFrom);
            }
        }

        /// <summary>
        /// Breadth First Traversal in graph
        /// </summary>
        /// <param name="source">Start vertex</param>
        /// <returns></returns>
        public HashSet<T> BFS(T source)
        {
            var visited = new HashSet<T>();
            if (!AdjacencyList.ContainsKey(source))
                return visited;

            var queue = new Queue<T>();
            queue.Enqueue(source);

            while (queue.Count != 0)
            {
                var vertex = queue.Dequeue();
                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach (var neighbor in AdjacencyList[vertex].Where(neighbor => !visited.Contains(neighbor.Key)))
                {
                    queue.Enqueue(neighbor.Key);
                }
            }

            return visited;
        }

        /// <summary>
        /// Find shortest path in graph using bfs. O(V+E)
        /// </summary>
        /// <param name="start"> Start vertex</param>
        /// <param name="end"> End vertex</param>
        /// <returns></returns>
        public Stack<T> ShortestPath(T start, T end)
        {
            //bfs
            var path = new Dictionary<T, T>();
            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                var vertex = queue.Dequeue();
                if (!AdjacencyList.ContainsKey(vertex))
                    return null;
                foreach (var neighbor in AdjacencyList[vertex]
                    .Where(neighbor => !path.ContainsKey(neighbor.Key)))
                {
                    path[neighbor.Key] = vertex;
                    queue.Enqueue(neighbor.Key);
                }
            }

            if (path.Count == 0)
                return null;

            //find shortest path
            var shortestPath = new Stack<T>();
            var temp = end;
            while (!temp.Equals(start))
            {
                shortestPath.Push(temp);
                if (!path.ContainsKey(temp))
                    return null;
                temp = path[temp];
            }
            shortestPath.Push(start);
            return shortestPath;
        }

        /// <summary>
        /// Depth First Traversal in graph
        /// </summary>
        /// <param name="source">Start vertex</param>
        /// <returns></returns>
        public void  Dfs(T source, HashSet<T> visited)
        {
            if (!AdjacencyList.ContainsKey(source))
                return;

            var stack = new Stack<T>();
            stack.Push(source);

            while (stack.Count != 0)
            {
                var vertex = stack.Pop();
                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);
                //Count++;
                foreach (var neighbor in AdjacencyList[vertex].Where(neighbor => !visited.Contains(neighbor.Key) 
                                                                                 /*&& neighbor.Value == 0*/))
                {
                    stack.Push(neighbor.Key);
                }
            }
        }

        /// <summary>
        /// Depth First Traversal in graph recursive.
        /// </summary>
        /// <param name="vertex">Start vertex</param>
        /// <param name="visited">Visited vertexes</param>
        /// <returns></returns>
        public void DfsWithRecursion(T vertex, HashSet<T> visited)
        {
            Traverse(vertex, visited);
        }

        private void Traverse(T v, HashSet<T> visited)
        {
            visited.Add(v);
            // Console.Write(v + " ");
            // _currentGraph.Add(v);
            //Count++;
            if (!AdjacencyList.ContainsKey(v)) return;
            foreach (var neighbor in AdjacencyList[v].Where(a => !visited.Contains(a.Key) /*&& a.Value == 0*/))
                Traverse(neighbor.Key, visited);

        }

        /// <summary>
        /// Find all connected components in graph.
        /// </summary>
        /// <returns></returns>
        public int CountComponents()
        {
            var count = 0;
            var visited = new HashSet<T>();
            foreach (var adj in AdjacencyList.Where(adj => !visited.Contains(adj.Key)))
            {
                DfsWithRecursion(adj.Key, visited);
                count++;
            }

            return count;
        }

        /// <summary>
        /// Get Adjacency List
        /// </summary>
        /// <returns></returns>
        public Dictionary<T, Dictionary<T, double>> GetAdjacencyList()
        {
            return AdjacencyList;
        }

        /// <summary>
        /// Linear ordering of vertices in Directed Acyclic Graph(DAG).
        /// </summary>
        /// <returns></returns>
        public List<T> Kahnsalgorithm()
        {
            var L = new List<T>(); // will contain the sorted elements
            var S = new SortedSet<T>(AdjacencyList.Keys.Where(n => EdgeSortedSet.All(e => !e.Item2.Equals(n)))); // no incoming edges

            while (S.Any())
            {
                var n = S.First();
                S.Remove(n);
                L.Add(n);
                foreach (var e in EdgeSortedSet.Where(e => e.Item1.Equals(n)).ToList())
                {
                    var m = e.Item2;
                    EdgeSortedSet.Remove(e);
                    if (EdgeSortedSet.All(incoming => !incoming.Item2.Equals(m)))
                        S.Add(m);
                }
            }
            return EdgeSortedSet.Any() ? null : L;
        }

        /// <summary>
        /// Check if graph can divided into two independent sets.
        /// </summary>
        /// <returns></returns>
        public bool IsBipartite()
        {
            var visited = new HashSet<T>();
            var black = new HashSet<T>();
            var white = new HashSet<T>();
            return AdjacencyList.Keys.Where(a => !visited.Contains(a)).All(vertex => IsBipartite(vertex, visited, white, black));
        }
        private bool IsBipartite(T vertex, HashSet<T> visited, HashSet<T> white, HashSet<T> black)
        {
            visited.Add(vertex);
            if (!black.Contains(vertex) && !white.Contains(vertex))
                black.Add(vertex);
            foreach (var edge in AdjacencyList[vertex].Where(a => !visited.Contains(a.Key)))
            {
                if (black.Contains(vertex) && !black.Contains(edge.Key))
                    white.Add(edge.Key);
                else if (white.Contains(vertex) && !white.Contains(edge.Key))
                    black.Add(edge.Key);
                else return false;
            }

            return true;
        }

        /// <summary>
        /// Find shortest path in two vertexes in weighted graph with non-negative cycles. O((V+E)logV).
        /// </summary>
        /// <param name="start">start vertex</param>
        /// <param name="end">end vertex</param>
        /// <returns></returns>
        public Dictionary<T, double> DijkstraAlgorithm(T start, T end)
        {
            var sortedDict = new SortedSet<Tuple<T, double>>();
            var distance = new Dictionary<T, double>();
            // var path = new Dictionary<T, T>();
            foreach (var adj in AdjacencyList)
            {
                if (adj.Key.Equals(start))
                    distance[adj.Key] = 0;
                else distance[adj.Key] = double.PositiveInfinity;
            }

            sortedDict.Add(Tuple.Create(start, 0.0));
            while (sortedDict.Any())
            {
                var temp = sortedDict.First();
                var from = temp.Item1;

                foreach (var edge in AdjacencyList[from])
                {
                    if (distance[from] + edge.Value < distance[edge.Key])
                    {
                        distance[edge.Key] = distance[from] + edge.Value;

                        sortedDict.Add(Tuple.Create(edge.Key, distance[edge.Key]));

                        //if (path.ContainsKey(to))
                        //    path[to] = from;
                        //else path.Add(to, from);
                    }
                }
                sortedDict.Remove(temp);

            }

            return distance; //GetPath(start, end, path);
        }

        private Tuple<Dictionary<T, double>, Dictionary<T, T>> NonNegativeShortestDistance(T start)
        {
            var distance = new Dictionary<T, double>();
            var ordering = new Dictionary<T, T>();
            distance[start] = 0;
            foreach (var item in AdjacencyList)
            {
                distance[item.Key] = int.MaxValue - 1000;
            }
            distance[start] = 0;
            var queue = new Queue<Tuple<T, double>>();
            queue.Enqueue(Tuple.Create(start, 0d));
            while (queue.Count > 0)
            {
                var (item, minValue) = queue.Dequeue();
                if (distance[item] < minValue) continue;
                if (!AdjacencyList.ContainsKey(item)) return null;
                foreach (var (key, value) in AdjacencyList[item])
                {
                    var newDist = distance[item] + value;
                    if (!distance.ContainsKey(key) || !(newDist < distance[key])) continue;
                    ordering[key] = item;
                    distance[key] = newDist;
                    queue.Enqueue(Tuple.Create(key, newDist));
                }
            }
            return Tuple.Create(distance, ordering);
        }

        public List<Tuple<T, double>> OrderedListShortesDistance(T start, T end)
        {
            var shortesPath = NonNegativeShortestDistance(start);
            if (shortesPath == null) return null;
            var distance = shortesPath.Item1;
            var ordering = shortesPath.Item2;
            var orderedDist = new List<Tuple<T, double>>(ordering.Count + 50);
            for (var i = end; i != null; i = ordering[i])
            {
                orderedDist.Add(Tuple.Create(i, distance[i]));
                if (!ordering.ContainsKey(i)) break;
            }
            orderedDist.Reverse();
            return orderedDist;
        }

        /// <summary>
        /// Find all pairs shortest distance in graph with negative cycles. O(V^3).
        /// </summary>
        /// <returns></returns>
        public Dictionary<T, Dictionary<T, double>> FloydWarshall()
        {
            var verticesCount = AdjacencyList.Count;
            var distance = new Dictionary<T, Dictionary<T, double>>(verticesCount);
            foreach (var adj in AdjacencyList)
                distance.Add(adj.Key, adj.Value);

            foreach (var intermediate in AdjacencyList.Keys)
            {
                foreach (var start in AdjacencyList.Keys)
                {
                    foreach (var end in AdjacencyList.Keys.Where(end => distance[start].ContainsKey(intermediate) && distance[intermediate].ContainsKey(end) && distance[start].ContainsKey(end)))
                    {
                        if (distance[start][intermediate] + distance[intermediate][end] < distance[start][end])
                            distance[start][end] = distance[start][intermediate] + distance[intermediate][end];
                    }
                }
            }

            return distance;
        }

        /// <summary>
        /// Find shortest path in graph. O(V*E)
        /// </summary>
        /// <returns></returns>
        public HashSet<T> BellmanFord(T from, T to)
        {
            var distance = new Dictionary<T, double>(); // represents distances from source to the vertex being checked
            var predecessor = new Dictionary<T, T>(); // holds paths between vertexes.

            foreach (var vertex in AdjacencyList)
            {
                distance[vertex.Key] = double.PositiveInfinity;
                predecessor[vertex.Key] = default;
                foreach (var edge in vertex.Value)
                {
                    distance[edge.Key] = double.PositiveInfinity;
                    predecessor[edge.Key] = default;
                }
            }
            distance[from] = 0;

            // relax edges repeatedly
            for (var i = 1; i < AdjacencyList.Keys.Count; i++)
            {
                foreach (var (u, edges) in AdjacencyList)
                {
                    foreach (var (v, w) in edges)
                    {
                        if (distance[u] + w < distance[v])
                        {
                            distance[v] = distance[u] + w;
                            predecessor[v] = u;
                        }

                    }
                }
            }

            //check for negative-weight cycles
            if ((from vertex in AdjacencyList
                 let u = vertex.Key
                 where (from edge in vertex.Value
                        let v = edge.Key
                        let w = edge.Value
                        where distance[u] + w < distance[v]
                        select v).Any()
                 select vertex).Any())
            {
                throw new InvalidOperationException("Graph contains a negative-weight cycle");
            }

            return GetPath(from, to, predecessor);
        }

        public HashSet<T> GetPath(T from, T to, Dictionary<T, T> predecessors)
        {
            var resultPath = new HashSet<T>();
            for (var i = to; !i.Equals(from); i = predecessors[i])
                resultPath.Add(i);
            resultPath.Add(from);

            return resultPath.Reverse().ToHashSet();
        }

        public bool IsTopologicalSortValid(HashSet<T> sorted)
        {
            var checkArray = new Dictionary<T, int>(sorted.Count);
            var i = 0;
            foreach (var s in sorted)
                checkArray.Add(s, i++);

            return !(from adj in AdjacencyList
                     from a in adj.Value
                     where checkArray[adj.Key] > checkArray[a.Key]
                     select adj).Any();
        }

        /// <summary>
        /// Find Euler Path or Circuit in graph
        /// </summary>
        public List<Tuple<T, T>> EulerTour()
        {
            var startVertex = FindOddDegreeVertex();
            var tour = new List<Tuple<T, T>>();
            EulerFunction(startVertex, tour);
            tour.Reverse();
            return tour;
        }

        /// <summary>
        /// Find a vertex with odd degree
        /// </summary>
        /// <returns></returns>
        public T FindOddDegreeVertex()
        {
            T oddVertex = default;
            foreach (var vertices in AdjacencyList.Where(vertices => vertices.Value.Count % 2 == 1))
            {
                oddVertex = vertices.Key;
            }

            return oddVertex;
        }

        /// <summary>
        /// Count reachable vertices from v 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="visited"></param>
        private int DfsCount(T v, HashSet<T> visited)
        {
            visited.Add(v);
            return 1 + AdjacencyList[v].Where(a => !visited.Contains(a.Key)).
                Sum(neighbor => DfsCount(neighbor.Key, visited));
        }

        /// <summary>
        /// Check if edge firstVertex-secondVertex can be considered as next edge in Euler Tour
        /// </summary>
        /// <param name="firstVertex"> First Edge </param>
        /// <param name="secondVertex"> Second Edge </param>
        /// <returns></returns>
        public bool IsValidNextEdge(T firstVertex, T secondVertex)
        {
            // If secondVertex is the only adjacent vertex of firstVertex
            if (AdjacencyList[firstVertex].Count == 1)
                return true;

            // If there are multiple adjacents

            // Count of vertices reachable from firstVertex
            var isVisited = new HashSet<T>();
            var countFirst = DfsCount(firstVertex, isVisited);

            // Count of vertices reachable from secondVertex
            isVisited = new HashSet<T>();
            var countSecond = DfsCount(secondVertex, isVisited);

            AddEdge(firstVertex, secondVertex, 1.0);
            return countFirst <= countSecond;
        }

        /// <summary>
        /// Euler tour starting given vertex
        /// </summary>
        /// <param name="startVertex"> Start Vertex </param>
        /// <param name="tour"></param>
        public void EulerFunction(T startVertex, List<Tuple<T, T>> tour)
        {
            for (int i = AdjacencyList[startVertex].Count - 1; i >= 0; i--)
            {
                if (AdjacencyList[startVertex].Keys.Count == 0)
                    return;
                var vertex = AdjacencyList[startVertex].Keys.ElementAt(i);
                if (IsValidNextEdge(startVertex, vertex))
                {
                    tour.Add(Tuple.Create(vertex, startVertex));
                    RemoveEdge(startVertex, vertex);
                    EulerFunction(vertex, tour);
                }
            }
        }

        /// <summary>
        /// Find shortest path for every graph
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public Dictionary<T, double> ShortestDistance(T start)
        {
            var n = AdjacencyList.Count;
            var topSort = Kahnsalgorithm();
            var distance = new Dictionary<T, double>();
            foreach (var item in AdjacencyList)
                distance[item.Key] = double.MaxValue - 1000;
            distance[start] = 0;
            for (var i = 0; i < n; i++)
            {
                var nodeIndex = topSort[i];
                if (distance[nodeIndex] == double.MaxValue - 1000) continue;
                var edges = AdjacencyList[nodeIndex];
                if (edges == null) continue;
                foreach (var edge in edges)
                {
                    var newDist = distance[nodeIndex] + edge.Value;
                    if (distance[edge.Key] == double.MaxValue - 1000) distance[edge.Key] = newDist;
                    else distance[edge.Key] = Math.Min(distance[edge.Key], newDist);
                }
            }
            return distance;
        }

        public int CountSingleCycles()
        {
            var count = 0;
            var visited = new HashSet<T>();
            foreach (var adj in AdjacencyList)
            {
                if (!visited.Contains(adj.Key))
                {
                    _currentGraph = new List<T>();
                    Dfs(adj.Key, visited);
                    var flag = 1;
                    if (_currentGraph.Any(c => AdjacencyList[c].Count != 2))
                    {
                        flag = 0;
                    }

                    if (flag == 1)
                    {
                        count++;
                    }

                }
            }

            return count;
        }

        public int[] DijkstrasAlgorithm(int start, int[][][] edges)
        {
            var vertexCount = edges.Length;
            var shortestDist = new int[vertexCount];
            var visited = new HashSet<int>();

            for (int i = 0; i < vertexCount; i++)
                shortestDist[i] = int.MaxValue;

            shortestDist[start] = 0;

            while (vertexCount != visited.Count)
            {
                var vertexData = GetVertexWithMinDistance(shortestDist, visited);
                var u = vertexData[0];
                var currentDist = vertexData[1];
                if (currentDist == int.MaxValue) break;
                visited.Add(u);

                foreach (var edge in edges[u])
                {
                    var destination = edge[0];
                    var destinationDist = edge[1];

                    if (visited.Contains(destination)) continue;
                    if (currentDist + destinationDist < shortestDist[destination])
                        shortestDist[destination] = currentDist + destinationDist;
                }

            }
            for (int i = 0; i < vertexCount; i++)
            {
                if (shortestDist[i] == int.MaxValue)
                    shortestDist[i] = -1;
            }
            return shortestDist;
        }

        private int[] GetVertexWithMinDistance(int[] shortestDist, HashSet<int> visited)
        {
            var min = int.MaxValue;
            var minIndex = -1;

            for (int i = 0; i < shortestDist.Length; i++)
                if (!visited.Contains(i) && shortestDist[i] <= min)
                {
                    min = shortestDist[i];
                    minIndex = i;
                }

            return new int[] { minIndex, min };
        }

    }
}
