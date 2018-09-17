using System.Collections.Generic;

namespace LeetCode {
    public class LRUCache {
        private readonly int Capacity;
        private readonly Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> Set;

        private readonly LinkedList<KeyValuePair<int, int>> Order;

        public LRUCache(int capacity) {
            Capacity = capacity;
            Order = new LinkedList<KeyValuePair<int, int>>();
            Set = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>(capacity);
        }

        public int Get(int key) {
            if (!Set.TryGetValue(key, out LinkedListNode<KeyValuePair<int, int>> node))
                return -1;
            Reorder(node);
            return node.Value.Value;
        }

        public void Put(int key, int value) {
            var cacheItem = new KeyValuePair<int, int>(key, value);
            if (Set.TryGetValue(key, out LinkedListNode<KeyValuePair<int, int>> node)) {
                node.Value = cacheItem;
                Set[key] = node;
                Reorder(node);
                return;
            }
            LinkedListNode<KeyValuePair<int, int>> newnode = new LinkedListNode<KeyValuePair<int, int>>(cacheItem);
            Retention();
            Order.AddFirst(newnode);
            Set.Add(key, newnode);
        }

        private void Reorder(LinkedListNode<KeyValuePair<int, int>> node) {
            Order.Remove(node);
            Order.AddFirst(node);
        }

        private void Retention() {
            if (Set.Count != Capacity)
                return;
            var tail = Order.Last;
            Set.Remove(tail.Value.Key);
            Order.RemoveLast();
        }
    }
}
