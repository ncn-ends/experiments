using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day5Solutions
{
    [Test]
    public static void Run()
    {
        var example = """
                       seeds: 79 14 55 13
                       
                       seed-to-soil map:
                       50 98 2
                       52 50 48
                       
                       soil-to-fertilizer map:
                       0 15 37
                       37 52 2
                       39 0 15
                       
                       fertilizer-to-water map:
                       49 53 8
                       0 11 42
                       42 0 7
                       57 7 4
                       
                       water-to-light map:
                       88 18 7
                       18 25 70
                       
                       light-to-temperature map:
                       45 77 23
                       81 45 19
                       68 64 13
                       
                       temperature-to-humidity map:
                       0 69 1
                       1 0 69
                       
                       humidity-to-location map:
                       60 56 37
                       56 93 4
                       """;


        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example), Is.EqualTo(35));
        TestContext.Out.WriteLine(DoPart1(input).ToString());

        Assert.That(DoPart2(example), Is.EqualTo(46));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private class Path(long[] path)
    {
        private List<long> PathItems { get; set; } = path.ToList();

        public static Path WithSeed(long seed)
        {
            return new Path([seed]);
        }

        public void AddNext(long next)
        {
            PathItems.Add(next);
        }

        public long Last => PathItems.Last();
        public long Seed => PathItems[0];
        public long Soil => PathItems[1];
        public long Fert => PathItems[2];
        public long Water => PathItems[3];
        public long Light => PathItems[4];
        public long Temp => PathItems[5];
        public long Humid => PathItems[6];
        public long Loc => PathItems[7];
    };
    private static long DoPart1(string input)
    {
        var split = input.SplitBy(["\n\n"]);
        var paths = split[0].SplitBy([" "]).Skip(1).Select(x => Path.WithSeed(Convert.ToInt64(x))).ToList();
        var groups = split[1..].Select(x => x.SplitBy(["\n"]).Skip(1));
        foreach (var group in groups)
        {
            var maps = group.Select(x => x.ExtractLargeNumbers().Select(x => x.val).ToList());
            foreach (var path in paths)
            {
                var foundLink = maps.FirstOrDefault(map => path.Last >= map[1] && path.Last <= map[1] + map[2]);
                if (foundLink != default) path.AddNext(foundLink[0] + path.Last - foundLink[1]);
                else path.AddNext(path.Last);
            }
        }
        return paths.Min(x => x.Loc);
    }

    private class Path2((long start, long end)[] path)
    {
        public List<(long start, long end)> PathItems { get; private set; } = path.ToList();

        public static Path2 WithSeed(long seedStart, long seedEnd)
        {
            return new Path2([(seedStart, seedEnd)]);
        }

        public void AddNext(long nextStart, long nextEnd)
        {
            PathItems.Add((nextStart, nextEnd));
        }

        public void AddDirect()
        {
            PathItems.Add((Last.start, Last.end));
        }

        public void UpdateLast(long newStart, long newEnd)
        {
            PathItems.RemoveAt(PathItems.Count - 1);
            PathItems.Add((newStart, newEnd));
        }

        public static Path2 FromHere(IEnumerable<(long start, long end)> existingPath, long start, long end)
        {
            return new Path2([..existingPath, (start, end)]);
        }

        public (long start, long end) Last => PathItems.Last();
        public (long start, long end) Seed => PathItems[0];
        public (long start, long end) Soil => PathItems[1];
        public (long start, long end) Fert => PathItems[2];
        public (long start, long end) Water => PathItems[3];
        public (long start, long end) Light => PathItems[4];
        public (long start, long end) Temp => PathItems[5];
        public (long start, long end) Humid => PathItems[6];
        public (long start, long end) Loc => PathItems[7];
        public int Place => PathItems.Count;
    };

    private static long DoPart2(string input )
    {
        var split = input.SplitBy(["\n\n"]);
        var paths = split[0].SplitBy([" "]) /* first make seed pairs */
                            .Skip(1)
                            .Select(x => Convert.ToInt64(x))
                            .Chunk(2)
                            .Select(x => Path2.WithSeed(x[0], x[0] + x[1])) /* then make paths frm the seeds*/
                            .ToList();

        var groups = split[1..].Select(x => x.SplitBy(["\n"]).Skip(1)).ToList(); /* each group consists of maps. skip the group header. */
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            var maps = group.Select(x => x.ExtractLargeNumbers()
                                          .Select(x => x.val)
                                          .ToList());

            foreach (var map in maps)
            {
                var diff = map[0] - map[1]; /* if matched, diff is applied from source to destination */
                var minMatcher = map[1];
                var maxMatcher = map[1] + map[2];

                var ogPathsCount = paths.Count;
                for (var j = 0; j < ogPathsCount; j++)
                {
                    var path = paths[j];
                    if (path.Place == i + 2) continue; /* this means destination for path has already been added in this group */

                    var noMatch = path.Last.end < minMatcher || path.Last.start > maxMatcher;
                    if (noMatch) continue;

                    var completeOverlap = path.Last.start >= minMatcher && path.Last.end <= maxMatcher;
                    if (completeOverlap)
                    {
                        path.AddNext(path.Last.start + diff, path.Last.end + diff);
                        continue;
                    }

                    /* at this point there is a partial match */

                    var (matchedStart, matchedEnd, pathHasLeftOver) = path.Last.end >= maxMatcher
                            ? (path.Last.start, maxMatcher, true)
                            : (minMatcher, path.Last.end, false);

                    var nextStart = matchedStart + diff < 0
                            ? 0
                            : matchedStart + diff;
                    var nextEnd = matchedEnd + diff < 0
                            ? 0
                            : matchedEnd + diff;
                    /* create a new path based on current path, but continuing where matched */
                    paths.Add(Path2.FromHere(path.PathItems, nextStart, nextEnd));

                    /* the current path will be updated to the unmatched portion */
                    /* without doing this, later maps can't match on the remainder */
                    /* this means the path history is inaccurate, but doesn't matter for this puzzle */
                    var (unmatchedStart, unmatchedEnd) = pathHasLeftOver
                            ? (matchedEnd + 1, path.Last.end)
                            : (path.Last.start, matchedStart - 1);

                    paths[j].UpdateLast(unmatchedStart, unmatchedEnd);
                }
            }


            /* any paths without a destination will just use source value for the destination */
            foreach (var path in paths)
                if (path.Place == i + 1) path.AddDirect();
        }

        return paths.Min(x => x.Loc.start);
    }
}