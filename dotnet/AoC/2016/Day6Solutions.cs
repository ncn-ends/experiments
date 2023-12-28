using System.Diagnostics;
using Utils.Strings;

namespace AoC.Y2016;

public class Day6Solutions
{
    private static readonly string _example = """
                                              eedadn
                                              drvtee
                                              eandsr
                                              raavrd
                                              atevrs
                                              tsrnev
                                              sdttsa
                                              rasrtv
                                              nssdts
                                              ntnada
                                              svetve
                                              tesnvt
                                              vntsnd
                                              vrdear
                                              dvrsen
                                              enarar
                                              """;

    [Test][OutputTime]
    public void DoPart1()
    {
        Assert.That(SolvePart1(_example), Is.EqualTo("easter"));

        var res = SolvePart1(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res);
    }

    public static string SolvePart1(string input)
    {
        var dict = new Dictionary<int, Dictionary<char, int>>();
        var cols = input.SplitByLine()[0].Length;
        for (var i = 0; i < cols; i++)
            dict[i] = new();

        input.IterateOnEachLine((line, _) =>
        {
            for (var cha = 0; cha < line.Length; cha++)
            {
                if (dict[cha].ContainsKey(line[cha])) dict[cha][line[cha]]++;
                else
                {
                    dict[cha][line[cha]] = 1;
                }
            }
        });
        var msg = "";
        foreach (var dictValue in dict.Values)
        {
            msg += dictValue.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }
        return msg;
    }

    [Test][OutputTime]
    public static void DoPart2()
    {
        Assert.That(SolvePart2(_example), Is.EqualTo("advent"));

        var res = SolvePart2(AocHandler.ImportHttp());
        TestContext.Out.WriteLine(res);
    }

    public static string SolvePart2(string input)
    {
        var dict = new Dictionary<int, Dictionary<char, int>>();
        var cols = input.SplitByLine()[0].Length;
        for (var i = 0; i < cols; i++)
            dict[i] = new();

        input.IterateOnEachLine((line, _) =>
        {
            for (var cha = 0; cha < line.Length; cha++)
            {
                if (dict[cha].ContainsKey(line[cha])) dict[cha][line[cha]]++;
                else
                {
                    dict[cha][line[cha]] = 1;
                }
            }
        });

        var msg = "";
        foreach (var dictValue in dict.Values)
            msg += dictValue.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

        return msg;
    }
}