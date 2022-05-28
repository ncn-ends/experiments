using BenchmarkDotNet.Running;
using Benchmarks;
using Benchmarks.LeetCode;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<TwoSumBenchmarks>();
    }
}