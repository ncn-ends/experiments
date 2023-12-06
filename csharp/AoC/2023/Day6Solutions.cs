using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day6Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                       Time:      7  15   30
                       Distance:  9  40  200
                       """;


        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example), Is.EqualTo(288));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example), Is.EqualTo(71503));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var input2 = input.SplitByLine().Select(x => x.SplitBySpace().Skip(1).ToList()).ToList();
        var list = new List<(int time, int distance)>();
        for (var i = 0; i < input2[0].Count; i++)
        {
            list.Add((input2[0][i].ToInt(), input2[1][i].ToInt()));
        }

        var toReturn = 1;
        foreach (var pair in list)
        {
            var waysToWin = new List<int>(); // how much time held
            for (int i = 1; i < pair.time; i++)
            {
                var distTraveled = i * (pair.time - i);
                if (distTraveled > pair.distance) waysToWin.Add(i);
            }

            toReturn *= waysToWin.Count;
        }

        return toReturn;
    }

    private static int DoPart2(string input)
    {
        var input2 = input.SplitByLine().Select(x => string.Join("", x.SplitBySpace().Skip(1))).ToList();
        (long time, long distance) pair = (Convert.ToInt64(input2[0]), Convert.ToInt64(input2[1]));

        var waysToWin = new List<int>(); // how much time held
        for (int i = 1; i < pair.time; i++)
        {
            var distTraveled = i * (pair.time - i);
            if (distTraveled > pair.distance) waysToWin.Add(i);
        }

        return waysToWin.Count;
    }
}