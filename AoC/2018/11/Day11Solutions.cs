using Utils;
using Utils.Numbers;

namespace AoC.Y2018;

public class Day11Solutions
{
    private static int _gridSize = 5;
    private static int _squareSize = 3;
    private static string _example = "18";
    private static int _serialNumber = _example.ToInt();
    public static int SolvePart1()
    {
        (int total, int x, int y) max = (0, -1, -1);

        for (int y = 1; y < _gridSize - _squareSize + 1; y++)
        {
            for (int x = 1; x < _gridSize - _squareSize + 1; x++)
            {
                var total = MemoizedGetPowerLevel(x, y)
                            + MemoizedGetPowerLevel(x + 1, y)
                            + MemoizedGetPowerLevel(x + 2, y)
                            + MemoizedGetPowerLevel(x, y + 1)
                            + MemoizedGetPowerLevel(x, y + 2)
                            + MemoizedGetPowerLevel(x + 1, y + 1)
                            + MemoizedGetPowerLevel(x + 2, y + 1)
                            + MemoizedGetPowerLevel(x + 1, y + 2)
                            + MemoizedGetPowerLevel(x + 2, y + 2);

                if (max.total < total)
                {
                    max = (total, x, y);
                }
            }
        }

        return max.total;
    }


    private static Dictionary<(int x, int y), int> powerlevelsByCoord = new();

    private static int MemoizedGetPowerLevel(int x, int y)
    {
        if (powerlevelsByCoord.TryGetValue((x, y), out var value))
            return value;

        var plevel = GetPowerLevel(x, y);
        powerlevelsByCoord[(x, y)] = plevel;
        return plevel;
        // return GetPowerLevel(x, y);
    }
    private static int GetPowerLevel(int x, int y)
    {
        var power = 0;
        var rackId = x + 10;
        power = rackId * y;
        power += _serialNumber;
        power *= rackId;
        power = power.GetDigitAt100thPlace();
        power -= 5;
        return power;
    }
}