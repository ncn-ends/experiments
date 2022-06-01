using System.Collections;
using System.Diagnostics;

namespace Subjects.LeetCode;

/* https://leetcode.com/problems/two-sum/ */
public class TwoSum
{
    private readonly List<int> _nums;
    private readonly int _target;
    public TwoSum(List<int> nums, int target)
    {
        _nums = nums;
        _target = target;
    }
    
    /*
     * Brute force solution
     * - O(n^2)
     */
    public (int, int) SolutionA()
    {
        int firstPart = -1;
        int secondPart = -1;
        
        for (int i = 0; i < _nums.Count; i++)
        {
            if (i + 1 >= _nums.Count) break;
            for (int j = i + 1; j < _nums.Count; j++)
            {
                if (_nums[i] + _nums[j] != _target) continue;
                firstPart = i;
                secondPart = j;
            }
        }

        return (firstPart, secondPart);
    }

    public (int, int) SolutionB()
    {
        int firstPart = -1;
        int secondPart = -1;
        
        for (int i = 0; i < _nums.Count; i++)
        {
            if (_nums[i] > _target) continue; 
            if (i + 1 >= _nums.Count) break;
            for (int j = i + 1; j < _nums.Count; j++)
            {
                if (_nums[i] + _nums[j] != _target) continue;
                firstPart = i;
                secondPart = j;
            }
        }

        return (firstPart, secondPart);
    }

    /* --- NOT MY SOLUTION ---
     * - even though it's O(n), requires more memory allocation and takes longer for smaller datasets
     */
    public (int, int)? SolutionC()
    {
        Dictionary<int, int> dict = new();
        for (int i = 0; i < _nums.Count; i++)
        {
            dict[_nums[i]] = i;
        }

        for (int i = 0; i < _nums.Count; i++)
        {
            int compliment = _target - _nums[i];
            if (dict.ContainsKey(compliment) && dict[compliment] != i)
            {
                return (i, dict[compliment]);
            }
        }

        return null;
    }
    
    public (int, int)? SolutionD()
    {
        Dictionary<int, int> dict = new();

        for (int i = 0; i < _nums.Count; i++)
        {
            int compliment = _target - _nums[i];
            if (dict.ContainsKey(compliment))
            {
                return (dict[compliment], i);
            }
            dict[_nums[i]] = i;
        }

        return null;
    }
    
    
    public (int, int)? SolutionE()
    {
        Hashtable dict = new();

        for (int i = 0; i < _nums.Count; i++)
        {
            int compliment = _target - _nums[i];
            if (dict.ContainsKey(compliment))
            {
                return ((int)dict[compliment], i);
            }
            dict[_nums[i]] = i;
        }

        return null;
    }
}
