using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Subjects.Structures.Trees;

public class BSTNode<T> where T : IComparable
{
    public T Value { get; set; } = default!;
    public BSTNode<T>? Parent { get; set; }
    public BSTNode<T>? LeftNode { get; set; }
    public BSTNode<T>? RightNode { get; set; }

    public bool IsRoot => Parent is null;

    [MemberNotNullWhen(true, nameof(Parent))]
    public bool IsLeaf => Parent is not null && LeftNode is null && RightNode is null;

    public bool IsLeftNode => Parent?.LeftNode is not null &&
                              Parent.LeftNode.Value.CompareTo(Value) == 0;

    public bool IsRightNode => Parent?.RightNode is not null &&
                               Parent.RightNode.Value.CompareTo(Value) == 0;

    public BSTNode<T> AddAsLeftNode(T value)
    {
        var nodeToAdd = new BSTNode<T>
        {
            Parent = this,
            Value = value
        };
        LeftNode = nodeToAdd;
        return nodeToAdd;
    }

    public BSTNode<T> AddAsRightNode(T value)
    {
        var nodeToAdd = new BSTNode<T>
        {
            Parent = this,
            Value = value
        };
        RightNode = nodeToAdd;
        return nodeToAdd;
    }

    public BSTNode<T> Add(T value)
    {
        var compared = value.CompareTo(Value);
        if (compared == 0) throw new ArgumentException("Duplicate value attempted to be added.");
        if (compared > 0)
        {
            if (RightNode is not null) return RightNode.Add(value);
            return AddAsRightNode(value);
        }

        if (LeftNode is not null) return LeftNode.Add(value);
        return AddAsLeftNode(value);
    }

    public BSTNode<T> GetRoot() => Parent is null ? this : Parent.GetRoot();

    public BSTNode<T>? GetInOrderPredecessor()
    {
        return LeftNode is null
            ? null
            : GetInOrderPredecessor(LeftNode);
    }

    public BSTNode<T>? GetInOrderSuccessor()
    {
        return RightNode is null
            ? null
            : GetInOrderSuccessor(RightNode);
    }

    private BSTNode<T> GetInOrderPredecessor(BSTNode<T> currentNode)
    {
        return currentNode.RightNode is not null
            ? GetInOrderPredecessor(currentNode.RightNode)
            : currentNode;
    }

    private BSTNode<T>? GetInOrderSuccessor(BSTNode<T> currentNode)
    {
        return currentNode.LeftNode is not null
            ? GetInOrderPredecessor(currentNode.LeftNode)
            : currentNode;
    }
}

public enum TraversalMode
{
    PreOrder,
    InOrder,
    PostOrder
}

public class BST<T> where T : IComparable
{
    public BSTNode<T>? Root { get; set; }

    public void Traverse(Action<T> action, TraversalMode mode = TraversalMode.PreOrder)
    {
        switch (mode)
        {
            case TraversalMode.PreOrder:
                TraversePreOrder(Root);
                return;
            case TraversalMode.InOrder:
                TraverseInOrder(Root);
                return;
            case TraversalMode.PostOrder:
                TraversePostOrder(Root);
                return;
        }

        void TraversePreOrder(BSTNode<T>? currentNode)
        {
            if (currentNode is not null)
            {
                action(currentNode.Value);
                TraversePreOrder(currentNode.LeftNode);
                TraversePreOrder(currentNode.RightNode);
            }
        }

        void TraverseInOrder(BSTNode<T>? currentNode)
        {
            if (currentNode is not null)
            {
                TraverseInOrder(currentNode.LeftNode);
                action(currentNode.Value);
                TraverseInOrder(currentNode.RightNode);
            }
        }

        void TraversePostOrder(BSTNode<T>? currentNode)
        {
            if (currentNode is not null)
            {
                TraversePostOrder(currentNode.LeftNode);
                TraversePostOrder(currentNode.RightNode);
                action(currentNode.Value);
            }
        }
    }

    public List<T> ToList(TraversalMode mode = TraversalMode.PreOrder)
    {
        var list = new List<T>();
        Traverse(x => list.Add(x), mode);
        return list;
    }

    public List<T> ToOrderedList()
    {
        return ToList(TraversalMode.InOrder);
    }

    public BSTNode<T> Add(T data)
    {
        if (Root is not null) return Root.Add(data);

        Root = new BSTNode<T>
        {
            Value = data,
            Parent = null
        };
        return Root;
    }


    public BSTNode<T>? FindNodeByValue(T value)
    {
        BSTNode<T>? currentNode = Root;

        while (currentNode is not null)
        {
            var compared = value.CompareTo(currentNode.Value);
            if (compared == 0) return currentNode;
            if (compared < 0) currentNode = currentNode.LeftNode;
            else currentNode = currentNode.RightNode;
        }

        return null;
    }

    public bool Contains(T value) => FindNodeByValue(value) is not null;

    public string Print()
    {
        var lines = "graph TB;\n";
        var usedKeys = new HashSet<string>();

        PrintForNode(Root);
        return lines;

        void PrintForNode(BSTNode<T> node, string? key = null)
        {
            key ??= GetRandomCharacters();
            var rootOnlyField = node.IsRoot ? $"(({node.Value}))" : "";
            if (node.LeftNode is not null)
            {
                var leftKey = GetRandomCharacters();
                lines += $"{key}{rootOnlyField}-->{leftKey}(({node.LeftNode.Value}))\n";
                PrintForNode(node.LeftNode, leftKey);
            }

            if (node.RightNode is not null)
            {
                var rightKey = GetRandomCharacters();
                lines += $"{key}{rootOnlyField}-->{rightKey}(({node.RightNode.Value}))\n";
                PrintForNode(node.RightNode, rightKey);
            }
        }

        string GetRandomCharacters()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var toReturn = "";
            for (int i = 0; i < 3; i++)
            {
                Random rand = new Random();
                int num = rand.Next(0, text.Length);
                toReturn += text[num];
            }

            if (usedKeys.Contains(toReturn)) return GetRandomCharacters();
            usedKeys.Add(toReturn);
            return toReturn;
        }
    }

    /*
     * Remove = (data: T) => bool;
     *      
     * find node to delete
     *      - compare nodeToDelete to currentNode
     *          - if equal to 0, found node
     *          - if less than 0, check leftNode
     *              - if leftNode is not null, go to leftNode
     *              - if leftNode is null, return false
     *          - if greater than 0, check rightNode
     *              - if rightNode is not null, go to rightNode
     *              - if rightNode is null, return false
     * root
     *      - traits
     *          - parent is null
     *          - has 2 children
     *      - implementation 
     *          - 
     * leaf
     *      - traits
     *          - parent is not null
     *          - leftNode and rightNode are both null
     *      - implementation 
     *          - just remove node
     * node
     *      - traits
     *          - parent is not null
     *          - leftNode might be null
     *          - rightNode might be null
     *      - implementation
     *          - 
     */
    public bool Remove(T data)
    {
        var node = FindNodeByValue(data);

        if (node is null) return false;

        if (node.IsLeaf)
        {
            if (node.IsLeftNode)
            {
                node.Parent.LeftNode = null;
                return true;
            }

            if (node.IsRightNode)
            {
                node.Parent.RightNode = null;
                return true;
            }
        }

        if (node.RightNode is null && node.LeftNode is not null)
        {
            node.LeftNode.Parent = node.Parent;
            
            if (node.Parent is null) node.LeftNode.Parent = null;
            else if (node.IsLeftNode) node.Parent.LeftNode = node.LeftNode;
            else node.Parent.RightNode = node.LeftNode;
            
            return true;
        }

        if (node.RightNode is not null && node.LeftNode is null)
        {
            node.RightNode.Parent = node.Parent;
            
            if (node.Parent is null) node.RightNode.Parent = null;
            else if (node.IsLeftNode) node.Parent.LeftNode = node.LeftNode;
            else node.Parent.RightNode = node.LeftNode;

            return true;
        }

        var p = node.GetInOrderPredecessor();
        var s = node.GetInOrderSuccessor();

        if (p is null || s is null) return false;

        // TODO: make difference operation to decide whether to use predecessor or successor
        // TODO: make it so that you can delete root
        
        if (node.IsRoot) return false;

        s.LeftNode = node.LeftNode;
        s.RightNode = node.RightNode;
        node.LeftNode!.Parent = s;
        node.RightNode!.Parent = s;

        if (node.IsLeftNode) node.Parent!.LeftNode = s;
        else if (node.IsRightNode) node.Parent!.RightNode = s;

        if (s.IsLeftNode) s.Parent!.LeftNode = null;
        else if (s.IsRightNode) s.Parent!.RightNode = null;

        return true;
    }
}