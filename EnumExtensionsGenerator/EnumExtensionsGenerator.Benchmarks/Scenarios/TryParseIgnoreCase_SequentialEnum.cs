using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class TryParseIgnoreCase_SequentialEnum
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
        => Enum.TryParse<SequentialEnum>("FRIDAY", true, out _);

    [Benchmark]
    public bool EnumsNet()
        => Enums.TryParse<SequentialEnum>("FRIDAY", true, out _);

    [Benchmark]
    public bool FastEnum()
        => FastEnumUtility.FastEnum.TryParse<SequentialEnum>("FRIDAY", true, out _);
    
    [Benchmark]
    public bool EnumExtensions()
        => SequentialEnumExtensions.TryParseIgnoreCase("FRIDAY", out _);
}
