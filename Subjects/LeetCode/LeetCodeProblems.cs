using System.Text;

namespace Subjects.LeetCode;

public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int x)
    {
        val = x;
        next = null;
    }
}

public class Node {
    public int val;
    public Node next;
    public Node random;

    public Node(int _val) {
        val = _val;
        next = null;
        random = null;
    }
}



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
            if (leftSequence[i] != rightSequence[i])
                return false;

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

    /* https://leetcode.com/problems/path-sum/ */
    static bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root is null) return false;
        return Do(root, targetSum);

        bool Do(TreeNode node, int sumLeft)
        {
            var sum = sumLeft - node.val;
            if (node.left is null && node.right is null)
            {
                if (sum == 0) return true;
                else return false;
            }


            if (node.left is not null)
            {
                var leftRes = Do(node.left, sum);
                if (leftRes) return true;
            }

            if (node.right is null) return false;
            var rightRes = Do(node.right, sum);
            if (rightRes) return true;
            return false;
        }
    }

    /* FIXME: failed to solve after 3 attempts / 15 min */
    /* https://leetcode.com/problems/extra-characters-in-a-string/ */
    static int MinExtraChar(string s, string[] dictionary)
    {
        var cleanedDict = dictionary.Where(d => s.Contains(d)).ToArray();
        var min = Int32.MaxValue;
        Do(s);
        return min;

        void Do(string str)
        {
            if (str.Length == 0)
            {
                min = 0;
                return;
            }

            bool changesHappened = false;
            foreach (var d in cleanedDict)
            {
                var di = str.IndexOf(d);
                if (di == -1) continue;
                changesHappened = true;
                var newStr = str.Remove(di, d.Length);
                Do(newStr);
            }

            if (!changesHappened && min > str.Length) min = str.Length;
        }
    }

    /* https://leetcode.com/problems/minimum-depth-of-binary-tree/ */
    static int MinDepth(TreeNode root)
    {
        if (root is null) return 0;
        var q = new Queue<(TreeNode node, int level)>();
        q.Enqueue((root, 1));
        var minLevel = Int32.MaxValue;

        while (q.Any())
        {
            var (node, level) = q.Dequeue();
            if (level > minLevel) continue;

            var isLeaf = node.left is null && node.right is null;
            if (isLeaf && level < minLevel) minLevel = level;

            if (node.left is not null) q.Enqueue((node.left, level + 1));
            if (node.right is not null) q.Enqueue((node.right, level + 1));
        }

        return minLevel;
    }


    /* https://leetcode.com/problems/kth-smallest-element-in-a-bst/ */
    static int KthSmallest(TreeNode root, int k)
    {
        if (root is null) return 0;
        var l = new List<int>();
        Do(root);
        return l[k - 1];

        void Do(TreeNode node)
        {
            if (node.left is not null) Do(node.left);
            if (l.Count == k) return;
            l.Add(node.val);
            if (node.right is not null) Do(node.right);
        }
    }

    /* https://leetcode.com/problems/binary-tree-right-side-view/ */
    static IList<int> RightSideView(TreeNode root)
    {
        if (root is null) return new List<int>();

        var q = new Queue<(TreeNode node, int level)>();
        q.Enqueue((root, 1));
        var dict = new Dictionary<int, int>(); // level, value

        while (q.Any())
        {
            var (node, level) = q.Dequeue();

            dict.TryAdd(level, node.val);

            if (node.right is not null) q.Enqueue((node.right, level + 1));
            if (node.left is not null) q.Enqueue((node.left, level + 1));
        }

        return dict.Select(x => x.Value).ToList();
    }

    /* https://leetcode.com/problems/linked-list-cycle/ */
    static bool HasCycle(ListNode head)
    {
        if (head == null || head.next == null) return false;
        var slowP = head;
        var fastP = head.next;

        while (fastP != null && slowP != null && fastP.next != null)
        {
            if (fastP == slowP) return true;
            slowP = slowP.next;
            fastP = fastP.next.next;
        }

        return false;
    }

    /* https://leetcode.com/problems/unique-paths-iii/ */
    static int UniquePathsIII(int[][] grid)
    {
        var allPaths = new List<HashSet<(int y, int x)>>();

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 1) Do(new HashSet<(int y, int x)>(), (y, x));
            }
        }

        var max = grid.Sum(x => x.Count(y => y != -1));
        return allPaths.Count(x => x.Count == max);

        void Do(HashSet<(int y, int x)> path, (int y, int x) pointCoords)
        {
            var (y, x) = pointCoords;
            var point = grid[y][x];
            if (point == -1) return;
            if (point == 2)
            {
                allPaths.Add(path.Concat(new[] {(y, x)}).ToHashSet());
                return;
            }
            if (path.Contains((y, x))) return;

            var newPath = path.Concat(new[] {(y, x)}).ToHashSet();

            if (y > 0) Do(newPath, (y - 1, x));
            if (x > 0) Do(newPath, (y, x - 1));
            if (y < grid.Length - 1) Do(newPath, (y + 1, x));
            if (x < grid[y].Length - 1) Do(newPath, (y, x + 1));
        }
    }

    /* https://leetcode.com/problems/copy-list-with-random-pointer/ */
    /* TODO: come back to this, make sure you can implement it again */
    static Node CopyRandomList(Node head)
    {
        if (head is null) return null;

        var map = new Dictionary<Node, Node>();
        var node = head;

        while (node is not null)
        {
            var copy = new Node(node.val);
            map[node] = copy;
            node = node.next;
        }

        node = head;
        while (node is not null)
        {
            var copy = map[node];
            if (node.next is null) copy.next = null;
            else copy.next = map[node.next];
            if (node.random is null) copy.random = null;
            else copy.random = map[node.random];

            node = node.next;
        }

        return map[head];
    }


    /* https://leetcode.com/problems/split-linked-list-in-parts/ */
    /* TODO: optimize */
    static ListNode[] SplitListToParts(ListNode head, int k)
    {
        ListNode? curr = head;
        var vals = new Queue<int>();
        while (curr is not null)
        {
            vals.Enqueue(curr.val);
            curr = curr.next;
        }

        var asd = new int[k];
        var n3 = 0;
        for (int i = vals.Count; i > 0; i--)
        {
            asd[n3]++;
            if (++n3 == k) n3 = 0;
        }

        var listPositioning = asd.ToList();
        var finalList = new ListNode[k];
        while (vals.Count > 0)
        {
            var pos = listPositioning.FindIndex(x => x > 0);

            var node = vals.Dequeue();
            if (finalList[pos] is null) finalList[pos] = new ListNode(node);
            else
            {
                var c = finalList[pos];
                while (c.next is not null) c = c.next;
                c.next = new ListNode(node);
            }

            listPositioning[pos]--;
        }

        return finalList;
    }

    /* https://leetcode.com/problems/reverse-linked-list-ii/ */
    /* FIXME: incomplete, need to fix */
    static ListNode ReverseBetween(ListNode head, int left, int right)
    {
        if (head is null) return head;

        var dict = new Dictionary<ListNode, ListNode>();
        var p = 1;
        var c = head;
        while (p <= right)
        {
            if (p >= left && p <= right)
            {
                // dict[]
            }
            c = c.next;
            p++;
        }

        p = 1;
        while (c is not null)
        {
            if (p + 1 >= left && p + 1 <= right)
            {

            }

            c = c.next;
        }



        return head;
    }


    /* https://leetcode.com/problems/pascals-triangle/ */
    static IList<IList<int>> Generate(int numRows)
    {
        var l = new List<IList<int>>();
        if (numRows == 0) return l;
        l.Add(new List<int>(){ 1 });
        if (numRows == 1) return l;
        l.Add(new List<int>(){ 1, 1 });
        if (numRows == 2) return l;

        for (int i = 2; i < numRows; i++)
        {
            l.Add(new List<int>() { 1 });
            for (int j = 1; j < i; j++)
            {
                var left = l[i - 1][j - 1];
                var right = l[i - 1][j];
                l[i].Add(left + right);
            }
            l[i].Add(1);
        }

        return l;
    }

    /* https://leetcode.com/problems/pascals-triangle-ii/ */
    /* TODO: to optimize */
    static IList<int> GetRow(int rowIndex)
    {
        var l = new List<IList<int>>();
        l.Add(new List<int> { 1 });
        l.Add(new List<int> { 1, 1 });
        if (rowIndex < 2) return l[rowIndex];

        for (int i = 2; i < rowIndex + 1; i++)
        {
            l.Add(new List<int>() { 1 });
            for (int j = 1; j < i; j++)
            {
                var left = l[i - 1][j - 1];
                var right = l[i - 1][j];
                l[i].Add(left + right);
            }
            l[i].Add(1);
        }

        return l[rowIndex];
    }

    /* https://leetcode.com/problems/combination-sum-iv/ */
    /* TODO: try bottom up approach */
    static int CombinationSum4(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        return Helper(target);

        int Helper(int remaining)
        {
            if (remaining == 0) return 1;
            if (remaining < 0) return 0;

            if (dict.ContainsKey(remaining)) return dict[remaining];

            var sum = 0;
            foreach (var num in nums)
                sum += Helper(remaining - num);

            dict[remaining] = sum;

            return sum;
        }
    }


/* https://leetcode.com/problems/maximum-average-subarray-i/ */
/* TODO: good practice problem */
    static double FindMaxAverage(int[] nums, int k)
    {
        var max = Double.MinValue;
        var winSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (i < k)
            {
                winSum += nums[i];
                max = winSum;
                continue;
            }

            winSum -= nums[i - k];
            winSum += nums[i];
            if (winSum > max) max = winSum;
        }

        return max / k;
    }

/* bad solution, O(n^2) */
    static double FindMaxAverage_Bad2(int[] nums, int k)
    {
        var q = new Queue<int>();
        var max = Double.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            q.Enqueue(nums[i]);
            if (q.Count < k) continue;
            var asd = q.Average();
            if (asd > max) max = asd;
            q.Dequeue();
        }

        return max;
    }


/* bad solution, time limit exceeded - but it's clean */
    static double FindMaxAverage_Bad(int[] nums, int k)
    {
        var max = Double.MinValue;
        for (int i = 0; i < nums.Length - k + 1; i++)
        {
            var asd = nums.Skip(i).Take(k).Average();
            if (asd > max) max = asd;
        }

        return max;
    }

    /* bad, doesn't work */
    static int[] DailyTemperatures(int[] temperatures)
    {
        var ret = new int[temperatures.Length];
        var s = new Stack<(int temp, int place)>();
        s.Push((temperatures[0], 0));

        for (var i = 1; i < temperatures.Length; i++)
        {
            if (s.Count == 0 || s.Peek().temp >= temperatures[i])
            {
                s.Push((temperatures[i], i));
                continue;
            }

            while (s.Count > 0 && s.Peek().temp < temperatures[i])
            {
                var asd = s.Pop();
                ret[asd.place] = i - asd.place;
            }
        }

        return ret;

    }

    /* TODO: could optimize somehow */
    static int MajorityElement(int[] nums)
    {
        var dict = new Dictionary<int, int>();

        foreach (var num in nums)
        {
            if (dict.ContainsKey(num)) dict[num]++;
            else dict[num] = 1;
        }

        return dict.MaxBy(x => x.Value).Key;
    }

    /* https://leetcode.com/problems/move-zeroes/ */
    static void MoveZeroes(int[] nums)
    {
        var p = 0;
        var z = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0) { z++; continue; }

            nums[p] = nums[i];
            p++;
        }

        for (int i = 0; i < z; i++)
        {
            nums[nums.Length - 1 - i] = 0;
        }
    }

    /* https://leetcode.com/problems/find-the-highest-altitude/ */
    static int LargestAltitude(int[] gain)
    {
        var h = 0;
        for (var i = 0; i < gain.Length; i++)
        {
            var last = i == 0 ? 0 : gain[i - 1];
            gain[i] = last + gain[i];
            if (h < gain[i]) h = gain[i];
        }

        return h;
    }

    /* https://leetcode.com/problems/find-pivot-index/ */
    static int PivotIndex(int[] nums)
    {
        var left = 0;
        var right = nums.Sum();
        for (var i = 0; i < nums.Length; i++)
        {
            left += nums[i];
            if (left == right) return i;
            right -= nums[i];
        }

        return -1;
    }

    /* https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/ */
    static int MaxVowels(string s, int k)
    {
        var set = new HashSet<char>()
        {
            'a', 'e', 'i', 'o', 'u'
        };

        var p1 = 0;
        var p2 = 0;
        var c = 0;
        var max = 0;

        while (p2 < k)
        {
            if (!set.Contains(s[p2])) p2++;
            else
            {
                p2++;
                c++;
                if (max < c) max = c;
            }
        }

        while (p2 < s.Length)
        {
            if (set.Contains(s[p1])) c--;
            if (set.Contains(s[p2])) c++;
            if (max < c) max = c;
            p1++;
            p2++;
        }

        return max;
    }

    /* https://leetcode.com/problems/min-stack/ */
    public class MinStack
    {
        private Stack<(int val, int min)> _internalStack = new Stack<(int val, int min)>();
        private int _internalMin = Int32.MaxValue;

        public MinStack() {

        }

        public void Push(int val)
        {
            if (val < _internalMin) _internalMin = val;
            _internalStack.Push((val, _internalMin));
        }

        public void Pop()
        {
            _internalStack.TryPop(out var asd);
            var passed = _internalStack.TryPeek(out var zxc);
            _internalMin = !passed ? Int32.MaxValue : zxc.min;
        }

        public int Top()
        {
            _internalStack.TryPeek(out var zxc);
            return zxc.val;
        }

        public int GetMin()
        {
            return _internalMin;
        }
    }

    /* https://leetcode.com/problems/unique-number-of-occurrences/ */
    static bool UniqueOccurrences(int[] arr)
    {
        var dict = new Dictionary<int, int>();

        foreach (var num in arr)
        {
            if (dict.ContainsKey(num)) dict[num]++;
            else dict[num] = 1;
        }

        return dict.Values.Count == dict.Values.ToHashSet().Count;
    }


    /* https://leetcode.com/problems/find-the-difference/ */
    static char FindTheDifference(string s, string t)
    {
        var dict = new Dictionary<char, int>();
        foreach (var c in t)
        {
            if (dict.ContainsKey(c)) dict[c]++;
            else dict[c] = 1;
        }

        foreach (var c in s) dict[c]--;

        return dict.First(x => x.Value > 0).Key;
    }

    /* https://leetcode.com/problems/sign-of-the-product-of-an-array/ */
    static int ArraySign(int[] nums)
    {
        var sign = 1;
        foreach (var num in nums)
        {
            if (num == 0) return 0;
            if (num < 0) sign *= -1;
        }

        return sign;
    }
}