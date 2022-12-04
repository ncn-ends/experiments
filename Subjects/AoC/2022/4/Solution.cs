using System.Diagnostics;
using System.Globalization;
using Utils;

namespace Subjects.AoC._2022._4;

public static class Day4Solution
{
    private static string _input = AOCInput.Import().Trim();
    
    public static int DoPart1()
    {
        var count = 0;
        var lines = _input.Split("\n");
        foreach (var l in lines)
        {
            var pairs = Array.ConvertAll(l.Split(","), x => Array.ConvertAll(x.Split("-"), int.Parse));
            var pairA = pairs[0];
            var pairB = pairs[1];

            var aInB = pairA[0] <= pairB[0] && pairA[1] >= pairB[1];
            var bInA = pairB[0] <= pairA[0] && pairB[1] >= pairA[1];

            if (aInB || bInA) count++;
        }
        
        return count;
    }

public static int DoPart2()
{
    var count = 0;
    var lines = _input.Split("\n");
    foreach (var l in lines)
    {
        var pairs = Array.ConvertAll(l.Split(","), x => Array.ConvertAll(x.Split("-"), int.Parse));
        if (pairs is not [var a, var b]) throw new Exception("Line had unexpected format.");

        var aOverlapsWithB = a[1] >= b[0] && a[0] <= b[1];
        var bOverlapsWithA = b[1] >= a[0] && b[0] <= a[1];

        if (aOverlapsWithB || bOverlapsWithA) count++;
    }
    
    return count;
}

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());
        
        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}