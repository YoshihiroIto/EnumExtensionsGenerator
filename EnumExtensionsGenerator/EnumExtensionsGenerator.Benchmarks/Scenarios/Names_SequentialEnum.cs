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
    public IReadOnlyList<string> DotNet()
        => Enum.GetNames<SequentialEnum>();

    [Benchmark]
    public IReadOnlyList<string> EnumsNet()
        => Enums.GetNames<SequentialEnum>();

    [Benchmark]
    public IReadOnlyList<string> FastEnum()
        => FastEnumUtility.FastEnum.GetNames<SequentialEnum>();
    
    [Benchmark]
    public IReadOnlyList<string> EnumExtensions()
        => SequentialEnumExtensions.Names;
}
