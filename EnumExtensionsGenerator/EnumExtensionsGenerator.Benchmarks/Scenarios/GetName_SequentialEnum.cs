using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class GetName_SequentialEnum
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
    public string? DotNet()
        => Enum.GetName(Value);

    [Benchmark]
    public string? EnumsNet()
        => Value.GetName();

    [Benchmark]
    public string? FastEnum()
        => FastEnumUtility.FastEnum.GetName(Value);
    
    [Benchmark]
    public string EnumExtensions()
        => SequentialEnumExtensions.ToName(Value);
}
