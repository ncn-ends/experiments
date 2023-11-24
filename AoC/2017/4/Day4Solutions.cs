using System.Diagnostics;
using AoC;
using Utils.Extensions;
using Utils.Strings;

namespace AoC.Y2017;

public static class Day4Solutions
{
    public static int SolvePart1()
    {
        var input = AocInputHandler.ImportHttp();
        var validPassphrases = 0;
        input.IterateOnEachLine((line, _) =>
        {
            var usedWords = new HashSet<string>();
            var hasDuplicates = false;
            line.IterateOnEachWord((word, endLoop) =>
            {
                var added = usedWords.Add(word);
                if (added) return;
                hasDuplicates = true;
                endLoop();
            });

            if (!hasDuplicates) validPassphrases++;
        });

        return validPassphrases;
    }
    public static int SolvePart2()
    {
        var input = AocInputHandler.ImportHttp();
        var validPassphrases = 0;
        input.IterateOnEachLine((line, _) =>
        {
            var usedWords = new HashSet<string>();
            var hasDuplicates = false;
            line.IterateOnEachWord((word, endLoop) =>
            {
                var s = word.ToSorted();
                var added = usedWords.Add(s);
                if (added) return;
                hasDuplicates = true;
                endLoop();
            });

            if (!hasDuplicates) validPassphrases++;
        });

        return validPassphrases;
    }
}