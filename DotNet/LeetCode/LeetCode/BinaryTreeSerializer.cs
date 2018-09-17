using System.Text;

namespace LeetCode {
    public class BinaryTreeSerializer {
        private const char Separator = ',';

        // Encodes a tree to a single string.
        public string serialize(TreeNode root) {
            StringBuilder sb = new StringBuilder();
            serialize(root, sb);
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data) {
            var items = data.Split(Separator);
            if (items.Length == 0)
                return null;
            var i = 0;
            var root = deserialize(items, ref i);

            return root;
        }

        private void serialize(TreeNode root, StringBuilder sb) {
            if (root == null)
                sb.Append(Separator);
            else {
                sb.Append(root.val).Append(Separator);
                serialize(root.left, sb);
                serialize(root.right, sb);
            }
        }

        private TreeNode deserialize(string[] items, ref int index) {
            if (index >= items.Length)
                return null;
            var node = BuildNode(items[index]);
            if (node == null)
                return null;
            index++;
            node.left = deserialize(items, ref index);
            index++;
            node.right = deserialize(items, ref index);
            return node;
        }

        private TreeNode BuildNode(string str) {
            return string.IsNullOrEmpty(str) || !int.TryParse(str, out int val) ? null : new TreeNode(val);
        }
    }
}
