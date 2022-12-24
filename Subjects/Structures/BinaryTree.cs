namespace Subjects.Structures;

public class BinaryTreeNode<T>
{
    public required T Value { get; init; }
    public  BinaryTreeNode<T>? Parent { get; set; }
    public BinaryTreeNode<T>? LeftNode { get; set; }  
    public BinaryTreeNode<T>? RightNode { get; set; }

    public BinaryTreeNode<T> AddAsLeftNode(BinaryTreeNode<T> nodeToAdd)
    {
        LeftNode = nodeToAdd;
        return nodeToAdd;
    }
    public BinaryTreeNode<T> AddAsRightNode(BinaryTreeNode<T> nodeToAdd)
    {
        nodeToAdd.Parent ??= this;
        RightNode = nodeToAdd;
        return nodeToAdd;
    }
}


public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }
    public int Count { get; set; }

    public void TraversePreOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            result.Add(currentNode.Value);
            TraversePreOrder(result, currentNode.LeftNode );
            TraversePreOrder(result, currentNode.RightNode );
        }
    }

    public void TraverseInOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraversePreOrder(result, currentNode.LeftNode );
            result.Add(currentNode.Value);
            TraversePreOrder(result, currentNode.RightNode );
        }
    }

    public void TraversePostOrder(List<T> result, BinaryTreeNode<T>? currentNode)
    {
        if (currentNode is not null && currentNode.Value is not null)
        {
            TraversePreOrder(result, currentNode.LeftNode );
            TraversePreOrder(result, currentNode.RightNode );
            result.Add(currentNode.Value);
        }

    }
}