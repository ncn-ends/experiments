using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Subjects.Structures.Trees;
using Xunit;

namespace Tests.Structures;

public class BSTTests
{
    private readonly BST<int> _tree;
    private readonly BST<int> _bigTree;

    public BSTTests()
    {
        _tree = new BST<int>();
        _tree.Add(94);
        _tree.Add(127);
        _tree.Add(34);
        _tree.Add(55);
        _tree.Add(13);
        _tree.Add(5);
        _tree.Add(56);
        _tree.Add(43);


        _bigTree = new BST<int>();
        var r = new Random();
        foreach (var _ in Enumerable.Range(0, 32))
        {
            try
            {
                _bigTree.Add(r.Next(0, 100));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    [Fact]
    public void BSTTests_TraversalAndList()
    {
        var tree = new BST<int>();
        tree.Add(5);
        var list = new List<int>();
        tree.Traverse(x => list.Add(x));
        var asList = tree.ToList();
        list.Should().Equal(asList);
    }

    [Fact]
    public void BSTTests_TraversalModes()
    {
        _tree.ToList(TraversalMode.PreOrder).Should().Equal(new List<int> {94, 34, 13, 5, 55, 43, 56, 127});
        _tree.ToList(TraversalMode.InOrder).Should().Equal(new List<int> {5, 13, 34, 43, 55, 56, 94, 127});
        _tree.ToList(TraversalMode.PostOrder).Should().Equal(new List<int> {5, 13, 43, 56, 55, 34, 127, 94});
        _tree.ToList().OrderBy(x => x).Should().Equal(_tree.ToList(TraversalMode.InOrder));
        _tree.ToOrderedList().Should().Equal(_tree.ToList(TraversalMode.InOrder));
    }

    [Fact]
    public void BSTTests_TraversalModes_BigTree()
    {
        _bigTree.ToList().OrderBy(x => x).Should().Equal(_bigTree.ToList(TraversalMode.InOrder));
        _bigTree.ToOrderedList().Should().Equal(_bigTree.ToList(TraversalMode.InOrder));
    }

    [Fact]
    public void BSTTests_Print()
    {
        var printed = _tree.Print();

        Debugger.Break();
    }

    [Fact]
    public void BSTTests_FindByValue()
    {
        var nodeA = _tree.FindNodeByValue(94);
        nodeA.Should().NotBeNull();
        nodeA!.IsRoot.Should().BeTrue();

        var nodeB = _tree.FindNodeByValue(500);
        nodeB.Should().BeNull();

        var nodeC = _tree.FindNodeByValue(5);
        nodeC.Should().NotBeNull();
        nodeC!.Value.Should().Be(5);

        nodeC.GetRoot().Value.Should().Be(94);
        nodeA.GetRoot().Should().BeEquivalentTo(nodeC.GetRoot());
    }

    [Fact]
    public void BSTTests_Contains()
    {
        _tree.Contains(94).Should().BeTrue();
        _tree.Contains(127).Should().BeTrue();
        _tree.Contains(43).Should().BeTrue();
        _tree.Contains(100).Should().BeFalse();
        _tree.Contains(0).Should().BeFalse();
    }

    [Fact]
    public void BSTTests_PredecessorAndSuccessor()
    {
        _tree.FindNodeByValue(34).GetInOrderPredecessor().Value.Should().Be(13);
        _tree.FindNodeByValue(34).GetInOrderSuccessor().Value.Should().Be(43);
        _tree.FindNodeByValue(94).GetInOrderSuccessor().Value.Should().Be(127);
        _tree.FindNodeByValue(5).GetInOrderPredecessor().Should().BeNull();
        _tree.FindNodeByValue(5).GetInOrderSuccessor().Should().BeNull();
    }

    [Fact]
    public void BSTTests_PropertyTests()
    {
        _tree.FindNodeByValue(5).IsLeaf.Should().BeTrue();
        _tree.FindNodeByValue(43).IsLeaf.Should().BeTrue();
        _tree.FindNodeByValue(56).IsLeaf.Should().BeTrue();
        _tree.FindNodeByValue(127).IsLeaf.Should().BeTrue();
        _tree.FindNodeByValue(34).IsLeaf.Should().BeFalse();

        _tree.FindNodeByValue(94).IsRoot.Should().BeTrue();
        _tree.FindNodeByValue(127).IsRoot.Should().BeFalse();

        _tree.FindNodeByValue(13).IsLeftNode.Should().BeTrue();
        _tree.FindNodeByValue(13).IsRightNode.Should().BeFalse();
        _tree.FindNodeByValue(56).IsRightNode.Should().BeTrue();
        _tree.FindNodeByValue(13).IsLeftNode.Should().BeTrue();
    }

    [Fact]
    public void BSTTests_Remove_Leaf()
    {
        _tree.Remove(5).Should().Be(true);
        _tree.FindNodeByValue(13).LeftNode.Should().BeNull();
        _tree.FindNodeByValue(5).Should().BeNull();
    }

    [Fact]
    public void BSTTests_Remove_OneChild_LeftNode()
    {
        _tree.Remove(13).Should().BeTrue();
        _tree.FindNodeByValue(5).Parent.Value.Should().Be(34);
        _tree.FindNodeByValue(34).LeftNode.Value.Should().Be(5);
        _tree.ToList().Should().NotContain(13);
        
    }
    
    [Fact]
    public void BSTTests_Remove_TwoChild_Root()
    {
        // TODO: temp solution to make it easier
        _tree.Remove(94).Should().BeFalse();
    }
    
    [Fact]
    public void BSTTests_Remove_TwoChild()
    {
        _tree.Remove(34).Should().BeTrue();
        _tree.FindNodeByValue(94).LeftNode.Value.Should().Be(43);
        _tree.FindNodeByValue(55).LeftNode.Should().BeNull();
        _tree.FindNodeByValue(13).Parent.Value.Should().Be(43);
    }
}