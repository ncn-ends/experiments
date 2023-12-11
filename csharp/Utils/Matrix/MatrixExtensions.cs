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
                                     .Select((val, x) => new MatrixNode(val: val, xPos: x, yPos: y, visited: false, weight: int.MaxValue))
                                     .ToList())
                     .ToList();
        return matrix;
    }

    // public static List<List<T>> Map<T>(this List<List<T>> matrix, Func<int, int> action) where T : MatrixNode
    // {
    //     for (var y = 0; y < matrix.Count; y++)
    //     {
    //         for (var x = 0; x < matrix.Count; x++)
    //         {
    //
    //         }
    //     }
    //
    //     return newMatrix;
    // }
}