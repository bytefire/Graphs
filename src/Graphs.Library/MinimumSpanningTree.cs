using Graphs.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class MinimumSpanningTree
    {
        /// <summary>
        /// Finds the min spanning tree in an undirected weighted graph.
        /// </summary>
        /// <param name="g">Undirected weighted graph.</param>
        /// <returns>Minimum spanning tree</returns>
        public UndirectedGraph Kruskal(UndirectedGraph g)
        {
            // algo:
            // 1. add each edge (just once, because this is undirected graph) to a min priority queue.
            // 2. take each edge one-by-one from the min priority queue and add it to the new graph
            //      as long as it doesn't create a cycle.
            // 3. when the number of edges is equal to number of vertices -1, then return.
            UndirectedGraph mst = new UndirectedGraph(g.Count);
            int maxNumberOfEdges = g.Count * (g.Count - 1) / 2;
            PriorityQueue<Edge> priorityQueue = new PriorityQueue<Edge>(maxNumberOfEdges);
            HashSet<Tuple<int,int>> alreadyIncludedEdges = new HashSet<Tuple<int,int>>();
            foreach (Node n in g)
            {
                foreach (Edge e in n.Edges)
                {
                    if (!alreadyIncludedEdges.Contains(new Tuple<int, int>(e.FromIndex, e.ToIndex)))
                    {
                        // since in undirected graph every directed edge must have a reverse edge
                        // counterpart, we add this edge to the hashset in revers, i.e. ToIndex first and then FromIndex
                        alreadyIncludedEdges.Add(new Tuple<int, int>(e.ToIndex, e.FromIndex));
                        priorityQueue.Insert(e);
                    }
                }
            }

            DisjointSet disjointSet = new DisjointSet(g.Count);
            Edge minEdge = null;
            int edgeCount = 0;
            while (priorityQueue.Count > 0)
            {
                minEdge = priorityQueue.ExtractMinimum();
                // if the edge forms a cycle then discard that edge
                if (disjointSet.AreSameSet(minEdge.FromIndex, minEdge.ToIndex))
                {
                    continue;
                }
                disjointSet.Union(minEdge.FromIndex, minEdge.ToIndex);
                mst.InsertEdge(minEdge.FromIndex, minEdge.ToIndex, minEdge.Weight);
                edgeCount++;
                if (edgeCount == mst.Count - 1)
                {
                    break;
                }
            }

            return mst;
        }
    }
}
