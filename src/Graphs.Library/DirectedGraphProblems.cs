using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// These are problems that only have meaning for directed graphs.
    /// </summary>
    public class DirectedGraphProblems
    {
        // this can only be performed on a directed acyclic graph.
        public static IEnumerable<Node> TopologicalSort(Graph g)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            // first check for cycles
            foreach (Node n in g)
            {
                if (n == null || n.Visited)
                {
                    continue;
                }

                if (ContainsCycle(g, n.Index))
                {
                    throw new ArgumentException("The graph contains cycles.");
                }
            }

            Stack<Node> stack = new Stack<Node>();
            Stack<Node> topologicallySorted = new Stack<Node>();
            Node temp;
            
            foreach (Node n in g)
            {
                if ((n == null) || n.Visited)
                {
                    continue;
                }
                stack.Push(n);

                while (stack.Count > 0)
                {
                    temp = stack.Pop();
                    bool hasUnvisitedEdges = false;
                    foreach (Edge e in temp.Edges)
                    {
                        if (!g[e.ToIndex].Visited)
                        {
                            stack.Push(g[e.ToIndex]);
                            hasUnvisitedEdges = true;
                        }
                    }

                    if (!hasUnvisitedEdges)
                    {
                        topologicallySorted.Push(temp);
                        temp.Visited = true;
                    }
                }
            }

            return topologicallySorted;
        }

        public static bool ContainsCycle(Graph g, int startingNode = 0)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            // perform DFS looking for a back edge
            Node n = g[startingNode];
            Stack<Node> stack = new Stack<Node>();
            n.Visited = true;
            stack.Push(n);

            while (stack.Count > 0)
            {
                n = stack.Pop();
                foreach (Edge e in n.Edges)
                {
                    if (g[e.ToIndex].Visited)
                    {
                        return true;
                    }
                    g[e.ToIndex].Visited = true;
                    stack.Push(g[e.ToIndex]);
                }
            }
            return false;
        }
    }
}
