using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2024;

public static class Day2Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       7 6 4 2 1
                       1 2 7 8 9
                       9 7 6 2 1
                       1 3 2 4 5
                       8 6 4 4 1
                       1 3 6 7 9
                       """;
        var expected1 = 2;

        // var example2 = """
        //                3   4
        //                4   3
        //                2   5
        //                1   3
        //                3   9
        //                3   3
        //                """;
        var expected2 = 4;


        // Assert.That(DoPart1(example1), Is.EqualTo(expected1));
        //
        var input = AocHandler.ImportHttp();

        //
        var res1 = DoPart1(input);
        TestContext.Out.WriteLine(res1);

        Assert.That(DoPart2(example1), Is.EqualTo(expected2));
        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
    }

    private static int DoPart1(string input)
    {
        // var safeCount = 0;
        var safes = new List<string>();

        input.IterateOnEachLine(line =>
        {
            var levels = line.ExtractNumbers();

            var increasing = levels[0].val < levels[1].val
                    ? 1
                    : -1;

            for (var i = 0; i < levels.Count - 1; i++)
            {
                var current = levels[i].val;
                var next = levels[i + 1].val;

                if (current == next) return;
                if ((current > next && increasing == 1) || (current < next && increasing == -1)) return;
                var diff = Math.Abs(current - next);
                if (diff is < 1 or > 3) return;
            }

            safes.Add(string.Join(" ", levels.Select(x => x.val)));
        });
        return safes.Count;
    }

    private static int DoPart2(string input)
    {
        // var safeCount = 0;
        var safes = new List<string>();
        var bads = new List<string>();

        input.IterateOnEachLine(line =>
        {
            var levels = line.ExtractNumbers().Select(x => x.val).ToList();

            var rowSafe = CheckRowSafety(levels, false);
            if (!rowSafe) rowSafe = CheckRowSafety(levels[1..], true);
            if (!rowSafe) rowSafe = CheckRowSafety([levels[0], ..levels[2..]], true);

            if (rowSafe) safes.Add(string.Join(" ", levels));
            else bads.Add(string.Join(" ", levels));
        });
        var badsPretty = string.Join("\n", bads);
        return safes.Count;
    }

    private static bool CheckLevelSafety(int a, int b, bool increasing)
    {
        if (a == b) return false;
        if (a > b && increasing) return false;
        if (a < b && !increasing) return false;
        var diff = Math.Abs(a - b);
        if (diff is < 1 or > 3) return false;

        return true;
    }

    private static bool CheckRowSafety(List<int> levels, bool deletionUsed)
    {

        var increasing = levels[0] < levels[1];

        var alreadyDeletedALevel = deletionUsed;

        for (var i = 0; i < levels.Count - 1; i++)
        {
            var current = levels[i];
            var next = levels[i + 1];

            var isSafe = CheckLevelSafety(current, next, increasing);
            if (isSafe) continue;

            if (alreadyDeletedALevel) return false;

            /* if a level hasn't been deleted yet, and is on the 2nd to last level, and it isn't safe, can just delete the last level to make it safe */
            if (i == levels.Count - 2) break;

            if (i >= levels.Count - 2) return false;

            var isSafeIfDeletedNextLevel = CheckLevelSafety(current, levels[i + 2], increasing);
            if (!isSafeIfDeletedNextLevel) return false;

            i++;
            alreadyDeletedALevel = true;
        }

        return true;
    }
}