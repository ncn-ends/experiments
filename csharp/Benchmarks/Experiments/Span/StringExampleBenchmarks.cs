using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Subjects.Experiments.Span;

namespace Benchmarks.Experiments.Span;

[MemoryDiagnoser] /* Information about memory usage */
[Orderer(SummaryOrderPolicy.FastestToSlowest)] /* Order results by fastest to slowest, duh */
[RankColumn]
public class StringExampleBenchmarks
{
    const string _date = "05 13 2018";
    private StringExample _stringExample = new ();

    public static void RunBenchmarks() =>
        BenchmarkRunner.Run<StringExampleBenchmarks>();


    [Benchmark(Baseline = true)]
    public void GetParseDateAsSubstring() => _stringExample.ParseDateAsSubstring(_date);
    
    [Benchmark]
    public void GetParseDateAsSpan() => _stringExample.ParseDateAsSpan(_date);
}