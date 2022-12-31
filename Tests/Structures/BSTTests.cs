using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using Subjects.LeetCode;
using Subjects.Structures;
using Xunit;

namespace Tests.LeetCode;

public class BSTTests
{
    private BinaryTree<int> _binaryTreeOfInts;

    // public BSTTests()
    // {
    //     var rootNode = new BinaryTreeNode<int>
    //     {
    //         Value = 5,
    //         Parent = null,
    //     };
    //
    //     var rootLeftNode = rootNode.AddAsLeftNode(new BinaryTreeNode<int>
    //     {
    //         Value = 3,
    //     });
    //     var rootLeftNodeLeftNode = rootLeftNode.AddAsLeftNode(new BinaryTreeNode<int>
    //     {
    //         Value = 9,
    //     });
    //     var rootLeftNodeLeftNodeLeftNode = rootLeftNodeLeftNode.AddAsLeftNode(new BinaryTreeNode<int>
    //     {
    //         Value = 4,
    //     });
    //     var rootRightNode = rootNode.AddAsRightNode(new BinaryTreeNode<int>
    //     {
    //         Value = 0,
    //     });
    //     _binaryTreeOfInts = new BinaryTree<int>
    //     {
    //         Root = rootNode,
    //         Count = 1
    //     };
    // }

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
    public void BSTTests_BasicAdd()
    {
        var tree = new BST<int>();
        tree.Add(94);
        tree.Add(127);
        tree.Add(34);
        tree.Add(55);
        tree.Add(13);
        tree.Add(5);
        tree.Add(56);
        tree.Add(43);

        var printed = tree.Print();
        Debugger.Break();
    }
}