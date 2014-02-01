using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class UndirectedGraph : Graph
    {
        public UndirectedGraph(int capacity)
            : base(capacity)
        {
        }

        public override void RemoveEdge(int nodeX, int nodeY)
        {
            base.RemoveEdge(nodeX, nodeY);
            base.RemoveEdge(nodeY, nodeX);
        }

        public override void InsertEdge(int nodeX, int nodeY, double weight = 0)
        {
            base.InsertEdge(nodeX, nodeY, weight);
            base.InsertEdge(nodeY, nodeX, weight);
        }
    }
}
