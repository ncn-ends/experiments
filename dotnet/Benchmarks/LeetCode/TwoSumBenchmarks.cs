using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Subjects.LeetCode;

namespace Benchmarks.LeetCode;

[MemoryDiagnoser] /* Information about memory usage */
[Orderer(SummaryOrderPolicy.FastestToSlowest)] /* Order results by fastest to slowest, duh */
[RankColumn]
public class TwoSumBenchmarks
{
    private static readonly List<int> _sample = new() {2, 7, 11, 15};
    private const int _target = 9;
    private static readonly TwoSum _twoSum = new(_sample, _target);
    
    public static void RunBenchmarks() => 
        BenchmarkRunner.Run<TwoSumBenchmarks>();


    [Benchmark(Baseline = true)]
    public void GetTwoSumSolutionA() => _twoSum.SolutionA();
    
    [Benchmark]
    public void GetTwoSumSolutionB() => _twoSum.SolutionB();

    [Benchmark]
    public void GetTwoSumSolutionC() => _twoSum.SolutionC();

    [Benchmark]
    public void GetTwoSumSolutionD() => _twoSum.SolutionD();

    [Benchmark]
    public void GetTwoSumSolutionE() => _twoSum.SolutionE();
}
