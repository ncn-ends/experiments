using System.Diagnostics;
using Utils.Numbers;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day8Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       RL
                       
                       AAA = (BBB, CCC)
                       BBB = (DDD, EEE)
                       CCC = (ZZZ, GGG)
                       DDD = (DDD, DDD)
                       EEE = (EEE, EEE)
                       GGG = (GGG, GGG)
                       ZZZ = (ZZZ, ZZZ)
                       """;

        var example2 = """
                       LLR
                       
                       AAA = (BBB, BBB)
                       BBB = (AAA, ZZZ)
                       ZZZ = (ZZZ, ZZZ)
                       """;

        var example3 = """
                       LR
                       
                       11A = (11B, XXX)
                       11B = (XXX, 11Z)
                       11Z = (11B, XXX)
                       22A = (22B, XXX)
                       22B = (22C, 22C)
                       22C = (22Z, 22Z)
                       22Z = (22B, 22B)
                       XXX = (XXX, XXX)
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(2));
        Assert.That(DoPart1(example2), Is.EqualTo(6));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example3), Is.EqualTo(6));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var split = input.SplitByLine();
        var directions = split[0].ToCharArray();
        var map = split[1..].Select(x => x.SplitBy(["=", " ", "(", ")", ","])).ToDictionary(x => x[0], x => new Tuple<string, string>(x[1], x[2]));
        var c = "AAA";
        var i = 0;
        var steps = 0;
        while (c != "ZZZ")
        {
            var nextPossible = map[c];
            c = directions[i] == 'L'
                    ? nextPossible.Item1
                    : nextPossible.Item2;
            i = i + 1 == directions.Length
                    ? 0
                    : i + 1;
            steps++;
        }
        return steps;
    }

    /* too slow */
    // private static int DoPart2(string input)
    // {
    //     var split = input.SplitByLine();
    //     var directions = split[0].ToCharArray();
    //     var map = split[1..].Select(x => x.SplitBy(["=", " ", "(", ")", ","])).ToDictionary(x => x[0], x => new Tuple<string, string>(x[1], x[2]));
    //     var tracked = map.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToList();
    //     var direction = 0;
    //     var steps = 0;
    //     while (tracked.Any(x => !x.EndsWith("Z")))
    //     {
    //         for (var i = 0; i < tracked.Count; i++)
    //         {
    //             var track = tracked[i];
    //             var nextPossible = map[track];
    //             tracked[i] = directions[direction] == 'L'
    //                     ? nextPossible.Item1
    //                     : nextPossible.Item2;
    //         }
    //         steps++;
    //         direction = direction + 1 == directions.Length
    //                 ? 0
    //                 : direction + 1;;
    //     }
    //     return steps;
    // }
    private static long DoPart2(string input)
    {
        var split = input.SplitByLine();
        var directions = split[0].ToCharArray();
        var map = split[1..].Select(x => x.SplitBy(["=", " ", "(", ")", ","])).ToDictionary(x => x[0], x => new Tuple<string, string>(x[1], x[2]));
        var tracked = map.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToList();
        var trackedZs = tracked
                        .Select((_, i) => i)
                        .ToDictionary(x => x, x => new Dictionary<string, int>());
        foreach (var pos in trackedZs.Keys)
        {
            var dir = 0;
            var cont = true;
            var steps = 0;
            while (cont)
            {
                var current = tracked[pos];
                var nextPossible = map[current];
                var next = directions[dir] == 'L'
                        ? nextPossible.Item1
                        : nextPossible.Item2;
                tracked[pos] = next;
                if (next.EndsWith("Z") && !trackedZs[pos].TryAdd(next, steps + 1)) break;
                dir = dir + 1 == directions.Length
                        ? 0
                        : dir + 1;
                steps++;
            }
        }

        return NumberHelpers.GetLCMLong(trackedZs.Values.SelectMany(x => x.Values).Select(x => (long)x));


        // while (!allZ)
        // {
        //     allZ = true;
        //     for (var i = 0; i < tracked.Count; i++)
        //     {
        //         var track = tracked[i];
        //         var nextPossible = map[track];
        //         tracked[i] = directions[direction] == 'L'
        //                 ? nextPossible.Item1
        //                 : nextPossible.Item2;
        //         if (!tracked[i].EndsWith("Z")) allZ = false;
        //         else trackedZs[i].Add(steps);
        //     }
        //     steps++;
        //     direction = direction + 1 == directions.Length
        //             ? 0
        //             : direction + 1;;
        // }
        return default;
    }

    [Test]
    public static void TestLCM()
    {
        List<int> case1 = [555, 444, 333];
        Assert.AreEqual(NumberHelpers.GetLCM(case1), 6660);

        List<int> case2 = [2, 3];
        Assert.AreEqual(NumberHelpers.GetLCM(case2), 6);

        List<long> case3 = [12169, 20093, 20659, 22357, 13301, 18961];
        Assert.AreEqual(NumberHelpers.GetLCMLong(case3), 15690466351717);
    }
}