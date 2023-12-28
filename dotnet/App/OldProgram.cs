// using System.Diagnostics;
// using Subjects.LeetCode;
// using static Tests.Utils.PrintUtility;
//
// // Thread mainThread = Thread.CurrentThread;
// // mainThread.Name = "Main thread";
// var thread1 = new Thread(CountDown);
// var thread2 = new Thread(CountUp);
// thread1.Start();
// thread2.Start();
//
// // Console.WriteLine($"{mainThread.Name} is complete");
//
// void CountDown()
// {
//     for (int i = 10; i >= 0; i--)
//     {
//         Console.WriteLine($"Timer #1: {i} seconds left");
//         Thread.Sleep(250);
//     }
//     Console.WriteLine("Timer #1 is complete!");
// }
//
// void CountUp()
// {
//     for (int i = 0; i <= 10; i++)
//     {
//         Console.WriteLine($"Timer #2: {i} seconds passed");
//         Thread.Sleep(250);
//     }
//     Console.WriteLine("Timer #2 is complete!");
// }
//
// // var asd = new ListNode(1);
// // asd.next = new ListNode(2);
// // asd.next.next = new ListNode(3);
// // asd.next.next.next = new ListNode(4);
// // asd.next.next.next.next = new ListNode(5);
// // // asd.next.next.next.next.next = new ListNode(6);
// // // asd.next.next.next.next.next.next = new ListNode(7);
// // // asd.next.next.next.next.next.next.next = new ListNode(8);
// // // asd.next.next.next.next.next.next.next.next = new ListNode(9);
// // // asd.next.next.next.next.next.next.next.next.next = new ListNode(10);
// //
// //
// // var a1 = new ListNode(2);
// // a1.next = new ListNode(4);
// // a1.next.next = new ListNode(3);
// // var a2 = new ListNode(5);
// // a2.next = new ListNode(6);
// // a2.next.next = new ListNode(4);
// //
// // var b = new ListNode(3);
// // b.next = new ListNode(2);
// // b.next.next = new ListNode(0);
// // b.next.next.next = new ListNode(-4);
// // b.next.next.next.next = b.next;
// //
// // var arr = new[] {0, 1, 2, 2, 3, 0, 4, 2};
// //
// // static int RemoveElement(int[] nums, int val)
// // {
// //     var q = new Queue<int>();
// //
// //     foreach (var num in nums)
// //     {
// //         if (num == val) continue;
// //         q.Enqueue(num);
// //     }
// //
// //     var k = q.Count;
// //
// //     var i = 0;
// //     while (q.Count > 0)
// //     {
// //         var n = q.Dequeue();
// //         nums[i] = n;
// //         i++;
// //     }
// //
// //     return k;
// // }
// //
// // static int RemoveDuplicates(int[] nums)
// // {
// //     var set = new HashSet<int>();
// //     foreach (var num in nums)
// //         set.Add(num);
// //
// //     var i = 0;
// //     foreach (var num in set)
// //     {
// //         nums[i] = num;
// //         i++;
// //     }
// //
// //     return set.Count;
// // }
// //
// // // var arr2 = new[] {1, 1, 1, 2, 2, 3};
// // // Print(RemoveDuplicatesII(arr2));
// // // Debugger.Break();
// //
// // static int RemoveDuplicatesII(int[] nums)
// // {
// //     var p = 2;
// //     for (var i = 2; i < nums.Length; i++)
// //     {
// //         if (nums[i - 1] == nums[i]) continue;
// //         nums[p] = nums[i];
// //         p++;
// //     }
// //
// //     return p;
// // }
// //
// // // Print(IsHappy(2));
// //
// // static bool IsHappy(int n)
// // {
// //     var set = new HashSet<int>();
// //     var target = n;
// //     while (true)
// //     {
// //         var wasAdded = set.Add(target);
// //         if (!wasAdded) return false;
// //         var nStr = target.ToString().Select(x => x - '0');
// //
// //         target = nStr.Aggregate(0, (acc, i) => acc + i * i);
// //
// //         if (target == 1) return true;
// //     }
// // }
// //
// // // Print(ContainsNearbyDuplicate(new[] {1, 2, 3, 1, 2, 3}, 2));
// // // Print(ContainsNearbyDuplicate(new[] {1, 2, 3, 1}, 3));
// // Print(ContainsNearbyDuplicate(new[] {13,23,1,2,3}, 5));
// //
// //
// // static bool ContainsNearbyDuplicate(int[] nums, int k)
// // {
// //     if (nums.Length == 1) return false;
// //     if (nums.Length == 2 && nums[0] == nums[1]) return true;
// //     if (nums.Length == 2 && nums[0] != nums[1]) return false;
// //     if (nums.Length == 1) return false;
// //     var p1 = 0;
// //     var p2 = k;
// //     var dict = new Dictionary<int, int>();
// //     for (int i = p1; i < p2; i++)
// //     {
// //         var added = dict.TryAdd(nums[i], 1);
// //         if (!added) return false;
// //     }
// //
// //     // dict.Remove(nums[p1]);
// //     // p1++;
// //     // p2++;
// //     // dict.Add(nums[p2], 1);
// //     do
// //     {
// //         if (p2 == nums.Length) return false;
// //         dict.Remove(nums[p1]);
// //         p1++;
// //         p2++;
// //         var added = dict.TryAdd(nums[p2], 1);
// //         if (!added) return true;
// //    } while (p2 < nums.Length - 1);
// //
// //     return false;
// // }
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
// public class TreeNode
// {
//     public int val;
//     public TreeNode? left;
//     public TreeNode? right;
//
//     public TreeNode(int val = 0,
//         TreeNode? left = null,
//         TreeNode? right = null)
//     {
//         this.val = val;
//         this.left = left;
//         this.right = right;
//     }
// }
//
// public static class ListNodeExtensions
// {
//     public static ListNode? FromTestCaseArray(int[] nums)
//     {
//         if (nums.Length == 0) return null;
//         var head = new ListNode(nums[0]);
//         var p = head;
//         foreach (var num in nums.Skip(1))
//         {
//             p.next = new ListNode(num);
//         }
//
//         return head;
//     }
// }