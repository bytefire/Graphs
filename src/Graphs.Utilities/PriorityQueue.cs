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
    /// This is based on the book The Algorithm Design Manual by Steven Skiena.
    /// </summary>
    /// <typeparam name="T">Type of elements. Elements must be comparable to eachother.</typeparam>
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


        private int Parent(int n)
        {
            if (n == 1)
            {
                return -1;
            }
            return n / 2;
        }
        private int YoungChild(int n)
        {
            return 2 * n;
        }
    }
}
