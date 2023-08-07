namespace Subjects.LeetCode;

public class ValidParantheses
{
    /* speed: 62ms, top 97%
       memory: 38.32mb, top 21%
     */
    public bool DoIterative(string s)
    {
        List<char> openings = new();
        var str = s;

        foreach (var c in str)
        {
            if (new[] {'(', '{', '['}.Contains(c))
            {
                openings.Add(c);
                continue;
            }

            /* this should only happen if the first char of the string is a closing character */
            var lastOpening = openings.LastOrDefault();
            if (lastOpening.Equals(default)) return false;

            /* if current char is a closing, last opening should always match */
            if (new[] {')', '}', ']'}.Contains(c))
            {
                var lastIndex = openings.Count - 1;
                switch (c)
                {
                    case ')' when lastOpening == '(':
                    case '}' when lastOpening == '{':
                    case ']' when lastOpening == '[':
                        openings.RemoveAt(lastIndex);
                        break;
                    default:
                        return false;
                }
            }
        }

        /* left over openings must mean its invalid */
        if (openings.Count > 0) return false;

        return true;
    }
}