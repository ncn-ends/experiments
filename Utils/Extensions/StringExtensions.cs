namespace Utils.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? s)
    {
        return s is null or "";
    }
}