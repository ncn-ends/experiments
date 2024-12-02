using System.Diagnostics;
using Utils.Strings;


namespace AoC.Y2024;

public static class Day2Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       7 6 4 2 1
                       1 2 7 8 9
                       9 7 6 2 1
                       1 3 2 4 5
                       8 6 4 4 1
                       1 3 6 7 9
                       """;
        var expected1 = 2;

        // var example2 = """
        //                3   4
        //                4   3
        //                2   5
        //                1   3
        //                3   9
        //                3   3
        //                """;
        var expected2 = 4;


        // Assert.That(DoPart1(example1), Is.EqualTo(expected1));
        //
        var input = AocHandler.ImportHttp();

        //
        var res1 = DoPart1(input);
        TestContext.Out.WriteLine(res1);

        Assert.That(DoPart2(example1), Is.EqualTo(expected2));
        var res2 = DoPart2(input);
        TestContext.Out.WriteLine(res2);
        // 688 too low
        // 698 too low
        // 708 = wrong
    }

    private static int DoPart1(string input)
    {
        // var safeCount = 0;
        var safes = new List<string>();

        input.IterateOnEachLine(line =>
        {
            var levels = line.ExtractNumbers();

            var increasing = levels[0].val < levels[1].val
                    ? 1
                    : -1;

            for (var i = 0; i < levels.Count - 1; i++)
            {
                var current = levels[i].val;
                var next = levels[i + 1].val;

                if (current == next) return;
                if ((current > next && increasing == 1) || (current < next && increasing == -1)) return;
                var diff = Math.Abs(current - next);
                if (diff is < 1 or > 3) return;
            }

            safes.Add(string.Join(" ", levels.Select(x => x.val)));
        });
        return safes.Count;
    }

    private static int DoPart2(string input)
    {
        // var safeCount = 0;
        var safes = new List<string>();
        var bads = new List<string>();

        input.IterateOnEachLine(line =>
        {
            var levels = line.ExtractNumbers().Select(x => x.val).ToList();

            var rowSafe = CheckRowSafety(levels, false);
            if (!rowSafe) rowSafe = CheckRowSafety(levels[1..], true);

            if (rowSafe) safes.Add(string.Join(" ", levels));
            else bads.Add(string.Join(" ", levels));
        });
        var badsPretty = string.Join("\n", bads);
        return safes.Count;
    }

    private static bool CheckLevelSafety(int a, int b, bool increasing)
    {
        if (a == b) return false;
        if (a > b && increasing) return false;
        if (a < b && !increasing) return false;
        var diff = Math.Abs(a - b);
        if (diff is < 1 or > 3) return false;

        return true;
    }

    private static bool CheckRowSafety(List<int> levels, bool deletionUsed)
    {

        var increasing = levels[0] < levels[1];

        var alreadyDeletedALevel = deletionUsed;

        for (var i = 0; i < levels.Count - 1; i++)
        {
            var current = levels[i];
            var next = levels[i + 1];

            var isSafe = CheckLevelSafety(current, next, increasing);
            if (isSafe) continue;

            if (alreadyDeletedALevel) return false;

            /* if a level hasn't been deleted yet, and is on the 2nd to last level, and it isn't safe, can just delete the last level to make it safe */
            if (i == levels.Count - 2) break;

            if (i >= levels.Count - 2) return false;

            var isSafeIfDeletedNextLevel = CheckLevelSafety(current, levels[i + 2], increasing);
            if (!isSafeIfDeletedNextLevel) return false;

            i++;
            alreadyDeletedALevel = true;
        }

        return true;
    }
}

/*
   48 51 54 61 63
   48 51 56 58 61 63 65 64
   13 14 19 20 23 23
   25 27 34 35 36 40
   59 61 64 67 68 73 74 81
   19 17 18 21 20
   94 92 95 97 98 98
   75 72 74 75 78 82
   49 48 50 51 56
   42 39 40 41 40 41 44
   42 40 42 41 42 40
   70 67 64 66 66
   50 48 49 48 51 55
   66 65 68 66 67 69 75
   91 88 88 89 90 91 92
   28 25 27 29 29 31 32 31
   77 74 75 75 75
   86 83 85 85 88 92
   26 23 24 24 27 33
   48 45 47 51 54 56 58
   43 41 42 43 46 50 49
   20 19 22 25 26 30 30
   11 10 14 15 19
   19 17 20 23 26 30 37
   23 20 21 24 26 31 34 36
   94 91 96 99 97
   88 86 88 89 92 98 98
   19 16 18 21 27 29 32 36
   69 66 67 69 75 80
   69 69 70 72 74 75 76 75
   23 23 26 29 29
   15 15 17 19 21 22 24 28
   34 34 35 38 44
   45 45 43 45 48
   52 52 54 56 54 57 56
   7 7 6 8 10 13 15 15
   81 81 84 83 87
   65 65 63 65 70
   23 23 24 25 25 27 29
   20 20 22 22 19
   2 2 4 6 6 6
   45 45 48 49 52 55 55 59
   74 74 76 76 79 82 87
   4 4 8 11 12 14
   38 38 41 43 47 44
   80 80 81 83 87 87
   3 3 6 8 10 12 16 20
   31 31 32 36 42
   81 81 82 88 91 94 97 98
   36 36 37 38 44 41
   2 2 3 8 8
   63 63 69 72 76
   2 2 7 10 15
   51 55 56 59 62 65 63
   52 56 59 60 62 64 64
   71 75 77 79 81 85
   7 11 12 15 18 21 26
   40 44 42 43 46 48 51 49
   14 18 21 24 21 21
   27 31 29 30 32 34 38
   56 60 61 64 66 69 66 72
   73 77 79 79 81 83 84
   5 9 10 13 14 14 17 16
   33 37 38 41 41 41
   64 68 70 70 73 77
   34 38 40 42 43 43 48
   46 50 53 55 59 62
   71 75 79 82 79
   38 42 46 49 51 51
   59 63 66 70 74
   82 86 90 91 97
   22 26 28 33 36 37
   64 68 73 76 74
   65 69 71 72 78 78
   20 24 27 30 36 40
   65 69 72 75 77 83 84 89
   32 39 41 43 44 42
   52 57 59 61 64 66 66
   80 87 90 91 93 95 99
   22 29 32 35 38 43
   86 92 94 91 92
   81 87 88 89 87 88 86
   22 27 28 31 33 30 32 32
   7 13 15 14 18
   52 59 60 61 58 64
   32 38 38 41 44 47 49
   25 32 33 34 34 35 36 35
   68 73 75 75 77 80 82 82
   79 84 87 87 91
   25 30 31 34 35 35 42
   2 7 9 13 15 17 18 19
   11 18 22 23 22
   4 9 12 13 16 18 22 22
   48 53 54 58 61 65
   11 16 20 21 24 29
   10 15 22 24 26
   6 12 14 21 23 26 24
   55 60 61 64 65 72 72
   64 70 72 74 76 82 85 89
   28 34 40 42 44 45 46 51
   74 72 70 72 71 68 67 65
   80 79 82 80 83
   46 43 41 38 37 38 38
   55 54 56 55 51
   79 77 76 74 73 74 67
   43 42 42 41 39 37 34 36
   30 28 26 26 24 24
   33 31 29 29 26 22
   94 91 89 86 86 80
   39 38 35 33 30 26 25 23
   17 15 14 12 8 5 5
   33 32 31 27 24 23 19
   44 42 38 36 33 30 23
   92 90 83 82 80
   24 21 15 12 15
   88 85 83 77 74 73 73
   31 29 27 21 17
   78 75 70 69 68 61
   92 95 94 91 94
   66 67 64 62 62
   67 70 67 64 60
   49 52 51 49 47 46 41
   94 95 97 94 93 90
   85 86 85 84 87 86 87
   40 43 40 39 41 38 38
   82 83 80 83 82 78
   95 97 99 96 90
   94 96 95 93 93 90
   29 31 31 28 27 30
   49 51 49 47 45 45 44 44
   16 19 19 16 15 13 12 8
   12 13 13 12 5
   59 61 57 55 54
   34 37 36 32 34
   45 47 43 42 41 41
   80 81 78 77 73 70 69 65
   88 91 90 86 79
   60 63 60 59 56 49 48
   83 85 83 81 80 74 72 73
   94 95 92 87 86 83 82 82
   67 70 69 67 60 56
   32 34 32 26 19
   25 25 24 23 24
   82 82 81 80 77 74 71 71
   87 87 85 83 79
   27 27 26 24 17
   66 66 69 66 63
   73 73 70 67 64 67 66 69
   90 90 93 90 88 88
   77 77 74 75 71
   57 57 56 57 54 52 50 43
   29 29 28 25 25 22
   52 52 50 50 53
   19 19 19 16 14 12 9 9
   73 73 70 69 67 67 65 61
   96 96 93 90 88 88 82
   18 18 14 12 10 7 6 4
   27 27 23 22 20 19 17 19
   21 21 20 17 15 11 11
   92 92 89 85 82 79 75
   38 38 37 33 31 28 22
   15 15 13 7 4 3 2
   58 58 53 52 55
   70 70 69 64 64
   39 39 37 34 33 26 23 19
   49 49 42 41 34
   79 75 73 70 67 70
   27 23 20 19 18 15 12 12
   92 88 87 84 81 78 77 73
   81 77 75 73 71 65
   60 56 58 55 52 54
   98 94 96 95 95
   87 83 86 85 84 80
   35 31 33 31 24
   41 37 37 34 31
   26 22 20 17 16 16 18
   63 59 58 58 56 56
   96 92 89 86 86 82
   90 86 85 84 84 77
   22 18 14 13 10 8 6
   71 67 66 62 64
   71 67 65 62 60 56 56
   97 93 90 86 82
   35 31 27 25 19
   96 92 89 87 80 78 76 74
   69 65 58 57 55 53 55
   19 15 14 8 5 5
   72 68 67 64 62 60 54 50
   80 76 75 70 63
   88 83 82 80 77 75 72 74
   82 76 73 72 72
   87 82 81 79 76 75 71
   99 93 90 89 86 81
   23 18 16 15 13 12 14 13
   82 77 75 72 69 71 74
   21 16 13 14 11 9 9
   67 62 60 63 60 56
   98 92 95 93 91 88 81
   14 9 9 8 6 3 2
   27 20 18 18 21
   20 13 13 11 11
   36 30 28 26 23 23 19
   51 46 46 45 38
   62 57 55 51 50 48
   45 38 34 31 33
   24 19 17 14 10 8 8
   88 81 77 76 74 70
   54 47 44 40 37 35 29
   90 83 80 78 71 68 65
   35 30 23 20 19 22
   76 69 63 60 58 57 57
   81 74 72 65 62 58
   29 24 21 20 13 10 5
   58 55 56 56 53
   43 40 38 33 30
   90 86 85 86 85 84 82 84
   46 50 53 51 52 56
   83 82 81 79 77 73 68
   56 60 63 66 68 70 73 77
   48 44 43 38 36 35 31
   61 54 53 51 52 45
   35 41 43 48 48
   4 8 11 11 18
   71 73 76 80 83
   42 47 50 51 55 57 60
   7 7 4 7 8 9 8
   64 64 62 61 59 60 63
   70 66 64 62 60 63 63
   13 10 13 18 23
   18 20 21 23 29 30 31 38
   38 38 41 40 44
   31 34 32 29 26 26
   46 44 46 49 51 53 60 64
   22 29 32 33 31 33 37
   34 34 28 26 25 24
   92 91 88 90 90
   18 17 19 20 23 23
   31 31 28 24 22 21 16
   84 83 80 76 76
   57 54 58 60 63 66
   74 73 66 65 62 55
   52 54 53 48 42
   86 83 80 77 75 71 69
   50 46 45 44 47 45 42
   42 42 45 43 41 38 35 29
   37 34 37 40 40 44
   96 95 94 92 93 90 84
   7 4 7 9 7 8 15
   58 52 55 53 53
   37 41 43 50 52 54 52
   60 64 65 69 70 73 74 74
   61 61 58 61 59 56 53 49
   44 44 41 40 37 31 27
   46 50 50 53 55 56 54
   25 27 28 27 25 23 21 20
   96 96 93 91 88 87 81 74
   80 80 81 79 76
   53 58 65 66 63
   82 75 74 76 75 71
   12 11 16 18 18
   5 9 11 12 15 20 24
   36 32 30 30 28 25 26
   15 19 21 26 28 28
   76 74 73 70 64 64
   76 72 70 66 64 61 57
   12 14 13 10 7 2 3
   24 27 26 24 24 23
   11 9 10 10 10
   88 84 80 79 78 73
   90 87 89 89 96
   69 67 70 73 70 73 74
   63 67 69 74 75 78
   27 23 19 17 15 13 10
   46 52 53 54 54 56 62
   41 35 33 31 30 25 25
   */