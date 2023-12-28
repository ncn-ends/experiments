using System.Diagnostics;
using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day18Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       R 6 (#70c710)
                       D 5 (#0dc571)
                       L 2 (#5713f0)
                       D 2 (#d2c081)
                       R 2 (#59c680)
                       D 2 (#411b91)
                       L 5 (#8ceee2)
                       U 2 (#caa173)
                       L 1 (#1b58a2)
                       U 2 (#caa171)
                       R 2 (#7807d2)
                       U 3 (#a77fa3)
                       L 2 (#015232)
                       U 2 (#7a21e3)
                       """;

        var example2 = """
                      
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(62));
        // 49868 too high
        // 3030 too low
        TestContext.Out.WriteLine(DoPart1(input));

        // Assert.That(DoPart2(example2), Is.EqualTo(0));
        // TestContext.Out.WriteLine(DoPart2(input));
    }

    public static int DoPart1(string input)
    {
        var x = 0;
        var y = 0;

        var visited = new HashSet<(int x, int y)>(); // y, visited x's
        input.IterateOnEachLine(line =>
        {
            var split = line.SplitBySpace();
            var dirMod = split[0].GetMovementModFromChar();
            var dist = split[1].ToInt();
            for (int i = 0; i < dist; i++)
            {
                visited.Add((x, y));
                x += dirMod.modX;
                y += dirMod.modY;
            }
        });
        /* get first point at the top line that is applicable */
        var minY = visited.MinBy(p => p.y).y;
        var p = visited.First(p => p.y == minY && visited.All(p2 => p2.y != minY + 1 || p2.x != p.x));

        var str = GridHelpers.ConstructGridFromPoints([..visited.ToList(), (p.x, p.y + 1)], '.', '#');
        Debugger.Break();

        var q = new Queue<(int x, int y)>();
        q.Enqueue((p.x, p.y + 1));
        while (q.Any())
        {
            // var str = GridHelpers.ConstructGridFromPoints(visited.ToList(), '.', '#');
            var c = q.Dequeue();

            if (!visited.Add(c)) continue;

            foreach (var dir in MovementHelpers.GetNonDiagnalMovements())
            {
                var next = (c.x + dir.modX, c.y + dir.modY);
                if (!q.Contains(next) && !visited.Contains(next)) q.Enqueue(next);
            }
        }


        return visited.Count;
    }

    // public static int DoPart1(string input)
    // {
    //     var dict = new Dictionary<int, HashSet<int>>(); // y, visited x's
    //     var x = 0;
    //     var y = 0;
    //
    //     input.IterateOnEachLine(line =>
    //     {
    //         var split = line.SplitBySpace();
    //         var dirMod = split[0].GetMovementModFromChar();
    //         var dist = split[1].ToInt();
    //         for (int i = 0; i < dist; i++)
    //         {
    //             if (!dict.TryAdd(y, [x]))
    //             {
    //                 dict[y].Add(x);
    //             }
    //             x += dirMod.modX;
    //             y += dirMod.modY;
    //         }
    //     });
    //     foreach (var (k, v) in dict)
    //     {
    //         var orderedXs = v.ToList().Order().ToList();
    //         /* trying to find the differences between each x, adding the in between ones to "visited" */
    //         for (var i = 0; i < orderedXs.Count - 1; i += 2)
    //         {
    //             var x1 = orderedXs[i];
    //             var x2 = orderedXs[i + 1];
    //             for (int newX = x1; newX < x2; newX++)
    //             {
    //                 dict[k].Add(newX);
    //             }
    //         }
    //     }
    //
    //     return dict.Values.Sum(set => set.Count);
    // }
    //
    // private static int DoPart1(string input)
    // {
    //     var dict = new Dictionary<int, (int minX, int maxX)>(); // y, and min/max x
    //
    //     var x = 0;
    //     var y = 0;
    //     input.IterateOnEachLine(line =>
    //     {
    //         var split = line.SplitBySpace();
    //         var dirMod = split[0].GetMovementModFromChar();
    //         var dist = split[1].ToInt();
    //         for (int i = 0; i < dist; i++)
    //         {
    //             if (!dict.TryAdd(y, (x, x)))
    //             {
    //                 if (dict[y].minX > x) dict[y] = (x, dict[y].maxX);
    //                 else if (dict[y].maxX < x) dict[y] = (dict[y].minX, x);
    //             }
    //             x += dirMod.modX;
    //             y += dirMod.modY;
    //         }
    //     });
    //
    //     var total = 0;
    //     foreach (var (minX, maxX) in dict.Values)
    //     {
    //         total += Math.Abs(maxX - minX) + 1;
    //     }
    //
    //     // var minPoints = dict.Select(d => (d.Value.minX, d.Key));
    //     // var maxPoints = dict.Select(d => (d.Value.maxX, d.Key));
    //     // List<(int x, int y)> points = [..minPoints, ..maxPoints];
    //     // var str = GridHelpers.ConstructGridFromPoints(points, '.', '#');
    //     // Debugger.Break();
    //
    //     return total;
    // }

    private static int DoPart2(string input)
    {
        return default;
    }
}