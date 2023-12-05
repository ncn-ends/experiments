using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day03
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       123 -> x
                       456 -> y
                       x AND y -> d
                       x OR y -> e
                       x LSHIFT 2 -> f
                       y RSHIFT 2 -> g
                       NOT x -> h
                       NOT y -> i
                       """;

        var example2 = """
                      
                       """;

        var exampleExpected = new Dictionary<string, int>()
        {
            { "d", 72 },
            { "e", 507 },
            { "f", 492 },
            { "g", 114 },
            { "h", 65412 },
            { "i", 65079 },
            { "x", 123 },
            { "y", 456 },
        };
        // DoPart1(example1);
        var input = AocHandler.ImportHttp();
        TestContext.Out.WriteLine(DoPart1(input));
    }

    private static int DoPart1(string input)
    {
        var dict = new Dictionary<string, ushort>(); /* wire, signal */
        var operations = new Queue<(string target, List<string> vals, string cmd)>();
        input.IterateOnEachLine(line =>
        {
            /* parsing and set up */
            var split = line.SplitBy([" -> "]);
            var target = split[1];
            var group = split[0];
            var commands = new HashSet<string>() { "AND", "OR", "LSHIFT", "RSHIFT", "NOT" };
            var cmd = group.ExtractOutOf(commands);
            var vals = split[0].SplitBy(commands);

            /* ensure wire exists in dict for each */
            List<string> toAdd = [..vals, target];
            foreach (var val in toAdd)
                if (!val.IsInt()) dict.TryAdd(val, 0);

            operations.Enqueue((target, vals, cmd));
        });
        while (operations.Any())
        {
            var (target, vals, cmd) = operations.Dequeue();
            var left = vals[0].IsInt()
                    ? (ushort) vals[0].ToInt()
                    : dict[vals[0]];
            ushort? right = vals.Count == 1
                    ? null
                    : vals[1].IsInt()
                            ? (ushort) vals[1].ToInt()
                            : dict[vals[1]];

            if (cmd == "ADD")
            {
                dict[target] += left;
            }
            else if (cmd == "AND")
            {
                dict[target] += (ushort) (left & right.Value);
            }
            else if (cmd == "OR")
            {
                dict[target] += (ushort) (left | right.Value);
            }
            else if (cmd == "LSHIFT")
            {
                dict[target] += (ushort) (left << right.Value);
            }
            else if (cmd == "RSHIFT")
            {
                dict[target] +=(ushort)( left >> (int) right.Value);
            }
            else if (cmd == "NOT")
            {
                dict[target] += (ushort) ~left;
            }
        }
        return dict["a"];
    }

    private static int DoPart2(string input)
    {
        return default;
    }
}