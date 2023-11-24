using System.Diagnostics;
using Subjects.Structures.Graphs;
using Utils;
using Utils.Extensions;
using static Utils.StringUtils;

namespace AoC.Y2015;

public static class Day9Solutions
{
    private static string _exampleInput =
"""
London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141
""";

    public static int SolvePart1()
    {
        // var input = AocInputHandler.ImportHttp();
        var input = _exampleInput;
        var map = new AdjacencyMap<string>();
        string? firstCity = null;
        input.IterateOnEachLine((line, _) =>
        {
            var split = line.SplitBySpace();
            var homeNodeName = split[0];
            var targetNodeName = split[2];
            var weight = split[4].ToInt();
            firstCity ??= homeNodeName;
            map.AddNodeWithEdge(homeNodeName, targetNodeName, weight);
        });

        // map.DoSimpleDFS(map.From(firstCity!), node =>
        // {
        //
        // });
        var arbitraryFirstNode = map.From(firstCity!);
        var completedPaths = new List<List<AdjacencyMapNode<string>>>();
        Helper([arbitraryFirstNode]);
        // var completedPathWeightSums = completedPaths.Select(x => x.Sum(y => y.))
        return default;

        void Helper(List<AdjacencyMapNode<string>> path)
        {
            var currentNode = path.Last();

            var unvisitedEdges = currentNode.Edges.Where(x => !path.Contains(x.TargetNode)).ToList();

            if (unvisitedEdges is [])
                completedPaths.Add(path);

            foreach (var edge in unvisitedEdges)
                Helper([..path, edge.TargetNode]);
        }
    }

    public static int SolvePart2()
    {
        return default;
    }
}