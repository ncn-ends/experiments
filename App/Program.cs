using System.Diagnostics;
using System.Text;
using Subjects.Algorithms;
using Subjects.AoC._2022._1;
using Subjects.AoC._2022._10;
using Subjects.AoC._2022._11;
using Subjects.AoC._2022._12;
using Subjects.AoC._2022._2;
using Subjects.AoC._2022._3;
using Subjects.AoC._2022._4;
using Subjects.AoC._2022._5;
using Subjects.AoC._2022._7;
using Subjects.AoC._2022._8;
using Subjects.AoC._2022._9;
using Subjects.OneOf;
using Subjects.Structures;

namespace App;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var ro = new ReturnObject();
            var asd = ro.GetResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}