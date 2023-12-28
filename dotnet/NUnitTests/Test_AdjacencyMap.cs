using Subjects.Structures.Graphs;
using Utils.Extensions;
using Utils.Strings;

namespace NUnitTests;

public class Test_AdjacencyMap
{
    [Test]
    public void Test_Validation()
    {
        /* add an empty node and make sure it exists in the maps nodes */
        Assert.Throws<Exception>(() => new AdjacencyMap<long>());
        Assert.Throws<Exception>(() => new AdjacencyMap<char>());
        var asd = new AdjacencyMap<string>();
        var qwe = new AdjacencyMap<int>();
    }

    [Test]
    public void Test_AddNodeWithEdges_Simple()
    {
        var map = new AdjacencyMap<int>();
        var node10 = map.AddNodeWithEdges(10, [(11, 0)]);
        var node11 = map.AddNodeWithEdges(11, [(10, 0)]);

        Assert.IsNull(map.FindNode(100));
        Assert.IsNull(map.FindNode(9000));

        var foundNode10 = map.FindNode(10);
        var foundNode11 = map.FindNode(11);

        Assert.IsNotNull(foundNode10);
        Assert.IsNotNull(foundNode11);

        Assert.AreSame(foundNode10, node10);
        Assert.AreSame(foundNode11, node11);

        Assert.AreEqual(1, foundNode10!.Connections.Count);
        Assert.AreEqual(1, foundNode11!.Connections.Count);

        Assert.AreSame(foundNode10, foundNode10.Connections[0].FromNode);
        Assert.AreSame(foundNode11, foundNode10.Connections[0].ToNode);
        Assert.AreSame(foundNode11, foundNode11.Connections[0].FromNode);
        Assert.AreSame(foundNode10, foundNode11.Connections[0].ToNode);
    }
}