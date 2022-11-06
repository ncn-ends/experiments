using System.Diagnostics;

namespace Subjects.Structures;

public class BasicTree<T> where T : notnull
{
    public TreeNode<T> Root;
    private HashSet<T> addedValues = new();

    public BasicTree(TreeNode<T> root)
    {
        Root = root;
    }

    public bool AddChildByValue(T searchValue, TreeNode<T> treeNode, TreeNode<T>? searchingNode = null)
    {
        if (addedValues.Contains(treeNode.Value)) return false;
        
        searchingNode ??= Root;

        if (searchValue.Equals(searchingNode.Value))
        {
            searchingNode.AddChild(treeNode);
            return addedValues.Add(treeNode.Value);
        }

        foreach (var child in searchingNode.Children)
        {
            AddChildByValue(searchValue, treeNode, child);
        }

        return false;
    }
    
    
    public bool AddChildByValue(Func<T, bool> predicate, TreeNode<T> treeNodeAdding, TreeNode<T>? searchingNode = null)
    {
        if (addedValues.Contains(treeNodeAdding.Value)) return false;
        
        searchingNode ??= Root;
        
        
        var matched = predicate(searchingNode.Value);
        if (matched)
        {
            searchingNode.AddChild(treeNodeAdding);
            return addedValues.Add(treeNodeAdding.Value);
        }

        foreach (var child in searchingNode.Children)
        {
            AddChildByValue(predicate, treeNodeAdding, child);
        }

        return false;
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

public class TreeNode<T> where T : notnull
{
    public readonly TreeNode<T>? Parent = null;
    public List<TreeNode<T>> Children = new();
    public T Value;

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