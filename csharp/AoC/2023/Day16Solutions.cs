using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day16Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       .|...\....
                       |.-.\.....
                       .....|-...
                       ........|.
                       ..........
                       .........\
                       ..../.\\..
                       .-.-/..|..
                       .|....-|.\
                       ..//.|....
                       """;

        var example2 = """

                       """;

        var input = AocHandler.ImportHttp();

        // Assert.That(DoPart1(example1), Is.EqualTo(46));

        // TestContext.Out.WriteLine(DoPart1(input));

        // Assert.That(DoPart2(example1), Is.EqualTo(51));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    // private static int DoPart1(string input)
    // {
    //     var grid = input.ToStringGrid();
    //     var beams = new Queue<(int x, int y, Direction dir, int lastEnergized)>();
    //     beams.Enqueue((0, 0, Direction.East, 0));
    //
    //     var energized = new HashSet<(int x, int y)>();
    //     var alreadyAdded = 0;
    //
    //     // var max = 0;
    //
    //     while (true)
    //     {
    //         var beam = beams.Dequeue();
    //         var (x, y, dir, lastEnergized) = beam;
    //
    //         /* beam has gone off grid */
    //         if (x < 0 || y < 0) continue;
    //         if (x > grid[0].Length - 1 || y > grid.Length - 1) continue;
    //
    //         var tileContents = grid[y][x];
    //         if (!energized.Add((x, y))) alreadyAdded++;
    //
    //         // else
    //         // {
    //         //     lastEnergized      = 0;
    //         //     beam.lastEnergized = 0;
    //         // }
    //
    //         // if (lastEnergized > max) max = lastEnergized;
    //
    //         if (alreadyAdded > energized.Count * 2000) break;
    //
    //         var continueMoving = tileContents == "."
    //                              || (tileContents == "-" && dir is Direction.East or Direction.West)
    //                              || (tileContents == "|" && dir is Direction.North or Direction.South);
    //         var timeToTurnSouth = (tileContents == "\\" && dir is Direction.East) ||
    //                               (tileContents == "/" && dir is Direction.West);
    //         var timeToTurnNorth = (tileContents == "\\" && dir is Direction.West) ||
    //                               (tileContents == "/" && dir is Direction.East);
    //         var timeToTurnWest = (tileContents == "\\" && dir is Direction.North) ||
    //                              (tileContents == "/" && dir is Direction.South);
    //         var timeToTurnEast = (tileContents == "\\" && dir is Direction.South) ||
    //                              (tileContents == "/" && dir is Direction.North);
    //         var timeToSplitSideways = (tileContents == "-" && dir is Direction.North or Direction.South);
    //         var timeToSplitUpDown = (tileContents == "|" && dir is Direction.East or Direction.West);
    //         if (continueMoving)
    //         {
    //             var newPosition = ApplyDirectionalMovement(beam.x, beam.y, beam.dir, beam.lastEnergized);
    //             beams.Enqueue(newPosition);
    //         }
    //         else if (timeToTurnNorth)
    //         {
    //             var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.North, beam.lastEnergized);
    //             beams.Enqueue(newPosition);
    //         }
    //         else if (timeToTurnEast)
    //         {
    //             var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.East, beam.lastEnergized);
    //             beams.Enqueue(newPosition);
    //         }
    //         else if (timeToTurnSouth)
    //         {
    //             var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.South, beam.lastEnergized);
    //             beams.Enqueue(newPosition);
    //         }
    //         else if (timeToTurnWest)
    //         {
    //             var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.West, beam.lastEnergized);
    //             beams.Enqueue(newPosition);
    //         }
    //         else if (timeToSplitSideways)
    //         {
    //             var newPositionA = ApplyDirectionalMovement(beam.x, beam.y, Direction.West, beam.lastEnergized);
    //             var newPositionB = ApplyDirectionalMovement(beam.x, beam.y, Direction.East, beam.lastEnergized);
    //             beams.Enqueue(newPositionA);
    //             beams.Enqueue(newPositionB);
    //         }
    //         else if (timeToSplitUpDown)
    //         {
    //             var newPositionA = ApplyDirectionalMovement(beam.x, beam.y, Direction.North, beam.lastEnergized);
    //             var newPositionB = ApplyDirectionalMovement(beam.x, beam.y, Direction.South, beam.lastEnergized);
    //             beams.Enqueue(newPositionA);
    //             beams.Enqueue(newPositionB);
    //         }
    //     }
    //
    //     // TestContext.Out.WriteLine(max);
    //     return energized.Count;
    // }

    private static int DoPart2(string input)
    {
        var grid = input.ToStringGrid();
        var startingPoints = new HashSet<(int x, int y, Direction dir)>();
        startingPoints.Add((0, 0, Direction.East));
        startingPoints.Add((0, 0, Direction.South));
        startingPoints.Add((grid[0].Length - 1, 0, Direction.West));
        startingPoints.Add((grid[0].Length - 1, 0, Direction.South));
        startingPoints.Add((grid[0].Length - 1, grid.Length - 1, Direction.North));
        startingPoints.Add((grid[0].Length - 1, grid.Length - 1, Direction.West));
        startingPoints.Add((0, grid.Length - 1, Direction.North));
        startingPoints.Add((0, grid.Length - 1, Direction.East));

        for (var y = 1; y < grid.Length - 1; y++)
        {
            startingPoints.Add((0, y, Direction.East));
            startingPoints.Add((grid[0].Length - 1, y, Direction.West));
        }

        for (var x = 1; x < grid[0].Length - 1; x++)
        {
            startingPoints.Add((x, 0, Direction.South));
            startingPoints.Add((x, grid[0].Length - 1, Direction.North));
        }

        var max = 0;
        foreach (var startingPoint in startingPoints)
        {
            var beams = new Queue<(int x, int y, Direction dir)>();
            beams.Enqueue(startingPoint);

            var energized = new HashSet<(int x, int y)>();
            var alreadyAdded = 0;

            while (beams.Any())
            {
                var beam = beams.Dequeue();
                var (x, y, dir) = beam;

                /* beam has gone off grid */
                if (x < 0 || y < 0) continue;
                if (x > grid[0].Length - 1 || y > grid.Length - 1) continue;

                var tileContents = grid[y][x];
                if (!energized.Add((x, y))) alreadyAdded++;

                if (alreadyAdded > energized.Count * 2000) break;

                var continueMoving = tileContents == "."
                                     || (tileContents == "-" && dir is Direction.East or Direction.West)
                                     || (tileContents == "|" && dir is Direction.North or Direction.South);
                var timeToTurnSouth = (tileContents == "\\" && dir is Direction.East) ||
                                      (tileContents == "/" && dir is Direction.West);
                var timeToTurnNorth = (tileContents == "\\" && dir is Direction.West) ||
                                      (tileContents == "/" && dir is Direction.East);
                var timeToTurnWest = (tileContents == "\\" && dir is Direction.North) ||
                                     (tileContents == "/" && dir is Direction.South);
                var timeToTurnEast = (tileContents == "\\" && dir is Direction.South) ||
                                     (tileContents == "/" && dir is Direction.North);
                var timeToSplitSideways = (tileContents == "-" && dir is Direction.North or Direction.South);
                var timeToSplitUpDown = (tileContents == "|" && dir is Direction.East or Direction.West);
                if (continueMoving)
                {
                    var newPosition = ApplyDirectionalMovement(beam.x, beam.y, beam.dir);
                    beams.Enqueue(newPosition);
                }
                else if (timeToTurnNorth)
                {
                    var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.North);
                    beams.Enqueue(newPosition);
                }
                else if (timeToTurnEast)
                {
                    var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.East);
                    beams.Enqueue(newPosition);
                }
                else if (timeToTurnSouth)
                {
                    var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.South);
                    beams.Enqueue(newPosition);
                }
                else if (timeToTurnWest)
                {
                    var newPosition = ApplyDirectionalMovement(beam.x, beam.y, Direction.West);
                    beams.Enqueue(newPosition);
                }
                else if (timeToSplitSideways)
                {
                    var newPositionA = ApplyDirectionalMovement(beam.x, beam.y, Direction.West);
                    var newPositionB = ApplyDirectionalMovement(beam.x, beam.y, Direction.East);
                    beams.Enqueue(newPositionA);
                    beams.Enqueue(newPositionB);
                }
                else if (timeToSplitUpDown)
                {
                    var newPositionA = ApplyDirectionalMovement(beam.x, beam.y, Direction.North);
                    var newPositionB = ApplyDirectionalMovement(beam.x, beam.y, Direction.South);
                    beams.Enqueue(newPositionA);
                    beams.Enqueue(newPositionB);
                }
            }

            if (max < energized.Count) max = energized.Count;
        }

        return max;
    }

    private static (int x, int y, Direction dir) ApplyDirectionalMovement(int x,
        int y,
        Direction dir)
    {
        if (dir == Direction.North) return (x, y - 1, dir);
        if (dir == Direction.East) return (x + 1, y, dir);
        if (dir == Direction.South) return (x, y + 1, dir);
        if (dir == Direction.West) return (x - 1, y, dir);
        return (x, y, dir);
    }
}

enum Direction
{
    North,
    South,
    West,
    East
}