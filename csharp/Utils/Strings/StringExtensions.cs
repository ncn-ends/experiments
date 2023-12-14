using System.Text;
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

    public static List<string> SplitBy(this string s, IEnumerable<string> delimiters)
    {
        return new List<string>(s.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.Trim()).ToList();
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
        const string pattern = @"-?\d+";
        var matches = Regex.Matches(s, pattern);
        return matches.Select(x => (int.Parse(x.Value), x.Index)).ToList();
    }
    public static List<(long val, int pos)> ExtractLargeNumbers(this string s)
    {
        const string pattern = @"-?\d+";
        var matches = Regex.Matches(s, pattern);
        return matches.Select(x => (Convert.ToInt64(x.Value), x.Index)).ToList();
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

    /// <summary>
    /// Shouldn't be used in real code.
    /// </summary>
    public static int ToInt(this char s)
    {
        var parsed = Int32.TryParse(s.ToString(), out var num);
        if (!parsed) throw new Exception("Tried to parse number but failed.");
        return num;
    }

    public static bool IsInt(this string s)
    {
        var parsed = Int32.TryParse(s, out var num);
        return parsed;
    }

    public static bool IsInt(this char s)
    {
        var parsed = Int32.TryParse(s.ToString(), out var num);
        return parsed;
    }

    /* TODO: add tests for this */
    public static IEnumerable<int> GetAllIndexesOf(this string s, string target)
    {
        if (target.Length > s.Length) return Enumerable.Empty<int>();

        return Enumerable.Range(0, s.Length - target.Length + 1)
                         .Where(i => s.Substring(i, target.Length).Equals(target));
    }

    /// <summary>
    /// Extract the first item of possible items that appears in the string. Will default to the last item in the enumerable.
    /// </summary>
    public static string ExtractOutOf(this string str, IEnumerable<string> possible)
    {
        var cmd = possible.FirstOrDefault(s => str.Contains(s)) ?? possible.Last();
        return cmd;
    }

    public static string[][] ToStringGrid(this string str)
    {
        return str.SplitByLine().Select(x => x.Select(y => y.ToString()).ToArray()).ToArray();
    }

    public static int[][] ToIntMatrix(this string str)
    {
        return str.SplitByLine().Select(x => x.ExtractDigits().Select(x => x.val).ToArray()).ToArray();
    }

    public static List<string> SplitByChar(this string str)
    {
        var toReturn = str.Select(c => c.ToString()).ToList();

        return toReturn;
    }

    public static string WithModifiedChar(this string str,
                                          int index,
                                          char c)
    {
        var asd = str.ToCharArray();
        asd[index] = c;
        return new string(asd);

    }

    public static string WithModifiedChars(this string str,
                                           int startIndex,
                                           int endIndex,
                                           char c)
    {
        var sb = new StringBuilder(str);
        for (int i = startIndex; i < endIndex; i++)
        {
            sb[i] = c;
        }
        return sb.ToString();
    }
}