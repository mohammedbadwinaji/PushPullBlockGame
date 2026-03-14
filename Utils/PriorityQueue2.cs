using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushPullBlocksGame
{
        public sealed class PriorityQueue2<TElement, TPriority>
        {
            private readonly List<Node> _heap;
            private readonly IComparer<TPriority> _priorityComparer;

            private struct Node
            {
                public TElement Element;
                public TPriority Priority;

                public Node(TElement element, TPriority priority)
                {
                    Element = element;
                    Priority = priority;
                }
            }

            public PriorityQueue2()
                : this(null)
            {
            }

            public PriorityQueue2(IComparer<TPriority> priorityComparer)
            {
                _heap = new List<Node>();
                _priorityComparer = priorityComparer ?? Comparer<TPriority>.Default;
            }

            public int Count => _heap.Count;

            public void Enqueue(TElement element, TPriority priority)
            {
                var node = new Node(element, priority);
                _heap.Add(node);
                BubbleUp(_heap.Count - 1);
            }

            public TElement Dequeue()
            {
                if (_heap.Count == 0)
                    throw new InvalidOperationException("The priority queue is empty.");

                TElement result = _heap[0].Element;

                int lastIndex = _heap.Count - 1;
                _heap[0] = _heap[lastIndex];
                _heap.RemoveAt(lastIndex);

                if (_heap.Count > 0)
                    BubbleDown(0);

                return result;
            }

            public TElement Peek()
            {
                if (_heap.Count == 0)
                    throw new InvalidOperationException("The priority queue is empty.");

                return _heap[0].Element;
            }

            public bool TryDequeue(out TElement element, out TPriority priority)
            {
                if (_heap.Count == 0)
                {
                    element = default(TElement);
                    priority = default(TPriority);
                    return false;
                }

                element = _heap[0].Element;
                priority = _heap[0].Priority;
                Dequeue();
                return true;
            }

            public bool TryPeek(out TElement element, out TPriority priority)
            {
                if (_heap.Count == 0)
                {
                    element = default(TElement);
                    priority = default(TPriority);
                    return false;
                }

                element = _heap[0].Element;
                priority = _heap[0].Priority;
                return true;
            }

            private void BubbleUp(int index)
            {
                while (index > 0)
                {
                    int parent = (index - 1) / 2;

                    if (_priorityComparer.Compare(
                            _heap[index].Priority,
                            _heap[parent].Priority) >= 0)
                    {
                        break;
                    }

                    Swap(index, parent);
                    index = parent;
                }
            }

            private void BubbleDown(int index)
            {
                int lastIndex = _heap.Count - 1;

                while (true)
                {
                    int left = index * 2 + 1;
                    int right = left + 1;

                    if (left > lastIndex)
                        break;

                    int smallest = left;

                    if (right <= lastIndex &&
                        _priorityComparer.Compare(
                            _heap[right].Priority,
                            _heap[left].Priority) < 0)
                    {
                        smallest = right;
                    }

                    if (_priorityComparer.Compare(
                            _heap[smallest].Priority,
                            _heap[index].Priority) >= 0)
                    {
                        break;
                    }

                    Swap(index, smallest);
                    index = smallest;
                }
            }

            private void Swap(int i, int j)
            {
                Node temp = _heap[i];
                _heap[i] = _heap[j];
                _heap[j] = temp;
            }
        }
    
}
