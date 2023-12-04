using System.Diagnostics;
using System.Xml.Xsl;
using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day3Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                       467..114..
                       ...*......
                       ..35..633.
                       ......#...
                       617*......
                       .....+.58.
                       ..592.....
                       ......755.
                       ...$.*....
                       .664.598..
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example).Sum(), Is.EqualTo(4361));

        var res1 = DoPart1(input);
        // List<int> expectedGoods =
        // [
        //         31, 339, 669, 575, 964, 692, 415, 627, 945, 144, 506, 182, 873, 756, 737, 784, 667, 258, 741, 707, 84, 520, 579, 258, 274,
        //         739, 157, 580, 893, 696, 889
        // ];
        // for (var i = 0; i < expectedGoods.Count; i++)
        // {
        //     TestContext.Out.WriteLine(i);
        //     Assert.AreEqual(res1[i], expectedGoods[i]);
        // }

        Assert.That(res1.Sum(), Is.Not.EqualTo(546877));
        Assert.That(res1.Sum(), Is.Not.EqualTo(390512));
        TestContext.Out.WriteLine(res1.Sum());

        Assert.That(DoPart2(example), Is.EqualTo(467835));

        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
    }

    private static List<int> DoPart1(string input)
    {
        var nums = new List<(int num, int pos, int y)>();
        var symbols = new List<(int x, int y)>();
        var lines = input.SplitByLine();
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var numss = line.ExtractNumbers();
            nums.AddRange(numss.Select(x => (x.val, x.pos, i)));

            for (var k = 0; k < line.Length; k++)
            {
                var c = line[k];
                if (!c.ToString().IsInt() && c != '.') symbols.Add((k, i));
            }
        }

        var goodNums = new List<int>();
        foreach (var num in nums)
        {
            var nearSymbol = symbols.Any(symbol =>
            {
                var nearX = symbol.x >= num.pos - 1 && symbol.x < num.pos + num.num.ToString().Length + 1;
                var nearY = Math.Abs(symbol.y - num.y) <= 1;
                return nearX && nearY;
            });
            if (nearSymbol) goodNums.Add(num.num);
        }

        return goodNums;
    }

    private static int DoPart2(string input)
    {
        var nums = new List<(int num, int pos, int y)>();
        var symbols = new List<(char symbol, int x, int y)>();
        var lines = input.SplitByLine();
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var numss = line.ExtractNumbers();
            nums.AddRange(numss.Select(x => (x.val, x.pos, i)));

            for (var k = 0; k < line.Length; k++)
            {
                var c = line[k];
                if (!c.ToString().IsInt() && c != '.') symbols.Add((c, k, i));
            }
        }

        var gears = symbols.Where(x => x.symbol == '*');
        var total = 0;
        foreach (var gear in gears)
        {
            /* TODO: turn this into a helper, but need to make it work for ones that span multiple cells, diagonal, and multi distance */
            var closeNums = nums.Where(num =>
            {
                var nearX = gear.x >= num.pos - 1 && gear.x < num.pos + num.num.ToString().Length + 1;
                var nearY = Math.Abs(gear.y - num.y) <= 1;
                return nearX && nearY;
            }).ToList();
            if (closeNums.Count != 2) continue;
            total += closeNums[0].num * closeNums[1].num;
        }

        return total;
    }
}