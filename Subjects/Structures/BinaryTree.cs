using System.Diagnostics;

namespace Subjects.Structures;

public class BinaryTreeNode<T>
{
    public required T Value { get; init; }
    public  BinaryTreeNode<T>? Parent { get; set; }
    public BinaryTreeNode<T>? LeftNode { get; set; }  
    public BinaryTreeNode<T>? RightNode { get; set; }

    public BinaryTreeNode<T> AddAsLeftNode(BinaryTreeNode<T> nodeToAdd)
    {
        nodeToAdd.Parent ??= this;
        LeftNode = nodeToAdd;
        return nodeToAdd;
    }
    public BinaryTreeNode<T> AddAsRightNode(BinaryTreeNode<T> nodeToAdd)
    {
        nodeToAdd.Parent ??= this;
        RightNode = nodeToAdd;
        return nodeToAdd;
    }

    public int GetHeight()
    {
        int GetHeightRecursive(BinaryTreeNode<T> currentNode)
        {
            Debugger.Break();
            if (currentNode.Parent is null) return 1;
            return GetHeightRecursive(currentNode.Parent) + 1;
        }

        return GetHeightRecursive(this);
    }

}

public enum TraversalModeEnum
{
    PREORDER,
    INORDER,
    POSTORDER
}
public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }
    public int Count { get; set; }

    private void TraversePreOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            result.Add(currentNode.Value);
            TraversePreOrder(result, currentNode.LeftNode );
            TraversePreOrder(result, currentNode.RightNode );
        }
    }

    private void TraverseInOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraverseInOrder(result, currentNode.LeftNode );
            result.Add(currentNode.Value);
            TraverseInOrder(result, currentNode.RightNode );
        }
    }

    private void TraversePostOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraversePostOrder(result, currentNode.LeftNode );
            TraversePostOrder(result, currentNode.RightNode );
            result.Add(currentNode.Value);
        }
    }

    public List<T> Traverse(TraversalModeEnum traversalMode)
    {
        var result = new List<T>();
        switch (traversalMode)
        {
            case TraversalModeEnum.PREORDER:
                TraversePreOrder(result, Root);
                break;
            case TraversalModeEnum.INORDER:
                TraverseInOrder(result, Root);
                break;
            case TraversalModeEnum.POSTORDER:
                TraversePostOrder(result, Root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(traversalMode), traversalMode, null);
        }

        return result;
    }

    // public void TraverseOn<R>(TraversalModeEnum traversalMode, Func<T, R> action)
    // {
    //     
    // }
}