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
        public static LinkedList<int> TopologicalSort(Graph g)
        {
            if (g.Count == 0)
            {
                throw new ArgumentException("Graph is empty.");
            }

            Stack<Node> stack = new Stack<Node>();
            LinkedList<int> topologicallySorted = new LinkedList<int>();
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
                        topologicallySorted.AddFirst(temp.Index);
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
            IEnumerable<int> topological = TopologicalSort(g);
            Graph reverse = g.Reverse();
            // NOTE: this array does the work of Node.Visited property. so when we get rid of Node.Visited property,
            //      we should use a data structure similar to this.
            bool[] marked = new bool[reverse.Capacity];
            int counter = 0;
            foreach (int n in topological)
            {
                if (marked[n])
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

        /// <summary>
        /// Hamiltonian path in a DAG. Given a directed acyclic graph, design a linear-time algorithm 
        /// to determine whether it has a Hamiltonian path (a simple path that visits every vertex), and if so, find one.
        /// (from coursera)
        /// </summary>
        /// <param name="dag">The directed acyclic graph.</param>
        /// <returns>A path that goes through every vertex.</returns>
        public static IEnumerable<int> HamiltonianPathInDAG(Graph dag)
        {
            if (dag.Count == 0)
            {
                throw new ArgumentException("The graph is empty.");
            }
            if (dag.Count == 1)
            {
                return new List<int>() { dag[0].Index };
            }
            // idea: first topologically sort the graph. this should be fine because every dag can be topologically sorted.
            // then every edge will be pointing forward. if there is an edge between each pair of adjacent topologically sorted
            // vertices then hamiltonian path exists and that is the hamiltonian path.

            LinkedList<int> topological = TopologicalSort(dag);
            List<int> path = new List<int>();
            LinkedListNode<int> first = topological.First;
            LinkedListNode<int> second = first.Next;
            path.Add(first.Value);
            while (second != null)
            {
                if (!dag[first.Value].Edges.Any(e => e.ToIndex == second.Value))
                {
                    return new List<int>();
                }
                path.Add(second.Value);
                first = first.Next;
                second = second.Next;
            }
            return path;
        }

        /// <summary>
        /// Design a linear-time algorithm to determine whether a DAG has a vertex that is 
        /// reachable from every other vertex, and if so, find one. 
        /// (from coursera)
        /// </summary>
        /// <param name="dag">The directed acyclic graph.</param>
        /// <returns>Index of the reachable vertex (node) if it exists; -1 otherwise.</returns>
        public static int ReachableVertexInDAG(Graph dag)
        {
            // find a vertex which doesn't have any outgoing edges. if there is only one such vertex then
            // that is the reachable vertex. if there are more than one, then reachable vertex doesn't exist.
            int vertex = -1;
            int count = 0;
            foreach (Node n in dag)
            {
                if (n.Edges.Count == 0)
                {
                    count++;
                    if (count > 1)
                    {
                        return -1;
                    }
                    vertex = n.Index;
                }
            }
            return vertex;
        }
        
        /// <summary>
        /// Design a linear-time algorithm to determine whether a digraph has a vertex that is 
        /// reachable from every other vertex, and if so, find one. (from coursera)
        /// </summary>
        /// <param name="g">The directed graph.</param>
        /// <returns>Set of vertices which are reachable from every other vertex in the graph.</returns>
        public static List<int> ReachableVerticesInDirectedGraph(Graph g)
        {
            // idea: first find strongly connected components using kosaraju's algo. this will return an array whose integer values
            // will represent a strongly connected component. we need to find a strongly connected component with "out-degree" of zero.
            // here out-degree refers to the number of edges going out from one strongly-connected component to another.
            // every vertex in the component with "out-degree" of zero will be reachable from every other vertex in the graph.

            int[] scc = StronglyConnectedComponents(g);
            HashSet<int> outEdgeComponents = new HashSet<int>();
            for (int n = 0; n < scc.Length; n++)
            {
                if (!outEdgeComponents.Contains(scc[n]))
                {
                    // search for an edge going to a component other than scc[n]
                    if (g[n].Edges.Any(e => scc[e.ToIndex] != scc[n]))
                    {
                        outEdgeComponents.Add(scc[n]);
                    }
                }
            }

            int motherComponent = -1;
            bool found = false;
            foreach (int component in scc)
            {
                if (!outEdgeComponents.Contains(component))
                {
                    if (found)
                    {
                        return new List<int>();
                    }
                    found = true;
                    motherComponent = component;
                }
            }

            List<int> reachableVertices = new List<int>();
            for (int n = 0; n < scc.Length; n++)
            {
                if (scc[n] == motherComponent)
                {
                    reachableVertices.Add(n);
                }
            }
            return reachableVertices;
        }

        /// <summary>
        /// Shortest directed cycle. Given a digraph G, design an efficient algorithm to find a directed cycle 
        /// with the minimum number of edges (or report that the graph is acyclic). 
        /// The running time of your algorithm should be at most proportional to V(E+V) and use space proportional 
        /// to E+V, where V is the number of vertices and E is the number of edges.
        /// (from coursera)
        /// </summary>
        /// <param name="g">Graph to find the cycle in.</param>
        /// <param name="startingIndex">Optional starting index.</param>
        /// <returns>List of indices for nodes which form the shortest cycle.</returns>
        public static IEnumerable<int> ShortestCycle(Graph g, int startingIndex = 0)
        {
            bool[] marked = new bool[g.Capacity];
            int[] connectedComponents = new int[g.Capacity];
            Array.ForEach(connectedComponents, cc => cc = -1);
            int currentComponent = 0;
            int shortestLength = Int32.MaxValue;
            IEnumerable<int> shortestCycle = null;
            int unmarked = -1;
            bool foundUnmarked = false;
            for (unmarked = 0, foundUnmarked = false; unmarked < marked.Length; unmarked++)
            {
                if (!marked[unmarked])
                {
                    foundUnmarked = true;
                    break;
                }
            }

            while (foundUnmarked)
            {
                IEnumerable<int> cycle = ShortestCycleInReachableNodes(g, unmarked, marked, connectedComponents, currentComponent);
                if (cycle.Count() < shortestLength)
                {
                    shortestLength = cycle.Count();
                    shortestCycle = cycle;
                }

                currentComponent++;

                for (unmarked = 0, foundUnmarked = false; unmarked < marked.Length; unmarked++)
                {
                    if (!marked[unmarked])
                    {
                        foundUnmarked = true;
                        break;
                    }
                }
            }

            if (shortestLength < Int32.MaxValue)
            {
                return shortestCycle;
            }
            return new List<int>();
        }


        private static IEnumerable<int> ShortestCycleInReachableNodes(Graph g, int startingNode, bool[] marked,
            int[] connectedComponents, int currentComponent)
        {
            // idea: perform a BFS and look for back edges and keep a running count for each back edge. at the end of BFS, shortest count
            // should represent shortest cycle.
            Queue<Node> q = new Queue<Node>();
            int[] levels = new int[g.Capacity];
            int[] parents = new int[g.Capacity];
            Array.ForEach(parents, p => p = -1);
            int currentLevel = 0;
            Node n = g[startingNode];
            int shortestDistance = Int32.MaxValue;
            int shortestFirst = -1, shortestLast = -1;
            
            marked[n.Index] = true;
            levels[n.Index] = currentLevel;
            connectedComponents[n.Index] = currentComponent;
            parents[n.Index] = n.Index;
            q.Enqueue(n);

            while (q.Count > 0)
            {
                n = q.Dequeue();
                currentLevel++;

                foreach (Edge e in n.Edges)
                {
                    if (marked[e.ToIndex])
                    {
                        if (connectedComponents[e.ToIndex] == currentComponent)
                        {
                            int distance = currentLevel - levels[e.ToIndex];
                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                shortestFirst = e.ToIndex;
                                shortestLast = n.Index;
                            }
                        }
                    }
                    else
                    {
                        marked[e.ToIndex] = true;
                        levels[e.ToIndex] = currentLevel;
                        connectedComponents[e.ToIndex] = currentComponent;
                        parents[e.ToIndex] = n.Index;
                        q.Enqueue(g[e.ToIndex]);
                    }
                }
            }

            if (shortestDistance < Int32.MaxValue)
            {
                return GetCycle(parents, shortestFirst, shortestLast);
            }
            return new List<int>();
        }

        private static IEnumerable<int> GetCycle(int[] parents, int shortestFirst, int shortestLast)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(shortestLast);
            int parent;
            do
            {
                parent = parents[shortestLast];
                stack.Push(parent);
            } while (parent != shortestFirst) ;
            return stack;
        }
    }
}
