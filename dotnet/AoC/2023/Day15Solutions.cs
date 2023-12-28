using Utils.Strings;


namespace AoC.Y2023;

public static class Day15Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(1320));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example1), Is.EqualTo(145));
        TestContext.Out.WriteLine(DoPart2(input));
    }


    [TestCase("HASH", 52)]
    [TestCase("pc", 3)]
    [TestCase("cm", 0)]
    [TestCase("qp", 1)]
    [TestCase("rn", 0)]
    public static void Test_HASHAlgo(string input, int expected)
    {
        Assert.That(expected, Is.EqualTo(HASHAlgo(input)));
    }

    private static int HASHAlgo(string s)
    {
        var toReturn = 0;
        foreach (var c in s)
        {
            toReturn += (int) c;
            toReturn *= 17;
            toReturn %= 256;
        }

        return toReturn;
    }

    private static int DoPart1(string input)
    {
        var codes = input.SplitBy([","]);
        return codes.Sum(HASHAlgo);
    }

    private static int DoPart2(string input)
    {
        var dict = new Dictionary<int, List<(string label, int lens)>>();
        for (int i = 0; i < 256; i++) dict.Add(i, []);

        var codes = input.SplitBy([","]);
        foreach (var code in codes)
        {
            var op = code.Contains('-')
                    ? "-"
                    : "=";
            var split = code.SplitBy(["=", "-"]);
            var label = split[0];
            var key = HASHAlgo(label);
            var existingIndexOfLabelAtBox = dict[key].FindIndex(x => x.label == label);
            if (op == "=")
            {
                var lensPower = code.ExtractNumbers()[0].val;

                if (existingIndexOfLabelAtBox == -1)
                    dict[key].Add((label, lensPower));
                else
                    dict[key][existingIndexOfLabelAtBox] = (label, lensPower);
            }
            else if (op == "-")
            {
                if (existingIndexOfLabelAtBox == -1) continue;
                dict[key].RemoveAt(existingIndexOfLabelAtBox);
            }
        }

        var total = 0;
        foreach (var (boxNumber, lenses) in dict)
        {
            for (var i = 0; i < lenses.Count; i++)
            {
                var (_, lensPower) = lenses[i];
                var calcPower = 1 + boxNumber;
                calcPower *= 1 + i;
                calcPower *= lensPower;
                total     += calcPower;
            }
        }

        return total;
    }
}