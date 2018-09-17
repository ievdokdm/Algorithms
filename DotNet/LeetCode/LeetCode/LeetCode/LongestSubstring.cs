using System;
using System.Collections.Generic;

namespace LeetCode {
    public class LongestSubstring {
        public int LengthOfLongestSubstring(string s) {
            Dictionary<char, int> letters = new Dictionary<char, int>();
            var maxLen = 0;
            var start = -1;
            for (int i = 0; i < s.Length; i++) {
                if (letters.TryGetValue(s[i], out int val)) {
                    if (val > start)
                        start = letters[s[i]];
                    letters[s[i]] = i;
                } else {
                    letters.Add(s[i], i);
                }
                maxLen = Math.Max(maxLen, i - start);
            }
            return maxLen;
        }
    }
}

