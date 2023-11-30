namespace MyStructures;



/// <summary>
/// Stores items, along with a Priority for each Item
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue<T> where T : IComparable {
    // Naive implementation - store items along with their Priority
    private IList<PrioritizedItem> contents = new MyStructures.CustomList<PrioritizedItem>();

    /// <summary>
    /// The number of items in the queue
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public int Count => contents.Count;

    public void Enqueue(T item, int priority) {
        // Create a new PrioritizedItem

        PrioritizedItem priorityItem = new PrioritizedItem(item, priority);

        // Add it to the end of the list
        contents.Add(priorityItem);

    }

    /// <summary>
    /// Returns the highest Priority Item from the queue
    /// and removes the Item
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">If the queue is empty</exception>
    public T Dequeue() {
        PrioritizedItem item = FindLargestItem();

        contents.Remove(item);

        return item.Item;
    }


    public T Peek() {
        return FindLargestItem().Item;
    }


    private PrioritizedItem FindLargestItem() {
        if (contents.Count == 0) {
            throw new InvalidOperationException("The queue is empty, nothing to search for");
        }

        PrioritizedItem largest = contents[0];
        foreach (PrioritizedItem item in contents) {
            if (item.Priority > largest.Priority) {
                largest = item;
            }
        }

        return largest;
    }


    /// <summary>
    /// Private struct only used by the Priority queue
    /// --> Structs are value types, not reference types
    /// </summary>
    internal struct PrioritizedItem : IComparable {
        public T Item;
        public int Priority;

        public PrioritizedItem(T value, int priority) {
            this.Item = value;
            this.Priority = priority;
        }

        public int CompareTo(object? obj) {
            return Item.CompareTo(obj);
        }
    }
}
