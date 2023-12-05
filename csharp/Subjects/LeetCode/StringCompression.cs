namespace Subjects.LeetCode;

public class StringCompression
{
    public static int Compress(char[] chars)
    {
        if (chars.Length == 1) return 1;
        var n = 1;
        var g = 0;
        var ret = 0;
        var skipNext = false;
        for (int i = 1; i < chars.Length; i++)
        {
            if (skipNext) { skipNext = false; n++; continue; }

            if (chars[i] == chars[i - 1])
            {
                n++;
                if (i != chars.Length - 1) continue;
            }

            if (n == 1)
            {
                n++;
                g++;
                ret++;
                continue;
            }

            chars[g * 2] = chars[i - 1];
            var strN = n.ToString();
            for (var k = 0; k < strN.Length; k++)
            {
                chars[g * 2 + 1 + k] = strN[k];
            }
            n = 1;
            g += strN.Length;
            ret++;
            skipNext = true;
        }

        return ret;
    }
}