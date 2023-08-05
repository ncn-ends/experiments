namespace Subjects.LeetCode;

public class ValidParantheses
{
    public bool DoIterative(string str)
    {
        List<char> openings = new();
        var s = str;

        foreach (var c in s)
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