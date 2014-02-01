using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// These problems don't care whether the graph is directed or not. However, the graph is expected to be unweighted.
    /// </summary>
    public class GeneralGraphProblems
    {
        public static void PerformDfsItertatively(Graph g, Action<Node> processNode, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }
            Node root = g[startingNode];
            Stack<Node> stack = new Stack<Node>();
            bool[] marked = new bool[g.Count];

            marked[root.Index] = true;
            stack.Push(root);
            Node temp;
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                processNode(temp);

                foreach (Edge e in temp.Edges)
                {
                    if (!marked[e.ToIndex])
                    {
                        marked[e.ToIndex] = true;
                        stack.Push(g[e.ToIndex]);
                    }
                }
            }
        }

        public static void PerformDfsRecursively(Graph g, Action<Node> processNode, bool[] marked, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            if (marked == null || marked.Length != g.Count)
            {
                throw new ArgumentException("The marked array's length must be equal to the capacity of the graph.");
            }

            Node n = g[startingNode];
            marked[n.Index] = true;
            processNode(n);

            foreach (Edge e in n.Edges)
            {
                if (!marked[e.ToIndex])
                {
                    PerformDfsRecursively(g, processNode, marked, e.ToIndex);
                }
            }
        }

        public static int[] PerformBfsAndReturnShortestPaths(Graph g, Action<Node> processNode, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            int[] paths = new int[g.Count];
            foreach (int p in paths)
            {
                paths[p] = -1;
            }
            Queue<Node> q = new Queue<Node>();
            bool[] marked = new bool[g.Count];
            Node n = g[startingNode];
            
            marked[n.Index] = true;
            q.Enqueue(n);
            paths[startingNode] = startingNode;

            while (q.Count > 0)
            {
                n = q.Dequeue();
                processNode(n);
                foreach (Edge e in n.Edges)
                {
                    if (!marked[e.ToIndex])
                    {
                        marked[e.ToIndex] = true;
                        q.Enqueue(g[e.ToIndex]);
                        paths[e.ToIndex] = n.Index;
                    }
                }
            }

            return paths;
        }

        /// <summary>
        /// Searches the shortest path from any of the starting nodes to a node whose data is equal to the searched value.
        /// </summary>
        /// <param name="g">Graph to search</param>
        /// <param name="startingNodes">The nodes from which to start.</param>
        /// <param name="searchedValue">The value being searched.</param>
        /// <returns>Path to a node containing that value. This will be null if the value is not found.</returns>
        public static int[] MultipleShortestPath(Graph g, int[] startingNodes, int searchedValue)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            int[] paths = new int[g.Count];
            foreach (int p in paths)
            {
                paths[p] = -1;
            }
            Queue<Node> q = new Queue<Node>();
            bool[] marked = new bool[g.Count];
            Node n;
            foreach (int i in startingNodes)
            {
                n = g[i];
                marked[n.Index] = true;
                q.Enqueue(n);
                paths[i] = i;
                if (n.Data == searchedValue)
                {
                    return paths;
                }
            }

            while (q.Count > 0)
            {
                n = q.Dequeue();
                foreach (Edge e in n.Edges)
                {
                    if (!marked[e.ToIndex])
                    {
                        marked[e.ToIndex] = true;
                        q.Enqueue(g[e.ToIndex]);
                        paths[e.ToIndex] = n.Index;
                        if (g[e.ToIndex].Data == searchedValue)
                        {
                            return paths;
                        }
                    }
                }
            }

            return null;
        }
    }
}
