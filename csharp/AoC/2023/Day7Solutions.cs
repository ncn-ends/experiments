using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day7Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       32T3K 765
                       T55J5 684
                       KK677 28
                       KTJJT 220
                       QQQJA 483
                       """;

        var example2 = """
                       32T3K 765
                       T55J5 684
                       KK677 28
                       KTJJT 220
                       QQQJA 483
                       """;

        var input = AocHandler.ImportHttp();

        // Assert.That(DoPart1(example1), Is.EqualTo(6440));
        // TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example2), Is.EqualTo(5905));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    public static class CardTypes
    {
        public static List<string> Fives { get; set; } = new();
        public static List<string> Fours { get; set; } = new();
        public static List<string> FullHouse { get; set; } = new();
        public static List<string> Threes { get; set; } = new();
        public static List<string> TwoPair { get; set; } = new();
        public static List<string> Twos { get; set; } = new();
        public static List<string> Highs { get; set; } = new();
    }
    private static int DoPart1(string input)
    {

        var games = input.SplitByLine()
                       .Select(x => x.SplitBySpace().ToList())
                       .ToDictionary(x => x[0], x => x[1].ToInt());

        var fives = new List<string>();
        var fours = new List<string>();
        var fullHouse = new List<string>();
        var threes = new List<string>();
        var twoPair = new List<string>();
        var twos = new List<string>();
        var highs = new List<string>();

        foreach (var game in games)
        {
            var hand = game.Key.ToCharArray();
            var dict = new Dictionary<char, int>();
            foreach (var c in hand)
                if (!dict.TryAdd(c, 1)) dict[c]++;

            if (dict.Values.Any(x => x == 5)) fives.Add(new string(hand));
            else if (dict.Values.Any(x => x == 4)) fours.Add(new string(hand));
            else if (dict.Values.Any(x => x == 3) && dict.Values.Any(x => x == 2)) fullHouse.Add(new string(hand));
            else if (dict.Values.Any(x => x == 3)) threes.Add(new string(hand));
            else if (dict.Values.Count(x => x == 2) == 2) twoPair.Add(new string(hand));
            else if (dict.Values.Count(x => x == 2) == 1) twos.Add(new string(hand));
            else highs.Add(new string(hand));
        }

        fives.Sort(SortHand);
        fours.Sort(SortHand);
        fullHouse.Sort(SortHand);
        threes.Sort(SortHand);
        twoPair.Sort(SortHand);
        twos.Sort(SortHand);
        highs.Sort(SortHand);

        List<string> combined = [..fives, ..fours, ..fullHouse, ..threes, ..twoPair, ..twos, ..highs];

        var totalWinnings = 0;
        for (var i = 0; i < combined.Count; i++)
        {
            var hand = combined[i];
            totalWinnings += games[hand] * (combined.Count - i);
        }

        return totalWinnings;
    }

    private static int GetCardValue(char c)
    {
        Dictionary<char, int> cardValues = new() {
                {'A', 14},
                {'K', 13},
                {'Q', 12},
                {'T', 10},
                {'9', 9},
                {'8', 8},
                {'7', 7},
                {'6', 6},
                {'5', 5},
                {'4', 4},
                {'3', 3},
                {'2', 2},
                {'J', 1},
        };

        return cardValues[c];
    }

    [Test]
    public static void TestSortHands()
    {
        var caseA = new List<string>(){"77888", "77788"};
        caseA.Sort(SortHand);
        Assert.AreEqual(caseA[0], "77888");

        var caseB = new List<string>(){"77788", "77888"};
        caseB.Sort(SortHand);
        Assert.AreEqual(caseB[0], "77888");

        var caseC = new List<string>(){"33332", "2AAAA"};
        caseC.Sort(SortHand);
        Assert.AreEqual(caseC[0], "33332");
    }

    private static int SortHand(string a, string b)
    {
        var aa = a.ToCharArray();
        var bb = b.ToCharArray();

        for (var i = 0; i < aa.Length; i++)
        {
            if (GetCardValue(aa[i]) == GetCardValue(bb[i])) continue;
            if (GetCardValue(aa[i]) > GetCardValue(bb[i])) return -1;
            if (GetCardValue(aa[i]) < GetCardValue(bb[i])) return 1;
        }

        return 0;
    }

    private static int DoPart2(string input)
    {
        var games = input.SplitByLine()
                         .Select(x => x.SplitBySpace().ToList())
                         .ToDictionary(x => x[0], x => x[1].ToInt());

        var fives = new List<string>();
        var fours = new List<string>();
        var fullHouse = new List<string>();
        var threes = new List<string>();
        var twoPair = new List<string>();
        var twos = new List<string>();
        var highs = new List<string>();

        var series = new List<List<string>>() {fives, fours, fullHouse, threes, twoPair, twos, highs};

        foreach (var game in games)
        {
            var hand = game.Key.ToCharArray();
            var dict = new Dictionary<char, int>();
            foreach (var c in hand)
            {
                if (c == 'J') continue;
                if (!dict.TryAdd(c, 1)) dict[c]++;
            }

            var target = int.MaxValue;
            if (dict.Values.Any(x => x == 5)) target = 0;
            else if (dict.Values.Any(x => x == 4)) target = 1;
            else if (dict.Values.Any(x => x == 3) && dict.Values.Any(x => x == 2)) target = 2;
            else if (dict.Values.Any(x => x == 3)) target = 3;
            else if (dict.Values.Count(x => x == 2) == 2) target = 4;
            else if (dict.Values.Count(x => x == 2) == 1) target = 5;
            else target = 6;

            var n = hand.Count(x => x == 'J');

            for (int i = 0; i < n; i++)
            {
                if (target == 0) break;

                if (target == 3) target      = 1;
                else if (target == 4) target = 2;
                else if (target == 5) target = 3;
                else target--;
            }

            series[target].Add(new string(hand));
        }

        fives.Sort(SortHand);
        fours.Sort(SortHand);
        fullHouse.Sort(SortHand);
        threes.Sort(SortHand);
        twoPair.Sort(SortHand);
        twos.Sort(SortHand);
        highs.Sort(SortHand);

        List<string> combined = [..fives, ..fours, ..fullHouse, ..threes, ..twoPair, ..twos, ..highs];

        var totalWinnings = 0;
        for (var i = 0; i < combined.Count; i++)
        {
            var hand = combined[i];
            totalWinnings += games[hand] * (combined.Count - i);
        }

        return totalWinnings;
    }
}