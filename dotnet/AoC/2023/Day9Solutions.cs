using Utils.Strings;


namespace AoC.Y2023;

public static class Day9Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       0 3 6 9 12 15
                       1 3 6 10 15 21
                       10 13 16 21 30 45
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(114));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example1), Is.EqualTo(2));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var sequences = input.SplitByLine().Select(x => x.ExtractNumbers().Select(x => x.val)).ToList();
        var res = 0;
        foreach (var seq in sequences)
        {
            var pyr = GetPyramid([..seq]);
            var nextValues = new int[pyr.Count];
            for (var i = pyr.Count - 1; i >= 0; i--)
            {
                var cSeq = pyr[i];
                if (cSeq.All(x => x == 0))
                {
                    nextValues[i] = 0;
                    continue;
                }

                var lastNextVal = nextValues[i + 1];
                var nextVal = cSeq.Last() + lastNextVal;
                nextValues[i] = nextVal;
            }

            res += nextValues.First();
        }
        return res;
    }

    private static List<List<int>> GetPyramid(List<int> seq)
    {
        var pyr = new List<List<int>>();
        pyr.Add(seq);
        var cont = true;
        while (cont)
        {
            var lastSeq = pyr.Last();
            if (lastSeq.All(x => x == 0)) break;
            var nextSeq = new List<int>();
            for (int i = 1; i < lastSeq.Count; i++)
            {
                nextSeq.Add(lastSeq[i] - lastSeq[i - 1]);
            }
            pyr.Add(nextSeq);
        }

        return pyr;
    }

    private static int DoPart2(string input)
    {
        var sequences = input.SplitByLine().Select(x => x.ExtractNumbers().Select(x => x.val)).ToList();
        var res = 0;
        foreach (var seq in sequences)
        {
            var pyr = GetPyramid([..seq]);
            var prevValues = new int[pyr.Count];
            for (var i = pyr.Count - 1; i >= 0; i--)
            {
                var cSeq = pyr[i];
                if (cSeq.All(x => x == 0))
                {
                    prevValues[i] = 0;
                    continue;
                }

                var lastPrevVal = prevValues[i + 1];
                var nextVal = cSeq.First() - lastPrevVal;
                prevValues[i] = nextVal;
            }

            res += prevValues.First();
        }
        return res;
    }
}