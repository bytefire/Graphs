using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class UndirectedGraphProblems
    {
        public void PerformDfsItertatively(Node root, Action<Node> processNode)
        {
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
                    if (!e.To.Visited)
                    {
                        e.To.Visited = true;
                        stack.Push(e.To);
                    }
                }
            }
        }

        /// <summary>
        /// Given a connected graph with no cycles, find diameter.
        /// Diameter: design a linear-time algorithm to find the longest simple path in the graph.
        /// </summary>
        /// <remarks>
        /// Source: Algorithms II course on coursera.org.
        /// </remarks>
        public Stack<Node> DiameterOfAGraph(Node root)
        {
            // algo: first find the farthest node from given node (root). that will be one end of diameter.
            //      then find the farthest node from that node. the path thus obtained will be the diameter.

            Node end1 = FindPathToFarthestNode(root);
            Node end2 = FindPathToFarthestNode(end1);

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
        public Node FindPathToFarthestNode(Node root)
        {
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
                    if (!e.To.Visited)
                    {
                        hasUnvisitedNeighbours = true;
                        e.To.Visited = true;
                        e.To.Distance = temp.Distance + 1;
                        e.To.Parent = temp;
                        stack.Push(e.To);
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
