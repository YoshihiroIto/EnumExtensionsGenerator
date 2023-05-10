using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class Values_UnsequentialEnum
{
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetValues<UnsequentialEnum>();
        _ = Enums.GetValues<UnsequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetValues<UnsequentialEnum>();
        _ = UnsequentialEnumExtensions.Values;
    }
    
    [Benchmark(Baseline = true)]
    public object DotNet()
        => Enum.GetValues<UnsequentialEnum>();

    [Benchmark]
    public object EnumsNet()
        => Enums.GetValues<UnsequentialEnum>();

    [Benchmark]
    public object FastEnum()
        => FastEnumUtility.FastEnum.GetValues<UnsequentialEnum>();
    
    [Benchmark]
    public object EnumExtensions()
        => UnsequentialEnumExtensions.Values;
}
