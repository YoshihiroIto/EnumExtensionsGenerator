using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class Names_UnsequentialEnum
{
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<UnsequentialEnum>();
        _ = Enums.GetNames<UnsequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetNames<UnsequentialEnum>();
        _ = UnsequentialEnumExtensions.NamesSpan;
    }
    
    [Benchmark(Baseline = true)]
    public object DotNet()
        => Enum.GetNames<UnsequentialEnum>();

    [Benchmark]
    public object EnumsNet()
        => Enums.GetNames<UnsequentialEnum>();

    [Benchmark]
    public object FastEnum()
        => FastEnumUtility.FastEnum.GetNames<UnsequentialEnum>();
    
    [Benchmark]
    public object EnumExtensions()
        => UnsequentialEnumExtensions.Names;
}
