using System.Diagnostics;
using Plotly.NET.CSharp;
using Subjects.LeetCode;

namespace Subjects.Structures.Graphs;

public record AdjacencyMapEdge<T>(AdjacencyMapNode<T> TargetNode,
                                  int? Weight) where T : notnull;

public record AdjacencyMapNode<T>(T Key, AdjacencyMap<T> Map) where T : notnull
{
    public List<AdjacencyMapEdge<T>> Edges { get; set; } = [];

    public void ConnectNode(AdjacencyMapNode<T> node, int? weight = null) =>
        Edges.Add(new(node, weight));

    public bool HasConnection(AdjacencyMapNode<T> node) =>
        Edges.Any(edge => edge.TargetNode == node);
}

public class AdjacencyMap<T> where T : notnull
{
    public Dictionary<T, AdjacencyMapNode<T>> Nodes { get; init; } = [];

    public AdjacencyMapNode<T> AddEmptyNode(T nodeKey)
    {
        var node = new AdjacencyMapNode<T>(nodeKey, this);
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

    public AdjacencyMapNode<T> AddNodeWithEdges(T nodeKey, List<T> edgeValues)
    {
        var edgeNodes = edgeValues.Select(FindOrCreateNode).ToList();
        var homeNode = FindOrCreateNode(nodeKey);
        Interconnect(homeNode, edgeNodes);

        return homeNode;
    }

    public AdjacencyMapNode<T> AddNodeWithEdge(T homeNodeKey, T targetHomeKey, int? weight)
    {
        var homeNode = FindOrCreateNode(homeNodeKey);
        var targetNode = FindOrCreateNode(targetHomeKey);
        DirectedConnect(homeNode, [targetNode], weight);
        return homeNode;
    }

    public AdjacencyMapNode<T>? FindNodeByKey(T nodeKey)
    {
        Nodes.TryGetValue(nodeKey, out var node);
        return node;
    }

    public AdjacencyMapNode<T> FindOrCreateNode(T nodeKey)
    {
        var possiblyFoundNode = FindNodeByKey(nodeKey);
        possiblyFoundNode ??= AddEmptyNode(nodeKey);
        return possiblyFoundNode;
    }

    public AdjacencyMapNode<T> From(T startingNode) => FindNodeByKey(startingNode) ?? throw new InvalidOperationException();

    public bool DoesNodeExist(T nodeKey) => FindNodeByKey(nodeKey) is not null;

    private void DirectedConnect(AdjacencyMapNode<T> homeNode,
                                 List<AdjacencyMapNode<T>> connectingNodes,
                                 int? weight)
    {
        if (!DoesNodeExist(homeNode.Key) || connectingNodes.Any(x => !DoesNodeExist(x.Key)))
            throw new Exception("Trying to connect node that doesn't exist yet");

        foreach (var connectingNode in connectingNodes)
        {
            if (connectingNode != homeNode) homeNode.ConnectNode(connectingNode, weight);
        }
    }

    private void Interconnect(AdjacencyMapNode<T> homeNode, List<AdjacencyMapNode<T>> connectingNodes)
    {
        /* TODO: no weight applied */
        if (!DoesNodeExist(homeNode.Key) || connectingNodes.Any(x => !DoesNodeExist(x.Key)))
            throw new Exception("Trying to connect node that doesn't exist yet");

        foreach (var connectingNode in connectingNodes)
        {
            if (connectingNode != homeNode) homeNode.ConnectNode(connectingNode);
            if (!homeNode.HasConnection(connectingNode)) connectingNode.ConnectNode(homeNode);
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

    public void DoSimpleDFS(AdjacencyMapNode<T> startingNode,
                            Action<AdjacencyMapNode<T>> action)
    {
        Helper(startingNode);

        void Helper(AdjacencyMapNode<T> node)
        {
            action(node);
            foreach (var edge in node.Edges)
                Helper(edge.TargetNode);
        }
    }

    public record BFSState
    {
        public HashSet<AdjacencyMapNode<T>> Visited { get; set; }
        public Queue<AdjacencyMapNode<T>> Queue { get; set; }
    }
}