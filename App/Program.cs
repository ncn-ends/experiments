using System.Diagnostics;
using Subjects.LeetCode;

class Program
{
    static void Main(string[] args)
    {
        // Benchmarks.Main.RunAllBenchmarks();

        // Benchmarks.LeetCode.ReverseStringBenchmarks.RunBenchmarks();
        
        Benchmarks.Experiments.Span.StringExampleBenchmarks.RunBenchmarks();
    }
}