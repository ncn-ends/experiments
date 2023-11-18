using Subjects.LeetCode;

namespace Subjects.Structures.Graphs;

public record AdjacencyMapNode(int Key, AdjacencyMap Map)
{
    public List<AdjacencyMapNode> Edges { get; set; } = [];

    public AdjacencyMapNode AddConnectedNode(int nodeKey)
    {
        var node = Map.AddNodeWithEdges(nodeKey, [this]);
        Edges.Add(node);
        return node;
    }

    public void ConnectNode(AdjacencyMapNode node)
    {
        Edges.Add(node);
    }

    public bool HasConnection(AdjacencyMapNode node) => Edges.Contains(node);
}

public class AdjacencyMap
{
    /* TODO: this should be a dict */
    public Dictionary<int, AdjacencyMapNode> Nodes { get; init; } = [];

    public AdjacencyMapNode AddEmptyNode(int nodeKey)
    {
        var node = new AdjacencyMapNode(nodeKey, this);
        Nodes.TryAdd(nodeKey, node);
        return node;
    }

    public AdjacencyMapNode AddNodeWithEdges(int nodeKey, List<AdjacencyMapNode> edges)
    {
        var node = new AdjacencyMapNode(nodeKey, this)
        {
            Edges = edges
        };
        Nodes.Add(nodeKey, node);
        return node;
    }

    public AdjacencyMapNode AddNodeWithEdges(int nodeKey, List<int> edgeValues)
    {
        var edgeNodes = edgeValues.Select(FindOrCreateNode).ToList();
        var homeNode = FindOrCreateNode(nodeKey);
        Interconnect(homeNode, edgeNodes);

        return homeNode;
    }

    public AdjacencyMapNode? FindNodeByKey(int nodeKey)
    {
        Nodes.TryGetValue(nodeKey, out var node);
        return node;
    }

    public AdjacencyMapNode FindOrCreateNode(int nodeKey)
    {
        var possiblyFoundNode = FindNodeByKey(nodeKey);
        possiblyFoundNode ??= AddEmptyNode(nodeKey);
        return possiblyFoundNode;
    }

    public bool DoesNodeExist(int nodeKey) => FindNodeByKey(nodeKey) is not null;

    private void Interconnect(AdjacencyMapNode homeNode, List<AdjacencyMapNode> connectingNodes)
    {
        if (!DoesNodeExist(homeNode.Key) || connectingNodes.Any(x => !DoesNodeExist(x.Key)))
            throw new Exception("Trying to connect node that doesn't exist yet");

        foreach (var connectingNode in connectingNodes)
        {
            if (connectingNode != homeNode) homeNode.ConnectNode(connectingNode);
            if (!homeNode.HasConnection(connectingNode)) connectingNode.ConnectNode(homeNode);
        }
    }
}