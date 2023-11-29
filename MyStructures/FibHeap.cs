namespace MyStructures; 

public class FibHeap<T> where T : IComparable {
    LinkedList<HeapTree> treelist = new LinkedList<HeapTree>();

    private HeapTree minimumElement;

    public void Insert(T element) {
        HeapTree tree = new HeapTree(element);
        this.treelist.Add(tree);
        if (element.CompareTo(this.minimumElement.minimum) < 0) {
            this.minimumElement = tree;
        }
    }

    public void Merge(FibHeap<T> heap) {
        this.treelist.AddFrom(heap.treelist);
    }

    internal class HeapTree {
        public T minimum;
        public int degree;

        private CustomList<HeapTree> children;
        public HeapTree(T element) {
            throw new NotImplementedException("HEAP TREE NOT IMPLEMENTED");
        }
    }
}