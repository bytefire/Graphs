using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class Node
    {
        public int Data { get; set; }

        public List<Edge> Neighbours { get; set; }

        #region Auxiliary Properties
        
        public bool Visited { get; set; }
        public int Distance { get; set; }
        public Node Parent { get; set; } // used for paths

        #endregion
    }
}
