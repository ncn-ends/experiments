using Utils.Numbers;
using Utils.Strings;


namespace AoC.Y2016;

public static class Day13Solutions
{
    [Test] [OutputTime]
    public static void Run()
    {
        // example test
        Assert.That(SolvePart1(10, 7, 4), Is.EqualTo(11));

        var input = AocInputHandler.ImportHttp();

        var res1 = SolvePart1(input.ToInt(), 31, 39);
        TestContext.Out.WriteLine(res1);

        var res2 = SolvePart2(input.ToInt());
        TestContext.Out.WriteLine(res2);
    }

    private static int SolvePart1(int fav, int goalX, int goalY)
    {
        var q = new Queue<(int x, int y, int steps)>();
        var visited = new HashSet<(int x, int y)>();
        q.Enqueue((1, 1, 0));
        for (; q.Any() ;)
        {
            var c = q.Dequeue();
            visited.Add((c.x, c.y));
            if (c.x == goalX && c.y == goalY) return c.steps;

            (int x, int y) left  = (c.x - 1 , c.y);
            (int x, int y) right = (c.x + 1 , c.y);
            (int x, int y) up    = (c.x     , c.y - 1);
            (int x, int y) down  = (c.x     , c.y + 1);

            if (!visited.Contains(left) && left.x > 0 && IsWalkable(left.x, left.y, fav))
                q.Enqueue((left.x, left.y, c.steps + 1));
            if (!visited.Contains(right) && right.y > 0 && IsWalkable(right.x, right.y, fav))
                q.Enqueue((right.x, right.y, c.steps + 1));
            if (!visited.Contains(up) && IsWalkable(up.x, up.y, fav))
                q.Enqueue((up.x, up.y, c.steps + 1));
            if (!visited.Contains(down) && IsWalkable(down.x, down.y, fav))
                q.Enqueue((down.x, down.y, c.steps + 1));
        }

        return int.MinValue;
    }

    private static int SolvePart2(int fav)
    {
        var q = new Queue<(int x, int y, int steps)>();
        var visited = new HashSet<(int x, int y)>();
        q.Enqueue((1, 1, 0));
        for (; q.Any() ;)
        {
            var c = q.Dequeue();
            if (c.steps > 50) continue;
            visited.Add((c.x, c.y));

            (int x, int y) left  = (c.x - 1 , c.y);
            (int x, int y) right = (c.x + 1 , c.y);
            (int x, int y) up    = (c.x     , c.y - 1);
            (int x, int y) down  = (c.x     , c.y + 1);

            foreach (var dir in new[] {left, right, up, down})
            {
                if (!visited.Contains(dir) && IsWalkable(dir.x, dir.y, fav))
                    q.Enqueue((dir.x, dir.y, c.steps + 1));
            }
        }

        return visited.Count;
    }

    private static bool IsWalkable(int x, int y, int fav)
    {
        if (x < 0 || y < 0) return false;

        var asd = (x * x) + (3 * x) + (2 * x * y) + y + (y * y);
        asd += fav;
        var bin = asd.ToBinary();
        return bin.Count(c => c == '1').IsEven();
    }

    /* TODO: put this in the right place */
    [Test]
    public static void Test_ToBinary()
    {
        Assert.AreEqual("10011010010", 1234.ToBinary());
    }
}