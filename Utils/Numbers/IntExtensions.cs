namespace Utils.Numbers;

public static class IntExtensions
{
    public static int GetDigitAt100thPlace(this int n)
    {
        var result = Math.Abs(n / 100) % 10;
        return result;
    }
}