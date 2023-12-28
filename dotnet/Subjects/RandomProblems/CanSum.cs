namespace Subjects.RandomProblems;

public class CanSum
{
    private Dictionary<int, bool> _memo = new();
    public static bool Do(int target, int[] numbers)
    {
        if (target == 0) return true;
        if (target < 0) return false;

        foreach (var number in numbers)
        {
            // call function
            var res = Do(target - number, numbers);
            if (res) return true;
        }

        return false;
    }
}