namespace Subjects.LeetCode;

public class GcdOfStringsGroup
{
    public static string GcdOfStrings(string str1, string str2)
    {
        var s = str1.Length < str2.Length ? str1 : str2;
        string wrk = "";
        for (var i = 0; i < s.Length; i++)
        {
            if (str1[i] == str2[i]) wrk += str1[i];
            else break;
        }

        return wrk;
    }
}