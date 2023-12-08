namespace Utils.Numbers;

public static class NumberHelpers
{
    public static int GetGCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static int GetLCM(int a, int b)
    {
        return (a / GetGCD(a, b)) * b;
    }

    public static int GetLCM(IEnumerable<int> nums)
    {
        if (nums.Count() == 0) throw new Exception("bad");
        if (nums.Count() == 1) return nums.First();
        return nums.Aggregate(nums.First(), (a, b) => GetLCM(a, b));
    }
    
    private static long GetGCDLong(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    private static long GetLCM(long a, long b)
    {
        return (a / GetGCDLong(a, b)) * b;
    }
    
    public static long GetLCMLong(IEnumerable<long> nums)
    {
        if (nums.Count() == 0) throw new Exception("bad");
        if (nums.Count() == 1) return nums.First();
        return nums.Aggregate(nums.First(), (a, b) => GetLCM(a, b));
    }
}