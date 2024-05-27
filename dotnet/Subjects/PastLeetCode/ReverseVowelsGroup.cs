namespace Subjects.LeetCode;

public class ReverseVowelsGroup
{
    public static string ReverseVowels(string s)
    {
        var sm = s.ToCharArray();
        var a = 0;
        var z = sm.Length - 1;
        var l = new Dictionary<char, bool>()
        {
            {'a', true},
            {'e', true},
            {'i', true},
            {'o', true},
            {'u', true},
            {'A', true},
            {'E', true},
            {'I', true},
            {'O', true},
            {'U', true},
        };
        while (a < z)
        {
            l.TryGetValue(sm[a], out var aV);
            l.TryGetValue(sm[z], out var aZ);
            if (aV && aZ)
            {
                var tmp = s[a];
                sm[a] = s[z];
                sm[z] = tmp;
                a++;
                z--;
            }

            if (!aV) a++;
            if (!aZ) z--;
        }

        return string.Concat(sm);
    }
}