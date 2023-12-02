using System.Diagnostics;
using Utils;
using Utils.Strings;

namespace AoC.Y2020;

public static class Day2Solutions
{
    private static readonly string _example = """
                                              1-3 a: abcde
                                              1-3 b: cdefg
                                              2-9 c: ccccccccc
                                              """;

    [Test][OutputTime]
    public static void DoPart1()
    {
        Assert.That(SolvePart1(_example), Is.EqualTo(2));

        var res = SolvePart1(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res);
    }

    public static int SolvePart1(string input)
    {
        var valid = 0;
        input.IterateOnEachLine((line, _) =>
        {
            var asd = line.SplitBy(["-", ":", " "]);
            var min = asd[0].ToInt();
            var max = asd[1].ToInt();
            var cha = asd[2][0];
            var pass = asd[3];

            var count = pass.Count(x => x == cha);
            if (min <= count && count <= max) valid++;
        });

        return valid;
    }

    [Test][OutputTime]
    public static void DoPart2()
    {
        Assert.That(SolvePart2(_example), Is.EqualTo(1));

        var res = SolvePart2(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res);
    }

    public static int SolvePart2(string input)
    {
        var valid = 0;
        input.IterateOnEachLine((line, _) =>
        {
            var asd = line.SplitBy(["-", ":", " "]);
            var min = asd[0].ToInt();
            var max = asd[1].ToInt();
            var cha = asd[2][0];
            var pass = asd[3];

            var first = pass[min - 1] == cha;
            var second = pass[max - 1] == cha;
            if ((first && !second) || (!first && second)) valid++;
        });
        return valid;
    }
}