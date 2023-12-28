using Utils;
using Utils.Strings;

namespace AoC.Y2021;

public static class Day6Solutions
{
    private static string _example = "3,4,3,1,2";
    public static double SolvePart1() => CommonSolution(80);
    public static double SolvePart2() => CommonSolution(256);

    private static double CommonSolution(int days)
    {
        var input = AocHandler.ImportHttp().Split(',').Clean().Select(x => x.ToInt()).ToList();
        var dict = new Dictionary<int, double>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
        };
        foreach (var n in input)
            dict[n]++;

        for (int _ = 1; _ <= days; _++)
        {
            var birthed = dict[0];
            dict[0] = dict[1];
            dict[1] = dict[2];
            dict[2] = dict[3];
            dict[3] = dict[4];
            dict[4] = dict[5];
            dict[5] = dict[6];
            dict[6] = dict[7] + birthed;
            dict[7] = dict[8];
            dict[8] = birthed;
        }

        return dict.Values.Sum();
    }
}