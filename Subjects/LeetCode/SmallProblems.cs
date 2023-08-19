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
}