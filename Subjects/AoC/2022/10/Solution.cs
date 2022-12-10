using System.Diagnostics;
using System.Globalization;
using Subjects.Structures;
using Utils;

namespace Subjects.AoC._2022._10;

class Line
{
    public required int CyclesLeft { get; set; }
    public required int Change { get; set; }
}

public static class Day10Solution
{
    private static string _input = AOCInput.Import().Trim();

    private static string _exampleA = """
noop
addx 3
addx -5
""";

    private static string _exampleB = """
addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop
""";
    
    public static int DoIt()
    {
        // foreach (var l in _exampleA.Split("\n"))
        // {
        //     var asd = l.Trim().Split(" ");
        //     var op = asd[0];
        //     if (op == "noop")
        //     {
        //         cycle++;
        //     }
        //
        //     if (op == "addx")
        //     {
        //         
        //     }
        // }

        var checks = new int[] { 20, 60, 100, 140, 180, 220 };
        var signalChecks = new List<(int cycle, int signalPower)>();
        
        List<Line> asd = _input.Split("\n").Select<string, Line>(x =>
        {
            var split = x.Split(" ");
            if (split[0] == "addx")
                return new Line
                {
                    CyclesLeft = 2,
                    Change = int.Parse(split[1])
                };
            return new Line
            {
                CyclesLeft = 1,
                Change = 0
            };
        }).ToList();

        var cycle = 0;
        var signal = 1;
        var crt = "";
        while (true)
        {
            if (asd.Count == 0) break;

            cycle++;
            var line = asd[0];

            // Debugger.Break();
            line.CyclesLeft--;
            
            if (checks.Any(x => x == cycle))
            {
                signalChecks.Add((cycle, cycle * signal));
            }


            // crt stuff
            var symbol = ".";
            if (crt.Length % 40 <= signal + 1 && crt.Length % 40 >= signal - 1)
            {
                symbol = "#";
            }

            crt += symbol;
            
            
            if (line.CyclesLeft == 0)
            {
                signal += line.Change;
                asd.RemoveAt(0);
            }

            // Debugger.Break();
        }

        Console.Write("Part 1: ");
        var ans = signalChecks.Sum(x => x.signalPower);
        Console.WriteLine(ans);

        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine(crt.Substring(i * 40, 40));
        }
        return ans;
    }

    public static void Output()
    {
        // Console.Write("Part 1: ");
        Console.WriteLine(DoIt());
        //
        // Console.Write("Part 2: ");
        // Console.WriteLine(DoPart2());
    }
}