using System.Diagnostics;

namespace Subjects.Structures;




public class BasicTree<T>
{
    public TreeNode<T> Root;

    public BasicTree(TreeNode<T> root)
    {
        Root = root;
    }

    public void AddChildByValue(T searchValue, TreeNode<T> treeNode, TreeNode<T>? searchingNode = null)
    {
        searchingNode ??= Root;
        if (searchValue == null) return;

        if (searchValue.Equals(searchingNode.Value)) searchingNode.AddChild(treeNode);

        foreach (var child in searchingNode.Children)
        {
            AddChildByValue(searchValue, treeNode, child);
        }

    }
    
    
    public void AddChildByValue(Func<T, bool> predicate, TreeNode<T> treeNodeAdding, TreeNode<T>? searchingNode = null)
    {
        searchingNode ??= Root;
        
        var matched = searchingNode.Value != null && predicate(searchingNode.Value);
        if (matched) searchingNode.AddChild(treeNodeAdding);
        else
        {
            foreach (var child in searchingNode.Children)
            {
                AddChildByValue(predicate, treeNodeAdding, child);
            }
        }

    }

    public TreeNode<T>? FindNodeByValue(T searchValue, TreeNode<T>? searchingNode = null)
    {
        
        searchingNode ??= Root;
        if (searchValue == null) return null;

        if (searchValue.Equals(searchingNode.Value)) return searchingNode;

        return searchingNode.Children
            .Select(child => FindNodeByValue(searchValue, child))
            .FirstOrDefault(found => found is not null);
    }
}

public class TreeNode<T>
{
    public readonly TreeNode<T>? Parent = null;
    public List<TreeNode<T>> Children = new();
    public T? Value = default;

    public TreeNode(T value, TreeNode<T>? parent = null)
    {
        if (parent is not null) Parent = parent;
        Value = value;
    }

    public void AddChild(TreeNode<T> childToAdd)
    {
        Children.Add(childToAdd);
    } 
    
    public void AddChildren(List<TreeNode<T>> childrenToAdd)
    {
        Children = new List<TreeNode<T>>(Children.Concat(childrenToAdd));
    } 
}