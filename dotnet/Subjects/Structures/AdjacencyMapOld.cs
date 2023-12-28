using System.Diagnostics;
using Plotly.NET.CSharp;
using Subjects.LeetCode;

namespace Subjects.Structures.Graphs;

public record AdjacencyMapEdgeOld<T>(AdjacencyMapNodeOld<T> TargetNodeOld,
                                  int? Weight) where T : notnull;

public record AdjacencyMapNodeOld<T>(T Key, AdjacencyMapOld<T> MapOld) where T : notnull
{
    public List<AdjacencyMapEdgeOld<T>> Edges { get; set; } = [];

    public void ConnectNode(AdjacencyMapNodeOld<T> nodeOld, int? weight = null) =>
        Edges.Add(new(nodeOld, weight));

    public bool HasConnection(AdjacencyMapNodeOld<T> nodeOld) =>
        Edges.Any(edge => edge.TargetNodeOld == nodeOld);
}

public class AdjacencyMapOld<T> where T : notnull
{
    public Dictionary<T, AdjacencyMapNodeOld<T>> Nodes { get; init; } = [];

    public AdjacencyMapNodeOld<T> AddEmptyNode(T nodeKey)
    {
        var node = new AdjacencyMapNodeOld<T>(nodeKey, this);
        Nodes.TryAdd(nodeKey, node);
        return node;
    }

    // public AdjacencyMapNode<T> AddNodeWithEdges(T nodeKey, List<AdjacencyMapNode<T>> edges)
    // {
    //     var node = new AdjacencyMapNode<T>(nodeKey, this)
    //     {
    //         Edges = edges
    //     };
    //     Nodes.Add(nodeKey, node);
    //     return node;
    // }

    public AdjacencyMapNodeOld<T> AddNodeWithEdges(T nodeKey, List<T> edgeValues)
    {
        var edgeNodes = edgeValues.Select(FindOrCreateNode).ToList();
        var homeNode = FindOrCreateNode(nodeKey);
        Interconnect(homeNode, edgeNodes);

        return homeNode;
    }

    public AdjacencyMapNodeOld<T> AddNodeWithEdge(T homeNodeKey, T targetHomeKey, int? weight)
    {
        var homeNode = FindOrCreateNode(homeNodeKey);
        var targetNode = FindOrCreateNode(targetHomeKey);
        DirectedConnect(homeNode, [targetNode], weight);
        return homeNode;
    }

    public AdjacencyMapNodeOld<T>? FindNodeByKey(T nodeKey)
    {
        Nodes.TryGetValue(nodeKey, out var node);
        return node;
    }

    public AdjacencyMapNodeOld<T> FindOrCreateNode(T nodeKey)
    {
        var possiblyFoundNode = FindNodeByKey(nodeKey);
        possiblyFoundNode ??= AddEmptyNode(nodeKey);
        return possiblyFoundNode;
    }

    public AdjacencyMapNodeOld<T> From(T startingNode) => FindNodeByKey(startingNode) ?? throw new InvalidOperationException();

    public bool DoesNodeExist(T nodeKey) => FindNodeByKey(nodeKey) is not null;

    private void DirectedConnect(AdjacencyMapNodeOld<T> homeNodeOld,
                                 List<AdjacencyMapNodeOld<T>> connectingNodes,
                                 int? weight)
    {
        if (!DoesNodeExist(homeNodeOld.Key) || connectingNodes.Any(x => !DoesNodeExist(x.Key)))
            throw new Exception("Trying to connect node that doesn't exist yet");

        foreach (var connectingNode in connectingNodes)
        {
            if (connectingNode != homeNodeOld) homeNodeOld.ConnectNode(connectingNode, weight);
        }
    }

    private void Interconnect(AdjacencyMapNodeOld<T> homeNodeOld, List<AdjacencyMapNodeOld<T>> connectingNodes)
    {
        /* TODO: no weight applied */
        if (!DoesNodeExist(homeNodeOld.Key) || connectingNodes.Any(x => !DoesNodeExist(x.Key)))
            throw new Exception("Trying to connect node that doesn't exist yet");

        foreach (var connectingNode in connectingNodes)
        {
            if (connectingNode != homeNodeOld) homeNodeOld.ConnectNode(connectingNode);
            if (!homeNodeOld.HasConnection(connectingNode)) connectingNode.ConnectNode(homeNodeOld);
        }
    }

    // public BFSState DoSimpleBFS(AdjacencyMapNode<T> startingNode)
    // {
    //     var state = new BFSState();
    //     var queue = state.Queue;
    //     var visited = state.Visited;
    //     queue.Enqueue(startingNode);
    //
    //     while (queue.Any())
    //     {
    //         var current = queue.Dequeue();
    //         visited.Add(current);
    //         foreach (var edge in current.Edges)
    //         {
    //             if (!visited.Contains(edge)) queue.Enqueue(edge);
    //         }
    //     }
    //
    //     return state;
    // }

    public void DoSimpleDFS(AdjacencyMapNodeOld<T> startingNodeOld,
                            Action<AdjacencyMapNodeOld<T>> action)
    {
        Helper(startingNodeOld);

        void Helper(AdjacencyMapNodeOld<T> node)
        {
            action(node);
            foreach (var edge in node.Edges)
                Helper(edge.TargetNodeOld);
        }
    }

    public record BFSState
    {
        public HashSet<AdjacencyMapNodeOld<T>> Visited { get; set; }
        public Queue<AdjacencyMapNodeOld<T>> Queue { get; set; }
    }
}