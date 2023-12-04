namespace MyStructures;

public class FibQueue<T> where T : IComparable {
    private FibHeap<PriorityQueue<T>.PrioritizedItem> heap = new FibHeap<PriorityQueue<T>.PrioritizedItem>();

    public int Count { get; private set; }

    public void Enqueue(T item, int priority) {
        PriorityQueue<T>.PrioritizedItem prioritizedItem = new PriorityQueue<T>.PrioritizedItem(item, priority);

        heap.Insert(prioritizedItem);
    }

    public T Dequeue() {
        PriorityQueue<T>.PrioritizedItem item = heap.RemoveMin();

        return item.Item;
    }
    public T Peek() {
        return heap.GetSmallest().Item;
    }
}
