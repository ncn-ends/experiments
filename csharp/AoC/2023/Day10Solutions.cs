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

        var example3 = """
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

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(4));
        Assert.That(DoPart1(example2), Is.EqualTo(8));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example3), Is.EqualTo(4));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static List<(int x, int y)> GetMainPipe(string input)
    {
        var matrix = input.ToWeightedGrid();

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

    private record struct NewGridNode(char val,
                                      Status status,
                                      bool inBetween);

    private static int DoPart2(string input)
    {
        var grid = input.ToWeightedGrid();
        var pipe = GetMainPipe(input);

        var newGrid = new List<List<NewGridNode>>();
        for (var y = 0; y < grid.Count; y++)
        {
            newGrid.Add([]);
            for (var x = 0; x < grid[y].Count; x++)
            {
                var status = Status.Unknown;
                var adjacentPipes = pipe
                                    .Where(pipeNode => pipeNode.x == x && pipeNode.y == y)
                                    .Select(pipeNode => (
                                                    x: pipeNode.x, y: pipeNode.y,
                                                    val: grid[pipeNode.y][pipeNode.x].val))
                                    .ToList();
                if (adjacentPipes.Any()) status = Status.Pipe;

                if (status == Status.Unknown)
                {
                    var hasPipeAdjacent = false;
                    grid.ToStringGrid().IterateAdjacentNodesSafely(x, y, (newX, newY) =>
                    {
                        if (pipe.Any(pipeNode => pipeNode.x == newX && pipeNode.y == newY)) hasPipeAdjacent = true;
                    });
                }

                var val = char.Parse(grid[y][x].val);
                newGrid[^1].Add(new NewGridNode(val, status, false));

                if (x < grid[y].Count - 1)
                {
                    newGrid[^1].Add(new NewGridNode('x', Status.Unknown, true));
                }
            }

            if (y < grid.Count - 1)
            {
                var betweener = Enumerable.Repeat(new NewGridNode('x', Status.Unknown, true), newGrid.Last().Count);
                newGrid.Add(betweener.ToList());
            }
        }

        /* loop through pipe filling marking unknowns as pipes when applicable */
        foreach (var pipeNodeCoords in pipe)
        {
            var (x, y) = pipeNodeCoords;
            var pipeNodeVal = grid[y][x];
            if (pipeNodeVal.val == "S") continue;
            var mods = pipeNodeVal.val.GetNodeMods();

            foreach (var mod in mods)
            {
                var newX = (x * 2) + mod.modX;
                var newY = (y * 2) + mod.modY;
                var newNode = newGrid[newY][newX];
                newGrid[newY][newX] = newNode with {status = Status.Pipe};
            }
        }

        /* starting from the top left, BFS through all nodes, stopping when reaching a pipe */
        var visited = new HashSet<(int x, int y)>();
        var q = new Queue<(int x, int y)>();
        for (int y = 0; y < newGrid.Count; y++)
        {
            var nodeFirst = newGrid[y][0];
            newGrid[y][0] = nodeFirst with
            {
                    status = nodeFirst.status == Status.Unknown
                            ? Status.Outside
                            : nodeFirst.status
            };
            var nodeLast = newGrid[y][newGrid[0].Count - 1];
            newGrid[y][newGrid[0].Count - 1] = nodeLast with
            {
                    status = nodeLast.status == Status.Unknown
                            ? Status.Outside
                            : nodeLast.status
            };
            q.Enqueue((0, y));
            q.Enqueue((newGrid[0].Count - 1, y));
        }

        for (int x = 0; x < newGrid.Count; x++)
        {
            var nodeFirst = newGrid[0][x];
            newGrid[0][x] = nodeFirst with
            {
                    status = nodeFirst.status == Status.Unknown
                            ? Status.Outside
                            : nodeFirst.status
            };
            var nodeLast = newGrid[^1][x];
            newGrid[^1][x] = nodeLast with
            {
                    status = nodeLast.status == Status.Unknown
                            ? Status.Outside
                            : nodeLast.status
            };
            q.Enqueue((x, 0));
            q.Enqueue((x, newGrid.Count - 1));
        }

        while (q.Any())
        {
            var (x, y) = q.Dequeue();
            var tile = newGrid[y][x];
            if (tile.status == Status.Pipe) continue;
            if (visited.Contains((x, y))) continue;

            visited.Add((x, y));
            newGrid[y][x] = newGrid[y][x] with {status = Status.Outside};

            newGrid.IterateAdjacentNodesSafely(x, y, (newX, newY) => { q.Enqueue((newX, newY)); });
        }

        /* find all the ones that were originally not outside */
        var total = 0;
        for (var y = 0; y < newGrid.Count; y++)
        {
            for (var x = 0; x < newGrid[y].Count; x++)
            {
                var node = newGrid[y][x];
                if (node.status == Status.Outside) continue;
                if (node.status == Status.Pipe) continue;
                if (node.inBetween) continue;
                newGrid[y][x] = newGrid[y][x] with {status = Status.Inside};
                total++;
            }
        }


        return total;
    }

    enum Status
    {
        Unknown,
        Pipe,
        Outside,
        Inside,
    }
}

public static class PipeMapper
{
    public static string[] SouthConnecting => ["|", "7", "F"];
    public static string[] NorthConnecting => ["|", "J", "L"];
    public static string[] EastConnecting => ["-", "L", "F"];
    public static string[] WestConnecting => ["-", "7", "J"];

    public static bool ConnectsToNorth(this string s) => NorthConnecting.Contains(s);
    public static bool ConnectsToSouth(this string s) => SouthConnecting.Contains(s);
    public static bool ConnectsToEast(this string s) => EastConnecting.Contains(s);
    public static bool ConnectsToWest(this string s) => WestConnecting.Contains(s);
    public static bool ConnectsToNorth(this char s) => NorthConnecting.Contains(s.ToString());
    public static bool ConnectsToSouth(this char s) => SouthConnecting.Contains(s.ToString());
    public static bool ConnectsToEast(this char s) => EastConnecting.Contains(s.ToString());
    public static bool ConnectsToWest(this char s) => WestConnecting.Contains(s.ToString());

    public static (int modX, int modY)[] GetNodeMods(this char c)
    {
        if (c == '-') return new[] {(-1, 0), (1, 0)};
        else if (c == '|') return new[] {(0, -1), (0, 1)};
        else if (c == '7') return new[] {(-1, 0), (0, 1)};
        else if (c == 'J') return new[] {(-1, 0), (0, -1)};
        else if (c == 'F') return new[] {(1, 0), (0, 1)};
        else if (c == 'L') return new[] {(1, 0), (0, -1)};
        return default;
    }

    public static (int modX, int modY)[] GetNodeMods(this string c)
    {
        return GetNodeMods(char.Parse(c));
    }
}

/*
 * visualize:
 *newGrid.Select(x => x.ToArray()).ToArray().ComplexVisualize((nodeCtx) =>
   {
       var x = nodeCtx.X;
       var y = nodeCtx.Y;
       var status = nodeCtx.OriginalData.status;
       nodeCtx.SetValue(nodeCtx.OriginalData.val);

       if (status == Status.Pipe) nodeCtx.SetBackgroundColor(ConsoleColor.Blue);
       if (status == Status.Outside) nodeCtx.SetBackgroundColor(ConsoleColor.DarkMagenta);
       if (status == Status.Unknown) nodeCtx.SetBackgroundColor(ConsoleColor.Gray);
       if (status == Status.Inside) nodeCtx.SetBackgroundColor(ConsoleColor.Red);

       if (nodeCtx.Val == '|') nodeCtx.SetValue("‖");
       else if (nodeCtx.Val == '7') nodeCtx.SetValue("\u2557");
       else if (nodeCtx.Val == 'J') nodeCtx.SetValue("\u255d");
       else if (nodeCtx.Val == 'F') nodeCtx.SetValue("\u2554");
       else if (nodeCtx.Val == 'L') nodeCtx.SetValue("\u255a");
       else if (nodeCtx.Val == '-') nodeCtx.SetValue("\u2550");
   });

 *
 */


/* for when working in Program.cs */
// public static (List<List<GridNode>> matrix, List<(int x, int y)> pipe) GetVisualizationMaterial()
// {
//     var inputA = AocHandler.ImportHttp();
//     var inputB = """
//                  ..........
//                  .S------7.
//                  .|F----7|.
//                  .||OOOO||.
//                  .||OOO0||.
//                  .|L-7F-J|.
//                  .|II||II|.
//                  .L--JL--J.
//                  ..........
//                  """;
//     var input = inputA;
//     var matrix = input.ToWeightedGrid();
//     var pipe = GetMainPipe(input);
//
//     return (matrix, pipe);
// }