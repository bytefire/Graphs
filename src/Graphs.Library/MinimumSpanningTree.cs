﻿using Graphs.Utilities;
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
        /// Finds the min spanning tree in an undirected weighted graph, using Kruskal's algorithm.
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

        /// <summary>
        /// Finds min spanning tree in an undirected graph using Prim's algorithm, the lazy approach.
        /// </summary>
        /// <param name="g">Undirected graph in which to look for MST.</param>
        /// <returns>Minimum spanning tree</returns>
        public UndirectedGraph Prim(UndirectedGraph g)
        {
            int maxNumberOfEdges = g.Count * (g.Count - 1) / 2;
            PriorityQueue<Edge> priorityQueue = new PriorityQueue<Edge>(maxNumberOfEdges);
            int edgeCount = 0; // counts number of edges currently in MST
            UndirectedGraph mst = new UndirectedGraph(g.Count);
            int newestVertex = 0;

            do
            {
                foreach (Edge e in g[newestVertex].Edges)
                {
                    // this makes sure that the other vertex of the edge hasn't already been added to MST
                    if (mst[e.ToIndex].Edges.Count == 0)
                    {
                        priorityQueue.Insert(e);
                    }
                }

                bool found = false;
                do
                {
                    Edge smallest = priorityQueue.ExtractMinimum();
                    if (mst[smallest.ToIndex].Edges.Count == 0)
                    {
                        found = true;
                        mst.InsertEdge(smallest.FromIndex, smallest.ToIndex, smallest.Weight);
                        newestVertex = smallest.ToIndex;
                        edgeCount++;
                    }
                } while (!found);
            } while (edgeCount < g.Count - 1);

            return mst;
        }

        public UndirectedGraph MinimumBottleneckSpanningTree(UndirectedGraph g)
        {
            // this is a modification of Prims
            // select the smallest edge from vertex zero
            // mark that as the current maximum
            // add it to the graph
            // then search for any edge that connects an unconnected vertex to the tree and its weight is less than current maximum

            int edgeCount = 0; // counts number of edge currently in MST
            UndirectedGraph mbst = new UndirectedGraph(g.Count);
            int newestVertex = 0;
            
            Edge minEdge = new Edge { Weight = Double.MaxValue };
            // Edge minEdgeSoFar = new Edge { Weight = Double.MaxValue };
            foreach (Edge e in g[newestVertex].Edges)
            {
                if (minEdge.Weight > e.Weight)
                {
                    minEdge = e;
                }
            }

            mbst.InsertEdge(minEdge.FromIndex, minEdge.ToIndex, minEdge.Weight);
            edgeCount++;
            newestVertex = minEdge.ToIndex;
            double currentMax = minEdge.Weight;
            List<Edge> candidateEdges = new List<Edge>();

            do
            {
                foreach (Edge e in g[newestVertex].Edges)
                {
                    // this makes sure that the other vertex of the edge hasn't already been added to MST
                    if (mbst[e.ToIndex].Edges.Count == 0)
                    {
                        candidateEdges.Add(e);
                    }
                }

                bool found = false;
                Edge nextSmallestEdge = new Edge { Weight = Double.MaxValue };
                foreach (Edge c in candidateEdges)
                {
                    if (mbst[c.ToIndex].Edges.Count == 0)
                    {
                        if (c.Weight <= currentMax)
                        {
                            mbst.InsertEdge(c.FromIndex, c.ToIndex, c.Weight);
                            newestVertex = c.ToIndex;
                            edgeCount++;
                            found = true;
                            break;
                        }
                        else
                        {
                            if (nextSmallestEdge.Weight > c.Weight)
                            {
                                nextSmallestEdge = c;
                            }
                        }
                    }
                }

                if (!found)
                {
                    currentMax = nextSmallestEdge.Weight;
                    mbst.InsertEdge(nextSmallestEdge.FromIndex, nextSmallestEdge.ToIndex, nextSmallestEdge.Weight);
                    newestVertex = nextSmallestEdge.ToIndex;
                    edgeCount++;
                }

            } while (edgeCount < g.Count - 1);

            return mbst;
        }
    }
}
