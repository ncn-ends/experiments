namespace Subjects.LeetCode;

public class Solution
{
// Print(FindClosestElements(new[] {1, 2, 3, 4, 5}, 4, 3)); // 1, 2, 3, 4
// Print(FindClosestElements(new[] {1, 2, 3, 4, 5}, 4, -1)); // 1, 2, 3, 4
// Print(FindClosestElements(new[] {11, 12, 13, 14, 15}, 3, 3)); // Expected Output: [11, 12, 13]
// Print(FindClosestElements(new[] {5, 6, 7}, 2, 5)); // Expected Output: [5, 6]
// Print(FindClosestElements(new[] {1, 2, 3, 4, 5}, 5, 3)); // Expected Output: [1, 2, 3, 4, 5]
// Print(FindClosestElements(new[] {1, 2, 3, 4, 5}, 3, 10)); // Expected Output: [3, 4, 5] or [2, 3, 4]
// Print(FindClosestElements(new[] {0, 1, 1, 1, 2, 3, 6, 7, 8, 9}, 9, 4)); // Expected Output: [0,1,1,1,2,3,6,7,8]
// Print(FindClosestElements(new[] {1, 1, 1, 10, 10, 10}, 1, 9)); // Expected Output: [10]
// Print(FindClosestElements(new[] {0,0,1,2,3,3,4,7,7,8}, 3, 5)); // Expected Output: [3,3,4]
// Print(FindClosestElements(new[] {0,0,0,1,3,5,6,7,8,8}, 2, 2));
// Print(FindClosestElements(new[] {0, 2, 2, 3, 4, 6, 7, 8, 9, 9}, 4, 5)); // 3, 4, 6, 7
// Print(FindClosestElements(new[] {0,1,2,2,2,3,6,8,8,9}, 5, 9)); // 3, 6, 8, 8, 9
// Print(FindClosestElements(new[] {3, 5, 8, 10}, 2, 15)); // 3, 6, 8, 8, 9
// [0,1,2,3,4,4,4,5,5,5,6,7,9,9,10,10,11,11,12,13,14,14,15,17,19,19,22,24,24,25,25,27,27,29,30,32,32,33,33,35,36,38,39,41,42,43,44,44,46,47,48,49,52,53,53,54,54,57,57,58,59,59,59,60,60,60,61,61,62,64,66,68,68,70,72,72,74,74,74,75,76,76,77,77,80,80,82,83,85,86,87,87,92,93,94,96,96,97,98,99]
// k = 25
//  x = 90
// expected: [72,74,74,74,75,76,76,77,77,80,80,82,83,85,86,87,87,92,93,94,96,96,97,98,99]

// Print(FindClosestElements(new int[] { }, 3, 5)); // Expected Output: []
// Print(FindClosestElements(new[] {1, 2, 3, 4, 5}, 0, 3)); // Expected Output: []
// Print(FindClosestElements(new[] {5}, 1, 5)); // Expected Output: [5]

/* improvements: break the first for loop sooner */

    static IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        int a = 0;
        int b = arr.Length - 1;
        while (b - a >= k)
        {
            if (Math.Abs(arr[a] - x) > Math.Abs(arr[b] - x)) a++;
            else b--;
        }

        List<int> res = new();
        for (int i = a; i <= b; i++)
        {
            res.Add(arr[i]);
        }

        return res;
    }

// static IList<int> FindClosestElements(int[] arr, int k, int x)
// {
//     int closestIndex = 0;
//     if (arr.Length <= 1) return arr;
//     if (k == 0) return new List<int>();
//
//     for (int i = 1; i < arr.Length; i++)
//     {
//         var iDist = Math.Abs(arr[i] - x);
//         var lastDist = Math.Abs(arr[closestIndex] - x);
//         if (k == 1 && iDist == lastDist)
//         {
//             continue;
//         }
//         if (iDist <= lastDist) closestIndex = i;
//     }
//
//     if (k == 1) return new List<int>() {arr[closestIndex]};
//
//     int a = closestIndex > 0 ? closestIndex - 1 : 0;
//     int b = closestIndex != arr.Length - 1 ? closestIndex + 1 : closestIndex;
//
//     if (k == 2)
//     {
//         var toReturn = new List<int>();
//         var aDiff = Math.Abs(arr[a] - x);
//         var bDiff = Math.Abs(arr[b] - x);
//         if (b == closestIndex || (aDiff < bDiff && a != closestIndex))
//         {
//             toReturn.Add(arr[a]);
//             toReturn.Add(arr[closestIndex]);
//             return toReturn;
//         }
//
//         toReturn.Add(arr[closestIndex]);
//         toReturn.Add(arr[b]);
//         return toReturn;
//     }
//
//     while (b - a < k)
//     {
//         if (a == 0)
//         {
//             b++;
//             continue;
//         }
//         if (b == arr.Length - 1)
//         {
//             a--;
//             continue;
//         }
//         var aDiff = Math.Abs(arr[a - 1] - x);
//         var bDiff = Math.Abs(arr[b] - x);
//         if (aDiff < bDiff || aDiff == bDiff && a < b)
//         {
//             a--;
//             continue;
//         }
//
//         b++;
//     }
//
//     var start = b == arr.Length - 1
//         ? a + 1
//         : a;
//     return arr.Skip(start).Take(b - a).ToList();
// }

/*
 * closestIndex is 6
 *      arr[6] = 4
 * starts off with a = 5, b = 7
 *      arr[5] = 3
 *      arr[7] = 7
 * b is chosen to be incremented, setting it to 8
 *      arr[8] = 7
 */
}