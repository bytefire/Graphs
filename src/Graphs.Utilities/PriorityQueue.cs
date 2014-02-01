using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Utilities
{
    /// <summary>
    /// A simple implementation of minimum priority queue using binary heap. 
    /// On reaching capacity, the priority doesn't resize. Instead it throws overflow
    /// exception.
    /// </summary>
    /// <typeparam name="T">Type of elements. Elements must be comparable to eachother.</typeparam>
    /// <remarks>
    /// This is based on the book The Algorithm Design Manual by Steven Skiena. Following changes have
    /// been made from the original:
    /// 1. Array indices start at zero rather than one.
    /// 2. Recursive methods have been made iterative.
    /// </remarks>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        T[] _queue;

        /// <summary>
        /// The maximum capacity. Note that the queue doesn't resize upon hitting capacity.
        /// </summary>
        public int Capacity
        {
            get
            {
                if (_queue == null)
                {
                    throw new Exception("The priority queue must first be initialised.");
                }
                return _queue.Length;
            }
        }
        /// <summary>
        /// Number of elements currently in the queue.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        public PriorityQueue(int capacity)
        {
            _queue = new T[capacity];
        }

        public void Insert(T item)
        {
            if (Count == Capacity)
            {
                throw new OverflowException("The priority queue has reached its capacity. The item cannot be inserted.");
            }
            Count++;
            _queue[Count - 1] = item;
            BubbleUp(Count - 1);
        }

        private void BubbleUp(int p)
        {
            // if root of the heap, i.e. no parent then return straightaway
            if (Parent(p) == -1)
            {
                return;
            }

            while (_queue[Parent(p)].CompareTo( _queue[p])>0)
            {
                SwapElementsAt(p, Parent(p));
                p = Parent(p);
            }
        }

        private void SwapElementsAt(int index1, int index2)
        {
            T temp = _queue[index2];
            _queue[index2] = _queue[index1];
            _queue[index1] = temp;
        }


        private int Parent(int n)
        {
            if (n == 0)
            {
                return -1;
            }
            return (n - 1) / 2;
        }
        private int YoungChild(int n)
        {
            return 2 * n + 1;
        }
    }
}
