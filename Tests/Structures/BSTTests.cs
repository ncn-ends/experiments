using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Subjects.LeetCode;
using Subjects.Structures;
using Xunit;

namespace Tests.LeetCode;

public class BSTTests
{
    private readonly BST<int> _tree;

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
        _tree.ToList(TraversalMode.PREORDER).Should().Equal(new List<int> {94, 34, 13, 5, 55, 43, 56, 127});
        _tree.ToList(TraversalMode.INORDER).Should().Equal(new List<int> {5, 13, 34, 43, 55, 56, 94, 127});
        _tree.ToList(TraversalMode.POSTORDER).Should().Equal(new List<int> {5, 13, 43, 56, 55, 34, 127, 94});
        _tree.ToList().OrderBy(x => x).Should().Equal(_tree.ToList(TraversalMode.INORDER));
        _tree.ToOrderedList().Should().Equal(_tree.ToList(TraversalMode.INORDER));
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
}