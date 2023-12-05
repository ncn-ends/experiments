using System.Collections.Generic;
using FluentAssertions;
using Subjects.Structures.Trees;
using Xunit;

namespace Tests.Structures;

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
        var rootLeftNodeLeftNodeLeftNode = rootLeftNodeLeftNode.AddAsLeftNode(new BinaryTreeNode<int>
        {
            Value = 4,
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
    public void BasicTraversalBinaryTreeOfInts()
    {
        var tree = _binaryTreeOfInts;
        
        var preOrderResults = tree.Traverse(TraversalModeEnum.PREORDER);
        preOrderResults.Should().Equal(new List<int> {5, 3, 9, 4, 0});

        var inOrderResults = tree.Traverse(TraversalModeEnum.INORDER); 
        inOrderResults.Should().Equal(new List<int> {4, 9, 3, 5, 0});
        inOrderResults.Should().NotEqual(preOrderResults);

        var postOrderResults = tree.Traverse(TraversalModeEnum.POSTORDER);
        postOrderResults.Should().Equal(new List<int> {4, 9, 3, 0, 5});
        postOrderResults.Should().NotEqual(preOrderResults);
        postOrderResults.Should().NotEqual(inOrderResults);

        tree.Root.LeftNode!.LeftNode!.LeftNode!.GetHeight().Should().Be(4);
    }

    // [Fact]
    // public void TraversalWithActionBinaryTreeOfInts()
    // {
    //     var tree = _binaryTreeOfInts;
    //     // Func<int, bool> predicate =  
    //     
    //     tree.TraverseOn(TraversalModeEnum.PREORDER);
    //
    // }
    
}