namespace Subjects.LeetCode;

public class SmallProblems
{
    public static int[] GetConcatenation(int[] nums)
    {
        var arr = new int[nums.Length * 2];
        for (int i = 0; i < nums.Length; i++)
        {
            arr[i] = nums[i];
            arr[i + nums.Length] = nums[i];
        }

        return arr;
    }

    static int[] BuildArray(int[] nums)
    {
        var l = new int[nums.Length];
        for (var i = 0; i < nums.Length; i++)
        {
            l[i] = nums[nums[i]];
        }

        return l;
    }

    /* https://leetcode.com/problems/contains-duplicate/description/ */
    static bool ContainsDuplicate(int[] nums)
    {
        var dict = new Dictionary<int, int?>();
        for (var i = 0; i < nums.Length; i++)
        {
            dict.TryGetValue(nums[i], out var val);
            if (val != null) return true;
            dict.Add(nums[i], i);
        }

        return false;
    }

    static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        return candies.Select(x => x + extraCandies >= candies.Max()).ToList();
    }

    static IList<string> FindRepeatedDnaSequences(string s)
    {
        var all = new HashSet<string>();
        var actual = new List<string>();
        var a = 0;
        for (var i = 0; i + 9 < s.Length; i++)
        {
            var win = s.Substring(a, 10);
            if (!all.Add(win)) actual.Add(win);
            a++;
        }

        return actual;
    }

    static IList<int> FindAnagrams(string s, string p)
    {
        var pCharArray = p.ToCharArray();
        Array.Sort(pCharArray);
        var pSorted = new string(pCharArray);

        var res = new List<int>();
        for (int i = 0; i <= s.Length - p.Length; i++)
        {
            var win = s.Substring(i, p.Length).ToCharArray();
            Array.Sort(win);
            var winSorted = new string(win);

            if (winSorted == pSorted) res.Add(i);
        }

        return res;
    }
}