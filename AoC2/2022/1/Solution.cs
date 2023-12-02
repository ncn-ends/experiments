using AoC;
using Utils;

namespace AoC.Y2022;

public static class Solution
{
    private static string _input = AocHandler.ImportHttp();

    // 67622
    public static int DoPart1()
    {
        var currentMax = 0;

        foreach (var g in _input.Split("\n\n"))
        {
            int sum = 0;
            foreach (var l in g.Split("\n"))
            {
                if (l == "") continue;
                int parsed;
                var didParse = Int32.TryParse(l, out parsed);
                if (!didParse) throw new Exception("Something went wrong when trying to parse.");

                sum += parsed;
            }

            if (currentMax < sum) currentMax = sum;
        }

        return currentMax;
    }

    // 207576
    public static int DoPart2()
    {
        var maxElves = new List<int>{0, 0, 0};

        foreach (var s1 in _input.Split("\n\n"))
        {
            int sum = 0;
            foreach (var s2 in s1.Split("\n"))
            {
                if (s2 == "") continue;
                int parsed;
                var didParse = Int32.TryParse(s2, out parsed);
                if (!didParse) throw new Exception("Something went wrong when trying to parse.");

                sum += parsed;
            }

            var clonedSums = new List<int>(maxElves);
            clonedSums.Add(sum);
            clonedSums = new List<int>(clonedSums.OrderByDescending(i => i));
            maxElves = clonedSums.GetRange(0, 3);
        }

        return maxElves.Sum();
    }

    // convert group to sum -> sort array of sum of groups -> pluck top 1 or top 3

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}