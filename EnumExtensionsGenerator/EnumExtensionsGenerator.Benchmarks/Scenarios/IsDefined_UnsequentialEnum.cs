using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class IsDefined_UnsequentialEnum
{
    private static UnsequentialEnum Value = UnsequentialEnum.K;
    
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<UnsequentialEnum>();
        _ = Enums.GetNames<UnsequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetNames<UnsequentialEnum>();
        _ = UnsequentialEnumExtensions.NamesSpan;
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
        => UnsequentialEnumExtensions.IsDefined(Value);
}
