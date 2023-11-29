using System.Numerics;
namespace Utils.Numbers;

public static class NumberExtensions
{
    public static int GetDigitAt100thPlace(this int n)
    {
        var result = Math.Abs(n / 100) % 10;
        return result;
    }

    public static string ToBinary(this int n)
    {
        var bin = Convert.ToString(n, 2);
        return bin;
    }

    public static bool IsOdd(this int n)
    {
        return n % 2 != 0;
    }

    public static bool IsEven(this int n)
    {
        return n % 2 == 0;
    }
}