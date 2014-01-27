using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Utilities;

namespace Graphs.Library
{
    public class UndirectedGraphProblems
    {
        /// <summary>
        /// Eulierian cycle. An Eulierian cycle in a graph is a cycle (not necessarily simple) that uses every edge in the graph exactly one.
        /// Design a linear-time algorithm to determine whether a graph has an Eulerian cycle, and if so, find one.
        /// </summary>
        /// <param name="g">The undirected graph.</param>
        /// <returns></returns>
        public bool HasEulerianCycle(UndirectedGraph g)
        {
            foreach (Node n in g)
            {
                if (n.Edges.Count % 2 != 0)
                {
                    return false;
                }
            }
            bool[] marked = new bool[g.Capacity];
            GeneralGraphProblems.PerformDfsItertatively(g, n => { marked[n.Index] = true; });
            foreach (Node n in g)
            {
                if (n.Edges.Count > 0 && !marked[n.Index])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Eulierian cycle. An Eulierian cycle in a graph is a cycle (not necessarily simple) that uses every edge in the graph exactly one.
        /// Design a linear-time algorithm to determine whether a graph has an Eulerian cycle, and if so, find one.
        /// </summary>
        /// <param name="root">Any node in a given graph.</param>
        /// <returns>Eulerian cycle.</returns>
        public LinkedList<Node> GetEulerianCycle(UndirectedGraph g)
        {
            // TestTODO: need to test this properly

            // Uses Hierholzer's algorithm (see: http://www.youtube.com/watch?v=3k5_oooad8U)
            LinkedList<Node> eulerianCycle = new LinkedList<Node>();
            LinkedList<Node> tempCycle;
            Node nextNode;
            Node startNode = null;
            Edge e;
            while (g.Count > 0)
            {
                // by the end of this loop startNode must be set to a non-null node because 
                // g.Count > 0.
                for (int i = 0; i < g.Capacity; i++)
                {
                    if (g[i] != null)
                    {
                        startNode = g[i];
                        break;
                    }
                }
                nextNode = startNode;
                tempCycle = new LinkedList<Node>();
                do
                {
                    tempCycle.AddLast(nextNode);
                    e = nextNode.Edges[0];
                    int fromIndex = nextNode.Index;
                    nextNode = g[e.ToIndex];
                    g.RemoveEdge(fromIndex, e.ToIndex);
                    g.RemoveNodeIfUnconnected(fromIndex);
                } while (nextNode != startNode);

                // merge temp into eulerian cycle
                eulerianCycle = eulerianCycle.InsertAtMatchingStart(tempCycle);
            }
            return eulerianCycle;
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
            bool[] marked = new bool[g.Capacity];

            root.Distance = 0;
            marked[root.Index] = true;
            root.Parent = null;
            stack.Push(root);

            Node temp;

            while (stack.Count > 0)
            {
                temp = stack.Pop();
                bool hasUnvisitedNeighbours = false;

                foreach (Edge e in temp.Edges)
                {
                    if (!marked[e.ToIndex])
                    {
                        hasUnvisitedNeighbours = true;
                        marked[e.ToIndex] = true;
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
