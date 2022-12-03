using System.Diagnostics;
using Utils;

namespace Subjects.AoC._2022._2;

public static class Day2Solution
{
    private static string _input = AOCInput.Import().Trim();
    
    private const int _numToSumTo = 2022;
    
        
    // A for Rock, B for Paper, and C for Scissors
    // X for Rock, Y for Paper, and Z for Scissors
    //      (1 for Rock, 2 for Paper, and 3 for Scissors)
    // 6 if won, 3 if same, 0 if lose
    public static int DoPart1()
    {
        int sum = 0;
        
        foreach (var l in _input.Split("\n"))
        {
            var s = l.Split(" ");
            var other = s[0];
            var my = s[1];

            var pointsBySelection = my switch
            {
                "X" => 1,
                "Y" => 2,
                "Z" => 3,
                _ => throw new ArgumentOutOfRangeException()
            };

            sum += pointsBySelection;

            if (other == "A")
            {
                sum += my switch
                {
                    "X" => 3,
                    "Y" => 6,
                    "Z" => 0,
                    _ => throw new ArgumentOutOfRangeException()
                };
                continue;
            }

            if (other == "B")
            {
                sum += my switch
                {
                    "X" => 0,
                    "Y" => 3,
                    "Z" => 6,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            if (other == "C")
            {
                sum += my switch
                {
                    "X" => 6,
                    "Y" => 0,
                    "Z" => 3,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        return sum;
    }
    
    // A for Rock, B for Paper, and C for Scissors
    // (1 for Rock, 2 for Paper, and 3 for Scissors)
    // 6 if won, 3 if same, 0 if lose
    public static int DoPart2()
    {
        var correctReaction = new Dictionary<string, Dictionary<string, string>>
        {
            {"A", new Dictionary<string, string>
            {
                {"X", "C"},
                {"Y", "A"},
                {"Z", "B"}
            }},
            {"B", new Dictionary<string, string>
            {
                {"X", "A"},
                {"Y", "B"},
                {"Z", "C"}
            }},
            {"C", new Dictionary<string, string>
            {
                {"X", "B"},
                {"Y", "C"},
                {"Z", "A"}
            }},
        };
        
        // X to lose, Y to tie, Z to win
        var pointsByAction = new Dictionary<string, int>
        {
            {"X", 0},
            {"Y", 3},
            {"Z", 6}
        };
        
        // 1 for rock, 2 for paper, 3 for scissors
        var pointsByGesture = new Dictionary<string, int>
        {
            {"A", 1},
            {"B", 2},
            {"C", 3}
        };
        
        int sum = 0;
        
        foreach (var l in _input.Split("\n"))
        {
            var s = l.Split(" ");
            var opp = s[0];
            var action = s[1];

            var actionPoints = pointsByAction[action];
            var gesturePoints = pointsByGesture[correctReaction[opp][action]];

            sum += gesturePoints + actionPoints;
        }

        return sum;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        Console.WriteLine(DoPart1());
        
        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}