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
        var map = split[1..]
                  .Select(x => x.SplitBy(["=", " ", "(", ")", ","]))
                  .ToDictionary(x => x[0], x => new[] {x[1], x[2]});
        var c = "AAA";
        var steps = 0;
        while (c != "ZZZ")
        {
            var nextPossible = map[c];
            var dir = steps % directions.Length;
            c = directions[dir] == 'L'
                    ? nextPossible[0]
                    : nextPossible[1];
            steps++;
        }
        return steps;
    }

    private static long DoPart2(string input)
    {
        var split = input.SplitByLine();
        /* the left/right directions */
        var directions = split[0].ToCharArray();

        /* map of points to their left/right nodes */
        var map = split[1..]
                  .Select(x => x.SplitBy(["=", " ", "(", ")", ","]))
                  .ToDictionary(x => x[0], x => new[] { x[1], x[2] });

        /* track nodes which originally ended with A */
        var tracked = map.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToList();
        /* track the tracked nodes by index and the step at which a node was found that ends with z */
        var trackedZs = tracked
                        .Select((_, i) => i)
                        .ToDictionary(x => x, x => new Dictionary<string, int>());

        /* for each tracked node, need to find all the # of steps which a node that ends with z */
        /* ensures duplicates aren't found based on the uniqueness of the map key */
        foreach (var pos in trackedZs.Keys)
        {
            var steps = 0;
            while (true)
            {
                var dir = steps % directions.Length;
                var current = tracked[pos];
                var nextPossible = map[current];
                var next = directions[dir] == 'L'
                        ? nextPossible[0]
                        : nextPossible[1];

                tracked[pos] = next;
                if (next.EndsWith("Z") && !trackedZs[pos].TryAdd(next, steps + 1)) break;

                steps++;
            }
        }

        /* get LCM of all the nodes that originally started with A by the steps in
         * which they found a node that ends with Z */
        return NumberHelpers.GetLCMLong(trackedZs.Values.SelectMany(x => x.Values).Select(x => (long)x));
    }
}