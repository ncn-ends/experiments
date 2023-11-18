using AoC.Y2017;
using Subjects.Structures.Graphs;

namespace NUnitTests;

public class Test_AdjacencyMap
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_DataManipulation_Units()
    {
        /* add an empty node and make sure it exists in the maps nodes */
        var map = new AdjacencyMap();
        var node1 = map.AddEmptyNode(1);
        Assert.Contains(node1, map.Nodes.Values);

        /* add connected node via previous node */
        var node2 = node1.AddConnectedNode(5);
        Assert.Contains(node2, map.Nodes.Values);
        Assert.Contains(node2, node1.Edges);
        Assert.Contains(node1, node2.Edges);
        Assert.That(node1, Is.EqualTo(node2.Edges[0]));
        Assert.That(node2, Is.EqualTo(node1.Edges[0]));

        /* find nodes */
        var nonExistantNode = map.FindNodeByKey(9000);
        Assert.IsNull(nonExistantNode);
        var existingNode = map.FindNodeByKey(5);
        Assert.IsNotNull(existingNode);
        Assert.That(map, Is.EqualTo(existingNode?.Map));

    }

    [Test]
    public void Test_AddNodeWithEdges_Simple()
    {
        var map = new AdjacencyMap();
        var node10 = map.AddNodeWithEdges(10, [11]);
        var node11 = map.AddNodeWithEdges(11, [10]);
        Assert.That(node11, Is.EqualTo(node10.Edges[0]));
        Assert.That(node10, Is.EqualTo(node11.Edges[0]));
    }

    [Test]
    public void Test_AddNodeWithEdges_Complex()
    {
        var map = new AdjacencyMap();
        var node1 = map.AddNodeWithEdges(1, [2, 3]);
        var node2 = map.AddNodeWithEdges(2, [1, 3]);
        var node3 = map.AddNodeWithEdges(3, [1, 2, 3, 4]);
        var node4 = map.AddNodeWithEdges(4, [3]);

        Assert.That(map.Nodes.Count, Is.EqualTo(4));

        Assert.That(node1.Edges.Count, Is.EqualTo(2));
        Assert.That(node2.Edges.Count, Is.EqualTo(2));
        Assert.That(node3.Edges.Count, Is.EqualTo(4));
        Assert.That(node4.Edges.Count, Is.EqualTo(1));

        Assert.Contains(node1, node2.Edges);
        Assert.Contains(node1, node3.Edges);
        Assert.Contains(node2, node1.Edges);
        Assert.Contains(node2, node3.Edges);
        Assert.Contains(node3, node1.Edges);
        Assert.Contains(node3, node2.Edges);
        Assert.Contains(node3, node4.Edges);
        Assert.Contains(node4, node3.Edges);
    }
}