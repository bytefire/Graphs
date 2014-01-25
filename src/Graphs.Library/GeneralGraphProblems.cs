using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// These problems don't care whether the graph is directed or not.
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

            root.Visited = true;
            stack.Push(root);
            Node temp;
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                processNode(temp);

                foreach (Edge e in temp.Edges)
                {
                    if (!g[e.ToIndex].Visited)
                    {
                        g[e.ToIndex].Visited = true;
                        stack.Push(g[e.ToIndex]);
                    }
                }
            }
        }

        public static void PerformDfsRecursively(Graph g, Action<Node> processNode, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            Node n = g[startingNode];
            n.Visited = true;
            processNode(n);

            foreach (Edge e in n.Edges)
            {
                if (!g[e.ToIndex].Visited)
                {
                    PerformDfsRecursively(g, processNode, e.ToIndex);
                }
            }
        }

        public static int[] PerformBfsAndReturnShortestPaths(Graph g, Action<Node> processNode, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            int[] paths = new int[g.Capacity];
            foreach (int p in paths)
            {
                paths[p] = -1;
            }
            Queue<Node> q = new Queue<Node>();
            Node n = g[startingNode];
            n.Visited = true;
            q.Enqueue(n);
            paths[startingNode] = startingNode;

            while (q.Count > 0)
            {
                n = q.Dequeue();
                processNode(n);
                foreach (Edge e in n.Edges)
                {
                    if (!g[e.ToIndex].Visited)
                    {
                        g[e.ToIndex].Visited = true;
                        q.Enqueue(g[e.ToIndex]);
                        paths[e.ToIndex] = n.Index;
                    }
                }
            }

            return paths;
        }
    }
}
