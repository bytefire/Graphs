using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class DjikstraShortestPath
    {
        private Graph _graph;
        private double[] _distanceTo;
        private Edge[] _edgeTo;
        private bool[] _visited;
        private int _sourceVertex = -1;

        public DjikstraShortestPath(Graph g)
        {
            _graph = g;
            _distanceTo = new double[_graph.Count];
            _edgeTo = new Edge[_graph.Count];
            _visited = new bool[_graph.Count];

            for (int i = 0; i < _graph.Count; i++)
            {
                _distanceTo[i] = double.MaxValue;
            }
        }

        public void Process(int sourceVertex)
        {
            _sourceVertex = sourceVertex;
            _distanceTo[_sourceVertex] = 0;

            int idx = GetMinIndex(_distanceTo, _visited);
            while (idx != -1)
            {
                Relax(idx);
                _visited[idx] = true;
                idx = GetMinIndex(_distanceTo, _visited);
            }
        }

        public IEnumerable<Edge> GetShortestPath(int destinationVertex)
        {
            Stack<Edge> path = new Stack<Edge>();
            if (destinationVertex == _sourceVertex)
            {
                return path;
            }
            Edge e;
            do
            {
                e = _edgeTo[destinationVertex];
                path.Push(e);
            } while (e.FromIndex != _sourceVertex);

            return path;
        }

        private void Relax(int vertex)
        {
            foreach (Edge e in _graph[vertex].Edges)
            {
                if (_distanceTo[e.ToIndex] > _distanceTo[vertex] + e.Weight)
                {
                    _distanceTo[e.ToIndex] = _distanceTo[vertex] + e.Weight;
                    _edgeTo[e.ToIndex] = e;
                }
            }
        }

        private int GetMinIndex(double[] arr, bool[] visited)
        {
            // TODO: implement this using priority queue where we can update priorities
            throw new NotImplementedException();
        }
    }
}
