using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day1Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       1abc2
                       pqr3stu8vwx
                       a1b2c3d4e5f
                       treb7uchet
                       """;

        var example2 = """
                      two1nine
                      eightwothree
                      abcone2threexyz
                      xtwone3four
                      4nineeightseven2
                      zoneight234
                      7pqrstsixteen
                      """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(142));

        var res1 = DoPart1(input);
        TestContext.Out.WriteLine(res1);

        Assert.That(DoPart2(example2), Is.EqualTo(281));

        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
        AocHandler.SubmitSolution(res2, AocSolutionPart.Part2);
    }

    private static int DoPart1(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var nums = line.ExtractDigits().Select(n => n.val);
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
            var numInds = line.ExtractDigits();

            numInds.AddRange(line.GetAllIndexesOf("one").Select(x => (1, x)));
            numInds.AddRange(line.GetAllIndexesOf("two").Select(x => (2, x)));
            numInds.AddRange(line.GetAllIndexesOf("three").Select(x => (3, x)));
            numInds.AddRange(line.GetAllIndexesOf("four").Select(x => (4, x)));
            numInds.AddRange(line.GetAllIndexesOf("five").Select(x => (5, x)));
            numInds.AddRange(line.GetAllIndexesOf("six").Select(x => (6, x)));
            numInds.AddRange(line.GetAllIndexesOf("seven").Select(x => (7, x)));
            numInds.AddRange(line.GetAllIndexesOf("eight").Select(x => (8, x)));
            numInds.AddRange(line.GetAllIndexesOf("nine").Select(x => (9, x)));

            var ordered = numInds.OrderBy(x => x.pos);
            var num = $"{ordered.First().val}{ordered.Last().val}".ToInt();

            total += num;
        });

        return total;
    }
}