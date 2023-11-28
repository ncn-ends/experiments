namespace Subjects.Structures.Graphs;

public record AdjacencyNode<T>(T Value) where T : IEquatable<T>
{
    public List<AdjacencyEdge<T>> Connections { get; set; } = [];
}

public class AdjacencyEdge<T>(AdjacencyMap<T> map,
                              AdjacencyNode<T> fromNode,
                              AdjacencyNode<T> toNode,
                              int weight = 0) where T : IEquatable<T>
{
    private AdjacencyMap<T> Map { get; set; } = map;
    public int? Weight { get; set; } = weight;
    public bool IsWeighted => Map.IsWeighted;
    public AdjacencyNode<T> FromNode { get; init; } = fromNode;
    public AdjacencyNode<T> ToNode { get; init; } = toNode;
}

public class AdjacencyMap<T> where T : IEquatable<T>
{
    #region setup/data

    public AdjacencyMap(bool isWeighted = false)
    {
        if (typeof(T) != typeof(int) && typeof(T) != typeof(string))
            throw new Exception("Invalid type, only strings or ints allowed");

        IsWeighted = isWeighted;
    }

    public bool IsWeighted { get; init; }
    public HashSet<AdjacencyNode<T>> Nodes { get; set; } = [];

    #endregion


    public AdjacencyNode<T> AddNodeWithEdges(T originNodeValue,
                                             (T nodeValue, int weight)[] connectingNodeValues,
                                             bool interConnect = false)
    {
        var (originNode, _) = FindOrAddNode(originNodeValue);

        foreach (var (nodeValue, weight) in connectingNodeValues)
        {
            var connectingNode = FindOrAddNode(nodeValue);

            var nodeIsAlreadyConnected =
                    connectingNode.found && originNode.Connections.Any(x => x.ToNode == connectingNode.node);
            if (nodeIsAlreadyConnected) continue;

            var newEdge = new AdjacencyEdge<T>(this, originNode, connectingNode.node, weight);
            originNode.Connections.Add(newEdge);

            if (!interConnect) continue;

            connectingNode.node.Connections.Add(newEdge);
        }

        return originNode;
    }

    public AdjacencyNode<T>? FindNode(T nodeValue)
    {
        var node = Nodes.FirstOrDefault(x => x.Value.Equals(nodeValue));
        return node;
    }


    #region helpers

    private (AdjacencyNode<T> node, bool found) FindOrAddNode(T nodeValue)
    {
        var node = Nodes.FirstOrDefault(x => x.Value.Equals(nodeValue));
        if (node != default)
            return (node, true);

        var newNode = new AdjacencyNode<T>(nodeValue);
        Nodes.Add(newNode);
        return (newNode, false);
    }

    #endregion
}