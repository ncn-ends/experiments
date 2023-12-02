using AoC;
using Utils;

namespace AoC.Y2022;

public static class Day3Solution
{
    private static string _input = AocHandler.ImportHttp();

    static int GetValueFromChar(char c)
    {
        // a -> 97
        // z -> 122
        // A -> 65
        // Z -> 90
        int code = c;
        if (code is >= 65 and <= 90) return code - 64 + 26;
        if (code is >= 97 and <= 122) return code - 96;
        throw new Exception("Bad argument");
    }

    static (string, string) SplitStringInHalf(string s)
    {
        var first = s[..(s.Length / 2)];
        var last = s.Substring(s.Length / 2, s.Length / 2);
        return (first, last);
    }

    public static int DoPart1()
    {
        var sum = 0;

        // optimization: sort the lines so that you loop through the shortest one

        foreach (var l in _input.Split("\n"))
        {
            var (firstHalf, secondHalf) = SplitStringInHalf(l);
            for (var i = 0; i < firstHalf.Length; i++)
            {
                char c = firstHalf[i];
                if (!secondHalf.Contains(c)) continue;
                sum += GetValueFromChar(c);
                break;
            }
        }

        return sum;
    }

    public static int DoPart2()
    {
        var lines = _input.Split("\n");
        var sum = 0;
        for (int i = 0; i < lines.Length; i += 3)
        {
            var line1 = lines[i + 0];
            var line2 = lines[i + 1];
            var line3 = lines[i + 2];

            // optimization: sort the lines so that you loop through the shortest one

            for (int k = 0; k < line1.Length; k++)
            {
                char c = line1[k];
                bool existsInAllThreeLines = line2.Contains(c) && line3.Contains(c);
                if (!existsInAllThreeLines) continue;
                sum += GetValueFromChar(c);
                break;
            }
        }

        return sum;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}