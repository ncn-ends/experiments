using System.Diagnostics;

namespace Subjects.AoC._2022._11;


class Monkey
{
    public required List<ulong> Items { get; set; }
    public required Func<ulong, ulong> Op { get; init; }
    public required Func<ulong, bool> Test { get; init; }
    public required int TrueTarget { get; init; }
    public required int FalseTarget { get; init; }
    public int Inspections { get; set; } = 0;
}


public static class Day11Solution
{
    private static List<Monkey> _monkeysA = new()
    {
        new Monkey
        {
            Items = new List<ulong> { 52, 60, 85, 69, 75, 75 },
            Op = old => old * 17,
            Test = i => i % 13 == 0,
            TrueTarget = 6,
            FalseTarget = 7
        },
        new Monkey
        {
            Items = new List<ulong> { 96, 82, 61, 99, 82, 84, 85 },
            Op = old => old + 8,
            Test = i => i % 7 == 0,
            TrueTarget = 0,
            FalseTarget = 7
        },
        new Monkey
        {
            Items = new List<ulong> { 95, 79 },
            Op = old => old + 6,
            Test = i => i % 19 == 0,
            TrueTarget = 5,
            FalseTarget = 3
        },
        new Monkey
        {
            Items = new List<ulong> { 88, 50, 82, 65, 77 },
            Op = old => old * 19,
            Test = i => i % 2 == 0,
            TrueTarget = 4,
            FalseTarget = 1
        },
        new Monkey
        {
            Items = new List<ulong> { 66, 90, 59, 90, 87, 63, 53, 88 },
            Op = old => old + 7,
            Test = i => i % 5 == 0,
            TrueTarget = 1,
            FalseTarget = 0
        },
        new Monkey
        {
            Items = new List<ulong> { 92, 75, 62 },
            Op = old => old * old,
            Test = i => i % 3 == 0,
            TrueTarget = 3,
            FalseTarget = 4
        },
        new Monkey
        {
            Items = new List<ulong> { 94, 86, 76, 67 },
            Op = old => old + 1,
            Test = i => i % 11 == 0,
            TrueTarget = 5,
            FalseTarget = 2
        },
        new Monkey
        {
            Items = new List<ulong> { 57 },
            Op = old => old + 2,
            Test = i => i % 17 == 0,
            TrueTarget = 6,
            FalseTarget = 2
        }
    };

    // private static List<Monkey> _monkeysB = new()
    // {
    //     new Monkey
    //     {
    //         Items = new List<ulong> { 79, 98 },
    //         Op = old => old * 19,
    //         Test = i => i % 23 == 0,
    //         TrueTarget = 2,
    //         FalseTarget = 3
    //     },
    //     new Monkey
    //     {
    //         Items = new List<ulong> { 54, 65, 75, 74 },
    //         Op = old => old + 6,
    //         Test = i => i % 19 == 0,
    //         TrueTarget = 2,
    //         FalseTarget = 0
    //     },
    //     new Monkey
    //     {
    //         Items = new List<ulong> { 79, 60, 97 },
    //         Op = old => old * old,
    //         Test = i => i % 13 == 0,
    //         TrueTarget = 1,
    //         FalseTarget = 3
    //     },
    //     new Monkey
    //     {
    //         Items = new List<ulong> { 74 },
    //         Op = old => old + 3,
    //         Test = i => i % 17 == 0,
    //         TrueTarget = 0,
    //         FalseTarget = 1
    //     }
    // };

    // public static int DoIt(int part)
    // {
    //     var monkeys = _monkeysA;
    //     var monkeyPos = 0;
    //     for (int round = 1; round <= 10000; round++)
    //     {
    //         // Debugger.Break();
    //         while (true)
    //         {
    //             var monkey = monkeys[monkeyPos];
    //             if (monkey.Items.Count == 0)
    //             {
    //                 if (monkeyPos == monkeys.Count - 1) break;
    //                 monkeyPos++;
    //                 continue;
    //             }
    //             var initialWorryScore = monkey.Items[0];
    //
    //             // Debugger.Break();
    //             monkey.Inspections++;
    //             var appliedWorryScore = monkey.Op(initialWorryScore);
    //             // var temp = Math.Floor(appliedWorryScore / 3);
    //             var finalWorryScore = appliedWorryScore;
    //
    //             var monkeyToThrowTo = monkey.Test(finalWorryScore)
    //                 ? monkey.TrueTarget
    //                 : monkey.FalseTarget;
    //             monkeys[monkeyToThrowTo].Items.Add(finalWorryScore);
    //             monkey.Items.RemoveAt(0);
    //
    //             var monkeyHasNoItems = monkeys[monkeyPos].Items.Count == 0;
    //             var monkeyIsLastMonkey = monkeyPos == monkeys.Count - 1;
    //             if (monkeyHasNoItems && monkeyIsLastMonkey) break;
    //             if (monkeyHasNoItems) monkeyPos++;
    //         }
    //
    //         monkeyPos = 0;
    //         Debugger.Break();
    //     }
    //
    //     var orderedByInspections = monkeys.OrderByDescending(x => x.Inspections).ToArray();
    //
    //     return orderedByInspections[0].Inspections * orderedByInspections[1].Inspections;
    // }

    public static ulong Rewrite()
    {
        var monkeys = _monkeysA;
        for (int round = 1; round <= 10000; round++)
        {
            for (var i = 0; i < monkeys.Count; i++)
            {
                var monkeyPos = i;
                var monkey = monkeys[monkeyPos];

                for (var j = 0; j < monkey.Items.Count; j++)
                {
                    var worryScore = monkey.Items[j];

                    monkeys[monkeyPos].Inspections++;
                    var appliedWorryScore = monkey.Op(worryScore);
                    appliedWorryScore %= 9699690; // manual lcm calculation of modulus numbers

                    var monkeyToThrowTo = monkey.Test(appliedWorryScore)
                        ? monkey.TrueTarget
                        : monkey.FalseTarget;
                    monkeys[monkeyToThrowTo].Items.Add(appliedWorryScore);
                }

                monkeys[monkeyPos].Items.Clear();
            }

            var asd = "qwe";
        }

        var orderedByInspections = monkeys.OrderByDescending(x => x.Inspections).ToArray();

        Debugger.Break();
        return (ulong)orderedByInspections[0].Inspections * (ulong)orderedByInspections[1].Inspections;
    }

    // TODO: can improve this further by converting it to queue instead of list
    public static void Output()
    {
        // Console.Write("Part 1: ");
        // Console.WriteLine(DoIt(1));

        Console.Write("Part 2: ");
        Console.WriteLine(Rewrite());
    }
}