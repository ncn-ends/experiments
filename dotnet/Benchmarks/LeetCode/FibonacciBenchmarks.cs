using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Subjects.LeetCode;

namespace Benchmarks.LeetCode;

[MemoryDiagnoser] /* Information about memory usage */
[Orderer(SummaryOrderPolicy.FastestToSlowest)] /* Order results by fastest to slowest, duh */
[RankColumn]
public class FibonacciBenchmarks
{
    // private static readonly List<char> _sample = new() {'h', 'e', 'l', 'l', 'o'};
    private static readonly Fibonacci _fibonacci = new();

    public static void RunBenchmarks() =>
        BenchmarkRunner.Run<FibonacciBenchmarks>();


    [Benchmark(Baseline = true)]
    public void SolutionA() => _fibonacci.FindByN(40);

    [Benchmark]
    public void SolutionB() => _fibonacci.FindByNWithNoMemo(40);

}