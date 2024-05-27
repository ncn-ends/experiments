namespace Subjects.LeetCode;

public class MergeStringsAlternately
{
    public static string Do(string word1, string word2)
    {
        string wrk = "";
        var max = word1.Length > word2.Length ? word1.Length : word2.Length;
        for (int i = 0; i < max; i++)
        {
            if (i < word1.Length) wrk += word1[i];
            if (i < word2.Length) wrk += word2[i];
        }

        return wrk;
    }
}