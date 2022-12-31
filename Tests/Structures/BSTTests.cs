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
    public void BSTTests_TraversalModes()
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

        tree.ToList(TraversalMode.PREORDER).Should().Equal(new List<int> { 94, 34, 13, 5, 55, 43, 56, 127 });
        tree.ToList(TraversalMode.INORDER).Should().Equal(new List<int> { 5, 13, 34, 43, 55, 56, 94, 127 });
        tree.ToList(TraversalMode.POSTORDER).Should().Equal(new List<int> { 5, 13, 43, 56, 55, 34, 127, 94});
        tree.ToList().OrderBy(x => x).Should().Equal(tree.ToList(TraversalMode.INORDER));
        tree.ToOrderedList().Should().Equal(tree.ToList(TraversalMode.INORDER));
    }

    [Fact]
    public void BSTTests_Print()
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