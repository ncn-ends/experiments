using System.Collections;
using System.Diagnostics;
using System.Text;
using NUnit.Framework.Internal;
using Utils.Queue;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day12Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                      ???.### 1,1,3
                      .??..??...?##. 1,1,3
                      ?#?#?#?#?#?#?#? 1,3,1,6
                      ????.#...#... 4,1,1
                      ????.######..#####. 1,6,5
                      ?###???????? 3,2,1
                      """;

        var example1 = """
                       ???.### 1,1,3
                       """;

        var example2 = """
                       .??..??...?##. 1,1,3
                       """;

        var example3 = """
                       ?#?#?#?#?#?#?#? 1,3,1,6
                       """;

        var example4 = """
                       ????.#...#... 4,1,1
                       """;

        var example5 = """
                       ????.######..#####. 1,6,5
                       """;

        var example6 = """
                       ?###???????? 3,2,1
                       """;

        var input = AocHandler.ImportHttp();

        // Assert.That(DoPart1(example1), Is.EqualTo(1));
        // Assert.That(DoPart1(example2), Is.EqualTo(4));
        // Assert.That(DoPart1(example3), Is.EqualTo(1));
        // Assert.That(DoPart1(example4), Is.EqualTo(1));
        // Assert.That(DoPart1(example5), Is.EqualTo(4));
        // Assert.That(DoPart1(example6), Is.EqualTo(10));
        // Assert.That(DoPart1(example), Is.EqualTo(21));
        //
        // Assert.That(DoPart1(".?????...? 1,1,1"), Is.EqualTo(7));
        // Assert.That(DoPart1("#????????.#?#?????? 2,1,1,5,1"), Is.EqualTo(1)); // can't verify if answer is correct
        // Assert.That(DoPart1("???##?###????? 1,2,3,4"), Is.EqualTo(7));

        // from input
        // > 5498
        // has to be more than 7519


        Assert.That(DoPart1BruteForce(example1), Is.EqualTo(1));
        Assert.That(DoPart1BruteForce(example2), Is.EqualTo(4));
        Assert.That(DoPart1BruteForce(example3), Is.EqualTo(1));
        Assert.That(DoPart1BruteForce(example4), Is.EqualTo(1));
        Assert.That(DoPart1BruteForce(example5), Is.EqualTo(4));
        Assert.That(DoPart1BruteForce(example6), Is.EqualTo(10));
        Assert.That(DoPart1BruteForce(example), Is.EqualTo(21));
        TestContext.Out.WriteLine(DoPart1BruteForce(input));
        //
        // Assert.That(DoPart1BruteForce(".?????...? 1,1,1"), Is.EqualTo(7));
        // Assert.That(DoPart1BruteForce("#????????.#?#?????? 2,1,1,5,1"), Is.EqualTo(1)); // can't verify if answer is correct
        // Assert.That(DoPart1BruteForce("???##?###????? 1,2,3,4"), Is.EqualTo(7));
    }

    public class Map
    {
        public Map(List<int> operationAmounts)
        {
            ExpectedOperatingAmounts = operationAmounts;
        }

        public Map(Map oldMap)
        {
            ExpectedOperatingAmounts = oldMap.ExpectedOperatingAmounts;
            OperatingPositions       = oldMap.OperatingPositions;
        }

        public Map(List<int> operatingAmounts, List<int> positions)
        {
            ExpectedOperatingAmounts = operatingAmounts;
            OperatingPositions       = positions;
        }

        public List<int> ExpectedOperatingAmounts { get; init; }
        public List<int> OperatingPositions { get; private set; } = [];

        public int FirstUnhandledExistingOperating(string pattern)
        {
            if (OperatingPositions.Count == 0) return pattern.IndexOf("#");
            var list = new List<int>();
            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '#') list.Add(i);
            }

            for (var i = 0; i < OperatingPositions.Count; i++)
            {
                var pos = OperatingPositions[i];
                var n = ExpectedOperatingAmounts[i];
                list = list.Where(x => x < pos || x > pos + n - 1).ToList();
            }

            if (list.Count == 0) return -1;

            return list.First();
        }

        public int NextOperatingAmount()
        {
            return ExpectedOperatingAmounts[OperatingPositions.Count];
        }

        public bool IsFulfilled()
        {
            return OperatingPositions.Count == ExpectedOperatingAmounts.Count;
        }

        public Map WithNextFulfilled(int n)
        {
            List<int> newPositions = [..OperatingPositions, n];
            return new Map(ExpectedOperatingAmounts, newPositions);
        }
    }

    [TestCase("..#..#?...?##.", 11)]
    public static void Test_MapMethods(string pattern, int expected)
    {
        var map = new Map([1, 1, 3], [2, 5]);
        Assert.That(map.FirstUnhandledExistingOperating(pattern), Is.EqualTo(expected));
    }

    private static int DoPart1(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var basePattern = line.SplitBySpace()[0];
            TestContext.Out.WriteLine(basePattern);
            var baseMap = new Map(line.SplitBySpace()[1]
                                      .ExtractNumbers()
                                      .Select(x => x.val).ToList());

            var arrangements = new HashSet<string>();
            var q = new Queue<(string basePattern, Map baseMap)>();
            q.Enqueue((basePattern, baseMap));

            while (q.Any())
            {
                var (pattern, queuedMap) = q.Dequeue();
                var map = new Map(queuedMap);

                var firstUnknown = pattern.IndexOf("?");
                var nextUnhandledOperating = map.FirstUnhandledExistingOperating(pattern);

                /* if map is fulfilled then pass it to arrangements.
                 * also get rid of any question marks left
                 */
                if (map.IsFulfilled())
                {
                    var setPattern = new StringBuilder(pattern);
                    for (int i = 0; i < setPattern.Length; i++)
                    {
                        if (setPattern[i] == '?') setPattern[i] = '.';
                    }

                    arrangements.Add(setPattern.ToString());
                    continue;
                }

                /* the required amount of spaces for the next group of sequences is too much before end of pattern */
                // var exceedsMaxLength = nextUnhandledOperating != -1 && map.NextOperatingAmount() + firstUnknown > pattern.Length - 1;
                // if (exceedsMaxLength) continue;

                /* when we can no longer rely on ? */
                /* or when an operating needs to be accounted for that hasn't been yet */
                if (nextUnhandledOperating != -1 && (nextUnhandledOperating < firstUnknown || firstUnknown == -1))
                {
                    var nOfOperatings = 0;
                    for (int i = nextUnhandledOperating; i < pattern.Length; i++)
                    {
                        if (pattern[i] == '.') break;
                        if (pattern[i] == '#') nOfOperatings++;
                    }

                    if (map.NextOperatingAmount() == nOfOperatings)
                    {
                        q.Enqueue((pattern, map.WithNextFulfilled(nextUnhandledOperating)));
                        continue;
                    }

                    /* for cases like .#?#?#?#... 1,3,1..., need to make sure it accounts for patterns like .#... */
                    var asd = 0;
                    var possiblyNewPattern = new StringBuilder(pattern);
                    for (int i = nextUnhandledOperating; i < pattern.Length; i++)
                    {
                        if (pattern[i] == '.') break;
                        if (i - nextUnhandledOperating >= map.NextOperatingAmount()) break;
                        asd++;
                        possiblyNewPattern[i] = '#';
                    }

                    /* there may be other patterns to be added ahead, so don't continue */
                    if (asd >= map.NextOperatingAmount())
                    {
                        q.Enqueue((possiblyNewPattern.ToString(), map.WithNextFulfilled(nextUnhandledOperating)));
                        continue;
                    }
                }

                if (firstUnknown == -1)
                {
                    continue;
                }

                ;


                var operatingIsImmediatelyBefore = firstUnknown != 0 && pattern[firstUnknown - 1] == '#';
                var operatingIsImmediatelyAfter =
                        firstUnknown != pattern.Length - 1 && pattern[firstUnknown + 1] == '#';

                /* find all unknowns AND knowns touching first? - make a note of the starting point and the number */
                /* if this number is equal to the next operating amount, perform pattern with the next fulfilled, replacing all the numbers with new ones and continue */
                /* otherwise DON'T continue */
                if (operatingIsImmediatelyBefore || operatingIsImmediatelyAfter)
                {
                    var asd = 0;

                    for (int i = firstUnknown; i < pattern.Length; i++)
                    {
                        if (pattern[i] == '.') break;
                        asd++;
                    }

                    var startingPoint = firstUnknown;
                    for (var i = firstUnknown - 1; i >= 0; i--)
                    {
                        if (pattern[i] == '.') break;
                        startingPoint--;
                        asd++;
                    }

                    if (asd == map.NextOperatingAmount())
                    {
                        var newPattern = new StringBuilder(pattern);
                        for (int i = startingPoint; i < startingPoint + asd; i++)
                        {
                            newPattern[i] = '#';
                        }

                        q.Enqueue((newPattern.ToString(), map.WithNextFulfilled(startingPoint)));
                        continue;
                    }

                    q.Enqueue((pattern.WithModifiedChar(firstUnknown, '.'), map));
                    continue;
                }

                var nOfUnknowns = 0;
                for (int i = firstUnknown; i < pattern.Length; i++)
                {
                    if (pattern[i] == '#') break;
                    if (pattern[i] == '?') nOfUnknowns++;
                }

                if (nOfUnknowns < map.NextOperatingAmount())
                {
                    q.Enqueue((pattern.WithModifiedChars(firstUnknown, firstUnknown + nOfUnknowns, '.'), map));
                    continue;
                }

                q.Enqueue((pattern.WithModifiedChar(firstUnknown, '.'), map));
                q.Enqueue((pattern.WithModifiedChars(firstUnknown, firstUnknown + map.NextOperatingAmount(), '#'),
                           map.WithNextFulfilled(firstUnknown)));
            }

            TestContext.Out.WriteLine(string.Join("\n", arrangements));
            total += arrangements.Count;
        });
        return total;
    }

    private static int DoPart1BruteForce(string input)
    {
        var total = 0;
        input.IterateOnEachLine(line =>
        {
            var basePattern = line.SplitBySpace()[0];
            var expectedOperatings = line.SplitBySpace()[1]
                              .ExtractNumbers()
                              .Select(x => x.val).ToList();
            var arrangements = new HashSet<string>();
            var q = new Queue<string>();
            q.Enqueue(basePattern);
            while (q.Any())
            {
                var pattern = q.Dequeue();
                var nextUnknown = pattern.IndexOf("?");
                if (nextUnknown == -1)
                {
                    arrangements.Add(pattern);
                    continue;
                };
                q.Enqueue(pattern.WithModifiedChar(nextUnknown, '.'));
                q.Enqueue(pattern.WithModifiedChar(nextUnknown, '#'));
            }

            var validArrangements = new List<string>();
            foreach (var arrangement in arrangements)
            {
                if (arrangement.Contains("?")) throw new Exception("bad");
                var gs = arrangement.ToGroupSerial();

                /* ensure number of # groups is expected */
                if (gs.Where(x => x.c == '#').Count() != expectedOperatings.Count) continue;

                /* ensure each # group has expected count of # */
                var opGroups = gs.Where(x => x.c == '#').ToList();
                var isGood = true;
                for (var i = 0; i < opGroups.Count; i++)
                {
                    if (opGroups[i].n == expectedOperatings[i]) continue;
                    isGood = false;
                    break;
                }
                if (!isGood) continue;

                /* ensure there's space between # groups */
                var lastWasOperating = false;
                for (var i = 0; i < gs.Count; i++)
                {
                    if (gs[i].c != '#')
                        lastWasOperating = false;
                    else
                    {
                        if (!lastWasOperating) lastWasOperating = true;
                        else
                        {
                            isGood = false;
                            break;
                        }
                    }
                }
                if (!isGood) continue;
                validArrangements.Add(arrangement);
            }

            total += validArrangements.Count;
        });

        return total;
    }

    private static int DoPart2(string input)
    {
        return default;
    }

    [Test]
    public static void Test_ToGroupSerial()
    {
        var asd = "#????????.#?#??????".ToGroupSerial();
        Assert.IsTrue(asd.Count == 7);
    }

    private static List<(char c, int n)> ToGroupSerial(this string s)
    {
        var list = new List<(char c, int n)>();
        for (var i = 0; i < s.Length; i++)
        {
            if (list.Any() && list.Last().c == s[i])
            {
                var last = list.Last();
                list.RemoveAt(list.Count - 1);
                last.n++;
                list.Add(last);
            }
            else
            {
                list.Add((s[i], 1));
            }
        }

        return list;
    }
}