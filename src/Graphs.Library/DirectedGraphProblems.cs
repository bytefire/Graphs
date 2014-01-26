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
        // this won't make sense if the graph is cyclic even though it will finish with a result.
        public static IEnumerable<Node> TopologicalSort(Graph g)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
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
                n.Visited = true;
                stack.Push(n);

                while (stack.Count > 0)
                {
                    temp = stack.Peek();
                    bool hasUnvisitedEdges = false;
                    foreach (Edge e in temp.Edges)
                    {
                        if (!g[e.ToIndex].Visited)
                        {
                            g[e.ToIndex].Visited = true;
                            stack.Push(g[e.ToIndex]);
                            hasUnvisitedEdges = true;
                        }
                    }

                    if (!hasUnvisitedEdges)
                    {
                        topologicallySorted.Push(temp);
                        stack.Pop();
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
