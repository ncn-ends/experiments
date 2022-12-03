using Utils;

namespace Subjects.AoC._2020._1;
//
// public static class Solution
// {
//     private static List<int> _input = AOCInput.Import();
//     
//     private const int _numToSumTo = 2020;
//     public static int DoPart1()
//     {
//         _input.Sort();
//         foreach (var num in _input)
//         {
//             int? match = FindMatching(num);
//             if (match is null) continue;
//             return (int) (match * num);
//         }
//
//         return -1;
//     }
//
//     private static int? FindMatching(int numA)
//     {
//         foreach (var numB in Enumerable.Reverse(_input))
//         {
//             var sum = numB + numA;
//             if (sum == _numToSumTo) return numB;
//             if (sum < _numToSumTo) break;
//         }
//
//         return null;
//     }
//
//     public static int DoPart1V2()
//     {
//         return _input
//             .Where(x => _input.Contains(2020 - x))
//             .Aggregate(1, (a, b) => a * b);
//     }
//     
//     // SELECT
//     //     a1.num*a2.num
//     // FROM
//     //     day1 a1
//     //     , day1 a2
//     // WHERE 
//     //     a1.num + a2.num = 2020
//     // LIMIT 1
//     
//     /* copied from elsewhere */
//     public static int DoPart2()
//     {
//         return _input
//             .Where(x => _input.FirstOrDefault(y => _input.Contains(2020 - x - y)) > 0)
//             .Aggregate(1, (a, b) => a * b);
//     }
// }