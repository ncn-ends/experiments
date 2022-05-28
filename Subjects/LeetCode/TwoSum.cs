namespace Subjects.LeetCode;
public class TwoSum
{
    private readonly List<int> _nums;
    private readonly int _target;
    public TwoSum(List<int> nums, int target)
    {
        _nums = nums;
        _target = target;
    }
    
    public (int, int) SolutionA()
    {
        int firstPart = -1;
        int secondPart = -1;
        
        for (int i = 0; i < _nums.Count; i++)
        {
            if (i + 1 >= _nums.Count) break;
            for (int j = i + 1; j < _nums.Count; j++)
            {
                if (_nums[i] + _nums[j] == _target)
                {
                    firstPart = i;
                    secondPart = j;
                }
            }
        }

        return (firstPart, secondPart);
    }
}
