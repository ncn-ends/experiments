using Subjects.Structures.Graphs;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day25Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       jqt: rhn xhk nvd
                       rsh: frs pzl lsr
                       xhk: hfx
                       cmg: qnr nvd lhk bvb
                       rhn: xhk bvb hfx
                       bvb: xhk hfx
                       pzl: lsr hfx nvd
                       qnr: nvd
                       ntq: jqt hfx bvb xhk
                       nvd: lhk
                       lsr: lhk
                       rzs: qnr cmg lsr rsh
                       frs: qnr lhk lsr
                       """;

        var example2 = """
                      
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(0));
        TestContext.Out.WriteLine(DoPart1(input));

        // Assert.That(DoPart2(example2), Is.EqualTo(0));
        // TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        /* create adjacency list */
        var adjacencyList = new AdjacencyMap<string>();
        input.IterateOnEachLine(line =>
        {
            var split = line.SplitBy([":", " "]);
            var source = split[0];
            var destination = split[1..];

            adjacencyList.AddNodeWithEdges(source, destination.Select(x => (x, 0)).ToArray());
        });

        var islands = adjacencyList.GetIslands();

        return default;
    }

    private static int DoPart2(string input)
    {
        return default;
    }
}