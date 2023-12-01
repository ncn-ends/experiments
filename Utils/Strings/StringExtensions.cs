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
            var line = lines[i].Trim();
            if (loopManager.ShouldEnd) break;
            action(line, loopManager);
        }
    }
    public static void IterateOnEachLine(this string s, Action<string> action)
    {
        var lines = s.SplitByLine();
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i].Trim();
            action(line);
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
            action(word.Trim(), EndLoop);
        }

        void EndLoop()
        {
            end = true;
        }
    }
    public static void IterateOnEachWord(this string s, Action<string> action)
    {
        var words = s.SplitBySpace();
        foreach (var word in words)
            action(word.Trim());
    }

    public static void IterateOnEachCharacter(this string s, Action<char> action)
    {
        var chars = s.Trim().ToCharArray();
        foreach (var c in chars)
            action(c);
    }

    public static string ToSorted(this string s)
    {
        var chars = s.ToCharArray().ToList();
        chars.Sort();
        return string.Join("", chars);
    }

    /* TODO: add tests for this */
    public static List<(int val, int pos)> ExtractNumbers(this string s)
    {
        const string pattern = @"\d+";
        var matches = Regex.Matches(s, pattern);
        return matches.Select(x => (int.Parse(x.Value), x.Index)).ToList();
    }

    /* TODO: add tests for this */
    public static List<(int val, int pos)> ExtractDigits(this string s)
    {
        const string pattern = @"\d";
        var matches = Regex.Matches(s, pattern);
        return matches.Select(x => (int.Parse(x.Value), x.Index)).ToList();
    }

    /// <summary>
    /// Shouldn't be used in real code.
    /// </summary>
    public static int ToInt(this string s)
    {
        var parsed = Int32.TryParse(s, out var num);
        if (!parsed) throw new Exception("Tried to parse number but failed.");
        return num;
    }

    public static bool IsInt(this string s)
    {
        var parsed = Int32.TryParse(s, out var num);
        return parsed;
    }

    /* TODO: add tests for this */
    public static IEnumerable<int> GetAllIndexesOf(this string s, string target)
    {
        if (target.Length > s.Length) return Enumerable.Empty<int>();

        return Enumerable.Range(0, s.Length - target.Length + 1)
                         .Where(i => s.Substring(i, target.Length).Equals(target));
    }
}