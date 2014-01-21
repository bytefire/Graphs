using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class UndirectedGraphProblems
    {
        public void PerformDfsItertatively(Graph g, Action<Node> processNode)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }
            Node root = g[0];
            Stack<Node> stack = new Stack<Node>();

            root.Visited = true;
            stack.Push(root);
            Node temp;
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                processNode(temp);

                foreach (Edge e in temp.Neighbours)
                {
                    if (!g[e.ToIndex].Visited)
                    {
                        g[e.ToIndex].Visited = true;
                        stack.Push(g[e.ToIndex]);
                    }
                }
            }
        }

        /// <summary>
        /// Eulierian cycle. An Eulierian cycle in a graph is a cycle (not necessarily simple) that uses every edge in the graph exactly one.
        /// Design a linear-time algorithm to determine whether a graph has an Eulerian cycle, and if so, find one.
        /// </summary>
        /// <param name="root">Any node in a given graph.</param>
        /// <returns></returns>
        public bool HasEulerianCycle(Node root)
        {
            throw new NotImplementedException();
        }

        public Stack<Node> GetEulerianCycle(Node root)
        {
            // Uses Hierholzer's algorithm (see: http://www.youtube.com/watch?v=3k5_oooad8U)
            throw new NotImplementedException();

        }

        /// <summary>
        /// Given a connected graph with no cycles, find its center.
        /// Center: design a linear-time algorithm to find a vertex such that its maximum distance from any other vertex is minimized.
        /// </summary>
        /// <param name="root">Any node of the graph.</param>
        /// <returns>Centre of the graph.</returns>
        /// <remarks>
        /// Source: Algorithms II course on coursera.org.
        /// </remarks>
        public Node CentreOfGraph(Graph g)
        {
            Stack<Node> diameter = DiameterOfGraph(g);
            int centerLocationCounter = diameter.Count/2;
            while (centerLocationCounter > 0)
            {
                diameter.Pop();
                centerLocationCounter--;
            }
            return diameter.Pop();
        }

        /// <summary>
        /// Given a connected graph with no cycles, find diameter.
        /// Diameter: design a linear-time algorithm to find the longest simple path in the graph.
        /// </summary>
        /// <remarks>
        /// Source: Algorithms II course on coursera.org.
        /// </remarks>
        public Stack<Node> DiameterOfGraph(Graph g)
        {
            // algo: first find the farthest node from given node (root). that will be one end of diameter.
            //      then find the farthest node from that node. the path thus obtained will be the diameter.

            Node end1 = FindPathToFarthestNode(g);
            Node end2 = FindPathToFarthestNode(g);

            Node temp = end2;
            Stack<Node> diameter = new Stack<Node>();

            do
            {
                diameter.Push(temp);
                temp = temp.Parent;
            } while (temp != null);

            return diameter;
        }

        /// <summary>
        /// Given an undirected, unweighted and acyclic graph, this finds a node that is farthest from
        ///  the give root node.
        /// </summary>
        /// <param name="root">The node to measure distance from.</param>
        /// <returns></returns>
        public Node FindPathToFarthestNode(Graph g)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            Node root = g[0];
            Node farthest = root;
            int maximumDistance = 0;
            Stack<Node> stack = new Stack<Node>();

            root.Distance = 0;
            root.Visited = true;
            root.Parent = null;
            stack.Push(root);

            Node temp;

            while (stack.Count > 0)
            {
                temp = stack.Pop();
                bool hasUnvisitedNeighbours = false;

                foreach (Edge e in temp.Neighbours)
                {
                    if (!g[e.ToIndex].Visited)
                    {
                        hasUnvisitedNeighbours = true;
                        g[e.ToIndex].Visited = true;
                        g[e.ToIndex].Distance = temp.Distance + 1;
                        g[e.ToIndex].Parent = temp;
                        stack.Push(g[e.ToIndex]);
                    }
                }

                if (!hasUnvisitedNeighbours) // this is a leaf node
                {
                    if (temp.Distance > maximumDistance)
                    {
                        maximumDistance = temp.Distance;
                        farthest = temp;
                    }
                }
            }

            return farthest;
        }
    }
}
