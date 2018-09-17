import java.util.ArrayList;
import java.util.List;

public class WordSearch2 {
    public List<String> findWords(char[][] board, String[] words) {
        List<String> res = new ArrayList<>();
        TrieNode root = buildTrie(words);
        for (int i = 0; i < board.length; i++) {
            for (int j = 0; j < board[0].length; j++) {
                dfs (board, i, j, root, res, root);
            }
        }
        return res;
    }

    private void removeWordFromTrie(TrieNode root, TrieNode current, String word) {
        current.word = null;
        for(int i = 0; i < 26; i++) {
            if(current.next[i]!=null)
                return;
        }
        TrieNode cur = root;
        int pos;
        boolean hasMoreChildren;
        char[] characters = word.toCharArray();
        TrieNode[] nodes = new TrieNode[characters.length];
        for (int i = 0; i < nodes.length; i++){
            nodes[i] = cur;
            cur = cur.next[characters[i] - 'a'];
        }
        for (int i = characters.length - 1; i >= 0; i--){
            pos = characters[i] - 'a';
            hasMoreChildren = false;
            for(int j = 0; j < 26; j++) {
                if(j == pos)
                    continue;
                if(nodes[i].next[j] != null){
                    hasMoreChildren = true;
                    break;
                }
            }
            if(!hasMoreChildren) {
                nodes[i].next[pos] = null;
            } else {
                return;
            }
        }
    }

    public void dfs(char[][] board, int i, int j, TrieNode p, List<String> res, TrieNode root) {
        char c = board[i][j];
        if (c == '#' || p.next[c - 'a'] == null) return;
        p = p.next[c - 'a'];
        if (p.word != null) {   // found one
            res.add(p.word);
            removeWordFromTrie(root, p, p.word);
        }

        board[i][j] = '#';
        if (i > 0) dfs(board, i - 1, j ,p, res, root);
        if (j > 0) dfs(board, i, j - 1, p, res, root);
        if (i < board.length - 1) dfs(board, i + 1, j, p, res, root);
        if (j < board[0].length - 1) dfs(board, i, j + 1, p, res, root);
        board[i][j] = c;
    }

    public TrieNode buildTrie(String[] words) {
        TrieNode root = new TrieNode();
        for (String w : words) {
            TrieNode p = root;
            for (char c : w.toCharArray()) {
                int i = c - 'a';
                if (p.next[i] == null) p.next[i] = new TrieNode();
                p = p.next[i];
            }
            p.word = w;
        }
        return root;
    }

    class TrieNode {
        TrieNode[] next = new TrieNode[26];
        String word;
    }
}
