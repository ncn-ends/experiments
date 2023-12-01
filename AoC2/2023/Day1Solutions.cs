using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day1Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                      two1nine
                      eightwothree
                      abcone2threexyz
                      xtwone3four
                      4nineeightseven2
                      zoneight234
                      7pqrstsixteen
                      """;

        var input = AocInputHandler.ImportHttp();

        var res1 = DoPart1(input);
        TestContext.Out.WriteLine(res1);

        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
    }

    private static int DoPart1(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var nums = new List<int>();
            var asd = line.ToCharArray();
            foreach (var c in asd)
            {
                if (int.TryParse(c.ToString(), out var parsed))
                {
                    nums.Add(parsed);
                }
            }

            var num = $"{nums.First()}{nums.Last()}".ToInt();
            total += num;
        });

        return total;
    }

    private static int DoPart2(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var numInds = new List<(int ind, int val)>();

            numInds.AddRange(line.GetAllIndexesOf("one").Select(x => (x, 1)));
            numInds.AddRange(line.GetAllIndexesOf("two").Select(x => (x, 2)));
            numInds.AddRange(line.GetAllIndexesOf("three").Select(x => (x, 3)));
            numInds.AddRange(line.GetAllIndexesOf("four").Select(x => (x, 4)));
            numInds.AddRange(line.GetAllIndexesOf("five").Select(x => (x, 5)));
            numInds.AddRange(line.GetAllIndexesOf("six").Select(x => (x, 6)));
            numInds.AddRange(line.GetAllIndexesOf("seven").Select(x => (x, 7)));
            numInds.AddRange(line.GetAllIndexesOf("eight").Select(x => (x, 8)));
            numInds.AddRange(line.GetAllIndexesOf("nine").Select(x => (x, 9)));

            var asd = line.ToCharArray();
            for (var i = 0; i < asd.Length; i++)
            {
                var c = asd[i];
                if (int.TryParse(c.ToString(), out var parsed))
                {
                    numInds.Add((i, parsed));
                }
            }

            var ordered = numInds.OrderBy(x => x.ind);
            var num = $"{ordered.First().val}{ordered.Last().val}".ToInt();

            total += num;
        });

        return total;
    }

    private static IEnumerable<int> GetAllIndexesOf(this string s, string target)
    {
        if (target.Length > s.Length) return Enumerable.Empty<int>();

        return Enumerable.Range(0, s.Length - target.Length + 1)
                         .Where(i => s.Substring(i, target.Length).Equals(target));

    }
}