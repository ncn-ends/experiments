using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day11Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       ...#......
                       .......#..
                       #.........
                       ..........
                       ......#...
                       .#........
                       .........#
                       ..........
                       .......#..
                       #...#.....
                       """;

        var example2 = """

                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(374));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example1, 10), Is.EqualTo(1030));
        Assert.That(DoPart2(example1, 100), Is.EqualTo(8410));
        TestContext.Out.WriteLine(DoPart2(input, 1000000));
    }

    private static int DoPart1(string input)
    {
        var grid = input.ToStringGrid();
        var galaxies = new HashSet<(int x, int y)>();
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == "#") galaxies.Add((x, y));
            }
        }

        var expandedYs = new HashSet<int>();
        var expandedXs = new HashSet<int>();

        for (int i = 0; i < grid[0].Length; i++)
        {
            if (galaxies.Any(g => g.x == i)) continue;
            expandedXs.Add(i);
        }

        for (int i = 0; i < grid.Length; i++)
        {
            if (galaxies.Any(g => g.y == i)) continue;
            expandedYs.Add(i);
        }

        var dict = new Dictionary<(int x1, int y1, int x2, int y2), int>();

        foreach (var homeGalaxy in galaxies)
        {
            var q = new Queue<(int x, int y, int totalCost)>();
            var visited = new HashSet<(int x, int y)>();
            q.Enqueue((homeGalaxy.x, homeGalaxy.y, 0));
            while (q.Any())
            {
                var c = q.Dequeue();
                if (!visited.Add((c.x, c.y))) continue;
                if (grid[c.y][c.x] == "#")
                {
                    var targetGalaxy = (x: c.x, y: c.y);
                    if (!dict.ContainsKey((homeGalaxy.x, homeGalaxy.y, targetGalaxy.x, targetGalaxy.y)) &&
                        !dict.ContainsKey((targetGalaxy.x, targetGalaxy.y, homeGalaxy.x, homeGalaxy.y)))
                    {
                        dict.Add((homeGalaxy.x, homeGalaxy.y, c.x, c.y), c.totalCost);
                    }
                }

                foreach (var dir in MovementHelpers.GetNonDiagnalMovements())
                {
                    var cost = c.totalCost;
                    var next = (x: c.x + dir.modX, y: c.y + dir.modY);
                    if (visited.Contains(next)) continue;
                    if (!IsValidForGrid(next.x, next.y)) continue;
                    if (expandedXs.Contains(next.x)) cost += 1;
                    if (expandedYs.Contains(next.y)) cost += 1;
                    cost += 1;
                    q.Enqueue((next.x, next.y, cost));
                }
            }
        }

        return dict.Sum(x => x.Value);

        bool IsValidForGrid(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x > grid[0].Length - 1 || y > grid.Length - 1) return false;
            return true;
        }
    }

    private static long DoPart2(string input, int scale)
    {
        var grid = input.ToStringGrid();
        var galaxies = new HashSet<(int x, int y)>();
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == "#") galaxies.Add((x, y));
            }
        }

        var expandedYs = new HashSet<int>();
        var expandedXs = new HashSet<int>();

        for (int i = 0; i < grid[0].Length; i++)
        {
            if (galaxies.Any(g => g.x == i)) continue;
            expandedXs.Add(i);
        }

        for (int i = 0; i < grid.Length; i++)
        {
            if (galaxies.Any(g => g.y == i)) continue;
            expandedYs.Add(i);
        }

        var dict = new Dictionary<(int x1, int y1, int x2, int y2), long>();

        foreach (var homeGalaxy in galaxies)
        {
            var q = new Queue<(int x, int y, long totalCost)>();
            var visited = new HashSet<(int x, int y)>();
            q.Enqueue((homeGalaxy.x, homeGalaxy.y, 0));
            while (q.Any())
            {
                var c = q.Dequeue();
                if (!visited.Add((c.x, c.y))) continue;
                if (grid[c.y][c.x] == "#")
                {
                    var targetGalaxy = (x: c.x, y: c.y);
                    if (!dict.ContainsKey((homeGalaxy.x, homeGalaxy.y, targetGalaxy.x, targetGalaxy.y)) &&
                        !dict.ContainsKey((targetGalaxy.x, targetGalaxy.y, homeGalaxy.x, homeGalaxy.y)))
                    {
                        dict.Add((homeGalaxy.x, homeGalaxy.y, c.x, c.y), c.totalCost);
                    }
                }

                foreach (var dir in MovementHelpers.GetNonDiagnalMovements())
                {
                    var cost = c.totalCost;
                    var next = (x: c.x + dir.modX, y: c.y + dir.modY);
                    if (visited.Contains(next)) continue;
                    if (!IsValidForGrid(next.x, next.y)) continue;
                    if (expandedXs.Contains(next.x)) cost += scale - 1;
                    if (expandedYs.Contains(next.y)) cost += scale - 1;
                    cost += 1;
                    q.Enqueue((next.x, next.y, cost));
                }
            }
        }

        return dict.Sum(x => x.Value);

        bool IsValidForGrid(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x > grid[0].Length - 1 || y > grid.Length - 1) return false;
            return true;
        }
    }
}