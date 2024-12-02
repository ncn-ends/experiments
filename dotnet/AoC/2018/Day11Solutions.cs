using System.Diagnostics;
using AoC;
using Utils;
using Utils.Numbers;
using Utils.Strings;


namespace Y2018;

public class Day11Solutions_Old
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
    public static int GetPowerLevel(int x, int y)
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

public class Day11Solutions
{
    private static int _gridSize = 300;
    private static string _example = "18";
    private static int _serialNumber = AocHandler.ImportHttp().ToInt();

    [TestCase(3, 5, 8, 4)]
    [TestCase(122, 79, 57, -5)]
    [TestCase(217, 196, 39, 0)]
    [TestCase(101, 153, 71, 4)]
    [TestCase(1, 1, 18, -2)]
    [TestCase(33, 45, 18, 4)]
    public void Test_GetPowerLevel(int xInput,
                                   int yInput,
                                   int serialNumber,
                                   int expected)
    {
        Assert.That(GetPowerLevel(xInput, yInput, serialNumber), Is.EqualTo(expected));
    }

    [Test][OutputTime]
    public void DoPart1()
    {
        Assert.That(DoBaseSolution(3, 18).total, Is.EqualTo(29));
        Assert.That(DoBaseSolution(3, 42).total, Is.EqualTo(30));

        var result = SolvePart1();
        TestContext.Out.WriteLine(result);
    }

    [Test][OutputTime][Ignore("Part 2 incomplete")]
    public void DoPart2()
    {
        Assert.That(SolvePart2(18).total, Is.EqualTo(113));

        var result = SolvePart2(18);
        TestContext.Out.WriteLine(result);
    }

    public static string SolvePart1(int? serialNumber = null)
    {
        serialNumber ??= _serialNumber;
        var result = DoBaseSolution(3, serialNumber.Value);
        return $"{result.x},{result.y}";
    }

    public static (int total, int x, int y, int size) SolvePart2(int? serialNumber = null)
    {
        serialNumber ??= _serialNumber;
        (int total, int x, int y, int size) max = (int.MinValue, -1, -1, -1);
        for (int squareSize = 1; squareSize < 300; squareSize++)
        {
            var result = DoBaseSolution(squareSize, serialNumber.Value);
            if (max.total < result.total) max = (result.total, result.x, result.y, squareSize);
        }

        return max;
    }

    private static (int total, int x, int y) DoBaseSolution(int squareSize, int serialNumber)
    {
        (int total, int x, int y) max = (0, -1, -1);

        for (int y = 1; y < _gridSize - squareSize + 1; y++)
        {
            for (int x = 1; x < _gridSize - squareSize + 1; x++)
            {
                var total = GetPowerLevel(x, y, serialNumber)
                            + GetPowerLevel(x + 1, y, serialNumber)
                            + GetPowerLevel(x + 2, y, serialNumber)
                            + GetPowerLevel(x, y + 1, serialNumber)
                            + GetPowerLevel(x, y + 2, serialNumber)
                            + GetPowerLevel(x + 1, y + 1, serialNumber)
                            + GetPowerLevel(x + 2, y + 1, serialNumber)
                            + GetPowerLevel(x + 1, y + 2, serialNumber)
                            + GetPowerLevel(x + 2, y + 2, serialNumber);

                if (max.total < total)
                {
                    max = (total, x, y);
                }
            }
        }

        return max;
    }

    private static int GetPowerLevel(int x, int y, int? serialNumber = null)
    {
        serialNumber ??= _serialNumber;

        var power = 0;
        var rackId = x + 10;
        power = rackId * y;
        power += serialNumber.Value;
        power *= rackId;
        power = power.GetDigitAt100thPlace();
        power -= 5;
        return power;
    }
}