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
            bool[] marked = new bool[g.Capacity];
            Node temp;
            
            foreach (Node n in g)
            {
                if ((n == null) || marked[n.Index])
                {
                    continue;
                }
                marked[n.Index] = true;
                stack.Push(n);

                while (stack.Count > 0)
                {
                    temp = stack.Peek();
                    bool hasUnvisitedEdges = false;
                    foreach (Edge e in temp.Edges)
                    {
                        if (!marked[e.ToIndex])
                        {
                            marked[e.ToIndex] = true;
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
            bool[] marked = new bool[g.Capacity];
            marked[n.Index] = true;
            stack.Push(n);

            while (stack.Count > 0)
            {
                n = stack.Pop();
                foreach (Edge e in n.Edges)
                {
                    if (marked[e.ToIndex])
                    {
                        return true;
                    }
                    marked[e.ToIndex] = true;
                    stack.Push(g[e.ToIndex]);
                }
            }
            return false;
        }

        /// <summary>
        /// Uses Kosaraju's algorithm (http://en.wikipedia.org/wiki/Kosaraju%27s_algorithm) to find strongly
        /// connected components.
        /// </summary>
        /// <param name="g">The graph to find the components in.</param>
        /// <returns>Array whose indices represent nodes and values denote strongly connected components.</returns>
        public static int[] StronglyConnectedComponents(Graph g)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            int[] scc = new int[g.Capacity];
            for (int i = 0; i < scc.Length; i++)
            {
                scc[i] = -1;
            }
            IEnumerable<Node> topological = TopologicalSort(g);
            Graph reverse = g.Reverse();
            // NOTE: this array does the work of Node.Visited property. so when we get rid of Node.Visited property,
            //      we should use a data structure similar to this.
            bool[] marked = new bool[reverse.Capacity];
            int counter = 0;
            foreach (Node n in topological)
            {
                if (marked[n.Index])
                {
                    continue;
                }

                GeneralGraphProblems.PerformDfsItertatively(reverse, node => 
                { 
                    marked[node.Index] = true;
                    scc[node.Index] = counter;
                });
                counter++;
            }
            return scc;
        }
    }
}
