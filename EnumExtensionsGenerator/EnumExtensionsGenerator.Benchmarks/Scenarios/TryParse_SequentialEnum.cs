using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class TryParse_SequentialEnum
{
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetValues<SequentialEnum>();
        _ = Enums.GetValues<SequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetValues<SequentialEnum>();
        _ = SequentialEnumExtensions.ValuesSpan;
    }
    
    [Benchmark(Baseline = true)]
    public bool DotNet()
        => Enum.TryParse<SequentialEnum>("Friday", out _);

    [Benchmark]
    public bool EnumsNet()
        => Enums.TryParse<SequentialEnum>("Friday", out _);

    [Benchmark]
    public bool FastEnum()
        => FastEnumUtility.FastEnum.TryParse<SequentialEnum>("Friday", out _);
    
    [Benchmark]
    public bool EnumExtensions()
        => SequentialEnumExtensions.TryParse("Friday", out _);
}
