namespace Subjects.Algorithms;

public class Quicksort
{
    public static int[] SolutionA(int[] nums)
    {
        if (nums.Length == 1) return nums;
        
        int pivot = 0;
        int a = pivot + 1;
        int b = nums.Length - 1;

        // var bClause = nums[b] > nums[pivot] && b > pivot;
        // var aClause = nums[a] < nums[pivot] && a < nums.Length - 1;

        while (b > a)
        {
            while (nums[b] > nums[pivot] && b > pivot) b--;
            while (nums[a] < nums[pivot] && a < nums.Length - 1) a++;

            if (a > b) break;
            
            if (nums[a] > nums[pivot] && nums[b] < nums[pivot])
            {
                (nums[a], nums[b]) = (nums[b], nums[a]);
            }
        }

        if (nums[b] < nums[pivot])
        {
            (nums[b], nums[pivot]) = (nums[pivot], nums[b]);
        }
        
        // 123, 94, 6110, 5, 13, 335
        // (0, b-1)
        // (b)
        var bottomPartitionInput = nums.Take(b).ToArray();
        var topPartitionInput = nums.Skip(b).ToArray();

        var bottomPartitionSorted = SolutionA(bottomPartitionInput);
        var topPartitionSorted = SolutionA(topPartitionInput);
        return bottomPartitionSorted.Concat(topPartitionSorted).ToArray();
    }
}