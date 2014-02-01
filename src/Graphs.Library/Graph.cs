using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// Directed weighted graph.
    /// </summary>
    public class Graph : IEnumerable
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
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _nodes.Length; i++)
            {
                yield return _nodes[i];
            }
        }

        public virtual void RemoveEdge(int nodeX, int nodeY)
        {
            Edge e = null;
            int i;
            // find the edge to remove
            for (i = 0; i < _nodes[nodeX].Edges.Count; i++)
            {
                if (_nodes[nodeX].Edges[i].ToIndex == nodeY)
                {
                    e = _nodes[nodeX].Edges[i];
                    break;
                }
            }
            // if edge not found then simply return
            if (e == null)
            {
                return;
            }
            _nodes[nodeX].Edges.RemoveAt(i);
        }

        public virtual void InsertEdge(int nodeX, int nodeY, int weight = 0)
        {
            Edge e = new Edge();
            e.FromIndex = nodeX;
            e.ToIndex = nodeY;
            e.Weight = weight;
            _nodes[nodeX].Edges.Add(e);
        }

        // OptimiseTODO: this is O(V*E). can this be optimised?
        public Graph Reverse()
        {
            Graph reverse = new Graph(this.Capacity);
            // first create all nodes
            foreach (Node n in _nodes)
            {
                if (n == null)
                {
                    continue;
                }
                reverse._nodes[n.Index] = new Node(n.Index);
                reverse._nodes[n.Index].Data = n.Data;
            }

            // insert reversed edges
            foreach (Node n in _nodes)
            {
                if (n == null)
                {
                    continue;
                }
                
                foreach (Edge e in n.Edges)
                {
                    reverse.InsertEdge(e.ToIndex, n.Index, e.Weight);
                }
            }
            return reverse;
        }
    }
}
