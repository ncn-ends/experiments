using System.Numerics;


namespace Utils.Numbers;

public static class NumberHelpers
{
    public static T GetGCD<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static T GetLCM<T>(T a, T b) where T : INumber<T>
    {
        return (a / GetGCD(a, b)) * b;
    }

    public static T GetLCM<T>(IEnumerable<T> nums) where T : INumber<T>
    {
        if (nums.Count() == 0) throw new Exception("bad");
        if (nums.Count() == 1) return nums.First();
        return nums.Aggregate(nums.First(), GetLCM);
    }
}