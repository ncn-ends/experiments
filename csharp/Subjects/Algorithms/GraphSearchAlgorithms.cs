namespace Subjects.Algorithms;

// var graph = new Dictionary<char, List<(char targetNode, int weight)>>();
// graph.Add('A', new List<(char targetNode, int weight)>()
// {
//     ('B', 4), ('C', 1)
// });
// graph.Add('B', new List<(char targetNode, int weight)>()
// {
//     ('C', 6)
// });
// graph.Add('C', new List<(char targetNode, int weight)>()
// {
//     ('A', 4), ('B', 1), ('D', 2)
// });
// graph.Add('D', new List<(char targetNode, int weight)>());
//
// var list = new List<char>();
// GraphSearchAlgorithms.DFS(graph, c => list.Add(c));
// Print(list);


public static class GraphSearchAlgorithms
{
    public static void DFS(Dictionary<char, List<(char targetNode, int weight)>> graph, Action<char> action, char? start = null)
    {
        start ??= graph.Keys.FirstOrDefault();
        if (start is null) throw new Exception("Graph was empty.");

        var visited = new Dictionary<char, bool>();

        Iterate((char)start);

        void Iterate(char at)
        {
            if (visited.ContainsKey(at)) return;
            visited[at] = true;

            action(at);

            var connections = graph[at];
            foreach (var (targetNode, _) in connections)
            {
                Iterate(targetNode);
            }
        }
    }

    // /// <summary>
    // ///
    // /// </summary>
    // /// <param name="graph"></param>
    // /// <param name="action"></param>
    // /// <param name="start">If not provided, will default to the first key in the adjacency map</param>
    // /// <param name="end">If not provided, will default to the last key in the adjacency map</param>
    // /// <exception cref="Exception"></exception>
    // public static void BFS(Dictionary<char, List<(char targetNode, int weight)>> graph,
    //     Action<char> action,
    //     char? start = null,
    //     char? end = null)
    // {
    //     start ??= graph.Keys.FirstOrDefault();
    //     end ??= graph.Keys.LastOrDefault();
    //     if (start is null || end is null) throw new Exception("Graph was empty.");
    //
    //     var q = new Queue<char>();
    //     q.Enqueue((char)start);
    //
    //     var visited = new Dictionary<char, bool>();
    //     visited[(char) start] = true;
    //
    //     var prev = new Dictionary<char, char>(); /* may want to change this structure */
    //     while (q.Any())
    //     {
    //         var node = q.Dequeue();
    //         var connections = graph[node]; /* this access will change depending on graph type */
    //
    //         foreach (var (targetNode, weight) in connections)
    //         {
    //             if (visited.ContainsKey(targetNode)) continue;
    //
    //             q.Enqueue(targetNode);
    //             visited[targetNode] = true;
    //             prev[targetNode] = node;
    //         }
    //     }
    //
    //     Debugger.Break();
    // }
}