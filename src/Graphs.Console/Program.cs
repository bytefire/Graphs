using Graphs.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            stack.Push(8);

            foreach (int i in stack)
            {
                System.Console.WriteLine(i);
            }

            System.Console.WriteLine("stack.Count = " + stack.Count);


            LinkedList<int> parent = new LinkedList<int>();
            parent.AddLast(1);
            parent.AddLast(2);
            parent.AddLast(6);
            parent.AddLast(7);
            parent.AddLast(8);

            LinkedList<int> child = new LinkedList<int>();
            child.AddLast(3);
            child.AddLast(4);
            child.AddLast(5);

            LinkedListNode<int> temp = parent.Find(2);
            //parent.AddAfter(temp, child.First);

            foreach (int val in parent)
            {
                System.Console.Write("{0}, ", val);
            }
            System.Console.ReadKey();
            // http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx

            SortedDictionary<Guid, Edge> sortedEdges = new SortedDictionary<Guid, Edge>();
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.5 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.6 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.2 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.9 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.1 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.1 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.6 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.7 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.5 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.5 });
            sortedEdges.Add(Guid.NewGuid(), new Edge() { FromIndex = 0, ToIndex = 1, Weight = 0.5 });

            Edge min = sortedEdges.Min().Value;
        }
    }
}
