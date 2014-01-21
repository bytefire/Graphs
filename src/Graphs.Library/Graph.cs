using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class Graph
    {
        private Node[] _nodes;

        // NOTE: this is different from capacity. this indicates how many vertices
        //      the graph has. capacity is the maximum number of vertices the graph
        //      could have.
        public int Count
        {
            get;
            private set;
        }

        public Graph(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("Capacity should be at least 1.");
            }
            _nodes = new Node[capacity];
        }

        public Node this[int index]
        {
            get
            {
                return _nodes[index];
            }
            set
            {
                _nodes[index] = value;
            }
        }
    }
}
