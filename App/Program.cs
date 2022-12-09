using System.Diagnostics;
using System.Text;
using Subjects.Algorithms;
using Subjects.AoC._2022._1;
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

            // Benchmarks.LeetCode.ReverseStringBenchmarks.RunBenchmarks();

            // Benchmarks.Experiments.Span.StringExampleBenchmarks.RunBenchmarks();


            // var root = new TreeNode<int>(100);
            // var layer1 = new List<TreeNode<int>>
            // {
            //     new(50),
            //     new(1),
            //     new(150)
            // };
            // root.AddChildren(layer1);
            // foreach (var child in root.Children)
            // {
            //     Console.WriteLine(child.Value);
            // }
            // var tree = new BasicTree<int>(root);
            var asd = new HashSet<(int, int)>();
            asd.Add((0, 0));
            asd.Add((1, 0));
            asd.Add((0, 0));

            Day9Solution.Output();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}