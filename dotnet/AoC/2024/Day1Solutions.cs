using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2024;

public static class Day1Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       3   4
                       4   3
                       2   5
                       1   3
                       3   9
                       3   3
                       """;
        var expected1 = 11;

        var example2 = """
                       3   4
                       4   3
                       2   5
                       1   3
                       3   9
                       3   3
                       """;
        var expected2 = 31;


        // Assert.That(DoPart1(example1), Is.EqualTo(expected1));
        //
        var input = AocHandler.ImportHttp();
        //
        // var res1 = DoPart1(input);
        // TestContext.Out.WriteLine(res1);

        Assert.That(DoPart2(example2), Is.EqualTo(expected2));
        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
        // TestContext.Out.WriteLine(DoPart1(input));

        // AocHandler.SubmitSolution(res2, AocSolutionPart.Part2);
    }

    private static int DoPart1(string input)
    {
        List<int> leftList = [];
        List<int> rightList = [];

        input.IterateOnEachLine(line =>
        {
            var nums = line.ExtractNumbers();
            leftList.Add(nums[0].val);
            rightList.Add(nums[1].val);
        });
        leftList.Sort();
        rightList.Sort();
        List<int> distances = [];
        for (var i = 0; i < leftList.Count; i++)
        {
            var left = leftList[i];
            var right = rightList[i];
            distances.Add(Math.Abs(left - right));
        }

        return distances.Sum();
    }

    private static int DoPart2(string input)
    {
        List<int> leftList = [];
        Dictionary<int, int> frequency = new();
        input.IterateOnEachLine(line =>
        {
            var nums = line.ExtractNumbers();
            leftList.Add(nums[0].val);

            if (!frequency.TryAdd(nums[1].val, 1))
            {
                frequency[nums[1].val] += 1;
            }
        });

        var similarityScore = 0;
        foreach (var num in leftList)
        {
            var freq = frequency.GetValueOrDefault(num, 0);

            similarityScore += num * freq;
        }

        return similarityScore;
    }
}