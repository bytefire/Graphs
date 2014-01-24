using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public abstract class Graph
    {
        protected Node[] _nodes;

        /// <summary>
        /// Gets the number of nodes in the graph.
        /// </summary>
        /// <remarks>
        /// This is different from capacity. this indicates how many vertices
        /// the graph has. capacity is the maximum number of vertices the graph
        /// could have.
        /// </remarks>
        public int Count
        {
            get;
            protected set;
        }

        public int Capacity
        {
            get;
            protected set;
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

        public virtual void RemoveEdge(int nodeX, int nodeY)
        {
            Edge e = null;
            int i;
            // find the edge to remove
            for (i = 0; i < _nodes[nodeX].Neighbours.Count; i++)
            {
                if (_nodes[nodeX].Neighbours[i].ToIndex == nodeY)
                {
                    e = _nodes[nodeX].Neighbours[i];
                    break;
                }
            }
            // if edge not found then simply return
            if (e == null)
            {
                return;
            }
            _nodes[nodeX].Neighbours.RemoveAt(i);
        }
    }
}
