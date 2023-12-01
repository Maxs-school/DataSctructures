using System.Collections;
using System.Xml.Linq;

namespace MyStructures;

public class LinkedList<T>: IEnumerable<T> {
    private LinkedItem? first;
    private LinkedItem? last;

    public int Count { get; private set; }
    public bool IsReadOnly {
        get => false;
    }

    public IEnumerator<T> GetEnumerator() {
        if (first == null) {
            throw new InvalidOperationException("Cannot iterate over an empty list");
        }

        LinkedItem? currentNode = first;

        while (currentNode != last && currentNode != null) {
            yield return currentNode.value;
            currentNode = currentNode.after;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(T item) {
        LinkedItem currentLast = last;


        LinkedItem linkedItem = new LinkedItem(item, currentLast, null);

        if (currentLast != null) {
            currentLast.after = linkedItem;
        }

        last = linkedItem;
        Count++;
    }

    /// <summary>
    /// Adds all the values from one enumerator to the end of this list
    /// </summary>
    /// <param name="enumer">Any enumerable to add from</param>
    public void AddFrom(IEnumerable<T> enumer) {
        foreach (var elem in enumer) {
            this.Add(elem);
        }
    }
    public void AddFrom(LinkedList<T> linkedList) {
        this.last.after = linkedList.first;
        linkedList.first = this.last;

        this.last = linkedList.last;
    }

    public void Clear() {
        first = null;
        last = null;
        Count = 0;
    }

    public bool Contains(T item) {
        if (IndexOf(item) >=0) {
            return true;
        }

        return false;
    }

    public bool Remove(T item) {
        int index = IndexOf(item);
        LinkedItem thingToRemove;
        try {
            thingToRemove = InternalIndexAccess(index);
        }
        catch (Exception e) {
            return false;
        }


        LinkedItem itemBefore = thingToRemove.before;
        LinkedItem itemAfter = thingToRemove.after;

        itemBefore.after = itemAfter;
        itemAfter.before = itemBefore;

        return true;
    }

    public int IndexOf(T item) {
        int i = 0;
        foreach (var element in this) {
            i++;
            // Shouldn't be derefrenceable
            if (item.Equals(element)) {
                return i;
            }
        }

        return -1;
    }


    private LinkedItem InternalIndexAccess(int index) {
        int itemIndex;
        int direction;
        LinkedItem currentNode;

        if (index > Count/2) {
            currentNode = this.last;
            itemIndex = Count -1;
            // This could technically be an enum but that seems overkill
            direction = -1;
        }
        else {
            currentNode = this.first;
            itemIndex = 0;
            direction = 1;
        }


        while (itemIndex != index) {
            if (currentNode == null) {
                throw new Exception("Can't search from a null node");
            }

            itemIndex += direction;

            if (direction == 1 && currentNode.after == null) {
                throw new Exception("Cannot access null item in linked list");
            }
            if (direction == -1 && (currentNode.before == null)) {
                throw new Exception("Cannot access null item in linked list");
            }

            currentNode = currentNode.after;
        }

        if (currentNode == null) {
            throw new InvalidOperationException("Unable to get index of item somehow");
        }

        return currentNode;
    }


    public T this[int index] {
        get {
            return InternalIndexAccess(index).value;
        }
        set => throw new NotImplementedException();
    }

    private class LinkedItem {
        public LinkedItem? before;
        public LinkedItem? after;
        public T value;

        public LinkedItem(T value, LinkedItem? before, LinkedItem? after) {
            this.value = value;
            this.before = before;
            this.after = after;
        }
    }
}