namespace Subjects.LeetCode;

public class Solution2521
{
    public int DistinctPrimeFactors(int[] nums) {
        var set = new HashSet<int>();

        foreach (var t in nums)
        {
            var asd = FactorPrimes(t);
            foreach (var f in asd)
            {
                set.Add(f);
            }
        }

        return set.Count;
    }

    private IList<int> FactorPrimes(int num)
    {
        int n = 0;
        for (int i = 2; i <= num; i++)
        {
            if (num % i == 0)
            {
                n = i;
                break;
            }
        }

        if (n == 0)
        {
            return new List<int>();
        }

        var asd = FactorPrimes(num / n);
        asd.Add(n);
        return asd;
    }
}