using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Utilities
{
    public static class LinkedListExtensions
    {
        /// <summary>
        /// Inserts the child linked list into parent at the place where start node of child matches a node in parent.
        /// E.g. parent = 1->2->3->4->5 and child=3->6->9 then this method will return 1->2->3->6->9->4->5. This 
        /// method leaves parent linked list as it is.
        /// </summary>
        /// <param name="parent">The list to merge into.</param>
        /// <param name="child">The list being merged.</param>
        /// <returns>Merged list.</returns>
        /// <remarks>
        /// NOTE: this is an O(m+n) operation in both time and space, rather than constant time which would have been typical of a linked list.
        /// May be we need to create our own linked list class which behaves the way we want it.
        /// </remarks>
        public static LinkedList<T> InsertAtMatchingStart<T>(this LinkedList<T> parent, LinkedList<T> child)
        {
            if (child.Count < 2)
            {
                return parent;
            }
            if (parent.Count < 2)
            {
                return child;
            }
            LinkedList<T> merged = new LinkedList<T>();
            LinkedListNode<T> curr = parent.First;

            while (curr != null && !curr.Value.Equals(child.First.Value)) // not using operator != because that cannot be used with type T.
            {
                curr = curr.Next;
                merged.AddLast(curr.Value);
            }
            if (curr == null)
            {
                throw new ArgumentException("The parent list doesn't contain a node matching with the starting node of child list. " +
                    "Failed to merge child into parent.");
            }
            LinkedListNode<T> childPtr = child.First;
            while (childPtr != null)
            {
                merged.AddLast(childPtr.Value);
                childPtr = childPtr.Next;
            }
            curr = curr.Next;
            while (curr != null)
            {
                merged.AddLast(curr.Value);
                curr = curr.Next;
            }

            return merged;
        }
    }
}
