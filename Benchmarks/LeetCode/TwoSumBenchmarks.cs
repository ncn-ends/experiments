using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
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


    [Benchmark]
    public void GetTwoSumSolutionA() => _twoSum.SolutionA();
}
