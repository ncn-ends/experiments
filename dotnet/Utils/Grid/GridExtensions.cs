using Utils.Strings;


namespace Utils.Matrix;

public class GridVisualizerNodeContext<T>
{
    public required int X { get; init; }
    public required int Y { get; init; }
    public char Val { get; private set; }
    public required T OriginalData { get; init; }
    public ConsoleColor? BgColor { get; private set; }
    public ConsoleColor? FgColor { get; private set; }


    public void SetBackgroundColor(ConsoleColor color)
    {
        BgColor = color;
    }

    public void SetForegroundColor(ConsoleColor color)
    {
        FgColor = color;
    }

    public void SetValue(char c)
    {
        Val = c;
    }

    public void SetValue(string c)
    {
        Val = char.Parse(c);
    }
}

public record GridNode(string val,
                       int xPos,
                       int yPos,
                       bool visited,
                       int weight);

public static class GridExtensions
{
    public static List<List<GridNode>> ToWeightedGrid(this string str)
    {
        var matrix = str.SplitByLine()
                        .Select((line, y) => line
                                             .SplitByChar()
                                             .Select((val, x) => new GridNode(
                                                             val: val, xPos: x, yPos: y, visited: false,
                                                             weight: int.MaxValue))
                                             .ToList())
                        .ToList();
        return matrix;
    }

    public static string[][] ToStringGrid(this List<List<GridNode>> grid)
    {
        return grid.Select(x => x.Select(y => y.val).ToArray()).ToArray();
    }

    public static string[][] ToStringGrid<T>(this List<List<T>> grid, Func<T, string> getVal)
    {
        return grid.Select(x => x.Select(getVal).ToArray()).ToArray();
    }

    public static string ToVisualizedString(this string[][] grid)
    {
        return string.Join("\n", grid.Select(x => string.Join("", x)).ToArray());
    }

    /* example:
     var asArray = matrix.Select(x => x.Select(y => y.val).ToArray()).ToArray();
       asArray.ComplexVisualize((nodeCtx) =>
       {
           var x = nodeCtx.X;
           var y = nodeCtx.Y;
           if (nodeStatusMatrix.TryGetValue((x, y), out var status))
           {
               if (status == Status.Pipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkCyan);
               if (status == Status.PipeTouchingOutside) nodeCtx.SetBackgroundColor(ConsoleColor.DarkBlue);
               if (status == Status.Outside) nodeCtx.SetBackgroundColor(ConsoleColor.Magenta);
               if (status == Status.OutsideTouchingPipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkMagenta);
               if (status == Status.Unknown) nodeCtx.SetBackgroundColor(ConsoleColor.Red);
               if (status == Status.In) nodeCtx.SetBackgroundColor(ConsoleColor.Green);
               if (status == Status.PathFromOutside) nodeCtx.SetBackgroundColor(ConsoleColor.Yellow);
               if (status == Status.PreviouslyUnknownNowConsideredOutSide) nodeCtx.SetBackgroundColor(ConsoleColor.Gray);
           }


           if (nodeCtx.Val == '|') nodeCtx.SetAlias("‖");
           else if (nodeCtx.Val == '7') nodeCtx.SetAlias("\u2557");
           else if (nodeCtx.Val == 'J') nodeCtx.SetAlias("\u255d");
           else if (nodeCtx.Val == 'F') nodeCtx.SetAlias("\u2554");
           else if (nodeCtx.Val == 'L') nodeCtx.SetAlias("\u255a");
           else if (nodeCtx.Val == '-') nodeCtx.SetAlias("\u2550");
       });
     */
    public static void ComplexVisualize<T>(this T[][] grid,
                                           Action<GridVisualizerNodeContext<T>> applyVisualizerContext)
    {
        var yAxisDisplayWidth = grid.Count().ToString().Length + 1;
        PrintXAxisNumbers(yAxisDisplayWidth);

        /* print contents visualized and print y axis numbers */
        for (var y = 0; y < grid.Count(); y++)
        {
            Console.Write("{0," + yAxisDisplayWidth + "} ", y);
            for (var x = 0; x < grid[y].Length; x++)
            {
                var node = grid[y][x];
                var visualizerNodeContext = new GridVisualizerNodeContext<T>()
                {
                        X            = x,
                        Y            = y,
                        OriginalData = node
                };
                applyVisualizerContext(visualizerNodeContext);

                if (visualizerNodeContext.BgColor is not null)
                    Console.BackgroundColor = visualizerNodeContext.BgColor.Value;
                if (visualizerNodeContext.FgColor is not null)
                    Console.ForegroundColor = visualizerNodeContext.FgColor.Value;

                var charToVisualize = visualizerNodeContext.Val;
                Console.Write(charToVisualize.ToString());
                Console.ResetColor();
            }

            Console.Write("{0," + yAxisDisplayWidth + "} ", y);

            Console.WriteLine();
        }

        PrintXAxisNumbers(yAxisDisplayWidth);

        void PrintXAxisNumbers(int yAxisDisplayWidth)
        {
            var xAxisDisplayWidth = grid[0].Length.ToString().Length;

            for (int digitIndex = 0; digitIndex < xAxisDisplayWidth; digitIndex++)
            {
                Console.Write("{0," + (yAxisDisplayWidth + 1) + "}", " ");

                for (int x = 0; x < grid[0].Length; x++)
                {
                    var numStr = x.ToString().PadLeft(xAxisDisplayWidth, ' ');

                    var toPrint = digitIndex < numStr.Length
                            ? numStr[digitIndex].ToString()
                            : " ";
                    Console.Write(toPrint);
                }

                Console.WriteLine();
            }
        }
    }

    public static string ToSimpleString(this string[][] grid)
    {
        return string.Join("", grid.Select(x => string.Join("", x)).ToArray());
    }

    public static string[][] Transpose(this string[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        string[][] transposed = new string[cols][];

        for (int i = 0; i < cols; i++)
        {
            transposed[i] = new string[rows];
            for (int j = 0; j < rows; j++)
            {
                transposed[i][j] = grid[j][i];
            }
        }

        return transposed;
    }

    private static void IterateNeighborsSafely<T>((int modX, int modY)[] movements,
                                                  T[][] grid,
                                                  int x,
                                                  int y,
                                                  Action<int, int> action)
    {
        foreach (var dir in movements)
        {
            var newX = x + dir.modX;
            var newY = y + dir.modY;
            if (newX < 0 || newY < 0) continue;
            if (newY > grid.Length - 1 || newX > grid[0].Length - 1) continue;
            action(newX, newY);
        }
    }

    public static void IterateAdjacentNodesSafely<T>(this T[][] grid,
                                                     int x,
                                                     int y,
                                                     Action<int, int> action)
    {
        IterateNeighborsSafely(MovementHelpers.GetAdjacentMovements(), grid, x, y, action);
    }

    private static void IterateNeighborsSafely<T>((int modX, int modY)[] movements,
                                                  List<List<T>> grid,
                                                  int x,
                                                  int y,
                                                  Action<int, int> action)
    {
        foreach (var dir in movements)
        {
            var newX = x + dir.modX;
            var newY = y + dir.modY;
            if (newX < 0 || newY < 0) continue;
            if (newY > grid.Count - 1 || newX > grid[0].Count - 1) continue;
            action(newX, newY);
        }
    }

    public static void IterateAdjacentNodesSafely<T>(this List<List<T>> grid,
                                                     int x,
                                                     int y,
                                                     Action<int, int> action)
    {
        IterateNeighborsSafely(MovementHelpers.GetAdjacentMovements(), grid, x, y, action);
    }
}