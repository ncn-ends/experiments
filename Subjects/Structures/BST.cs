namespace Subjects.Structures;

public class BSTNode<T> where T : IComparable
{
    public T Value { get; set; }
    public BSTNode<T>? Parent { get; set; }
    public BSTNode<T>? LeftNode { get; set; }
    public BSTNode<T>? RightNode { get; set; }
    public bool IsRoot => Parent is null;

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
}

public enum TraversalMode
{
    PREORDER,
    INORDER,
    POSTORDER
}

public class BST<T> where T : IComparable
{
    public new BSTNode<T>? Root { get; set; }
    // public bool Contains(T data)
    // {
    //     BSTNode<T>? currentNode = Root;
    //
    //     while (currentNode is not null)
    //     {
    //         var compared = data.CompareTo(currentNode.Value);
    //         if (compared == 0) return true;
    //         if (compared > 1) currentNode = currentNode.RightNode;
    //         else if (compared < 1) currentNode = currentNode.LeftNode;
    //         else break;
    //     }
    //
    //     return false;
    // }


    public void Traverse(Action<T> action, TraversalMode mode = TraversalMode.PREORDER)
    {
        switch (mode)
        {
            case (TraversalMode.PREORDER):
                TraversePreOrder(Root);
                return;
            case TraversalMode.INORDER:
                TraverseInOrder(Root);
                return;
            case TraversalMode.POSTORDER:
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

    public List<T> ToList(TraversalMode mode = TraversalMode.PREORDER)
    {
        var list = new List<T>();
        Traverse(x => list.Add(x), mode);
        return list;
    }
    public List<T> ToOrderedList()
    {
        return ToList(TraversalMode.INORDER);
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

}