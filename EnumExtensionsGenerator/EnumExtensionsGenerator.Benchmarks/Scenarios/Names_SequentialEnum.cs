using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class Names_SequentialEnum
{
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<SequentialEnum>();
        _ = Enums.GetNames<SequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetNames<SequentialEnum>();
        _ = SequentialEnumExtensions.NamesSpan;
    }
    
    [Benchmark(Baseline = true)]
    public object DotNet()
        => Enum.GetNames<SequentialEnum>();

    [Benchmark]
    public object EnumsNet()
        => Enums.GetNames<SequentialEnum>();

    [Benchmark]
    public object FastEnum()
        => FastEnumUtility.FastEnum.GetNames<SequentialEnum>();
    
    [Benchmark]
    public object EnumExtensions()
        => SequentialEnumExtensions.Names;
}
