using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Utilities
{
    /// <summary>
    /// A simple implementation of disjoint set using trees. 
    /// see http://www.youtube.com/watch?v=gcmjC-OcWpI
    /// </summary>
    public class DisjointSet
    {
        int[] _arr;

        public int Capacity
        {
            get
            {
                if (_arr == null)
                {
                    throw new Exception("This disjoint set hasn't been initialised yet.");
                }
                return _arr.Length;
            }
        }

        public DisjointSet(int capacity)
        {
            _arr = new int[capacity];
            Array.ForEach(_arr, e => e = -1);
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                // optimisation: merge smaller tree into bigger tree
                // if x is in a bigger tree than y, then make root of y a child of root of x, i.e. merge y's tree into x's tree.
                if (_arr[rootX] < _arr[rootY])
                {
                    _arr[rootY] = rootX;
                    _arr[rootX]--;
                }
                else // otherwise merge x's tree into y's tree.
                {
                    _arr[rootX] = rootY;
                    _arr[rootY]--;
                }
            }
        }

        /// <summary>
        /// Finds the root of given index.
        /// </summary>
        /// <param name="x">The index of element whose root is sought</param>
        /// <returns>Index of the root element.</returns>
        public int Find(int x)
        {
            int root = x;
            while (_arr[root] >= 0)
            {
                root = _arr[root];
            }
            // optimisation: path compression
            _arr[x] = root;
            return root;
        }

        public bool AreSameSet(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            return rootX == rootY;
        }
    }
}
