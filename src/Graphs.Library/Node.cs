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
        public int Data { get; set; }

        public List<Edge> Edges { get; set; }

        #region Auxiliary Properties
        
        public bool Visited { get; set; }
        public int Distance { get; set; }
        public Node Parent { get; set; } // used for paths

        #endregion

        public Node(int index)
        {
            Index = index;
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
