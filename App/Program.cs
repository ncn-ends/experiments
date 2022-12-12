using System.Diagnostics;
using System.Text;
using Subjects.Algorithms;
using Subjects.AoC._2022._1;
using Subjects.AoC._2022._10;
using Subjects.AoC._2022._11;
using Subjects.AoC._2022._2;
using Subjects.AoC._2022._3;
using Subjects.AoC._2022._4;
using Subjects.AoC._2022._5;
using Subjects.AoC._2022._7;
using Subjects.AoC._2022._8;
using Subjects.AoC._2022._9;
using Subjects.Structures;

namespace App;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Benchmarks.Main.RunAllBenchmarks();

            // ulong asd = 80;
            // int qwe = 23;
            // var zxc = asd % (ulong)qwe;
            //
            // Func<ulong, bool> asdd = x => x % 23 == 0;
            // var qwee = asdd(70);
            // Debugger.Break();
            Day11Solution.Output();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}