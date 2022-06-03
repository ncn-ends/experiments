using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Subjects.LeetCode;

namespace Benchmarks.LeetCode;

[MemoryDiagnoser] /* Information about memory usage */
[Orderer(SummaryOrderPolicy.FastestToSlowest)] /* Order results by fastest to slowest, duh */
[RankColumn]
public class ReverseStringBenchmarks
{
    private static readonly List<char> _sample = new() {'h', 'e', 'l', 'l', 'o'};
    private static readonly ReverseString _reverseString = new(_sample);

    public static void RunBenchmarks() =>
        BenchmarkRunner.Run<ReverseStringBenchmarks>();


    [Benchmark(Baseline = true)]
    public void GetReverseStringSolutionA() => _reverseString.SolutionA();
    
    [Benchmark]
    public void GetReverseStringSolutionB() => _reverseString.SolutionB();
    
    [Benchmark]
    public void GetReverseStringSolutionC() => _reverseString.SolutionC();
}