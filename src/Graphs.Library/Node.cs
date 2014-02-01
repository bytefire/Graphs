using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class Node
    {
        public int Index { get; private set; }
        // TODO: make data type of data generic
        public int Data { get; set; }

        public List<Edge> Edges { get; set; }

        public Node(int index)
        {
            Index = index;
            Edges = new List<Edge>();
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public override bool Equals(object obj)
        {
            return (GetHashCode() == obj.GetHashCode());
        }

        public static bool operator ==(Node left, Node right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Node left, Node right)
        {
            return !left.Equals(right);
        }
    }
}
