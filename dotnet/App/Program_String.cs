using System.Diagnostics;
using static Tests.Utils.PrintUtility;

// var asd = PrefixFunc("abcabcd");
// Debugger.Break();


// static List<int> NaiveSearch(string text, string word)
// {
//     var matches = new List<int>();
//     for (int i = 0; i < text.Length - word.Length + 1; i++)
//     {
//         if (text.Substring(i, word.Length) == word) matches.Add(i);
//     }
//
//     return matches;
// }
//
// static int[] PrefixFunc_Bad(string str)
// {
//     var prefixArr = new int[str.Length];
//     prefixArr[0] = 0;
//
//     var sub = str[0].ToString();
//     for (var i = 1; i < str.Length; i++)
//     {
//         sub += str[i];
//
//         for (; prefixArr[i] < sub.Length - 1; prefixArr[i]++)
//         {
//             var place = prefixArr[i];
//             var pre = sub[place];
//             var suf = sub[sub.Length - place - 1];
//             var match = pre == suf;
//             if (!match) break;
//         }
//     }
//
//     return prefixArr;
// }

// given a string s...
// instantiate an array pi with length equal to s
//
//
// List<int> prefix_function(string s) {
//     var n = s.Length;
//     List<int> pi = new List<int>(n);
//     for (int i = 1; i < n; i++) {
//         int j = pi[i-1];
//         while (j > 0 && s[i] != s[j])
//             j = pi[j-1];
//         if (s[i] == s[j])
//             j++;
//         pi[i] = j;
//     }
//     return pi;
// }