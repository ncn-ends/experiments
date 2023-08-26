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
}