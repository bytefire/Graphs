using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// Represents a directed edge.
    /// </summary>
    public class Edge : IComparable<Edge>
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public double Weight { get; set; }

        int IComparable<Edge>.CompareTo(Edge other)
        {
            return this.Weight.CompareTo(other.Weight);
        }
    }
}
