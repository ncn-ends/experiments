using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tests.Utils;

public static class PrintUtility
{
    public static void Print<T>(IEnumerable<T> l)
    {
        var p = string.Join(",", l);
        Console.WriteLine(p);
    }

    public static void Print(IConvertible b)
    {
        Console.WriteLine(b.ToString(CultureInfo.InvariantCulture));
    }

    public static void Print(string s) => Console.WriteLine(s);
}