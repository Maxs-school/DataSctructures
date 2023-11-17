namespace Tests;
using MyStructures;

[TestClass]
public class ListTest {
    private const int LoopUpperBound = 1_000_000;

    [TestMethod]
    public void AddingItem() {
        IList<int> list = CustomList<int>.CreateList(new int[] { 3 });
        list.Add(5);
        list.Add(4);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(5, list[1]);
        Assert.AreEqual(4, list[2]);
    }

    [TestMethod]
    public void Length() {
        IList<int> list = CustomList<int>.CreateList(new int[0]);
        Assert.AreEqual(list.Count, 0);
        for (int i = 1; i <= 10000; i++) {
            list.Add(i);
            Assert.AreEqual(list.Count, i);
        }
    }

    [TestMethod]
    public void ClearWorks() {
        IList<int> list = CustomList<int>.CreateList(new int[] { 5, 3, 5, 6, 7, 4, 3 });
        list.Clear();
        Assert.AreEqual(list.Count, 0);
    }


    [TestMethod]
    public void TestContains() {
        for (int i = 0; i < LoopUpperBound; i++) {

            Random rand = new Random(i);
            var array = GetRandomArray(rand);

            IList<int> list = CustomList<int>.CreateList(array);

            int checkWithin = rand.Next(1, 3);
            switch (checkWithin) {
                case 1: {
                    int val = rand.Next(0, 10);
                    Assert.IsTrue(list.Contains(array[val]),
                        $"Expected to find {array[val]} in {String.Join(",", array)}, seed value: {i}");
                    break;
                }
                case 2: {
                    int val = rand.Next();
                    Assert.IsFalse(list.Contains(val) && array.Contains(val),
                        $"Didn't expect to find {val} in {String.Join(",", array)}, seed value: {i}");
                    break;
                }
                default: {
                    throw new Exception("Random generated out of range, your test sucks");
                }
            }
        }
    }

    private static int[] GetRandomArray(Random rand) {
        int[] array = new int[10];

        for (int j = 0; j < 10; j++) {
            array[j] = rand.Next();
        }

        return array;
    }

    [TestMethod]
    public void IndexOf() {
        for (int i = 0; i < LoopUpperBound; i++) {
            Random rand = new Random(i);
            int[] array = GetRandomArray(rand);

            IList<int> list = CustomList<int>.CreateList(array);

            bool testValNotInArray = rand.Next(0, 2) == 1;

            int expectedIndex;
            int valueToFind;
            if (testValNotInArray) {
                expectedIndex = -1;
                do {
                    valueToFind = rand.Next();
                } while (array.Contains(valueToFind));
            }
            else {
                expectedIndex = rand.Next(0, 10);
                valueToFind = list[expectedIndex];
            }

            int actualIndex = list.IndexOf(valueToFind);
            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }


    [TestMethod]
    public void Insert() {
        for (int i = 0; i < LoopUpperBound; i++) {
            Random rand = new Random(i);
            int[] arr = GetRandomArray(rand);

            IList<int> list = CustomList<int>.CreateList(arr);

            int index = rand.Next(0, 10);
            int value = rand.Next();
            list.Insert(index, value);

            Assert.AreEqual(list[index], value);
            if (index != 0) {
                Assert.AreEqual(list[index - 1], arr[index - 1]);
            }

            if (index != 9) {
                Assert.AreEqual(list[index + 1], arr[index]);
            }

            Assert.AreEqual(list.Count, 11);
        }
    }

    [TestMethod]
    [Timeout(20000)]
    // Timeout is to prevent any weird issues with Count not being updated properly
    // This test should in no way take more than 20 seconds even on a very slow computer
    public void Remove() {
        for (int i = 0; i < LoopUpperBound; i++) {
            Random rand = new Random(i);
            int[] arr = GetRandomArray(rand);

            IList<int> list = CustomList<int>.CreateList(arr);

            while (list.Count >= 2) {
                bool removeFromList = rand.Next(0, 2) == 1;

                if (removeFromList) {
                    int val = list[rand.Next(0, list.Count)];
                    Assert.IsTrue(
                        list.Remove(val)
                    );
                }
                else {
                    int safeValue = rand.Next();
                    while (arr.Contains(safeValue)) {
                        safeValue = rand.Next();
                    }

                    Assert.IsFalse(list.Remove(safeValue));
                }
            }
        }
    }

    [TestMethod]
    public void RemoveAt() {
        for (int i = 0; i < LoopUpperBound; i++) {
            Random rand = new Random(i);
            int[] arr = GetRandomArray(rand);

            IList<int> list = CustomList<int>.CreateList(arr);

            int expectedCount = 10;
            while (list.Count >= 2) {
                int val = rand.Next(0, list.Count);
                list.RemoveAt(val);
                expectedCount -= 1;
                Assert.AreEqual(list.Count, expectedCount);
            }
        }
    }

    [TestMethod]
    public void EnumeratorWorks() {
        Random rand = new Random();
        int[] arr = GetRandomArray(rand);

        IList<int> list = CustomList<int>.CreateList(arr);

        int i = 0;
        foreach (var item in list) {
            Assert.AreEqual(arr[i], item);
            i++;
        }
    }
}
