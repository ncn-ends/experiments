using System;
using System.Collections.Generic;

namespace Tests.Utils;

public static class PrintUtility
{
    public static void Print<T>(IEnumerable<T> l)
    {
        var p = string.Join(",", l);
        Console.WriteLine(p);
    }
}