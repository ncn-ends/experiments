using Utils.Strings;


namespace Utils.Matrix;

public static class MatrixExtensions
{
    public static List<List<(string val, int xPos, int yPos, bool visited, int weight)>> ToWeightedMatrix(this string str)
    {
        var matrix = str.SplitByLine()
                     .Select((line, y) => line
                                     .SplitByChar()
                                     .Select((val, x) => (val: val, xPos: x, yPos: y, visited: false, weight: int.MaxValue))
                                     .ToList())
                     .ToList();
        return matrix;
    }
}