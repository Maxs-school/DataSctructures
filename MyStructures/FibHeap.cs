namespace MyStructures; 

public class FibHeap<T> where T : IComparable {
    LinkedList<HeapTree> treelist = new LinkedList<HeapTree>();

    private HeapTree? maxElement;

    public void Insert(T element) {
        HeapTree tree = new HeapTree(element);
        this.treelist.Add(tree);


        if (this.maxElement == null || element.CompareTo(this.maxElement.Root) > 0) {
            this.maxElement = tree;
        }
    }

    /// <summary>
    /// Merges with another heap and updates the minimum pointer, safe to use externally
    /// </summary>
    /// <param name="heap">The fibonacci heap to merge with</param>
    public void Merge(FibHeap<T> heap) {
        this.Merge(heap.treelist);

        if (heap.maxElement == null) {
            throw new NullReferenceException("Cannot merge with an empty heap");
        }

        if (this.maxElement == null) {
            throw new NullReferenceException("Cannot merge with an empty heap");
        }

        if (heap.maxElement.Root.CompareTo(this.maxElement.Root) < 0) {
            this.maxElement = heap.maxElement;
        }
    }

    /// <summary>
    /// Returns the smallest element in the list without removing it
    /// </summary>
    /// <returns>The smallest element</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty</exception>
    public T GetSmallest() {
        if (this.maxElement == null) {
            throw new InvalidOperationException("Cannot get the smallest element from an empty list");
        }
        return this.maxElement.Root;
    }

    /// <summary>
    /// Merges the internal heaptree list with another heaptree list
    /// DOES NOT update the minimum, use with caution
    /// </summary>
    /// <param name="tree">The tree to merge in</param>
    private void Merge(LinkedList<HeapTree> tree) {
        this.treelist.AddFrom(tree);
    }

    public T RemoveMin() {
        if (maxElement == null) {
            throw new InvalidOperationException("Cannot remove from an empty heap");

        }

        T min = maxElement.Root;
        var childen = maxElement.RemoveParent();

        if (childen != null) {
            Merge(childen);
        }

        HeapTree smallest = treelist[0];
        int maxDegree = 0;
        foreach (var tree in this.treelist) {
            maxDegree = Math.Max(maxDegree, tree.Degree);
            if (tree.Root.CompareTo(smallest.Root) < 0) {
                smallest = tree;
            }
        }

        HeapTree?[] degreeList = new HeapTree?[maxDegree];
        bool changed;
        do {
            changed = false;
            foreach (HeapTree tree in treelist) {
                if (degreeList[tree.Degree] != null) {
                    if (tree == degreeList[tree.Degree]) {
                        continue;
                    }

                    // It's really dumb that this "non-nullable" type is giving a warning about being null possibly
                    tree.Merge(degreeList[tree.Degree]);
                    changed = true;
                }
                else {
                    degreeList[tree.Degree] = tree;
                }

            }
        } while (changed);

        return min;
    }

    internal class HeapTree {
        public T Root;
        public int Degree;

        private LinkedList<HeapTree> children;
        public HeapTree(T element) {
            this.Root = element;
            this.Degree = 0;
            this.children = new LinkedList<HeapTree>();
        }

        public LinkedList<HeapTree>? RemoveParent() {
            if (children.Count > 0) {
                return this.children;
            }
            else {
                return null;
            }
        }

        public void Merge(HeapTree tree) {
            this.children.Add(tree);
            this.Degree++;
        }
    }
}