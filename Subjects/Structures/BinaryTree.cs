using System.Diagnostics;

namespace Subjects.Structures;

public interface IBinaryTreeNode<T>
{
    T Value { get; init; }
    IBinaryTreeNode<T>? Parent { get; set; }
    IBinaryTreeNode<T>? LeftNode { get; set; }
    IBinaryTreeNode<T>? RightNode { get; set; }
    IBinaryTreeNode<T> AddAsLeftNode(IBinaryTreeNode<T> nodeToAdd);
    IBinaryTreeNode<T> AddAsRightNode(IBinaryTreeNode<T> nodeToAdd);
    int GetHeight();
}

public class BinaryTreeNode<T> : IBinaryTreeNode<T>
{
    public required T Value { get; init; }
    public  IBinaryTreeNode<T>? Parent { get; set; }
    public IBinaryTreeNode<T>? LeftNode { get; set; }  
    public IBinaryTreeNode<T>? RightNode { get; set; }

    public IBinaryTreeNode<T> AddAsLeftNode(IBinaryTreeNode<T> nodeToAdd)
    {
        nodeToAdd.Parent ??= this;
        LeftNode = nodeToAdd;
        return nodeToAdd;
    }
    public IBinaryTreeNode<T> AddAsRightNode(IBinaryTreeNode<T> nodeToAdd)
    {
        nodeToAdd.Parent ??= this;
        RightNode = nodeToAdd;
        return nodeToAdd;
    }

    public int GetHeight()
    {
        int GetHeightRecursive(IBinaryTreeNode<T> currentNode)
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
    public BinaryTreeNode<T>? Root { get; set; }
    public int Count { get; set; }

    private void TraversePreOrder(List<T> result, IBinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            result.Add(currentNode.Value);
            TraversePreOrder(result, currentNode.LeftNode );
            TraversePreOrder(result, currentNode.RightNode );
        }
    }

    private void TraverseInOrder(List<T> result, IBinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraverseInOrder(result, currentNode.LeftNode );
            result.Add(currentNode.Value);
            TraverseInOrder(result, currentNode.RightNode );
        }
    }

    private void TraversePostOrder(List<T> result, IBinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraversePostOrder(result, currentNode.LeftNode );
            TraversePostOrder(result, currentNode.RightNode );
            result.Add(currentNode.Value);
        }
    }

    public List<T> Traverse(TraversalModeEnum traversalMode = TraversalModeEnum.PREORDER)
    {
        Debugger.Break();
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