using System.Diagnostics;
using AoC;
using Utils;

namespace AoC.Y2022;

// class Path
// {
//     public string Steps { get; set; }
//     public bool Redundant { get; set; }
// }

public static class Day12Solution
{
    private static char[][] _input = AocHandler.ImportHttp().Trim().Split("\n").Select(x => x.Trim().ToCharArray()).ToArray();
//     private static char[][] _input = """
// Sabqponm
// abcryxxl
// accszExk
// acctuvwj
// abdefghi
// """.Split("\n").Select(x => x.ToCharArray()).ToArray();

    // private static (int x, int y) startingPoint = (0, 20);

    public static (int answer, string bestPath) DoPart1()
    {
        var startingPoints = new List<(int x, int y)>();
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input.Length; j++)
            {
                var c = _input[i][j];
                if (c is 'a' or 'S')
                {
                    if ((i > 0 && _input[i - 1][j] == 'b') || (j < _input[0].Length - 1 && _input[i][j + 1] == 'b') ||
                        (i < _input.Length - 1 && _input[i + 1][j] == 'b') || (j > 0 && _input[i][j - 1] == 'b')) startingPoints.Add((j, i));
                }
            }
        }

        Debugger.Break();
        var bestPaths = new List<(int answer, string bestPath)>();
        foreach (var startingPoint in startingPoints)
        {
            Func<string, (int x, int y)> getPositionFromPath = path =>
            {
                var pathSteps = path.ToCharArray();
                var hereX = startingPoint.x;
                var hereY = startingPoint.y;
                foreach (var step in pathSteps)
                {
                    if (step == '<') hereX -= 1;
                    if (step == '>') hereX += 1;
                    if (step == 'v') hereY += 1;
                    if (step == '^') hereY -= 1;
                }

                return (hereX, hereY);
            };

            Func<(int x, int y), bool> canGoRight = pos =>
            {
                var (x, y) = pos;
                if (x >= _input[0].Length - 1) return false;
                var currentChar = _input[y][x];
                var rightChar = _input[y][x + 1];
                return GetValueFromChar(rightChar) - GetValueFromChar(currentChar) <= 1;
            };

            Func<(int x, int y), bool> canGoLeft = pos =>
            {
                var (x, y) = pos;
                if (x <= 0) return false;
                var currentChar = _input[y][x];
                var leftChar = _input[y][x - 1];
                return GetValueFromChar(leftChar) - GetValueFromChar(currentChar) <= 1;
            };

            Func<(int x, int y), bool> canGoUp = pos =>
            {
                var (x, y) = pos;
                if (y <= 0) return false;
                var currentChar = _input[y][x];
                var upChar = _input[y - 1][x];
                return GetValueFromChar(upChar) - GetValueFromChar(currentChar) <= 1;
            };

            Func<(int x, int y), bool> canGoDown = pos =>
            {
                var (x, y) = pos;
                if (y >= _input.Length - 1) return false;
                var currentChar = _input[y][x];
                var downChar = _input[y + 1][x];
                return GetValueFromChar(downChar) - GetValueFromChar(currentChar) <= 1;
            };


            Func<(int x, int y), char?> nextToPeak = pos =>
            {
                var (x, y) = pos;

                if (_input[y][x] == 'E') return 'E';
                if (_input[y][x] != 'z' && _input[y][x] != 'y') return null;
                if (y > 0 && _input[y - 1][x] == 'E') return '^';
                if (x < _input[0].Length - 1 && _input[y][x + 1] == 'E') return '>';
                if (y < _input.Length - 1 && _input[y + 1][x] == 'E') return 'v';
                if (x > 0 && _input[y][x - 1] == 'E') return '<';

                return null;
            };


            HashSet<string> checkedPaths = new();
            HashSet<string> completedPaths = new();
            Queue<string> undiscoveredPaths = new();
            undiscoveredPaths.Enqueue("");

            do
            {
                var path = undiscoveredPaths.Dequeue();
                var posAfterPath = getPositionFromPath(path);
                checkedPaths.Add(path);

                // Debugger.Break();
                // Console.WriteLine(path);

                /* if it's possible to end the path,
                 * it's logical to end the path before checking for any other potential paths.
                 * since it's already dequeued, don't have to do anything else. */
                var next = nextToPeak(posAfterPath);
                if (next is not null)
                {
                    var completedPath = $"{path}{next}";
                    completedPaths.Add(completedPath);
                    continue;
                }

                var potentialPathsToAdd = new List<string>();
                if (canGoUp(posAfterPath)) potentialPathsToAdd.Add($"{path}^");
                if (canGoRight(posAfterPath)) potentialPathsToAdd.Add($"{path}>");
                if (canGoDown(posAfterPath)) potentialPathsToAdd.Add($"{path}v");
                if (canGoLeft(posAfterPath)) potentialPathsToAdd.Add($"{path}<");

                /* now we get rid of the paths that can be added that are redundant */
                foreach (var p in potentialPathsToAdd)
                {
                    var pointsInPath = new List<(int x, int y)> {(0, 0)};
                    var pointX = 0;
                    var pointY = 0;
                    foreach (var c in p.ToCharArray())
                    {
                        if (c == '^') pointY -= 1;
                        else if (c == '>') pointX += 1;
                        else if (c == 'v') pointY += 1;
                        else if (c == '<') pointX -= 1;
                        pointsInPath.Add((pointX, pointY));
                    }

                    /* checking to make sure the path hasn't created a self closing loop */
                    var hasLoopedInItself = false;
                    for (int i = 0; i < pointsInPath.Count; i++)
                    {
                        var (x, y) = pointsInPath[i];
                        var firstPosOfPoint = pointsInPath.FindIndex(asd => asd.x == x && asd.y == y);
                        if (firstPosOfPoint < i)
                        {
                            hasLoopedInItself = true;
                            break;
                        }
                    }

                    if (hasLoopedInItself) continue;

                    var alreadyChecked = checkedPaths.Contains(p);
                    if (alreadyChecked) continue;

                    var alreadyQueued = undiscoveredPaths.Contains(p);
                    if (alreadyQueued) continue;

                    // Debugger.Break();
                    /* making sure path isn't redundant */
                    var (fpX, fpY) = getPositionFromPath(p);
                    Func<string, bool> pred = cp =>
                    {
                        // Debugger.Break();
                        var (cpX, cpY) = getPositionFromPath(cp);
                        if (fpX != cpX || fpY != cpY) return false;
                        return cp.Length <= p.Length;
                    };
                    var pIsRedundant = checkedPaths.Any(pred) || undiscoveredPaths.Any(pred);
                    if (pIsRedundant) continue;

                    undiscoveredPaths.Enqueue(p);
                }

                Debugger.Break();
            } while (undiscoveredPaths.Count > 0);


            /* finished solving and formatting  answer */
            var completedPathsOrdered = completedPaths.Where(x => !x.Contains('-')).OrderBy(x => x.Length).ToArray();
            if (completedPathsOrdered.Length == 0) continue;
            var bestPath = completedPathsOrdered[0];
            var answer = bestPath.Length;

            bestPaths.Add((answer, bestPath));
            Console.WriteLine($"Found bestPath: {bestPaths.Count}/{startingPoints.Count} with step cost of {answer}");
        }

        return bestPaths.OrderBy(x => x.answer).ToArray()[0];
    }

    // TODO: this doesn't work properly, the return value never gets emitted, but since the console is getting output i manually checked
    // this solution is also extremely slow, needs to be optimized
    static int GetValueFromChar(char c)
    {
        // a -> 97
        // z -> 122
        // A -> 65
        // Z -> 90
        int code = c;
        if (code is >= 65 and <= 90) return code - 64 + 26;
        if (code is >= 97 and <= 122) return code - 96;
        throw new Exception("Bad argument");
    }

    public static int DoPart2()
    {
        return 0;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        // Console.WriteLine(DoPart1());
        DoPart1();

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}