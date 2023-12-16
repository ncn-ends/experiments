using System.Diagnostics;
using System.Drawing;
using AoC.Y2023;
using FluentAssertions.Execution;
using Microsoft.FSharp.Core;
using NUnit.Framework;
using Subjects.LeetCode;
using Utils.Matrix;
using static Tests.Utils.PrintUtility;


var (grid, pipe) = Day10Solutions.GetVisualizationMaterial();

/* set up new grid */
var list = new List<List<(char val, GridStatus status, bool inBetween)>>();
for (var y = 0; y < grid.Count; y++)
{
    list.Add(new());
    var thingsAdded = 0;
    for (var x = 0; x < grid[y].Count; x++)
    {
        var status = GridStatus.Unknown;
        if (pipe.Any(pipeNode => pipeNode.x == x && pipeNode.y == y))
            status = GridStatus.Pipe;

        if (status == GridStatus.Unknown)
        {
            var hasNodeAdjacent = false; // tbd

            grid.ToStringGrid().IterateAdjacentNodesSafely(x, y, (newX, newY) =>
            {
                if (pipe.Any(pipeNode => pipeNode.x == newX && pipeNode.y == newY)) hasNodeAdjacent = true;
            });

            if (hasNodeAdjacent) status = GridStatus.PipeAdjacent;
            else status                 = GridStatus.Open;
        }

        var val = char.Parse(grid[y][y].val);
        list[y].Add((val, status, false));
        thingsAdded++;

        if (x < grid[y].Count - 1)
        {
            list[y].Add(('x', GridStatus.Unknown, true));
            thingsAdded++;
        }
    }

    if (y < grid.Count - 1)
    {
        var betweener = Enumerable.Repeat(('x', GridStatus.Unknown, true), thingsAdded);
        list.Add(betweener.ToList());
    }
}

/* visualization */
list.ToStringGrid(n => n.val.ToString()).ComplexVisualize((nodeCtx) =>
{
    var x = nodeCtx.X;
    var y = nodeCtx.Y;

    // if (nodeStatusMatrix.TryGetValue((x, y), out var status))
    // {
    //     if (status == Status.Pipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkCyan);
    //     if (status == Status.PipeTouchingOutside) nodeCtx.SetBackgroundColor(ConsoleColor.DarkBlue);
    //     if (status == Status.Outside) nodeCtx.SetBackgroundColor(ConsoleColor.Magenta);
    //     if (status == Status.OutsideTouchingPipe) nodeCtx.SetBackgroundColor(ConsoleColor.DarkMagenta);
    //     if (status == Status.Unknown) nodeCtx.SetBackgroundColor(ConsoleColor.Red);
    //     if (status == Status.In) nodeCtx.SetBackgroundColor(ConsoleColor.Green);
    //     if (status == Status.PathFromOutside) nodeCtx.SetBackgroundColor(ConsoleColor.Yellow);
    //     if (status == Status.PreviouslyUnknownNowConsideredOutSide) nodeCtx.SetBackgroundColor(ConsoleColor.Gray);
    // }

    if (nodeCtx.Val == '|') nodeCtx.SetAlias("‖");
    else if (nodeCtx.Val == '7') nodeCtx.SetAlias("\u2557");
    else if (nodeCtx.Val == 'J') nodeCtx.SetAlias("\u255d");
    else if (nodeCtx.Val == 'F') nodeCtx.SetAlias("\u2554");
    else if (nodeCtx.Val == 'L') nodeCtx.SetAlias("\u255a");
    else if (nodeCtx.Val == '-') nodeCtx.SetAlias("\u2550");
});

enum GridStatus
{
    Unknown,
    Pipe,
    PipeAdjacent,
    Open,
}