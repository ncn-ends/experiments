using System.Diagnostics;
using Utils;
using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day13Solution
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       #.##..##.
                       ..#.##.#.
                       ##......#
                       ##......#
                       ..#.##.#.
                       ..##..##.
                       #.#.##.#.

                       #...##..#
                       #....#..#
                       ..##..###
                       #####.##.
                       #####.##.
                       ..##..###
                       #....#..#
                       """;

        var input = AocHandler.ImportHttp();

        // Assert.That(DoPart1(example1), Is.EqualTo(405));
        // TestContext.Out.WriteLine(DoPart1(input));

        // 32796 too low
        // Assert.That(DoPart2(example1), Is.EqualTo(400));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var groups = input.SplitBy(["\n\n"]);

        var totalLinesHorizontal = 0;
        var totalLinesVertical = 0;
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            var groupLines = group.SplitByLine();
            var (hasReflectionHorizontal, nA) = IsGroupReflection1(groupLines);

            if (hasReflectionHorizontal)
            {
                totalLinesHorizontal += nA;
                continue;
            }

            var grid = group.ToStringGrid();
            var transposedGrid = grid.Transpose();
            var transposeGroupedLines = transposedGrid.Select(x => string.Join("", x)).ToList();
            var (hasReflectionVertical, nB) = IsGroupReflection1(transposeGroupedLines);
            if (hasReflectionVertical)
            {
                totalLinesVertical += nB;
                continue;
            }

            Debugger.Break();
        }

        return totalLinesHorizontal * 100 + totalLinesVertical;
    }

    private static (bool isReflection, int topIndex) IsGroupReflection1(List<string> groupLines)
    {
        for (var i = 1; i < groupLines.Count; i++)
        {
            var diff = 1;
            var isReflection = true;
            while (i - diff >= 0 && i + diff - 1 <= groupLines.Count - 1)
            {
                var top = groupLines[i - diff];
                var bottom = groupLines[i + diff - 1];
                if (top != bottom)
                {
                    isReflection = false;
                    break;
                }

                diff++;
            }

            if (isReflection) return (true, i);
        }

        return default;
    }
    private static (bool isReflection, int topIndex, bool usedSmudge) IsGroupReflection2(List<string> groupLines)
    {
        for (var i = 1; i < groupLines.Count; i++)
        {
            var diff = 1;
            var isReflection = true;
            var usedSmudge = false;
            while (i - diff >= 0 && i + diff - 1 <= groupLines.Count - 1)
            {
                var top = groupLines[i - diff];
                var bottom = groupLines[i + diff - 1];
                var charactersUnaligned = StringHelpers.MatchDistance(top, bottom);
                if (!usedSmudge && charactersUnaligned == 1)
                {
                    usedSmudge = true;
                    diff++;
                    continue;
                }
                if (top != bottom)
                {
                    isReflection = false;
                    break;
                }

                diff++;
            }

            if (isReflection) return (true, i, usedSmudge);
        }

        return default;
    }

    private static int DoPart2(string input)
    {
        var groups = input.SplitBy(["\n\n"]);

        var totalLinesHorizontal = 0;
        var totalLinesVertical = 0;
        // regular debugging start at i == 18
        // usedsmudge debugging start after 30
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            var groupLines = group.SplitByLine();
            var (hasReflectionHorizontal, nA, usedSmudgeA) = IsGroupReflection2(groupLines);

            if (hasReflectionHorizontal)
            {
                totalLinesHorizontal += nA;
                continue;
            }

            var grid = group.ToStringGrid();
            var transposedGrid = grid.Transpose();
            var transposeGroupedLines = transposedGrid.Select(x => string.Join("", x)).ToList();
            var (hasReflectionVertical, nB, usedSmudgeB) = IsGroupReflection2(transposeGroupedLines);
            if (hasReflectionVertical)
            {
                totalLinesVertical += nB;
                continue;
            }

            Debugger.Break();
        }

        return totalLinesHorizontal * 100 + totalLinesVertical;
    }
}