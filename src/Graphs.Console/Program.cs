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
            parent.AddAfter(temp, child.First);

            foreach (int val in parent)
            {
                System.Console.Write("{0}, ", val);
            }
            System.Console.ReadKey();
        }
    }
}
