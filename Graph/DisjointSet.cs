using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Edge<T>
    {
        public double Weight { get; set; }
        public T From { get; set; }
        public T To { get; set; }

        public Edge(T from, T to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class DisjointSet
    {
        public int[] Parent { get; }
        public int[] Rank { get; }
        public int[] Size { get; }
        public int Count { get; }
        public int SetCount { get; private set; }

        /// <summary>
        /// Initializes a new Disjoint-Set data structure.
        /// </summary>
        /// <param name="count"> Count elements </param>
        public DisjointSet(int count)
        {
            Count = count; 
            SetCount = count;
            Parent = new int[Count + 1];
            Rank = new int[Count + 1];
            //Size = new int[Count + 1];

            for (var i = 1; i <= Count; i++)
            {
                Parent[i] = i;
               // Size[i] = 1;
                Rank[i] = 1;
            }
        }

        /// <summary>
        /// Find the parent of the specified element.
        /// </summary>
        /// <param name="element"> The specified element. </param>
        /// <returns></returns>
        public int FindRepresentative(int element)
        {
            return Parent[element] == element ? element : FindRepresentative(Parent[element]);
        }

        /// <summary>
        /// Unite the sets that the specified elements belong to.
        /// </summary>
        /// <param name="right"> First element </param>
        /// <param name="left"> Second element </param>
        public bool Union(int right, int left)
        {
            var r = FindRepresentative(right);
            var l = FindRepresentative(left);
        
            if (r.Equals(l))
                return false;
            var rRank = Rank[right];
            var lRank = Rank[left];
          
            SetCount--;

            if (lRank < rRank)
            {
                Parent[r] = l;
                //Size[l] += Size[r];
            }
            else if (rRank < lRank)
            {
                Parent[l] = r;
               // Size[r] += Size[l];
            }
            else
            {
                Parent[r] = l;
               // Size[l] += Size[r];
                Rank[l]++;
            }

            return true;
        }

        /// <summary>
        /// Finds an edge of the least possible weight that connects any two trees in the forest.
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public List<Edge<int>> KruskalMTS(List<Edge<int>> edges, List<int> builtStations = null)
        {
            if (builtStations != null)
            {
                foreach (var station in builtStations)
                {
                    var start = FindRepresentative(station);
                    var end = FindRepresentative(builtStations[0]);
                    Parent[end] = start;
                }
            }
            edges = edges.OrderBy(e => e.Weight).ToList();
            var spanningTree = new List<Edge<int>>();
            foreach (var edge in edges)
            {
                var start = FindRepresentative(edge.From);
                var end = FindRepresentative(edge.To);

                if (start == end) continue;
                spanningTree.Add(edge);
                Parent[end] = start;
            }

            return spanningTree;
        }

    }
}
