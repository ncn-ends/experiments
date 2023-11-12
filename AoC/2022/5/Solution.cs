using AoC;
using Utils;

namespace Subjects.AoC._2022._5;

public static class Day5Solution
{
    private static string _input = AocInputHandler.ImportFile().Trim();

    private static Dictionary<int, Stack<char>> _crates = new()
    {
        {1, new Stack<char>(new[] {'B', 'W', 'N'})},
        {2, new Stack<char>(new[] {'L', 'Z', 'S', 'P', 'T', 'D', 'M', 'B'})},
        {3, new Stack<char>(new[] {'Q', 'H', 'Z', 'W', 'R'})},
        {4, new Stack<char>(new[] {'W', 'D', 'V', 'J', 'Z', 'R'})},
        {5, new Stack<char>(new[] {'S', 'H', 'M', 'B'})},
        {6, new Stack<char>(new[] {'L', 'G', 'N', 'J', 'H', 'V', 'P', 'B'})},
        {7, new Stack<char>(new[] {'J', 'Q', 'Z', 'F', 'H', 'D', 'L', 'S'})},
        {8, new Stack<char>(new[] {'W', 'S', 'F', 'J', 'G', 'Q', 'B'})},
        {9, new Stack<char>(new[] {'Z', 'W', 'M', 'S', 'C', 'D', 'J'})}
    };

    private static (int, int, int) GetActionDetails(string line)
    {
        var count = int.Parse(line.Split(" ")[1]);
        var from = int.Parse(line.Split(" ")[3]);
        var to = int.Parse(line.Split(" ")[5]);
        return (count, from, to);
    }
    
    public static string DoPart1()
    {
        foreach (var l in _input.Split("\n"))
        {
            if (!l.Contains("move")) continue;
            var (count, from, to) = GetActionDetails(l);
            var moving = new List<char>();
            
            while (count > 0)
            {
                moving.Add(_crates[from].Pop());
                count--;
            }

            moving.Reverse();
            foreach (var box in moving)
            {
                _crates[to].Push(box);
            }
        }

        var final = "";
        foreach (var (key, value) in _crates)
        {
            final += value.First();
        }
        return final;
    }

    public static int DoPart2()
    {
        var count = 0;
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