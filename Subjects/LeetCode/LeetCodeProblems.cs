using System.Text;

namespace Subjects.LeetCode;

public class LeetCodeProblems
{
    public static int[] GetConcatenation(int[] nums)
    {
        var arr = new int[nums.Length * 2];
        for (int i = 0; i < nums.Length; i++)
        {
            arr[i] = nums[i];
            arr[i + nums.Length] = nums[i];
        }

        return arr;
    }

    static int[] BuildArray(int[] nums)
    {
        var l = new int[nums.Length];
        for (var i = 0; i < nums.Length; i++)
        {
            l[i] = nums[nums[i]];
        }

        return l;
    }

    /* https://leetcode.com/problems/contains-duplicate/description/ */
    static bool ContainsDuplicate(int[] nums)
    {
        var dict = new Dictionary<int, int?>();
        for (var i = 0; i < nums.Length; i++)
        {
            dict.TryGetValue(nums[i], out var val);
            if (val != null) return true;
            dict.Add(nums[i], i);
        }

        return false;
    }

    static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        return candies.Select(x => x + extraCandies >= candies.Max()).ToList();
    }

    static IList<string> FindRepeatedDnaSequences(string s)
    {
        var all = new HashSet<string>();
        var actual = new List<string>();
        var a = 0;
        for (var i = 0; i + 9 < s.Length; i++)
        {
            var win = s.Substring(a, 10);
            if (!all.Add(win)) actual.Add(win);
            a++;
        }

        return actual;
    }

    static IList<int> FindAnagrams(string s, string p)
    {
        var pCharArray = p.ToCharArray();
        Array.Sort(pCharArray);
        var pSorted = new string(pCharArray);

        var res = new List<int>();
        for (int i = 0; i <= s.Length - p.Length; i++)
        {
            var win = s.Substring(i, p.Length).ToCharArray();
            Array.Sort(win);
            var winSorted = new string(win);

            if (winSorted == pSorted) res.Add(i);
        }

        return res;
    }

    /* https://leetcode.com/problems/removing-stars-from-a-string/?envType=study-plan-v2&envId=leetcode-75 */
    static string RemoveStars(string s)
    {
        var retS = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0 && s[i] == '*') continue;
            if (s[i] == '*') retS.Remove(retS.Length - 1, 1);
            else retS.Append(s[i]);
        }

        return retS.ToString();
    }

    /* https://leetcode.com/problems/top-k-frequent-elements/description/ */
    static int[] TopKFrequent(int[] nums, int k)
    {
        var dict = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            dict.TryGetValue(num, out var exists);
            if (exists != default) dict[num]++;
            else dict.Add(num, 1);
        }

        return dict
            .OrderByDescending(x => x.Value)
            .Take(k)
            .Select(x => x.Key)
            .ToArray();
    }

    /* https://leetcode.com/problems/missing-number/description/ */
    static int MissingNumber(int[] nums)
    {
        int total = nums.Length * (nums.Length + 1) / 2;
        for (int i = 0; i < nums.Length; i++) total -= nums[i];
        return total;
    }

    /* https://leetcode.com/problems/flood-fill/ */
    static int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        var origin = image[sr][sc];
        var q = new Queue<(int x, int y)>();
        q.Enqueue((sr, sc));
        var visited = new HashSet<(int x, int y)>();
        while (q.Any())
        {
            var node = q.Dequeue();
            if (image[node.x][node.y] != origin) continue;
            image[node.x][node.y] = color;

            if (node.x > 0 && !visited.Contains((node.x - 1, node.y)))
            {
                visited.Add((node.x - 1, node.y));
                q.Enqueue((node.x - 1, node.y));
            }

            if (node.x < image.Length - 1 && !visited.Contains((node.x + 1, node.y)))
            {
                visited.Add((node.x + 1, node.y));
                q.Enqueue((node.x + 1, node.y));
            }

            if (node.y < image[node.x].Length - 1 && !visited.Contains((node.x, node.y + 1)))
            {
                visited.Add((node.x, node.y + 1));
                q.Enqueue((node.x, node.y + 1));
            }

            if (node.y > 0 && !visited.Contains((node.x, node.y - 1)))
            {
                visited.Add((node.x, node.y - 1));
                q.Enqueue((node.x, node.y - 1));
            }

            visited.Add(node);
        }

        return image;
    }


    /* https://leetcode.com/problems/deepest-leaves-sum/description/ */
    static int DeepestLeavesSum(TreeNode root)
    {
        var leavesByDepth = new Dictionary<int, List<int>>(); // depth, leaf values
        FindAndSetLeaves(root, 1);
        return leavesByDepth.MaxBy(x => x.Key).Value.Sum();

        void FindAndSetLeaves(TreeNode node, int depth)
        {
            if (node.left is null && node.right is null)
            {
                if (leavesByDepth.ContainsKey(depth)) leavesByDepth[depth].Add(node.val);
                else leavesByDepth[depth] = new List<int> {node.val};
                return;
            }

            if (node.left is not null)
                FindAndSetLeaves(node.left, depth + 1);
            if (node.right is not null)
                FindAndSetLeaves(node.right, depth + 1);
        }
    }


    /* https://leetcode.com/problems/longest-increasing-path-in-a-matrix/ */
    static int LongestIncreasingPath(int[][] matrix)
    {
        var memo = new Dictionary<(int y, int x), int>();
        var overallMax = 0;

        for (var y = 0; y < matrix.Length; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                Asd(y, x);
            }
        }

        return overallMax;

        int Asd(int y, int x)
        {
            var current = matrix[y][x];
            var paths = new List<int>();

            if (memo.ContainsKey((y, x))) return memo[(y, x)];

            if (x > 0 && matrix[y][x - 1] > current) paths.Add(Asd(y, x - 1));
            if (y > 0 && matrix[y - 1][x] > current) paths.Add(Asd(y - 1, x));
            if (y < matrix.Length - 1 && matrix[y + 1][x] > current) paths.Add(Asd(y + 1, x));
            if (x < matrix[0].Length - 1 && matrix[y][x + 1] > current) paths.Add(Asd(y, x + 1));

            var max = paths.Count > 0
                ? 1 + paths.Max()
                : 1;
            if (overallMax < max) overallMax = max;
            memo.Add((y, x), max);
            return max;
        }
    }


    /* https://leetcode.com/problems/same-tree/ */
    static bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p is null && q is not null ||
            p is not null && q is null) return false;

        if (p is null && q is null) return true;

        var pq = new Queue<TreeNode>();
        var qq = new Queue<TreeNode>();
        pq.Enqueue(p);
        qq.Enqueue(q);

        while (pq.Any())
        {
            var pc = pq.Dequeue();
            var qc = qq.Dequeue();

            if (pc.val != qc.val) return false;
            if (pc.left is not null && qc.left is null ||
                pc.left is null && qc.left is not null) return false;
            if (pc.left is not null)
            {
                pq.Enqueue(pc.left);
                qq.Enqueue(qc.left!);
            }

            if (pc.right is not null && qc.right is null ||
                pc.right is null && qc.right is not null) return false;
            if (pc.right is not null)
            {
                pq.Enqueue(pc.right);
                qq.Enqueue(qc.right!);
            }
        }

        return pq.Count == qq.Count;
    }


    /* https://leetcode.com/problems/max-area-of-island/ */
    static int MaxAreaOfIsland(int[][] grid)
    {
        var visited = new HashSet<(int y, int x)>();
        var maxArea = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                Explore(y, x);
            }
        }

        return maxArea;

        int Explore(int y, int x)
        {
            if (grid[y][x] == 0) return 0;
            if (visited.Contains((y, x))) return 0;
            visited.Add((y, x));

            var sum = 1;
            if (x > 0) sum += Explore(y, x - 1);
            if (y > 0) sum += Explore(y - 1, x);
            if (y < grid.Length - 1) sum += Explore(y + 1, x);
            if (x < grid[y].Length - 1) sum += Explore(y, x + 1);

            if (maxArea < sum) maxArea = sum;
            return sum;
        }
    }


    /* FIXME: incomplete, the list types were causing problems */
    /* https://leetcode.com/problems/binary-tree-level-order-traversal/ */
    static IList<IList<int>> LevelOrder(TreeNode root)
    {
        var q = new Queue<(TreeNode node, int level)>();
        q.Enqueue((root, 0));
        var result = new List<IList<int>>();

        while (q.Any())
        {
            var (node, level) = q.Dequeue();
            if (result.Count - 1 == level) result[level].Add(node.val);
            else result.Add(new List<int> {node.val});
            if (node.left is not null) q.Enqueue((node.left, level + 1));
            if (node.right is not null) q.Enqueue((node.right, level + 1));
        }

        return result;
    }

    /* https://leetcode.com/problems/maximum-depth-of-binary-tree */
    static int MaxDepth(TreeNode root)
    {
        if (root is null) return 0;
        var q = new Queue<(TreeNode node, int level)>();
        q.Enqueue((root, 1));
        var max = 1;
        while (q.Any())
        {
            var c = q.Dequeue();
            if (max < c.level) max = c.level;
            if (c.node.left is not null) q.Enqueue((c.node.left, c.level + 1));
            if (c.node.right is not null) q.Enqueue((c.node.right, c.level + 1));
        }

        return max;
    }

    /* https://leetcode.com/problems/leaf-similar-trees/ */
    static bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        var leftSequence = new List<int>();
        var rightSequence = new List<int>();

        Do(root1, leftSequence);
        Do(root2, rightSequence);

        if (leftSequence.Count != rightSequence.Count) return false;
        for (var i = 0; i < leftSequence.Count; i++)
            if (leftSequence[i] != rightSequence[i]) return false;

        return true;

        void Do(TreeNode node, IList<int> sequence)
        {
            if (node.left is null && node.right is null)
            {
                sequence.Add(node.val);
            }

            if (node.left is not null) Do(node.left, sequence);
            if (node.right is not null) Do(node.right, sequence);
        }
    }
}