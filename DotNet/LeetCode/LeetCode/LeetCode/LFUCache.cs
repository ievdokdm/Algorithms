using System.Collections.Generic;

namespace LeetCode {
    public class LFUCache {
        private readonly int Capacity;
        private LFUNode Head = null;
        private LFUNode Tail = null;
        private int Count = 0;
        private readonly Dictionary<int, LFUNode> Set;

        public LFUCache(int capacity) {
            Capacity = capacity;
            Set = new Dictionary<int, LFUNode>(capacity);
        }


        public int Get(int key) {
            if (!Set.TryGetValue(key, out LFUNode node))
                return -1;
            if (Capacity > 1)
                Touch(node);
            return node.Value;
        }

        public void Put(int key, int value) {
            if (Capacity <= 0)
                return;

            if (Set.TryGetValue(key, out LFUNode node)) {
                node.Value = value;
            } else {
                node = new LFUNode(key, value);
                if (Capacity == 1) {
                    Set.Clear();
                    node = new LFUNode(key, value);
                    Set.Add(node.Key, node);
                    return;
                } else {
                    AddNode(node);
                }
                Set.Add(node.Key, node);
            }
            Touch(node);
        }

        private void AddNode(LFUNode node) {
            if (Head == null) {
                Head = node;
                Tail = node;
                Count++;
                return;
            }

            if (Count == Capacity) {
                Set.Remove(Tail.Key);
                if (Head == Tail) {
                    Head = node;
                    Tail = node;
                    return;
                }
                Tail.Left.Right = null;
                Tail = Tail.Left;
            } else {
                Count++;
            }

            Tail.Right = node;
            node.Left = Tail;
            Tail = node;
        }

        private void Touch(LFUNode node) {
            node.Frequency++;
            LFUNode parent = node.Left;

            if (parent == null || parent.Frequency > node.Frequency)
                return;

            while (parent != null && parent.Frequency <= node.Frequency)
                parent = parent.Left;

            //cutoff node from its position
            node.Left.Right = node.Right;
            if (node.Right != null) {
                node.Right.Left = node.Left;
            } else {
                Tail = node.Left;
            }

            //insert node after parent
            node.Left = parent;
            if (parent != null) {
                node.Right = parent.Right;
                parent.Right.Left = node;
                parent.Right = node;
            } else {
                node.Right = Head;
                Head.Left = node;
                Head = node;
            }
        }
    }

    public class LFUNode {
        public int Key;
        public int Value;
        public long Frequency = 0;
        public LFUNode Left;
        public LFUNode Right;

        public LFUNode(int key, int value) {
            Key = key;
            Value = value;
        }
    }
}
