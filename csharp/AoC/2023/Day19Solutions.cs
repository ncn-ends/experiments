using Utils.Strings;


namespace AoC.Y2023;

public static class Day19Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       px{a<2006:qkq,m>2090:A,rfg}
                       pv{a>1716:R,A}
                       lnx{m>1548:A,A}
                       rfg{s<537:gd,x>2440:R,A}
                       qs{s>3448:A,lnx}
                       qkq{x<1416:A,crn}
                       crn{x>2662:A,R}
                       in{s<1351:px,qqz}
                       qqz{s>2770:qs,m<1801:hdj,R}
                       gd{a>3333:R,R}
                       hdj{m>838:A,pv}

                       {x=787,m=2655,a=1222,s=2876}
                       {x=1679,m=44,a=2067,s=496}
                       {x=2036,m=264,a=79,s=2244}
                       {x=2461,m=1339,a=466,s=291}
                       {x=2127,m=1623,a=2188,s=1013}
                       """;

        var example2 = """

                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(19114));
        TestContext.Out.WriteLine(DoPart1(input));

        // Assert.That(DoPart2(example2), Is.EqualTo(0));
        // TestContext.Out.WriteLine(DoPart2(input));
    }

    private record ElvenPart(int x,
                             int m,
                             int a,
                             int s)
    {
        public int Get(string c)
        {
            if (c is null) return default;
            if (c == "x") return x;
            if (c == "m") return m;
            if (c == "a") return a;
            if (c == "s") return s;
            throw new Exception("bad");
        }
    };

    private record Workflow(string destination,
                            string? part = null,
                            Comparison? comparison = null,
                            int? valueToCompare = null,
                            bool isDefault = false);

    private static List<ElvenPart> ParseElvenParts(string elvenPartsUnparsed)
    {
        List<ElvenPart> list = [];
        elvenPartsUnparsed.IterateOnEachLine(line =>
        {
            var nums = line.ExtractNumbers().Select(y => y.val).ToList();
            list.Add(new ElvenPart(nums[0], nums[1], nums[2], nums[3]));
        });
        return list;
    }

    private static Dictionary<string, List<Workflow>> ParseWorkflows(string workflowsUnparsed)
    {
        // List<Workflow> toReturn = [];
        var toReturn = new Dictionary<string, List<Workflow>>();
        workflowsUnparsed.IterateOnEachLine(line =>
        {
            var splitA = line.SplitBy(["{", "}"]);
            var key = splitA[0];
            var workflows = splitA[1].SplitBy([","]);
            toReturn.TryAdd(key, []);
            foreach (var workflowUnparsed in workflows)
            {
                var isDefault = workflowUnparsed == workflows.Last();
                if (isDefault)
                {
                    toReturn[key].Add(new Workflow(workflowUnparsed, isDefault: true));
                    continue;
                }

                var comparison = workflowUnparsed.Contains("<")
                        ? Comparison.LessThan
                        : Comparison.GreaterThan;
                var splitB = workflowUnparsed.SplitBy([":", "<", ">"]);
                var part = splitB[0];
                var valueToCompare = splitB[1].ToInt();
                var destination = splitB[2];
                toReturn[key].Add(new Workflow(destination, part, comparison, valueToCompare));
            }
        });
        return toReturn;
    }

    private static Dictionary<string, List<Workflow>> workflows { get; set; }

    private static bool ProcessElvenPartThroughWorkflows(ElvenPart ep)
    {
        var q = new Queue<string>();
        q.Enqueue("in");
        while (q.Any())
        {
            var c = q.Dequeue();
            var workflow = workflows[c];
            foreach (var step in workflow)
            {
                var epVal = ep.Get(step.part!);

                var wasGreaterThanAndPassed = step.comparison == Comparison.GreaterThan && epVal > step.valueToCompare;
                var wasLessThanAndPassed = step.comparison == Comparison.LessThan && epVal < step.valueToCompare;
                var passed = wasGreaterThanAndPassed || wasLessThanAndPassed || step.isDefault;

                if (!passed) continue;

                if (step.destination == "A") return true;
                if (step.destination == "R") return false;

                q.Enqueue(step.destination);
                break;
            }
        }

        throw new Exception("bad");
    }

    private static int DoPart1(string input)
    {
        var split = input.SplitBy(["\n\n"]);
        workflows = ParseWorkflows(split[0]);
        var elvenParts = ParseElvenParts(split[1]);

        var rejectedParts = new List<ElvenPart>();
        var acceptedParts = new List<ElvenPart>();
        foreach (var ep in elvenParts)
        {
            var passed = ProcessElvenPartThroughWorkflows(ep);
            if (passed) acceptedParts.Add(ep);
            else rejectedParts.Add(ep);
        }


        return acceptedParts.Sum(y => y.x + y.s + y.m + y.a);
    }

    private static int DoPart2(string input)
    {
        var split = input.SplitBy(["\n\n"]);
        workflows = ParseWorkflows(split[0]);
        var elvenParts = ParseElvenParts(split[1]);

        var rejectedParts = new List<ElvenPart>();
        var acceptedParts = new List<ElvenPart>();
        foreach (var ep in elvenParts)
        {
            var passed = ProcessElvenPartThroughWorkflows(ep);
            if (passed) acceptedParts.Add(ep);
            else rejectedParts.Add(ep);
        }

        return acceptedParts.Sum(y => y.x + y.s + y.m + y.a);
    }

    /* TODO: turn this into a utility */
    private enum Comparison
    {
        LessThan,
        GreaterThan
    }
}