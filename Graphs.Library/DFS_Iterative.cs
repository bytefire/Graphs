using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Library
{
    public class DFS_Iterative
    {
        public void PerformDFS_Itertatively(Node root, Action<Node> processNode)
        {
            Stack<Node> stack = new Stack<Node>();

            root.Visited = true;
            stack.Push(root);
            Node temp;
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                processNode(temp);

                foreach (Edge e in temp.Neighbours)
                {
                    if (!e.To.Visited)
                    {
                        e.To.Visited = true;
                        stack.Push(e.To);
                    }
                }
            }
        }
    }
}
