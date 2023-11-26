using System.Diagnostics;
using Subjects.Structures.Graphs;
using Utils.Extensions;
using Utils.Strings;

namespace AoC.Y2017;


public static class Day12Solutions
{
    public static int SolvePart1()
    {
        var importExample =
"""
0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5
""";
        var map = new AdjacencyMap<int>();

        // importExample.
        AocInputHandler.ImportHttp().IterateOnEachLine((x, _) =>
        {
            var nums = x.ExtractNumbers();
            var homeNodeVal = nums[0];
            var connections = nums.Skip(1);
            map.AddNodeWithEdges(homeNodeVal, [..connections]);
        });

        // var result = map.DoSimpleBFS(map.From(0));

        // return result.Visited.Count;
        return default;
    }

    public static int SolvePart2()
    {
        var importExample =
"""
0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5
""";
        var map = new AdjacencyMap<int>();

        // importExample
        AocInputHandler.ImportHttp().IterateOnEachLine((x, _) =>
        {
            var nums = x.ExtractNumbers();
            var homeNodeVal = nums[0];
            var connections = nums.Skip(1);
            map.AddNodeWithEdges(homeNodeVal, [..connections]);
        });

        var visited = new HashSet<AdjacencyMapNode<int>>();
        var queue = new Queue<AdjacencyMapNode<int>>();
        var groups = 0;

        for (var nextNode = map.From(0);
             nextNode != null;
             nextNode = map.Nodes.FirstOrDefault(pair => !visited.Contains(pair.Value)).Value)
        {
            queue.Enqueue(nextNode);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                visited.Add(current);
                // foreach (var edge in current.Edges)
                    // if (!visited.Contains(edge)) queue.Enqueue(edge);
            }

            groups++;
        }

        return groups;
    }
}