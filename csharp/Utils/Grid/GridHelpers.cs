namespace Utils.Matrix;

public static class GridHelpers
{
    public static string ConstructGridFromPoints(List<(int x, int y)> points,
                                                 char unmarkedNode,
                                                 char markedNode)
    {
        var minX = points.MinBy(n => n.x);
        var minY = points.MinBy(n => n.y);
        var maxX = points.MaxBy(n => n.x);
        var maxY = points.MaxBy(n => n.y);
        var gridLength = maxY.y - minY.y + 1;
        var rowLength = maxX.x - minX.x + 1;


        var str = "";
        for (int y = 0; y < gridLength; y++)
        {
            for (int x = 0; x < rowLength; x++)
            {
                if (points.Any(p => p.x == (x + minX.x) && p.y == (y + minY.y))) str += markedNode;
                else str                                       += unmarkedNode;
            }

            str += "\n";
        }

        return str;
    }
}