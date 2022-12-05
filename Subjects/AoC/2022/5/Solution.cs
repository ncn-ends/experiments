using System.Diagnostics;
using System.Globalization;
using Utils;

namespace Subjects.AoC._2022._5;

public static class Day5Solution
{
    private static string _input = AOCInput.Import().Trim();

    private static Dictionary<int, Stack<char>> _crates = new Dictionary<int, Stack<char>>()
    {
        {1, new Stack<char>(new char[] {'B', 'W', 'N'})},
        {2, new Stack<char>(new char[] {'L', 'Z', 'S', 'P', 'T', 'D', 'M', 'B'})},
        {3, new Stack<char>(new char[] {'Q', 'H', 'Z', 'W', 'R'})},
        {4, new Stack<char>(new char[] {'W', 'D', 'V', 'J', 'Z', 'R'})},
        {5, new Stack<char>(new char[] {'S', 'H', 'M', 'B'})},
        {6, new Stack<char>(new char[] {'L', 'G', 'N', 'J', 'H', 'V', 'P', 'B'})},
        {7, new Stack<char>(new char[] {'J', 'Q', 'Z', 'F', 'H', 'D', 'L', 'S'})},
        {8, new Stack<char>(new char[] {'W', 'S', 'F', 'J', 'G', 'Q', 'B'})},
        {9, new Stack<char>(new char[] {'Z', 'W', 'M', 'S', 'C', 'D', 'J'})}
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
            // Debugger.Break();
            foreach (var box in moving)
            {
                _crates[to].Push(box);
            }
            // Debugger.Break();
            // for (int i = 0; i < count; i++)
            // {
            //     var popped = _crates[from].Pop();
            //     _crates[to].Push(popped);
            // }
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