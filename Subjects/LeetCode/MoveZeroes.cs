using System.Diagnostics;

namespace Subjects.LeetCode;

public class MoveZeroes
{
    public static void Do(int[] nums)
    {
        var zeroes = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i - zeroes] = nums[i];

            if (nums[i] == 0) zeroes++;
            if (i == nums.Length - 1 && zeroes == 0) continue;
            if (nums.Length - i <= zeroes + 1) nums[i] = 0;
        }
    }
}