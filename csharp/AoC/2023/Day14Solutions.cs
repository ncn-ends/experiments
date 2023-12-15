using System.Diagnostics;
using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2023;

public static class Day14Solutions
{
    [Test]
    public static void Run()
    {
        var example1 = """
                       O....#....
                       O.OO#....#
                       .....##...
                       OO.#O....O
                       .O.....O#.
                       O.#..O.#.#
                       ..O..#O..O
                       .......O..
                       #....###..
                       #OO..#....
                       """;

        var input = AocHandler.ImportHttp();

        Assert.That(DoPart1(example1), Is.EqualTo(136));
        TestContext.Out.WriteLine(DoPart1(input));

        Assert.That(DoPart2(example1), Is.EqualTo(64));
        TestContext.Out.WriteLine(DoPart2(input));
    }

    private static int DoPart1(string input)
    {
        var grid = input.ToStringGrid();
        for (var x = 0; x < grid[0].Length; x++)
        {
            var bouldersQueued = 0;
            for (var y = grid.Length - 1; y >= 0; y--)
            {
                var c = grid[y][x];
                if (c == "O")
                {
                    bouldersQueued++;
                    grid[y][x] = ".";
                }
                else if (c == "#")
                {
                    DropBoulders(x, y + 1, bouldersQueued);
                    bouldersQueued = 0;
                }
            }

            DropBoulders(x, 0, bouldersQueued);
        }

        var rockLoads = new List<int>();
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == "O") rockLoads.Add(grid.Length - y);
            }
        }

        return rockLoads.Sum();

        void DropBoulders(int x,
                          int y,
                          int n)
        {
            for (int i = 0; i < n; i++)
            {
                grid[y + i][x] = "O";
            }
        }
    }

    /* expected south
       .....#....
       ....#....#
       ...0.##... 1
       ...#......
       0.0....0#0 4
       0.#..0.#.# 2
       0....#.... 1
       00....00.. 4
       #00..###.. 2
       #00.0#...0 4
     */

    /* expected west
       O....#....
       O00.#....#
       .....##...
       OO.#O0....
       0O......#.
       O.#0...#.#
       0....#O0..
       0.........
       #....###..
       #OO..#....
     */

    /* expected east
       ....0#....
       .0OO#....#
       .....##...
       .O0#....0O
       ......0O#.
       .0#...0#.#
       ....0#..0O
       .........0
       #....###..
       #..00#....
     */

    private static int DoPart2(string input)
    {
        var grid = input.ToStringGrid();
        var dict = new Dictionary<string, (int sum, int cycle)>();

        var cycles = 1000000000;
        var skipped = false;
        var sum = 0;
        for (int i = 1; i <= cycles; i++)
        {
            TiltGridNorth();
            TiltGridWest();
            TiltGridSouth();
            TiltGridEast();

            var rockLoads = new List<int>();
            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == "O") rockLoads.Add(grid.Length - y);
                }
            }

            sum = rockLoads.Sum();

            var key = grid.ToSimpleString();
            if (!dict.TryAdd(key, (sum, i)) && !skipped)
            {
                var everyXCycles = i - dict[key].cycle;

                // find highest number less than or equal to "cycles" that is a multiple of "everyXCycles" starting from i
                var t = i;
                for (; t + everyXCycles < cycles; t += everyXCycles)
                {
                }

                i       = t; // minus 1 or not? not sure
                skipped = true;
            }
        }

        return sum;

        void TiltGridNorth()
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                var bouldersQueued = 0;
                for (var y = grid.Length - 1; y >= 0; y--)
                {
                    var c = grid[y][x];
                    if (c == "O")
                    {
                        bouldersQueued++;
                        grid[y][x] = ".";
                    }
                    else if (c == "#")
                    {
                        for (int i = 0; i < bouldersQueued; i++)
                        {
                            grid[y + i + 1][x] = "O";
                        }

                        bouldersQueued = 0;
                    }
                }

                for (int i = 0; i < bouldersQueued; i++)
                {
                    grid[i][x] = "O";
                }
            }
        }

        void TiltGridSouth()
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                var bouldersQueued = 0;
                for (var y = 0; y < grid.Length; y++)
                {
                    var c = grid[y][x];
                    if (c == "O")
                    {
                        bouldersQueued++;
                        grid[y][x] = ".";
                    }
                    else if (c == "#")
                    {
                        for (int i = 0; i < bouldersQueued; i++)
                        {
                            grid[y - i - 1][x] = "O";
                        }

                        bouldersQueued = 0;
                    }
                }

                for (int i = 0; i < bouldersQueued; i++)
                {
                    grid[grid.Length - i - 1][x] = "O";
                }
            }
        }

        void TiltGridWest()
        {
            for (var y = 0; y < grid.Length; y++)
            {
                var bouldersQueued = 0;
                for (var x = grid[y].Length - 1; x >= 0; x--)
                {
                    var c = grid[y][x];
                    if (c == "O")
                    {
                        bouldersQueued++;
                        grid[y][x] = ".";
                    }
                    else if (c == "#")
                    {
                        for (int i = 0; i < bouldersQueued; i++)
                        {
                            grid[y][x + 1 + i] = "O";
                        }

                        bouldersQueued = 0;
                    }
                }

                for (int i = 0; i < bouldersQueued; i++)
                {
                    grid[y][i] = "O";
                }
            }
        }

        void TiltGridEast()
        {
            for (var y = 0; y < grid.Length; y++)
            {
                var bouldersQueued = 0;
                for (var x = 0; x < grid[y].Length; x++)
                {
                    var c = grid[y][x];
                    if (c == "O")
                    {
                        bouldersQueued++;
                        grid[y][x] = ".";
                    }
                    else if (c == "#")
                    {
                        for (int i = 0; i < bouldersQueued; i++)
                        {
                            grid[y][x - i - 1] = "O";
                        }
                        bouldersQueued = 0;
                    }
                }

                for (int i = 0; i < bouldersQueued; i++)
                {
                    grid[y][grid[y].Length - i - 1] = "O";
                }
            }
        }
    }
}