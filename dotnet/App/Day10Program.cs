using System.Diagnostics;
using System.Drawing;
using AoC.Y2023;
using FluentAssertions.Execution;
using Microsoft.FSharp.Core;
using NUnit.Framework;
using Subjects.LeetCode;
using Utils.Matrix;
using static Tests.Utils.PrintUtility;


var (matrix, pipe) = Day10Solutions.GetVisualizationMaterial();

var nodeStatusMatrix = new Dictionary<(int x, int y), Status>();

/* apply status of the pipe */
foreach (var node in pipe)
{
    nodeStatusMatrix.TryAdd((node.x, node.y), Status.Pipe);
}

var borderOutsideNodes = new Queue<(int x, int y)>();


/* apply status of outside */
var q = new Queue<(int x, int y)>();
var vst = new HashSet<(int x, int y)>();
q.Enqueue((0, 0));
q.Enqueue((matrix[0].Count - 1, matrix[0].Count));
while (q.Any())
{
    var c = q.Dequeue();
    if (!vst.Add(c)) continue;
    nodeStatusMatrix.TryAdd((c.x, c.y), Status.Outside);
    var movements = MovementHelpers.GetAdjacentMovements();
    foreach (var mv in movements)
    {
        var next = (x: c.x + mv.modX, y: c.y + mv.modY);
        if (vst.Contains((next.x, next.y))) continue;
        if (next.x < 0 || next.x >= matrix[0].Count) continue;
        if (next.y < 0 || next.y >= matrix.Count) continue;
        if (pipe.Contains((next.x, next.y)))
        {
            nodeStatusMatrix.TryAdd((next.x, next.y), Status.Pipe);
            nodeStatusMatrix[(c.x, c.y)] = Status.Outside;
            borderOutsideNodes.Enqueue((c.x, c.y));
            continue;
        }

        q.Enqueue((next.x, next.y));
    }
}

for (var y = 0; y < matrix.Count; y++)
{
    for (var x = 0; x < matrix[y].Count; x++)
    {
        nodeStatusMatrix.TryAdd((x, y), Status.Unknown);
    }
}

// for (var y = 0; y < matrix.Count; y++)
// {
//     for (var x = 0; x < matrix[y].Count; x++)
//     {
//         if (nodeStatusMatrix.ContainsKey((x, y))) continue;
//
//         nodeStatusMatrix.Add((x, y), Status.Unknown);
//
//         var node = matrix[y][x];
//         // if (!matrix[x - 1][y].val.ConnectsToNorth() || !matrix[x - 1][y - 1].val.ConnectsToSouth()) continue;
//         // if (!matrix[x - 1][y - 1].val.ConnectsToEast() || !matrix[x][y - 1].val.ConnectsToWest()) continue;
//         // if (!matrix[x][y - 1].val.ConnectsToEast() || !matrix[]) continue;
//
//         /* for each possible adjacent connection, need to make sure that they're closed */
//         /* if not closed, then unknown, so don't modify status */
//         // if (!matrix[y][x - 1].val.ConnectsToNorth() || !matrix[y - 1][x - 1].val.ConnectsToSouth()) continue;
//         // if (!matrix[y - 1][x - 1].val.ConnectsToEast() || !matrix[y - 1][x].val.ConnectsToWest()) continue;
//         List<(int modX, int modY, Predicate<string> apply)> asd = [
//             (-1, 0, str => str.ConnectsToNorth()), // left, checking top connection
//             (-1, 0, str => str.ConnectsToSouth()), // left, checking bottom connection
//             (0, -1, str => str.ConnectsToWest()), // top, checking left connection
//             (0, -1, str => str.ConnectsToEast()), // top, checking right connection
//             (1, 0, str => str.ConnectsToNorth()), // right, checking top connection
//             (1, 0, str => str.ConnectsToSouth()), // right, checking bottom connection
//             (0, 1, str => str.ConnectsToWest()), // bottom, checking left connection
//             (0, 1, str => str.ConnectsToEast()), // bottom, checking right connection
//         ];
//
//         var qwe = asd.Where(dir =>
//         {
//             var newX = dir.modX + x;
//             var newY = dir.modY + y;
//             if (newX < 0 || newX >= matrix[0].Count) return false;
//             if (newY < 0 || newY >= matrix.Count) return false;
//             var res = dir.apply(matrix[newY][newX].val);
//             return res;
//         }).ToList();
//
//         if (qwe.Count == 0) nodeStatusMatrix[(x, y)] = Status.In;
//     }
// }
//

var paths = new Queue<(int x1, int y1, int x2, int y2)>();

var visited = new HashSet<(int x1, int y1, int x2, int y2)>();

/* this is just setting the initial paths from the outside that will enter the borders of the maze */
while (borderOutsideNodes.Any())
{
    var c = borderOutsideNodes.Dequeue();

    foreach (var dir in MovementHelpers.GetAdjacentMovements())
    {
        var next = (x: c.x + dir.modX, y: c.y + dir.modY);
        if (next.x < 0 || next.x >= matrix[0].Count) continue;
        if (next.y < 0 || next.y >= matrix.Count) continue;

        // if (nodeStatusMatrix[next] == Status.Unknown) nodeStatusMatrix[next]   = Status.JustTesting;
        // else if (nodeStatusMatrix[next] == Status.Pipe) nodeStatusMatrix[next] = Status.PipeTouchingOutside;
    }

    EnqueuePathsFromThisPoint(c);
}

/* traversing pipes from the entry points */
while (paths.Any())
{
    var pathPoint = paths.Dequeue();

    /* an opening has been discovered */
    /* new paths will emerge from here possibly */
    var firstPointIsUnknown = nodeStatusMatrix[(pathPoint.x1, pathPoint.y1)] == Status.Unknown;
    var secondPointIsUnknown = nodeStatusMatrix[(pathPoint.x2, pathPoint.y2)] == Status.Unknown;
    if (firstPointIsUnknown || secondPointIsUnknown)
    {
        if (firstPointIsUnknown)
        {
            var firstPoint = (pathPoint.x1, pathPoint.y1);
            MarkUnknownGroupAsOpen(firstPoint);
        }

        if (secondPointIsUnknown)
        {
            var secondPoint = (pathPoint.x2, pathPoint.y2);
            MarkUnknownGroupAsOpen(secondPoint);
        }

        /* once an opening has been discovered, the path can't continue */
        continue;
    }

    if (!visited.Add(pathPoint)) continue;

    /* not sure if this is the right spot */
    /* mark any touching unknowns as open */
    var firstPnt = (x: pathPoint.x1, y: pathPoint.y1);
    var secondPnt = (x: pathPoint.x2, y: pathPoint.y2);
    var asd = new[] {firstPnt, secondPnt};
    foreach (var pnt in asd)
    {
        foreach (var dirr in MovementHelpers.GetAdjacentMovements())
        {
            var nextX = pnt.x + dirr.modX;
            var nextY = pnt.y + dirr.modY;
            if (!IsValidPointOnMatrix((nextX, nextY))) continue;
            if (nodeStatusMatrix[(nextX, nextY)] == Status.Unknown)
            {
                MarkUnknownGroupAsOpen((nextX, nextY));
            }
        }
    }

    var diffX = pathPoint.x1 - pathPoint.x2;
    var isMovingVertically = diffX != 0;

    var nextForward = pathPoint;
    var nextBackward = pathPoint;

    if (isMovingVertically)
    {
        nextForward.y1  += 1;
        nextBackward.y1 -= 1;
        nextForward.y2  += 1;
        nextBackward.y2 -= 1;
    }
    else
    {
        nextForward.x1  += 1;
        nextBackward.x1 -= 1;
        nextForward.x2  += 1;
        nextBackward.x2 -= 1;
    }

    var next = !visited.Contains(nextForward)
               && IsValidPointOnMatrix((nextForward.x1, nextForward.y1))
               && IsValidPointOnMatrix((nextForward.x2, nextForward.y2))
               && IsNotOpen((nextForward.x1, nextForward.y1))
               && IsNotOpen((nextForward.x2, nextForward.y2))
            ? nextForward
            : nextBackward;

    var dir = Direction.Unknown;
    if (isMovingVertically && next == nextForward) dir   = Direction.Down;
    if (isMovingVertically && next == nextBackward) dir  = Direction.Up;
    if (!isMovingVertically && next == nextForward) dir  = Direction.Right;
    if (!isMovingVertically && next == nextBackward) dir = Direction.Left;

    /* account left/right turns in path */

    // if (dir == Direction.Right)
    // {
    //     (int x, int y) bottomPoint = pathPoint.y2 < pathPoint.y1
    //             ? (pathPoint.x2, pathPoint.y2)
    //             : (pathPoint.x1, pathPoint.y1);
    //
    //     if (!matrix[bottomPoint.y][bottomPoint.x].val.ConnectsToEast()
    //         && IsValidPointOnMatrix((bottomPoint.x + 1, bottomPoint.y)))
    //         paths.Enqueue((bottomPoint.x, bottomPoint.y, bottomPoint.x + 1, bottomPoint.y));
    // }o


    // if (!matrix[topPoint.y][topPoint.x].val.ConnectsToWest())
    // {
    //     var pointToConsider = (x1: topPoint.x - 1, y1: topPoint.y, x2: topPoint.x, y2: topPoint.y);
    //     if (CanBeConsideredForPath(pointToConsider)) paths.Enqueue(pointToConsider);
    // }

    //
    //
    // if (!matrix[leftPoint.y][leftPoint.x].val.ConnectsToNorth())
    // {
    //     var pointToConsider = (x1: leftPoint.x, y1: leftPoint.y - 1, x2: leftPoint.x, y2: leftPoint.y);
    //     if (CanBeConsideredForPath(pointToConsider)) paths.Enqueue(pointToConsider);
    // }

    if (IsClosedOff(dir, (next.x1, next.y1), (next.x2, next.y2)))
    {
        var (topPoint, bottomPoint) = pathPoint.y1 < pathPoint.y2
                ? ((x: pathPoint.x1, y: pathPoint.y1), (x: pathPoint.x2, y: pathPoint.y2))
                : ((x: pathPoint.x2, y: pathPoint.y2), (x: pathPoint.x1, y: pathPoint.y1));

        var (leftPoint, rightPoint) = pathPoint.x1 < pathPoint.x2
                ? ((x: pathPoint.x1, y: pathPoint.y1), (x: pathPoint.x2, y: pathPoint.y2))
                : ((x: pathPoint.x2, y: pathPoint.y2), (x: pathPoint.x1, y: pathPoint.y1));

        /* if opening top left */
        if (!isMovingVertically && !matrix[topPoint.y][topPoint.x].val.ConnectsToWest())
        {
            var pointToConsider = (x1: topPoint.x - 1, y1: topPoint.y, x2: topPoint.x, y2: topPoint.y);
            if (CanBeConsideredForPath(pointToConsider)) paths.Enqueue(pointToConsider);
        }

        if (isMovingVertically && !matrix[leftPoint.y][leftPoint.x].val.ConnectsToSouth())
        {
            var pointToConsider = (x1: leftPoint.x, y1: leftPoint.y, x2: leftPoint.x, y2: leftPoint.y + 1);
            if (CanBeConsideredForPath(pointToConsider)) paths.Enqueue(pointToConsider);
        }

        if (isMovingVertically && !matrix[rightPoint.y][rightPoint.x].val.ConnectsToSouth())
        {
            var pointToConsider = (x1: rightPoint.x, y1: rightPoint.y, x2: rightPoint.x, y2: rightPoint.y + 1);
            if (CanBeConsideredForPath(pointToConsider)) paths.Enqueue(pointToConsider);
        }

        continue;
    }

    ;

    paths.Enqueue(next);
}

foreach (var v in visited)
{
    var pointA = nodeStatusMatrix[(v.x1, v.y1)];
    var pointB = nodeStatusMatrix[(v.x2, v.y2)];

    // if (pointA != Status.Unknown) nodeStatusMatrix[(v.x1, v.y1)] = Status.JustTesting;
    // if (pointB != Status.Unknown) nodeStatusMatrix[(v.x2, v.y2)] = Status.JustTesting;
    // if (pointA == Status.Unknown)
    // {
    //     MarkUnknownGroupAsOpen((v.x1, v.y1));
    //     continue;
    // }
    //
    // if (pointB == Status.Unknown)
    // {
    //     MarkUnknownGroupAsOpen((v.x2, v.y2));
    //     continue;
    // }
    if (pointA != Status.Unknown && pointA != Status.PreviouslyUnknownNowConsideredOutSide)
        nodeStatusMatrix[(v.x1, v.y1)] = Status.PathFromOutside;
    if (pointB != Status.Unknown && pointB != Status.PreviouslyUnknownNowConsideredOutSide)
        nodeStatusMatrix[(v.x2, v.y2)] = Status.PathFromOutside;
}


/* visualization */
matrix.ToStringGrid().ComplexVisualize((nodeCtx) =>
{
    var x = nodeCtx.X;
    var y = nodeCtx.Y;
    if (nodeStatusMatrix.TryGetValue((x, y), out var status))
    {
        if (status == Status.Pipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkCyan);
        if (status == Status.PipeTouchingOutside) nodeCtx.SetBackgroundColor(ConsoleColor.DarkBlue);
        if (status == Status.Outside) nodeCtx.SetBackgroundColor(ConsoleColor.Magenta);
        if (status == Status.OutsideTouchingPipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkMagenta);
        if (status == Status.Unknown) nodeCtx.SetBackgroundColor(ConsoleColor.Red);
        if (status == Status.In) nodeCtx.SetBackgroundColor(ConsoleColor.Green);
        if (status == Status.PathFromOutside) nodeCtx.SetBackgroundColor(ConsoleColor.Yellow);
        if (status == Status.PreviouslyUnknownNowConsideredOutSide) nodeCtx.SetBackgroundColor(ConsoleColor.Gray);
    }

    if (nodeCtx.Val == '|') nodeCtx.SetAlias("‖");
    else if (nodeCtx.Val == '7') nodeCtx.SetAlias("\u2557");
    else if (nodeCtx.Val == 'J') nodeCtx.SetAlias("\u255d");
    else if (nodeCtx.Val == 'F') nodeCtx.SetAlias("\u2554");
    else if (nodeCtx.Val == 'L') nodeCtx.SetAlias("\u255a");
    else if (nodeCtx.Val == '-') nodeCtx.SetAlias("\u2550");
});

// less than 646
Console.WriteLine(nodeStatusMatrix.Values.Count(x => x == Status.Unknown));

// static void PrintSurroundingNodes((int x, int y) point)
// {
//     for (int modY = -2; modY <= 2; modY++)
//     {
//         for (int modX = -2; modX <= 2; modX++)
//         {
//             if (!IsValidPointOnMatrix((point.x + modX, point.y + modY))) continue;
//             if (modX == 0 && modY == 0) Console.BackgroundColor = ConsoleColor.Red;
//
//             Console.Write(matrix[point.y + modY][point.x + modX]);
//
//             Console.ResetColor();
//         }
//         Console.WriteLine();
//     }
// }
//


bool CanBeConsideredForPath((int x1, int y1, int x2, int y2) path)
{
    var (x1, y1, x2, y2) = path;
    if (!IsValidPointOnMatrix((x1, y1))) return false;
    if (!IsValidPointOnMatrix((x2, y2))) return false;
    if (nodeStatusMatrix[(x1, y1)] != Status.Pipe) return false;
    if (nodeStatusMatrix[(x2, y2)] != Status.Pipe) return false;

    return true;
}


void EnqueuePathsFromThisPoint((int x, int y) point)
{
    var (x, y) = point;
    List<(int modX1, int modY1, Predicate<string> check1, int modX2, int modY2, Predicate<string> check2)> asd =
    [
            (-1, -1, str => str.ConnectsToEast(), 0, -1, str => str.ConnectsToWest()), // top left to top +
            (0, -1, str => str.ConnectsToEast(), 1, -1, str => str.ConnectsToWest()), // top to top right +
            (1, -1, str => str.ConnectsToSouth(), 1, 0, str => str.ConnectsToNorth()), // top right to right +
            (1, 0, str => str.ConnectsToSouth(), 1, 1, str => str.ConnectsToNorth()), // right to bottom right +
            (1, 1, str => str.ConnectsToWest(), 0, 1, str => str.ConnectsToEast()), // bottom right to bottom +
            (0, 1, str => str.ConnectsToWest(), -1, 1, str => str.ConnectsToEast()), // bottom to bottom left +
            (-1, 1, str => str.ConnectsToNorth(), -1, 0, str => str.ConnectsToSouth()), // bottom left to left +
            (-1, 0, str => str.ConnectsToNorth(), -1, -1, str => str.ConnectsToSouth()), // left to top left
    ];

    foreach (var dir in asd)
    {
        var newY1 = y + dir.modY1;
        var newY2 = y + dir.modY2;
        var newX1 = x + dir.modX1;
        var newX2 = x + dir.modX2;
        /* out of bounds check */
        if (newX1 < 0 || newY1 < 0 || newX2 < 0 || newY2 < 0) continue;
        var maxY = matrix.Count - 1;
        if (newY1 >= maxY || newY2 >= maxY) continue;
        var maxX = matrix[0].Count - 1;
        if (newX1 >= maxX || newX2 >= maxX) continue;

        var point1 = matrix[newY1][newX1];
        var point2 = matrix[newY2][newX2];

        /* for some reason S wasn't added to the pipe, but part 1 worked */
        if (point1.val == "S") continue;
        if (point2.val == "S") continue;

        /* since we're already touching the pipe, need to make sure the next path uses the pipe */
        if (!pipe.Contains((point1.xPos, point1.yPos))) continue;
        if (!pipe.Contains((point2.xPos, point2.yPos))) continue;

        if (visited.Contains((newX1, newY1, newX2, newY2))) continue;
        if (visited.Contains((newX2, newY2, newX1, newY1))) continue;

        if (nodeStatusMatrix[(point1.xPos, point1.yPos)] == Status.Outside) continue;
        if (nodeStatusMatrix[(point2.xPos, point2.yPos)] == Status.Outside) continue;

        /* actual check to make sure it's an opening */
        if (dir.check1(point1.val) && dir.check2(point2.val)) continue;

        /* don't add duplicates to the paths queue */
        var pathToBeAdded = (x1: point1.xPos, y1: point1.yPos, x2: point2.xPos, y2: point2.yPos);
        if (paths.Contains(pathToBeAdded)) continue;

        paths.Enqueue(pathToBeAdded);
    }
}

void MarkUnknownGroupAsOpen((int x, int y) point)
{
    var q = new Queue<(int x, int y)>();
    q.Enqueue(point);
    while (q.Any())
    {
        var c = q.Dequeue();
        var nodeStatus = nodeStatusMatrix[c];
        if (nodeStatus != Status.Unknown) continue;

        nodeStatusMatrix[c] = Status.PreviouslyUnknownNowConsideredOutSide;
        EnqueuePathsFromThisPoint(c);

        foreach (var dir in MovementHelpers.GetAdjacentMovements())
        {
            var nextX = c.x + dir.modX;
            var nextY = c.y + dir.modY;
            if (!IsValidPointOnMatrix((nextX, nextY))) continue;
            if (nodeStatusMatrix[(nextX, nextY)] == Status.Unknown) q.Enqueue((nextX, nextY));
        }
    }
}

bool IsValidPointOnMatrix((int x, int y) point)
{
    var (x, y) = point;
    return x >= 0 && y >= 0 && y <= matrix.Count - 1 && x <= matrix[0].Count - 1;
}

bool IsNotOpen((int x, int y) point)
{
    var n = nodeStatusMatrix[(point.x, point.y)];
    if (n == Status.Outside || n == Status.OutsideTouchingPipe) return false;
    return true;
}

bool IsClosedOff(Direction dir,
                 (int x, int y) pointA,
                 (int x, int y) pointB)
{
    /* janky but temp */
    if (!IsValidPointOnMatrix(pointA)) return true;
    if (!IsValidPointOnMatrix(pointB)) return true;
    if (nodeStatusMatrix[(pointA.x, pointA.y)] == Status.Unknown) return false;

    var nodeA = matrix[pointA.y][pointA.x];
    var nodeB = matrix[pointB.y][pointB.x];

    if (dir == Direction.Right && nodeA.val.ConnectsToSouth() && nodeB.val.ConnectsToNorth()) return true;
    if (dir == Direction.Down && nodeA.val.ConnectsToWest() && nodeB.val.ConnectsToEast()) return true;
    if (dir == Direction.Left && nodeA.val.ConnectsToNorth() && nodeB.val.ConnectsToSouth()) return true;
    if (dir == Direction.Up && nodeA.val.ConnectsToEast() && nodeB.val.ConnectsToWest()) return true;

    return false;
}

public static class PipeMapper
{
    public static string[] SouthConnecting => ["|", "7", "F"];
    public static string[] NorthConnecting => ["|", "J", "L"];
    public static string[] EastConnecting => ["-", "L", "F"];
    public static string[] WestConnecting => ["-", "7", "J"];

    public static bool ConnectsToNorth(this string s) => NorthConnecting.Contains(s);
    public static bool ConnectsToSouth(this string s) => SouthConnecting.Contains(s);
    public static bool ConnectsToEast(this string s) => EastConnecting.Contains(s);
    public static bool ConnectsToWest(this string s) => WestConnecting.Contains(s);
}

enum Direction
{
    Unknown,
    Down,
    Up,
    Left,
    Right
}

[Flags]
enum Status
{
    Unknown = 0,
    Outside = 1,
    PathFromOutside = 2,
    OutsideTouchingPipe = 4,
    In = 8,
    Pipe = 16,
    PipeTouchingOutside = 32,
    PreviouslyUnknownNowConsideredOutSide = 64
}