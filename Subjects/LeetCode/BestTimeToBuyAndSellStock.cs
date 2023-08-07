namespace Subjects.LeetCode;

public class BestTimeToBuyAndSellStock
{
    /*
     * set diff as 0, representing the highest diff found between 2 days
     * for each item (p) in the array (prices):
     *      - if p is smaller than diff, continue
     *      - find the highest p that comes later (max)
     *         - subtract the difference and if higher than (diff), mutate diff to this value
     *
     * possible optimizations:
     *      - sort the proceeding list for each iteration
     */

    public int Do(int[] prices)
    {
        var diff = 0;
        for (var i = 0; i < prices.Length; i++)
        {
            var p = prices[i];
            var highest = FindHighestByPos(prices, i);
            var cDiff = highest - p;
            if (cDiff > diff) diff = cDiff;
        }

        return diff;
    }

    private Dictionary<int, int> _memo = new();

    private int FindHighestByPos(IEnumerable<int> prices, int pos)
    {
        _memo.TryGetValue(pos, out var foundValue);
        if (foundValue != default) return foundValue;

        var highestForPos = prices.Skip(pos).Max();
        _memo.Add(pos, highestForPos);
        return highestForPos;
    }
}