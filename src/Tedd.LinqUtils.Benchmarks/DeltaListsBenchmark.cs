using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Linq;

namespace Tedd.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class DeltaListsBenchmark
{
    private int[] firstArray = null!;
    private int[] secondArray = null!;

    [Params(10, 1000, 100000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        firstArray = Enumerable.Range(0, N).ToArray();
        secondArray = Enumerable.Range(N / 2, N).ToArray();
    }

    [Benchmark(Baseline = true)]
    public Tedd.Archive.DeltaResult<int, int> Legacy_DeltaLists()
    {
        return Tedd.Archive.LinqListManipulationMethods.DeltaLists(firstArray, secondArray);
    }

    [Benchmark]
    public Tedd.DeltaResult<int, int> Optimized_DeltaLists()
    {
        return Tedd.LinqListManipulationMethods.DeltaLists(firstArray, secondArray);
    }
}
