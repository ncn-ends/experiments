namespace Subjects.LeetCode;

public class LengthOfLongestSubStringGroup
{
    // Print(LengthOfLongestSubstring("abcabcbb"));
    // Print(LengthOfLongestSubstring("bbbbb"));
    // Print(LengthOfLongestSubstring("pwwkew"));
    // Print(LengthOfLongestSubstring("pwwkez"));
    // Print(LengthOfLongestSubstring("dvdf"));
    // Print(LengthOfLongestSubstring("tmmzuxt"));
    // https://leetcode.com/problems/longest-substring-without-repeating-characters/
    // pwwkez
    /*
     * iterating over
     * on p
     *      set biggest to 1
     *      set d["p"] to 0
     * on w
     *      set biggest to 2
     *      set d["w"] to 1
     * on w
     *      set a to 2
     *      set d["w"] to 2
     * on k
     *      set d["k"] to 3
     * on e
     *      set biggest to 3
     *      set d["e"] to 4
     * on z
     *      set biggest to 4
     *      set d["z"] to 4
     */
    static int LengthOfLongestSubstring(string s)
    {
        // d = dictionary holds characters that have been found
        // a = start of window
        // z = end of window
        // biggest = biggest substring window
        // loop from 0 to s.length - 1, where z is the iterator
        //      char = s[z]
        //      memoed = d[char]
        //      if in dict: set a to be z
        //      else
        //          if difference between a and z  + 1 > biggest, set biggest
        //      set d[char] to z

        var dict = new Dictionary<char, int?>();
        var a = 0;
        int biggest = 0;
        for (var z = 0; z < s.Length; z++)
        {
            var c = s[z];
            dict.TryGetValue(c, out var f);
            if (f != null)
            {
                var newA = (int) f + 1;
                for (int i = a; i < newA; i++)
                {
                    dict.Remove(s[i]);
                }
                a = (int) f + 1;
            }
            else if (z + 1 - a > biggest) biggest = z + 1 - a;
            dict[c] = z;
        }

        return biggest;

    }
}