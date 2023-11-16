namespace Utils;

public static class StringUtils
{
    public static char SafeGet(string s, int place)
    {
        if (s.Length - 1 < place) return default;
        return s[place];
    }
}