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
}