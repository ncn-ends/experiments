namespace Subjects.LeetCode;

public class Fibonacci
{
    private Dictionary<int, int> _memo = new ();

    public int FindByN(int n)
    {
        if (n < 1)  throw new Exception();
        if (n is 1 or 2) return 1;

        _memo.TryGetValue(n, out var foundValue);
        if (foundValue != default) return foundValue;

        var res = FindByN(n - 1) + FindByN(n - 2);
        _memo.Add(n, res);
        return res;
    }

    public int FindByNWithNoMemo(int n)
    {
        if (n < 1)  throw new Exception();
        if (n is 1 or 2) return 1;

        var res = FindByN(n - 1) + FindByN(n - 2);
        return res;
    }
}