using System.Diagnostics;
using System.Globalization;
using Utils;

namespace Subjects.AoC._2022._6;

public static class Day6Solution
{
    private static string _input = AOCInput.Import().Trim();
    
    public static int SolveBySeqLimit(int seqLimit)
    {
        for (int i = seqLimit - 1; i < _input.Length; i++)
        {
            var seq = _input.Substring(i - seqLimit + 1, seqLimit);
            if (seq.Distinct().Count() == seqLimit) return i + 1;
        }
        throw new Exception("bad");
    }
    
    public static int DoPart1()
    {
        for (int i = 3; i < _input.Length; i++)
        {
            var seq = _input.Substring(i - 3, 4);
            if (seq.Distinct().Count() == 4) return i + 1;
        }
        throw new Exception("bad");
    }

    public static int DoPart2()
    {
        for (int i = 13; i < _input.Length; i++)
        {
            var seq = _input.Substring(i - 13, 14);
            if (seq.Distinct().Count() == 14) return i + 1;
        }
        throw new Exception("bad");
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());
        Console.WriteLine(SolveBySeqLimit(4));
        
        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
        Console.WriteLine(SolveBySeqLimit(14));
    }
}