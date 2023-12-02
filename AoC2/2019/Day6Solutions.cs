using System.Diagnostics;
using Subjects.Structures.Graphs;
using Utils.Strings;


namespace AoC.Y2019;

public class Day6Solutions
{
    private static string _example1 = """
                                      COM)B
                                      B)C
                                      C)D
                                      D)E
                                      E)F
                                      B)G
                                      G)H
                                      D)I
                                      E)J
                                      J)K
                                      K)L
                                      """;

    private static string _example2 = """
                                      COM)B
                                      B)C
                                      C)D
                                      D)E
                                      E)F
                                      B)G
                                      G)H
                                      D)I
                                      E)J
                                      J)K
                                      K)L
                                      K)YOU
                                      I)SAN
                                      """;

    [Test][OutputTime]
    public static void Run()
    {
        Assert.That(SolvePart1(_example1), Is.EqualTo(42));
        Assert.That(SolvePart2(_example2), Is.EqualTo(4));

        var res1 = SolvePart1(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res1);

        var res2 = SolvePart2(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res2);
    }

    private static int SolvePart1(string input)
    {
        var map = new AdjacencyMap<string>();
        input.IterateOnEachLine(line =>
        {
            var split = line.SplitBy([")"]);
            map.AddNodeWithEdges(split[1], [(split[0], 0)]);
        });

        var totalConnections = 0;

        var stack = new Stack<AdjacencyNode<string>>();
        foreach (var originNode in map.Nodes)
        {
            var connections = -1;
            stack.Push(originNode);
            for (var currentNode = originNode;
                 stack.Any();
                 currentNode = stack.Pop(), connections++)
            {
                foreach (var connection in currentNode.Connections)
                    stack.Push(connection.ToNode);
            }

            totalConnections += connections;
        }

        return totalConnections;
    }

    private static int SolvePart2(string input)
    {
        var map = new AdjacencyMap<string>();
        input.IterateOnEachLine(line =>
        {
            var split = line.SplitBy([")"]);
            map.AddNodeWithEdges(split[1], [(split[0], 0)], true);
        });

        var q = new Queue<(AdjacencyNode<string> node, int steps)>();
        var visited = new HashSet<AdjacencyNode<string>>();
        q.Enqueue((map.FindNode("YOU") ?? throw new Exception(), -1));
        for (; q.Any();)
        {
            var (node, steps) = q.Dequeue();
            visited.Add(node);
            if (node.Connections.Any(x => x.ToNode.Value == "SAN"
                                          || x.FromNode.Value == "SAN"))
                return steps;

            foreach (var connection in node.Connections)
            {
                if (visited.All(x => x != connection.FromNode)) q.Enqueue((connection.FromNode, steps + 1));
                if (visited.All(x => x != connection.ToNode)) q.Enqueue((connection.ToNode, steps + 1));
            }
        }

        return int.MinValue;
    }
}