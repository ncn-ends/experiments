using System.Diagnostics;
using Utils;

namespace Subjects.AoC._2022._1;

public static class Solution
{
    private static string _input = AOCInput.Import();
    
    private const int _numToSumTo = 2022;
    
    // 67622
    public static int DoPart1()
    {
        var currentMax = 0;
        
        foreach (var s1 in _input.Split("\n\n"))
        {
            int sum = 0;
            foreach (var s2 in s1.Split("\n"))
            {
                if (s2 == "") continue;
                int parsed;
                var didParse = Int32.TryParse(s2, out parsed);
                if (!didParse) throw new Exception("Something went wrong when trying to parse.");

                sum += parsed;
            }

            if (currentMax < sum) currentMax = sum;
        }

        return currentMax;
    }
    
    // 207576
    public static int DoPart2()
    {
        var maxElves = new List<int>{0, 0, 0};
        
        foreach (var s1 in _input.Split("\n\n"))
        {
            int sum = 0;
            foreach (var s2 in s1.Split("\n"))
            {
                if (s2 == "") continue;
                int parsed;
                var didParse = Int32.TryParse(s2, out parsed);
                if (!didParse) throw new Exception("Something went wrong when trying to parse.");

                sum += parsed;
            }
            
            var clonedSums = new List<int>(maxElves);
            clonedSums.Add(sum);
            clonedSums = new List<int>(clonedSums.OrderByDescending(i => i));
            maxElves = clonedSums.GetRange(0, 3);
        }

        return maxElves.Sum();
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());
        
        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}