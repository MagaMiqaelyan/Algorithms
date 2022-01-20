using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Tree
    {
        private int _n;
        private int _level = 20;
        public Dictionary<int, Dictionary<int, int>> _tree;
        public int[] _depth;
        private int[,] _parent;
        public long[] _weighted;

        public Tree(int n)
        {
            _n = n + 5;
            _tree = new Dictionary<int, Dictionary<int, int>>(_n);
            _depth = new int[_n];
            _parent = new int[_n, _level];
            _weighted = new long[_n];

            for (var i = 0; i < _n; i++)
                _tree[i] = new Dictionary<int, int>();
        }

        public void AddEdge(int u, int v, int w)
        {
            _tree[u].Add(v, w);
            _tree[v].Add(u, w);
        }

        public void PreCalculate()
        {
            Memset(-1);
            Dfs(0, 0); // running dfs and precalculating depth of each node. 
            PrecomputeSparseMatrix(_n - 5);
        }

        private void Memset(int value)
        {
            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _level; j++)
                {
                    _parent[i, j] = value;
                }
            }
        }

        /// <summary>
        /// Re-compute the depth for each node and their first parent(2^0th parent)
        /// Time complexity : O(n) 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="parent"></param>
        private void Dfs(int current, int parent)
        {
            _depth[current] = _depth[parent] + 1;
            _parent[current, 0] = parent;
            foreach (var t in _tree[current].Where(t => t.Key != parent))
            {
                _weighted[t.Key] = _weighted[current] + t.Value;
                Dfs(t.Key, current);
            }
        }

        /// <summary>
        /// Dynamic Programming Sparse Matrix Approach populating 2^i parent for each node
        /// Time complexity : O(nlogn) 
        /// </summary>
        /// <param name="n"></param>
        private void PrecomputeSparseMatrix(int n)
        {
            for (var i = 1; i < _level; i++)
            {
                for (var node = 1; node <= n; node++)
                {
                    if (_parent[node, i - 1] != -1)
                        _parent[node, i] = _parent[_parent[node, i - 1], i - 1];
                }
            }
        }

        /// <summary>
        /// Returning the LCA of u and v  Time complexity : O(log n) 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public int LCA(int u, int v)
        {
            if (_depth[v] < _depth[u])
            {
                u += v;
                v = u - v;
                u -= v;
            }

            var diff = _depth[v] - _depth[u];

            for (var i = 0; i < _level; i++)
                if (((diff >> i) & 1) == 1)
                    v = _parent[v, i];

            if (u == v)
                return u;

            for (var i = _level - 1; i >= 0; i--)
                if (_parent[u, i] != _parent[v, i])
                {
                    u = _parent[u, i];
                    v = _parent[v, i];
                }

            return _parent[u, 0];
        }

    }
}
