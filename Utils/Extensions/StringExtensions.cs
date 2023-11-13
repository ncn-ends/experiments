namespace Utils.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? s)
    {
        return s is null or "";
    }

    public static List<string> SplitByLine(this string s)
    {
        return s.Split("\n").Where(x => !x.IsNullOrEmpty()).ToList();
    }

    public static void IterateOnEachLine(this string s, Action<string> action)
    {
        var lines = s.SplitByLine();
        foreach (var line in lines)
            action(line);
    }

    public static List<string> SplitBySpace(this string s)
    {
        return s.Split(" ").ToList();
    }

    public static void IterateOnEachWord(this string s, Action<string, Action> action)
    {
        var end = false;
        var words = s.SplitBySpace();
        foreach (var word in words)
        {
            if (end) break;
            action(word, EndLoop);
        }

        void EndLoop()
        {
            end = true;
        }
    }

    public static string ToSorted(this string s)
    {
        var chars = s.ToCharArray().ToList();
        chars.Sort();
        return string.Join("", chars);
    }
}