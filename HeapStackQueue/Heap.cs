using System;
using System.Collections.Generic;

namespace HeapStackQueue
{
    public class Heap<T>
    {
        private enum HeapType
        {
            Min,
            Max
        };

        private readonly HeapType _heapType;
        private List<T> _heap;
        private readonly IComparer<T> comparer;

        public Heap(bool isMaxHeap = false) : this(Comparer<T>.Default, isMaxHeap)
        {
        }

        public Heap(IComparer<T> comparer, bool isMaxHeap = false)
        {
            _heapType = isMaxHeap ? HeapType.Max : HeapType.Min;
            _heap = new List<T>();
            this.comparer = comparer;
        }

        public int Count => _heap.Count;

        public bool IsEmpty => _heap.Count == 0;

        public void Add(T val)
        {
            _heap.Add(val);
            ShiftUp(_heap.Count - 1);
        }

        public T Peek()
        {
            if (_heap.Count == 0) throw new ArgumentOutOfRangeException("No values in heap");
            return _heap[0];
        }

        public T Remove()
        {
            T output = Peek();
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            ShiftDown(0);
            return output;
        }

        // O(log(n)) time, O(1) space
        private void ShiftUp(int heapIndex)
        {
            if (heapIndex == 0) return;
            int parentIndex = (heapIndex - 1) / 2;
            bool shouldShift = DoCompare(parentIndex, heapIndex) > 0;
            if (!shouldShift) return;
            Swap(parentIndex, heapIndex);
            ShiftUp(parentIndex);
        }

        // O(log(n)) time, O(1) space
        private void ShiftDown(int heapIndex)
        {
            int child1 = heapIndex * 2 + 1;
            if (child1 >= _heap.Count) return;
            int child2 = child1 + 1;

            int preferredChildIndex = (child2 >= _heap.Count || DoCompare(child1, child2) <= 0) ? child1 : child2;
            if (DoCompare(preferredChildIndex, heapIndex) > 0) return;
            Swap(heapIndex, preferredChildIndex);
            ShiftDown(preferredChildIndex);
        }

        private void Swap(int index1, int index2)
        {
            T temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        private int DoCompare(int initialIndex, int contenderIndex)
        {
            T initial = _heap[initialIndex];
            T contender = _heap[contenderIndex];
            int value = comparer.Compare(initial, contender);
            if (_heapType == HeapType.Max) value = -value;
            return value;
        }

    }
}
