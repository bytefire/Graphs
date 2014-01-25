using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class UndirectedGraph : Graph
    {
        public UndirectedGraph(int capacity)
            : base(capacity)
        {
        }

        public override void RemoveEdge(int nodeX, int nodeY)
        {
            base.RemoveEdge(nodeX, nodeY);
            base.RemoveEdge(nodeY, nodeX);
        }

        /// <summary>
        /// Removes a node if it is not connected to any other node. If the node is connected
        /// to at least one other node then this method returns false and leaves the node as it is.
        /// </summary>
        /// <param name="node">The node to remove.</param>
        /// <returns>True if node is unconnected and hence successfully removed; false otherwise.</returns>
        public bool RemoveNodeIfUnconnected(int node)
        {
            if ((node >= _nodes.Length) || (node<0))
            {
                throw new ArgumentOutOfRangeException("The supplied node index doesn't exist in the graph.");
            }
            // idempotence
            if (_nodes[node] == null)
            {
                return true;
            }

            // node is unconnected if it has no edges.
            // NOTE: this check only makes sense in undirected graphs.
            if (_nodes[node].Edges.Count == 0)
            {
                _nodes[node] = null;
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
