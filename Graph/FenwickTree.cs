namespace Graph
{
    public class FenwickTree
    {
        static readonly int MAX = 300010;
        static long[] BITree = new long[MAX];
        public int n;

        public FenwickTree(int n)
        {
            this.n = n;
        }

        public long Sum(int index)
        {
            long sum = 0;
            while (index > 0)
            {
                sum += BITree[index];
                index = GetParent(index);
            }
            return sum;
        }

        public void Add(int index, long val)
        {
            while (index <= n)
            {
                BITree[index] += val;
                index = GetNext(index);
            }
        }

        public void Update(int l, int r, int val)
        {
            Add(l, val);
            Add(r + 1, -val);
        }

        public int GetParent(int index)
        {
            return index - (index & -index);
        }

        public int GetNext(int index)
        {
            return index + (index & -index);
        }
    }
}
