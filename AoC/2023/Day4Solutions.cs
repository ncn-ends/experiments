using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day4Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                       Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                       Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                       Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                       Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                       Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                       Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example), Is.EqualTo(13));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example), Is.EqualTo(30));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var toReturn = 0;
        input.IterateOnEachLine(line =>
        {
            var groups = line.SplitBy([" | "]);
            var winners = groups[0].ExtractNumbers().Skip(1).Select(x => x.val);
            var held = groups[1].ExtractNumbers().Select(x => x.val);
            var toAdd = held.Aggregate(0, (total, val) =>
            {
                if (!winners.Contains(val)) return total;
                if (total == 0) return 1;
                return total * 2;
            });
            toReturn += toAdd;
        });
        return toReturn;
    }

    private static int DoPart2(string input)
    {
        var extras = new Dictionary<int, int>(); // card key, extras to account for
        input.IterateOnEachLine(line =>
        {
            var groups = line.SplitBy([" | "]);
            var firstGroup = groups[0].ExtractNumbers();
            var card = firstGroup[0].val;
            if (extras.ContainsKey(card)) extras[card] += 1;
            else
            {
                extras[card] = 1;
            }

            var multiplier = extras[card];
            var winners = firstGroup.Skip(1).Select(x => x.val);
            var held = groups[1].ExtractNumbers().Select(x => x.val);
            var nOfMatches = held.Count(x => winners.Contains(x));
            for (int i = 0; i < nOfMatches; i++)
            {
                if (extras.ContainsKey(card + i + 1)) extras[card + i + 1] += 1 * multiplier;
                else
                {
                    extras[card + i + 1] = 1 * multiplier;
                }
            }
        });
        return extras.Sum(x => x.Value);
    }
}