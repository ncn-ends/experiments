using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day2Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                       Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                       Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                       Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                       Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                       Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example), Is.EqualTo(8));
        var res1 = DoPart1(input);
        TestContext.Out.WriteLine(res1);

        Assert.That(DoPart2(example), Is.EqualTo(2286));
        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
    }

    private static int DoPart1(string input)
    {
        var maxBlues = 14;
        var maxGreens = 13;
        var maxReds = 12;

        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var asd = line.SplitBy([":", ";"]);
            var gameId = asd[0].SplitBy([" "])[1].ToInt();
            var groups = asd[1..];
            var bad = false;
            foreach (var group in groups)
            {
                foreach (var ok in group.SplitBy([", "]))
                {
                    if (bad) break;
                    var num = ok.ExtractNumbers()[0].val;
                    if (ok.Contains("blue") && num > maxBlues) bad = true;
                    if (ok.Contains("green") && num > maxGreens) bad = true;
                    if (ok.Contains("red") && num > maxReds) bad = true;
                }
            }

            if (!bad) total += gameId;
        });
        return total;
    }

    private static int DoPart2(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var asd = line.SplitBy([":", ";"]);
            var gameId = asd[0].SplitBy([" "])[1].ToInt();
            var groups = asd[1..];
            var minBlues = int.MinValue;
            var minGreens = int.MinValue;
            var minReds = int.MinValue;
            foreach (var group in groups)
            {
                foreach (var ok in group.SplitBy([", "]))
                {
                    var num = ok.ExtractNumbers()[0].val;
                    if (ok.Contains("blue") && num > minBlues) minBlues = num;
                    if (ok.Contains("green") && num > minGreens) minGreens = num;
                    if (ok.Contains("red") && num > minReds) minReds = num;
                }
            }

            total += minBlues * minGreens * minReds;
        });
        return total;
    }
}