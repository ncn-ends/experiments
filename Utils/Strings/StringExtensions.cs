using System.Text.RegularExpressions;

namespace Utils.Strings;

public class LoopManager
{
    public int TotalLength { get; }
    public int CurrentPosition => CurrentIndex + 1;
    public int CurrentIndex { get; private set; }
    public LoopManager(int length)
    {
        TotalLength = length;
    }
    public bool ShouldEnd { get; private set; }
    public bool IsFinalIteration => CurrentPosition == TotalLength;
    public bool IsFirstIteration => CurrentIndex == 0;

    public void SetIndex(int i)
    {
        CurrentIndex = i;
    }

    public void StopLoop()
    {
        ShouldEnd = true;
    }
}

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? s) => s is null or "";

    public static List<string> Clean(this IEnumerable<string> str)
    {
        return str.Where(x => !x.IsNullOrEmpty()).ToList();
    }

    public static List<string> SplitBy(this string s, string[] delimiters)
    {
        return new List<string>(s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
    }

    public static List<string> SplitByLine(this string s) => s.Split("\n").Clean();

    public static List<string> ToListByLine(this string s) => SplitByLine(s);

    public static void IterateOnEachLine(this string s, Action<string, LoopManager> action)
    {
        var loopManager = new LoopManager(s.Length);
        var lines = s.SplitByLine();
        for (var i = 0; i < lines.Count; i++)
        {
            loopManager.SetIndex(i);
            var line = lines[i];
            if (loopManager.ShouldEnd) break;
            action(line, loopManager);
        }
    }

    public static List<string> SplitBySpace(this string s) => s.Split(" ").Clean();

    public static List<string> ToListBySpace(this string s) => SplitBySpace(s);

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

    public static List<int> ExtractNumbers(this string s)
    {
        const string pattern = @"\d+";
        var matches = Regex.Matches(s, pattern);
        return matches.Select(x => int.Parse(x.Value)).ToList();
    }

}