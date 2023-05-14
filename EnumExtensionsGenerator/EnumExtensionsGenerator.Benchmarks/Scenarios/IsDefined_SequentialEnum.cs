using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class IsDefined_SequentialEnum
{
    private static SequentialEnum Value = SequentialEnum.Friday;
    
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<SequentialEnum>();
        _ = Enums.GetNames<SequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetNames<SequentialEnum>();
        _ = SequentialEnumExtensions.NamesSpan;
    }
    
    [Benchmark(Baseline = true)]
    public bool DotNet()
        => Enum.IsDefined(Value);

    [Benchmark]
    public bool EnumsNet()
        => Enums.IsDefined(Value);

    [Benchmark]
    public bool FastEnum()
        => FastEnumUtility.FastEnum.IsDefined(Value);
    
    [Benchmark]
    public bool EnumExtensions()
        => SequentialEnumExtensions.IsDefined(Value);
}
