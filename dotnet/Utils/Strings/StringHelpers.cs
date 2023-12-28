namespace Utils;

public static class StringHelpers
{
    public static char SafeGet(string s, int place)
    {
        if (s.Length - 1 < place) return default;
        return s[place];
    }

    public static int MatchDistance(string a, string b)
    {
        var shorter = a.Length < b.Length
                ? a
                : b;

        var count = 0;
        for (var i = 0; i < shorter.Length; i++)
        {
            if (a[i] != b[i]) count++;
        }

        count += Math.Abs(a.Length - b.Length);
        return count;
    }

}