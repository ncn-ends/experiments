using System.Diagnostics;
using Subjects.Structures.Graphs;
using Utils.Extensions;

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
        var map = new AdjacencyMap();

        // importExample.
        AocInputHandler.ImportHttp().IterateOnEachLine((x, _) =>
        {
            var nums = x.ExtractNumbers();
            var homeNodeVal = nums[0];
            var connections = nums.Skip(1);
            map.AddNodeWithEdges(homeNodeVal, [..connections]);
        });

        var visited = new HashSet<AdjacencyMapNode>();
        var queue = new Queue<AdjacencyMapNode>();
        queue.Enqueue(map.From(0));

        while (queue.Any())
        {
            var current = queue.Dequeue();
            visited.Add(current);
            foreach (var edge in current.Edges)
            {
               if (!visited.Contains(edge)) queue.Enqueue(edge);
            }
        }

        return visited.Count;
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
        var map = new AdjacencyMap();

        // importExample
        AocInputHandler.ImportHttp().IterateOnEachLine((x, _) =>
        {
            var nums = x.ExtractNumbers();
            var homeNodeVal = nums[0];
            var connections = nums.Skip(1);
            map.AddNodeWithEdges(homeNodeVal, [..connections]);
        });

        var visited = new HashSet<AdjacencyMapNode>();
        var queue = new Queue<AdjacencyMapNode>();
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
                foreach (var edge in current.Edges)
                    if (!visited.Contains(edge)) queue.Enqueue(edge);
            }

            groups++;
        }

        return groups;
    }
}