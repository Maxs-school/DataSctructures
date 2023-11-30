using System.Collections;

namespace MyStructures;
public class CustomList<T>: IList<T> {
    public int Capacity;
    private static Random rng = new Random();

    private T[] array;
    public int Count { get; private set; }
    public bool IsReadOnly { get; } = false;

    public static void Main(string[] args) {
        Console.Write("Hello there, you should run the tests");
    }

    public CustomList(T[] items) {
        if (items.Length > 4) {
            // Gives you the root doublings to reach the capacity of the inputed array
            Capacity = MinimumDoubledCount(items.Length);
        }
        else {
            this.Capacity = 4;
        }
        array = new T[Capacity];
        items.CopyTo(array,0);

        Count = items.Length;
    }

    // From this stack overflow post
    // https://stackoverflow.com/questions/273313/randomize-a-listt
    /// <summary>
    /// Randomly shuffles the array. Runs in O(n) time
    /// </summary>
    public void Shuffle()
    {
        int n = this.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = this[k];
            this[k] = this[n];
            this[n] = value;
        }
    }

    public CustomList<T> Clone() {
        CustomList<T> list = new CustomList<T>();

        foreach (T item in this) {
            list.Add(item);
        }

        return list;
    }


    public CustomList() : this(new T[0]) { }

    private static int MinimumDoubledCount(int count) {
        return (int) Math.Pow(2,(Math.Ceiling(Math.Log2(count))));
    }


    public static CustomList<T> CreateList(T[] items) {
        // return new List<T>(items);
        return new CustomList<T>(items);
    }

    public IEnumerator<T> GetEnumerator() {
        return new ArraySegment<T>(this.array, 0, Count).GetEnumerator();
    }



    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(T item) {
        if (this.Capacity < this.Count + 1) {
            this.Capacity *= 2;
            T[] tempArray = new T[this.Capacity];
            this.array.CopyTo(tempArray,0);
            this.array = tempArray;
        }

        this.array[Count] = item;
        Count+=1;
    }

    public void Clear() {
        this.Count = 0;
        this.array = new T[Capacity];
    }

    public bool Contains(T item) {
        return array.Contains(item);
    }

    public void CopyTo(T[] targetArray, int arrayIndex) {
        this.array.CopyTo(targetArray,0);
    }

    public bool Remove(T item) {
        int index = IndexOf(item);
        if (index <= -1) { return false; }

        T[] temparray = new T[Capacity];
        new ArraySegment<T>(this.array, 0,index).CopyTo(temparray);
        new ArraySegment<T>(this.array, index+1,Count-1-index).CopyTo(temparray, index);

        this.array = temparray;
        this.Count -= 1;

        return true;
    }

    public void RemoveRange(int index, int count) {
        if (count < 0) {
            throw new ArgumentException("Count must be a positive integer");
        }

        if (index < 0) {
            throw new ArgumentException("Index must be a positive integer");
        }

        if (index + count > this.Count) {
            throw new IndexOutOfRangeException(
                $"Tried to remove past the end of the array. removing {count} past index {index} but total length is {this.Count}");
        }

        int newCount = this.Count - count;
        int newCapacity = MinimumDoubledCount(newCount);
        T[] temparray = new T[newCapacity];
        new ArraySegment<T>(this.array, 0,index).CopyTo(temparray);
        new ArraySegment<T>(this.array, index+count,Count-index-count).CopyTo(temparray,index);

        this.array = temparray;
        this.Count = newCount;
    }


    public int IndexOf(T item) {
        return Array.IndexOf(array, item, 0, Count);
    }

    public void Insert(int index, T item) {
        if (this.Capacity < this.Count + 1) {
            this.Capacity *= 2;
            T[] tempArray = new T[this.Capacity];
            this.array.CopyTo(tempArray,0);
            this.array = tempArray;
        }

        T[] temparray = new T[Capacity];
        new ArraySegment<T>(this.array, 0,index).CopyTo(temparray);
        temparray[index] = item;
        new ArraySegment<T>(this.array, index,Count-index).CopyTo(temparray,index+1);

        this.array = temparray;
        this.Count += 1;
    }

    public void RemoveAt(int index) {
        T[] temparray = new T[Capacity];
        new ArraySegment<T>(this.array, 0,index).CopyTo(temparray);
        new ArraySegment<T>(this.array, index,Count-1-index).CopyTo(temparray, index);

        this.array = temparray;
        this.Count -= 1;
    }

    public void TrimExcess() {
        this.Capacity = Count;
    }

    public T this[int index] {
        get {
            if (Count - 1 < index) {
                throw new IndexOutOfRangeException($"Index {index} was greater than the length of the array {Count}");
            }

            return this.array[index];
        }
        set {
            if (Count - 1 < index) {
                throw new IndexOutOfRangeException($"Index {index} was greater than the length of the array {Count}");
            }

            this.array[index] = value;
        }
    }
}
