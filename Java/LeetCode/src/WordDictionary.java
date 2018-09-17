import java.util.*;

public class WordDictionary {
    private TrieNode root;
    /** Initialize your data structure here. */
    public WordDictionary() {
        root = new TrieNode();
    }

    /** Adds a word into the data structure. */
    public void addWord(String word) {
        TrieNode current = root;
        TrieNode newNode;
        for (Character c: word.toCharArray()) {
            if(current.childs.containsKey(c)) {
                current = current.childs.get(c);
            } else {
                newNode = new TrieNode();
                current.childs.put(c, newNode);
                current.dot.add(newNode);
                current = newNode;
            }
        }
        current.isWordEnd = true;
    }

    /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
    public boolean search(String word) {
        ArrayDeque<TrieNode> currentqueue = new ArrayDeque<>();
        ArrayDeque<TrieNode> childqueue = new ArrayDeque<>();
        currentqueue.push(root);
        for (Character c: word.toCharArray()) {
            while (!currentqueue.isEmpty()) {
                TrieNode current= currentqueue.pop();
                if(c=='.') {
                    for (TrieNode child: current.dot) {
                        childqueue.push(child);
                    }
                } else if(current.childs.containsKey(c)) {
                    childqueue.push(current.childs.get(c));
                }
            }
            currentqueue = childqueue;
            childqueue = new ArrayDeque<>();
        }

        while (!currentqueue.isEmpty()) {
            TrieNode current = currentqueue.pop();
            if(current.isWordEnd)
                return true;
        }

        return false;
    }

    class TrieNode {
        public Map<Character,TrieNode> childs = new HashMap<>();
        public Set<TrieNode> dot = new HashSet<>();
        public boolean isWordEnd;
    }
}
