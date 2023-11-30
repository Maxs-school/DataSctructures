namespace MyStructures; 

public class FibHeap<T> where T : IComparable {
    LinkedList<HeapTree> treelist = new LinkedList<HeapTree>();

    private HeapTree minimumElement;


    public FibHeap() {
        throw NotImplementedException("NEED TO DO");
    }

    public void Insert(T element) {
        HeapTree tree = new HeapTree(element);
        this.treelist.Add(tree);
        if (element.CompareTo(this.minimumElement.root) < 0) {
            this.minimumElement = tree;
        }
    }

    /// <summary>
    /// Merges with another heap and updates the minimum pointer, safe to use externally
    /// </summary>
    /// <param name="heap">The fibonacci heap to merge with</param>
    public void Merge(FibHeap<T> heap) {
        this.Merge(heap.treelist);
        if ( heap.minimumElement.root.CompareTo(this.minimumElement.root) < 0) {
            this.minimumElement = heap.minimumElement;
        }
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
        T min = minimumElement.root;
        var childen = minimumElement.RemoveParent();

        if (childen != null) {
            Merge(childen);
        }

        HeapTree smallest = treelist[0];
        int maxDegree = 0;
        foreach (var tree in this.treelist) {
            maxDegree = Math.Max(maxDegree, tree.degree);
            if (tree.root.CompareTo(smallest.root) < 0) {
                smallest = tree;
            }
        }

        HeapTree?[] degreeList = new HeapTree?[maxDegree];
        bool changed;
        do {
            changed = false;
            foreach (HeapTree tree in treelist) {
                if (degreeList[tree.degree] != null) {
                    if (tree == degreeList[tree.degree]) {
                        continue;
                    }

                    tree.Merge(degreeList[tree.degree]);
                    changed = true;
                }
                else {
                    degreeList[tree.degree] = tree;
                }

            }
        } while (changed);

        return min;
    }

    internal class HeapTree {
        public T root;
        public int degree;

        private LinkedList<HeapTree> children;
        public HeapTree(T element) {
            throw new NotImplementedException("HEAP TREE NOT IMPLEMENTED");
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
        }
    }
}