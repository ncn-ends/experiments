using System.Diagnostics;
using static Tests.Utils.PrintUtility;

var stopwatch = new Stopwatch();
stopwatch.Start();

AoC2.Y2018.Day11Solutions.SolvePart1();

Console.WriteLine("-----");
Console.WriteLine($"Ran in {stopwatch.Elapsed.Microseconds}µs");
stopwatch.Stop();