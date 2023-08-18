namespace Subjects.LeetCode;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class FindNodeGroup
{
    public static TreeNode? FindNode(TreeNode node, int key)
    {
        if (node?.val == null) return null;
        if (node.val == key) return node;
        if (node.val < key && node.right != null) return FindNode(node.right, key);
        if (node.val > key && node.left != null) return FindNode(node.left, key);
        return null;
    }
}