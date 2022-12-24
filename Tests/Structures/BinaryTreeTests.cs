using System;
using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Subjects.Structures;
using Xunit;

namespace Tests.LeetCode;

public class BinaryTreeTests
{
    private BinaryTree<int> _binaryTreeOfInts;

    public BinaryTreeTests()
    {
        var rootNode = new BinaryTreeNode<int>
        {
            Value = 5,
            Parent = null,
        };

        var rootLeftNode = rootNode.AddAsLeftNode(new BinaryTreeNode<int>
        {
            Value = 3,
        });
        var rootLeftNodeLeftNode = rootLeftNode.AddAsLeftNode(new BinaryTreeNode<int>
        {
            Value = 9,
        });
        var rootRightNode = rootNode.AddAsRightNode(new BinaryTreeNode<int>
        {
            Value = 0,
        });
        _binaryTreeOfInts = new BinaryTree<int>
        {
            Root = rootNode,
            Count = 1
        };
    }

    [Fact]
    public void BinaryTreeOfInts()
    {
        var tree = _binaryTreeOfInts;
        
        var preOrderResults = new List<int>();
        tree.TraversePreOrder(preOrderResults, tree.Root);
        preOrderResults.Should().Equal(new List<int> {5, 3, 9, 0});

        var inOrderResults = new List<int>();
        tree.TraverseInOrder(inOrderResults, tree.Root);
        inOrderResults.Should().Equal(new List<int> {3, 9, 5, 0});
        inOrderResults.Should().NotEqual(preOrderResults);
        
        var postOrderResults = new List<int>();
        tree.TraversePostOrder(postOrderResults, tree.Root);
        postOrderResults.Should().Equal(new List<int> {3, 9, 0, 5});
        postOrderResults.Should().NotEqual(preOrderResults);
        postOrderResults.Should().NotEqual(inOrderResults);
    }

    
    // public class Person
    // {
    //     public int Id { get; set; } = -1;
    //     public string Name { get; set; } = "UNSET NAME";
    //     public string Title { get; set; } = "UNSET TITLE";
    // }
    //
    // [Fact]
    // public void BasicTreeOfPersons()
    // {
    //     var tree = new Tree<Person>(new TreeNode<Person>(new Person
    //     {
    //         Id = 1,
    //         Name = "Marcin Jamro",
    //         Title = "CEO"
    //     }));
    //     
    //     tree.AddChildByValue(p => p.Id == 1, new TreeNode<Person>(new Person
    //     {
    //         Id = 2,
    //         Name = "John Smith",
    //         Title = "Head of Development"
    //     }));
    //     
    //     tree.Root.Children.Count.Should().Be(1);
    // }
}