using System.Diagnostics;

namespace Subjects.LeetCode;

public class Tribonacci
{
    private static Dictionary<int, int?> _memo = new();
    public static int ByN(int n)
    {
        if (n is 0) return 0;
        if (n is 1 or 2) return 1;

        _memo.TryGetValue(n, out var v);
        if (v is not null)
            return v.Value;

        var res = ByN(n - 1) + ByN(n - 2) + ByN(n - 3);
        _memo.Add(n, res);
        return res;
    }
}