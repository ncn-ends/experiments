namespace Utils;

public static class StringUtils
{
    public static char SafeGet(string s, int place)
    {
        if (s.Length - 1 < place) return default;
        return s[place];
    }

    /// <summary>
    /// Shouldn't be used in real code.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int ToInt(this string s)
    {
        var parsed = Int32.TryParse(s, out var num);
        if (!parsed) throw new Exception("Tried to parse number but failed.");
        return num;
    }
}