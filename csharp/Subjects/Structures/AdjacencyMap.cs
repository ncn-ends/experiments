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

    public AdjacencyMap(bool isWeighted = false, bool isBidirectional = false)
    {
        if (typeof(T) != typeof(int) && typeof(T) != typeof(string))
            throw new Exception("Invalid type, only strings or ints allowed");

        IsWeighted      = isWeighted;
        IsBidirectional = isBidirectional;
    }

    public bool IsWeighted { get; init; }
    public bool IsBidirectional { get; init; }
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

    public List<List<AdjacencyNode<T>>> GetIslands()
    {
        var visisted = new HashSet<AdjacencyNode<T>>();
        var islands = new List<List<AdjacencyNode<T>>>();
        foreach (var node in Nodes)
        {
            if (visisted.Contains(node)) continue;
            var q = new Queue<AdjacencyNode<T>>();
            q.Enqueue(node);
            islands.Add([]);
            while (q.Any())
            {
                var c = q.Dequeue();
                if (visisted.Contains(c)) continue;
                islands.Last().Add(c);
                visisted.Add(c);
                foreach (var edge in c.Connections)
                    q.Enqueue(edge.ToNode);

                if (!IsBidirectional) continue;

                foreach (var adjacencyNode in Nodes)
                {
                    if (adjacencyNode.Connections.Any(n => n.ToNode == c)) q.Enqueue(adjacencyNode);
                }
            }
        }

        return islands;
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