namespace Tests;
using MyStructures;

[TestClass]
public class PriorityQueueTests {
    const int Interationcount = 10_000;


    [TestMethod]
    public void TestQueueing() {
        PriorityQueue<int> queue = new PriorityQueue<int>();
        CustomList<int> list = new CustomList<int>();

        // Insert them
        const int interationCount = 1000;
        for (int j = 0; j < interationCount; j++) {
            list.Add(j);
        }

        CustomList<int> list2 = list.Clone();


        list2.Shuffle();


        foreach (int thingy in list2) {
            queue.Enqueue(thingy, thingy);
        }

        // Check the order was correct
        while (queue.Count > 0) {
            int value = queue.Dequeue();
            Assert.AreEqual(list[list.Count-1], value);
            list.RemoveAt(list.Count-1);
        }
    }

    [TestMethod]
    public void PeekTest() {
        PriorityQueue<int> queue = new PriorityQueue<int>();
        CustomList<int> list = new CustomList<int>();

        // Insert them
        const int interationCount = 1000;
        for (int j = 0; j < interationCount; j++) {
            list.Add(j);
        }

        CustomList<int> list2 = list.Clone();


        list2.Shuffle();


        foreach (int thingy in list2) {
            queue.Enqueue(thingy, thingy);
        }

        int expectedItem = list[list.Count - 1];

        for (int i = 0; i < 10000; i++) {
            Assert.AreEqual(expectedItem, queue.Peek());
        }
    }
}