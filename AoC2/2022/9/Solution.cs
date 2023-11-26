using AoC;
using Utils;

namespace Subjects.AoC._2022._9;


public static class Day9Solution
{
    private static string _input = AocInputHandler.ImportFile().Trim();

    private static string _example = """
    R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
""";

    private static string _exampleB = """
    R 4
    U 4
    L 3
    D 1
    R 4
    D 1
    L 5
    R 2
""";

    private static HashSet<(int, int)> _tVisited = new() {(0, 0)};
    private static (int x, int y) _headPos = (0, 0);
    private static (int x, int y) _tailPos = (0, 0);

    private static void ConsiderTailMovement()
    {
        if (Math.Abs(_tailPos.y - _headPos.y) < 2
            && Math.Abs(_tailPos.x - _headPos.x) < 2) return;

        if (_tailPos.x < _headPos.x) _tailPos.x++;
        if (_tailPos.x > _headPos.x) _tailPos.x--;
        if (_tailPos.y < _headPos.y) _tailPos.y++;
        if (_tailPos.y > _headPos.y) _tailPos.y--;

        _tVisited.Add((_tailPos.x, _tailPos.y));
    }

    public static int DoPart1()
    {
        foreach (var l in _input.Split("\n"))
        {
            if (l.Trim().Split(" ") is not [var dir, var distStr]) throw new Exception("Line had unexpected format");
            var dist = int.Parse(distStr);
            for (int i = 0; i < dist; i++)
            {
                if (dir == "U") _headPos.y++;
                if (dir == "R") _headPos.x++;
                if (dir == "D") _headPos.y--;
                if (dir == "L") _headPos.x--;
                ConsiderTailMovement();
            }
        }

        return _tVisited.Count;
    }


    private static HashSet<(int, int)> _tsVisited = new() {(0, 0)};
    private static (int x, int y)[] _ropePosition = new (int x, int y)[10]
    {
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0)
    };

    private static void ApplyRopeMovement()
    {
        for (int i = 1; i < _ropePosition.Length; i++)
        {
            var segment = _ropePosition[i];
            var leadSegment = _ropePosition[i - 1];

            if (Math.Abs(segment.y - leadSegment.y) < 2
                && Math.Abs(segment.x - leadSegment.x) < 2) continue;

            if (segment.x < leadSegment.x) _ropePosition[i].x++;
            if (segment.x > leadSegment.x) _ropePosition[i].x--;
            if (segment.y < leadSegment.y) _ropePosition[i].y++;
            if (segment.y > leadSegment.y) _ropePosition[i].y--;
        }

        _tsVisited.Add((_ropePosition.Last().x, _ropePosition.Last().y));
    }


    public static int DoPart2()
    {
        foreach (var l in _input.Split("\n"))
        {
            if (l.Trim().Split(" ") is not [var dir, var distStr]) throw new Exception("Line had unexpected format");
            var dist = int.Parse(distStr);
            for (int i = 0; i < dist; i++)
            {
                if (dir == "U") _ropePosition[0].y++;
                if (dir == "R") _ropePosition[0].x++;
                if (dir == "D") _ropePosition[0].y--;
                if (dir == "L") _ropePosition[0].x--;
                ApplyRopeMovement();
            }
        }

        return _tsVisited.Count;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}