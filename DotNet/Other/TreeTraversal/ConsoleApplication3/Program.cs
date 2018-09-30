using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication3 {

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int x) {
            val = x;
        }
    }

    public class Solution {
        public IList<int> PreorderTraversalIterative(TreeNode root) {
            var elements = new List<int>();
            if (root == null)
                return elements;

            var stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any()) {
                var node = stack.Pop();
                elements.Add(node.val);
                if (node.right != null)
                    stack.Push(node.right);
                if (node.left != null)
                    stack.Push(node.left);
            }
            return elements;
        }

        public IList<int> PreorderTraversalRecursive(TreeNode root) {
            var elements = new List<int>();
            PreorderTraversalRecursiveHelper(root, elements);
            return elements;
        }

        private void PreorderTraversalRecursiveHelper(TreeNode root, IList<int> res) {
            if (root != null) {
                res.Add(root.val);
                if (root.left != null) {
                    PreorderTraversalRecursiveHelper(root.left, res);
                }
                if (root.right != null) {
                    PreorderTraversalRecursiveHelper(root.right, res);
                }
            }
        }

        public IList<int> InorderTraversalRecursive(TreeNode root) {
            var elements = new List<int>();
            InorderTraversalRecursiveHelper(root, elements);
            return elements;
        }

        private void InorderTraversalRecursiveHelper(TreeNode root, IList<int> res) {
            if (root != null) {
                if (root.left != null) {
                    InorderTraversalRecursiveHelper(root.left, res);
                }
                res.Add(root.val);
                if (root.right != null) {
                    InorderTraversalRecursiveHelper(root.right, res);
                }
            }
        }

        public IList<int> InorderTraversalIterative(TreeNode root) {
            var elements = new List<int>();
            if (root == null)
                return elements;
            var stack = new Stack<TreeNode>();
            var curr = root;
            do {
                while (curr != null) {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();
                elements.Add(curr.val);
                curr = curr.right;
            } while (stack.Any() || curr!=null);
            return elements;
        }

        public IList<int> PostorderTraversalRecursive(TreeNode root) {
            var elements = new List<int>();
            PostorderTraversalRecursiveHelper(root, elements);
            return elements;
        }

        private void PostorderTraversalRecursiveHelper(TreeNode root, IList<int> res) {
            if (root != null) {
                if (root.left != null) {
                    PostorderTraversalRecursiveHelper(root.left, res);
                }
                if (root.right != null) {
                    PostorderTraversalRecursiveHelper(root.right, res);
                }
                res.Add(root.val);
            }
        }
        /*
            iterativePostorder(node)
              s ← empty stack
              lastNodeVisited ← null
              while (not s.isEmpty() or node ≠ null)
                if (node ≠ null)
                  s.push(node)
                  node ← node.left
                else
                  peekNode ← s.peek()
                  // if right child exists and traversing node
                  // from left child, then move right
                  if (peekNode.right ≠ null and lastNodeVisited ≠ peekNode.right)
                    node ← peekNode.right
                  else
                    visit(peekNode)
                    lastNodeVisited ← s.pop()        
        */
        public IList<int> PostorderTraversalIterative(TreeNode root) {
            var elements = new List<int>();
            if (root == null)
                return elements;
            TreeNode popNode = null;
            var stack = new Stack<TreeNode>();
            var curr = root;
            do {
                if (curr != null) {
                    stack.Push(curr);
                    curr = curr.left;
                } else {
                    var peekNode = stack.Peek();
                    if(peekNode.right != null && peekNode.right != popNode) { 
                        curr = peekNode.right;
                    } else {
                        elements.Add(peekNode.val);
                        popNode = stack.Pop();
                    }
                }
            } while (stack.Any() || curr != null);
            return elements;
        }

        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
                return result;

            var queue = new Queue<KeyValuePair<int,TreeNode>>();
            queue.Enqueue(new KeyValuePair<int, TreeNode>(0,root));
            result.Add(new List<int>());
            while (queue.Any())
            {
                var element = queue.Dequeue();
                result[element.Key].Add(element.Value.val);
                if (element.Value.left != null || element.Value.right != null)
                {
                    if (element.Key + 1 >= result.Count)
                        result.Add(new List<int>());
                    if (element.Value.left != null)
                        queue.Enqueue(new KeyValuePair<int, TreeNode>(element.Key + 1, element.Value.left));
                    if (element.Value.right != null)
                        queue.Enqueue(new KeyValuePair<int, TreeNode>(element.Key + 1, element.Value.right));
                }
            }
            return result;
        }

        public IList<IList<int>> DictionaryLevelOrder(TreeNode root) {
            var queue = new Queue<KeyValuePair<int, TreeNode>>();
            var result = new Dictionary<int,IList<int>>();
            queue.Enqueue(new KeyValuePair<int, TreeNode>(0, root));
            result[0] = new List<int>();
            while (queue.Any()) {
                var element = queue.Dequeue();
                result[element.Key].Add(element.Value.val);
                if (element.Value.left != null || element.Value.right != null) {
                    if (!result.ContainsKey(element.Key+1))
                        result[element.Key+1] = new List<int>();
                    if (element.Value.left != null)
                        queue.Enqueue(new KeyValuePair<int, TreeNode>(element.Key + 1, element.Value.left));
                    if (element.Value.right != null)
                        queue.Enqueue(new KeyValuePair<int, TreeNode>(element.Key + 1, element.Value.right));
                }
            }
            return result.Values.ToList();
        }

        public int MaxDepth(TreeNode root) {
            if (root == null) {
                return 0;                                 
            }
            var leftDepth = MaxDepth(root.left);
            var rightDepth = MaxDepth(root.right);
            return Math.Max(leftDepth, rightDepth) + 1;      
        }
    }

    class Program {
        static void Main(string[] args)
        {
            var btSize = 1024*1024*32 - 1;
            var bt = BildBt(btSize);
            IList<int> res;
            Console.WriteLine("Builded Binary Try of {0} elements", btSize);
            Stopwatch sw = new Stopwatch();
            var solution = new Solution();
            sw.Start();
            /*res = solution.PreorderTraversalRecursive(bt);
            sw.Stop();
            Console.WriteLine("PreorderTraversalRecursive Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();
            res = solution.PreorderTraversalIterative(bt);
            sw.Stop();
            Console.WriteLine("PreorderTraversalIterative Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();
            res = solution.InorderTraversalRecursive(bt);
            sw.Stop();
            Console.WriteLine("InorderTraversalRecursive Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();
            res = solution.InorderTraversalIterative(bt);
            sw.Stop();
            Console.WriteLine("InorderTraversalIterative Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();
            res = solution.PostorderTraversalRecursive(bt);
            sw.Stop();
            Console.WriteLine("PostorderTraversalRecursive Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();
            res = solution.PostorderTraversalIterative(bt);
            sw.Stop();
            Console.WriteLine("PostorderTraversalIterative Size={0} Elapsed={1} ", res.Count, sw.Elapsed);
            sw.Restart();*/
            var level = solution.LevelOrder(bt);
            sw.Stop();
            Console.WriteLine("LevelOrder Size={0} Elapsed={1} ", level.Count, sw.Elapsed);
            sw.Restart();
            level = solution.DictionaryLevelOrder(bt);
            sw.Stop();
            Console.WriteLine("DictionaryLevelOrder Size={0} Elapsed={1} ", level.Count, sw.Elapsed);
            sw.Restart();
            var depth = solution.MaxDepth(bt);
            sw.Stop();
            Console.WriteLine("MaxDepth Size={0} Elapsed={1} ", depth, sw.Elapsed);

            /*var root = new TreeNode(1) { right = new TreeNode(2) { left = new TreeNode(3) } };
            var solution = new Solution();

            foreach (var element in solution.InorderTraversal(root)) {
                Console.WriteLine(element);
            }*/


            Console.ReadKey();
        }

        private static TreeNode BildBt(int maxValue) {
            return maxValue == 0 ? null : BildBstRecursive(0, maxValue-1);
        }

        private static TreeNode BildBstRecursive(int start, int end) {
            if (start > end)
                return null;
            var mid = (start + end) / 2;
            var root = new TreeNode(mid + 1)
            {
                left = BildBstRecursive(start, mid - 1),
                right = BildBstRecursive(mid + 1, end)
            };
            return root;
        }
    }
}
