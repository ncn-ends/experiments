using System.Diagnostics;
using Utils.Extensions;

namespace AoC.Y2017;

// convert a list of list of numbers to an adjacency list
//      optional parameter "directed", if so, will make the first item the key
public static class Day12Solutions
{
    public static int SolvePart1()
    {
        var dict = new Dictionary<int, List<int>> ();

        AocInputHandler.ImportHttp().IterateOnEachLine((x, _) =>
        {
            var nums = x.ExtractNumbers(); // 10, 10, 129, 147, 1394
            for (var i = 0; i < nums.Count; i++)
            {
                var num = nums[i];
                // add num to the map as a key with an empty value
                if (!dict.ContainsKey(num)) dict.Add(num, []);
                for (var j = 0; j < nums.Count; j++)
                {
                    var num2 = nums[j];
                }
            }
            Debugger.Break();
        });
        return default;
    }
}