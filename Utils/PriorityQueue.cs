using System;
using System.Collections.Generic;

namespace PushPullBlocksGame
{

    public class PriorityQueue<T>
    {
        private List<(T item, int priority)> heap = new List<(T, int)>();

        private int Parent(int i) => (i - 1) / 2;
        private int Left(int i) => 2 * i + 1;
        private int Right(int i) => 2 * i + 2;

        public int Count => heap.Count;

        public void Enqueue(T item, int priority)
        {
            heap.Add((item, priority));
            HeapifyUp(heap.Count - 1);
        }

        public T Dequeue()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");

            var root = heap[0].item;
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return root;
        }

        public T Peek()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
            return heap[0].item;
        }

        private void HeapifyUp(int i)
        {
            while (i > 0 && heap[i].priority < heap[Parent(i)].priority)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        private void HeapifyDown(int i)
        {
            int smallest = i;
            int left = Left(i);
            int right = Right(i);

            if (left < heap.Count && heap[left].priority < heap[smallest].priority)
                smallest = left;
            if (right < heap.Count && heap[right].priority < heap[smallest].priority)
                smallest = right;

            if (smallest != i)
            {
                Swap(i, smallest);
                HeapifyDown(smallest);
            }
        }

        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}