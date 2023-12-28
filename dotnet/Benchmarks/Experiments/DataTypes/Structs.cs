using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Subjects.Experiments.DataTypes;
using Subjects.Experiments.Span;

namespace Benchmarks.Experiments.DataTypes;

[MemoryDiagnoser] /* Information about memory usage */
[Orderer(SummaryOrderPolicy.FastestToSlowest)] /* Order results by fastest to slowest, duh */
[RankColumn]
public class DataTypesBenchmarks
{
    private readonly Structs _structs = new();
    
    public static void RunBenchmarks() =>
        BenchmarkRunner.Run<DataTypesBenchmarks>();


    [Benchmark(Baseline = true)]
    public void AsClass() => _structs.AsClass();
    
    [Benchmark]
    public void AsStruct() => _structs.AsStruct();
}