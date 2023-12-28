using Subjects.Structures;
using Utils.Matrix;
using Utils.Strings;


namespace AoC.Y2021;

// 0053
// public static class Day15Solutions
// {
//     [Test]
//     public static void Run()
//     {
//         var example1 = """
//                        1163751742
//                        1381373672
//                        2136511328
//                        3694931569
//                        7463417111
//                        1319128137
//                        1359912421
//                        3125421639
//                        1293138521
//                        2311944581
//                        """;
//
//         var example2 = """
//
//                        """;
//
//         var input = AocHandler.ImportHttp();
//
//         Assert.That(DoPart1(example1), Is.EqualTo(40));
//         TestContext.Out.WriteLine(DoPart1(input));
//
//         // Assert.That(DoPart2(example2), Is.EqualTo(0));
//         // TestContext.Out.WriteLine(DoPart2(input));
//     }
//
//     private static int DoPart1(string input)
//     {
//         var movements = MovementHelpers.GetNonDiagnalMovements();
//
//         var matrix = input.ToWeightedMatrix();
//         var start = matrix[0][0];
//         var end = matrix[^1][^1];
//         var c = start;
//         while (c.xPos != end.xPos && c.yPos != end.yPos)
//         {
//             c.visited = true;
//             var next = c;
//
//
//
//             if (next == c) throw new Exception("Bad");
//
//         }
//
//         return default;
//
//         //
//         // var c = start;
//         // while (c != end)
//         // {
//         //     var nodeInfo = dict[c];
//         //     nodeInfo.visited = true;
//         //     var list = new List<(int x, int y, int weight)>();
//         //     foreach (var movement in movements)
//         //     {
//         //         var toX = c.x + movement.modX;
//         //         var toY = c.y + movement.modY;
//         //         if (toX < 0 || toY < 0) continue;
//         //         if (toX >= matrix.Length || toY >= matrix.Length) continue;
//         //         var checkingNode = dict[(c.x + movement.modX, c.y + movement.modY)];
//         //         var originalValue = matrix[toY][toX];
//         //         var newWeight = originalValue + nodeInfo.weight;
//         //         if (newWeight < checkingNode.weight) checkingNode.weight = newWeight;
//         //         list.Add((toX, toY, newWeight));
//         //     }
//         //
//         //     list.Sort((a, b) => a.weight - b.weight);
//         //     c = (list.First().x, list.First().y);
//
//         return default;
//     }
//
//     private static int DoPart2(string input)
//     {
//         return default;
//     }
// }