using System.Collections.Generic;
using System.Linq;

namespace LeetCode {
    public class BinaryTreePostOrderTraversal {
        public IList<int> PostorderTraversal(TreeNode root) {
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
                    if (peekNode.right != null && peekNode.right != popNode) {
                        curr = peekNode.right;
                    } else {
                        elements.Add(peekNode.val);
                        popNode = stack.Pop();
                    }
                }
            } while (stack.Count > 0 || curr != null);
            return elements;
        }
    }

}
