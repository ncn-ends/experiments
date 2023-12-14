using Utils.Strings;


namespace Utils.Matrix;

public record MatrixNode(string val,
                         int xPos,
                         int yPos,
                         bool visited,
                         int weight);

public static class MatrixExtensions
{
    public static List<List<MatrixNode>> ToWeightedMatrix(this string str)
    {
        var matrix = str.SplitByLine()
                        .Select((line, y) => line
                                             .SplitByChar()
                                             .Select((val, x) => new MatrixNode(
                                                             val: val, xPos: x, yPos: y, visited: false,
                                                             weight: int.MaxValue))
                                             .ToList())
                        .ToList();
        return matrix;
    }

    public static string ToVisualizedString(this string[][] grid)
    {
        return string.Join("\n", grid.Select(x => string.Join("", x)).ToArray());
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
}