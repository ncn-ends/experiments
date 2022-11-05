using Subjects.Algorithms;

namespace App;

class Program
{
    static void Main(string[] args)
    {
        // Benchmarks.Main.RunAllBenchmarks();

        // Benchmarks.LeetCode.ReverseStringBenchmarks.RunBenchmarks();

        // Benchmarks.Experiments.Span.StringExampleBenchmarks.RunBenchmarks();

        var res = Subjects.AoC._2020._1.Solution.DoPart2();
        Console.WriteLine(res);
    }
}