using Utils;

namespace Subjects.AoC._2022._8;



class Point
{
    public required bool IsChecked { get; set; }
    public required bool IsVisible { get; set; }
    public required int Value { get; init; }
    public int ScenicScore { get; set; } = -1;
}

public static class Day8Solution
{
    private static string _input = AocInputHandler.ImportFile().Trim();

    private static string _example = """
                                        30373
                                        25512
                                        65332
                                        33549
                                        35390
                                    """;

    private static char[][] _grid = _input.Trim().Split("\n").Select(x => x.Trim().ToCharArray()).ToArray();
    private static Point[][]  _treesChecked = _grid.Select(yLine => yLine.Select(pointValue => new Point
    {
        IsChecked = false,
        IsVisible = false,
        Value = int.Parse(pointValue.ToString())
    }).ToArray()).ToArray();

    private static bool CheckIsVisibleX(int point, int x, int y)
    {
        return _treesChecked[y][..x].All(s => point > s.Value)
               || _treesChecked[y][(x + 1)..].All(s => point > s.Value);
    }

    private static bool CheckIsVisibleY(int point, int x, int y)
    {
        var aboveIsVisible = true;
        for (int i = 0; i < y; i++)
        {
            if (point <= _treesChecked[i][x].Value) aboveIsVisible = false;
        }

        var belowIsVisible = true;
        for (int i = y + 1; i < _treesChecked[0].Length; i++)
        {
            if (point <= _treesChecked[i][x].Value) belowIsVisible = false;
        }


        return aboveIsVisible || belowIsVisible;
    }

    public static int DoPart1()
    {
        var count = 0;
        for (var y = 0; y < _treesChecked.Length; y++)
        {
            for (var x = 0; x < _treesChecked[y].Length; x++)
            {
                var tree = _treesChecked[y][x];
                _treesChecked[y][x].IsChecked = true;
                var isVisible = CheckIsVisibleX(tree.Value, x, y)
                                 || CheckIsVisibleY(tree.Value, x, y);
                if (!isVisible) continue;
                _treesChecked[y][x].IsVisible = true;
                count++;
            }
        }

        var asd = _treesChecked;
        return count;
    }

    private static int GetScenicScore(int x, int y)
    {
        var line = _treesChecked[y];
        var value = _treesChecked[y][x].Value;

        var leftCount = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            leftCount++;
            if (line[i].Value >= value) break;
        }

        var rightCount = 0;
        for (int i = x + 1; i < line.Length; i++)
        {
            rightCount++;
            if (line[i].Value >= value) break;
        }

        var upCount = 0;
        for (int i = y - 1; i >= 0; i--)
        {
            upCount++;
            if (_treesChecked[i][x].Value >= value) break;
        }

        var downCount = 0;
        for (int i = y + 1; i < _treesChecked.Length; i++)
        {
            downCount++;
            if (_treesChecked[i][x].Value >= value) break;
        }

        return upCount * rightCount * downCount * leftCount;
    }

    public static int DoPart2()
    {
        var maxScenicScore = 0;
        for (var y = 1; y < _treesChecked.Length - 1; y++)
        {
            for (var x = 1; x < _treesChecked[y].Length - 1; x++)
            {
                var point = _treesChecked[y][x];
                var asd = GetScenicScore(x, y);
                point.ScenicScore = asd;
                if (asd > maxScenicScore) maxScenicScore = asd;
                // Debugger.Break();
            }
        }

        return maxScenicScore;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}