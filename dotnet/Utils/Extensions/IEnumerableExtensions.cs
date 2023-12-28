using System.Diagnostics;

namespace Utils.Extensions;

public static class IEnumerableExtensions
{
    public static void RemoveAt<T>(this IList<T> list, Index index)
    {
        if (!index.IsFromEnd)
        {
            list.RemoveAt(index.Value);
            return;
        }

        var asd = new Index(1, true);
        list.RemoveAt(list.Count - index.Value);
    }
}
// public static void RemoveAt<T>(this ref IEnumerable<T> enumerable , Index index)
// {
//     if (!index.IsFromEnd)
//     {
//         enumerable = enumerable.Where((_, i) => i != index.Value);
//         return;
//     }
//
//     enumerable = enumerable.Reverse().Where((_, i) => i != index.Value);
// }