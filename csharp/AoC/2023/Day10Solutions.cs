using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day10Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       .....
                       .S-7.
                       .|.|.
                       .L-J.
                       .....
                       """;

        var example2 = """
                       ..F7.
                       .FJ|.
                       SJ.L7
                       |F--J
                       LJ...
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(4));
        Assert.That(DoPart1(example2), Is.EqualTo(8));
        TestContext.Out.WriteLine(DoPart1(input));

        // Assert.That(DoPart2(example2), Is.EqualTo(0));
        // TestContext.Out.WriteLine(DoPart2(input));
    }

    private static List<(int x, int y)> GetMainPipe(string input)
    {
        var matrix = input.ToWeightedMatrix();

        /* find S */
        var s = (x: 0, y: 0);
        for (var y = 0; y < matrix.Count; y++)
        {
            for (var x = 0; x < matrix[y].Count; x++)
            {
                if (matrix[y][x].val != "S") continue;
                s.x = x;
                s.y = y;
                break;
            }
        }

        /* get the main pipe */
        var stack = new Stack<(int x, int y)>();
        var visited = new HashSet<(int x, int y)>();
        visited.Add(s);

        if (s.x > 0 && matrix[s.y][s.x - 1].val is "F" or "L" or "-") stack.Push((s.x - 1, s.y));
        else if (s.x < matrix[0].Count - 1 && matrix[s.y][s.x + 1].val is "J" or "7" or "-") stack.Push((s.x + 1, s.y));
        else if (s.y > 0 && matrix[s.y - 1][s.x].val is "|" or "7" or "F") stack.Push((s.x, s.y - 1));
        else if (s.y < matrix.Count - 1 && matrix[s.y + 1][s.x].val is "|" or "J" or "L") stack.Push((s.x, s.y + 1));

        while (stack.Any())
        {
            var c = stack.Pop();
            if (!visited.Add(c)) continue;

            if (c.x > 0 && matrix[c.y][c.x].val is "-" or "J" or "7" && !visited.Contains((c.x - 1, c.y)))
                stack.Push((c.x - 1, c.y));
            else if (c.x < matrix[0].Count - 1 && matrix[c.y][c.x].val is "-" or "L" or "F" &&
                     !visited.Contains((c.x + 1, c.y))) stack.Push((c.x + 1, c.y));
            else if (c.y > 0 && matrix[c.y][c.x].val is "|" or "L" or "J" && !visited.Contains((c.x, c.y - 1)))
                stack.Push((c.x, c.y - 1));
            else if (c.y < matrix.Count - 1 && matrix[c.y][c.x].val is "|" or "7" or "F" &&
                     !visited.Contains((c.x, c.y + 1))) stack.Push((c.x, c.y + 1));
        }

        return visited.ToList();
    }

    private static int DoPart1(string input)
    {
        var pipe = GetMainPipe(input);

        return (int) Math.Ceiling((decimal) (pipe.Count / 2));
    }

    public static (List<List<MatrixNode>> matrix, List<(int x, int y)> pipe) GetVisualizationMaterial()
    {
        var inputA = AocHandler.ImportHttp();
        var inputB = """
                    ..........
                    .S------7.
                    .|F----7|.
                    .||OOOO||.
                    .||OOO0||.
                    .|L-7F-J|.
                    .|II||II|.
                    .L--JL--J.
                    ..........
                    """;
        var input = inputA;
        var matrix = input.ToWeightedMatrix();
        var pipe = GetMainPipe(input);

        return (matrix, pipe);
    }

    private static int DoPart2(string input)
    {
        var matrix = input.ToWeightedMatrix();
        var pipe = GetMainPipe(input);

        return default;
    }
}