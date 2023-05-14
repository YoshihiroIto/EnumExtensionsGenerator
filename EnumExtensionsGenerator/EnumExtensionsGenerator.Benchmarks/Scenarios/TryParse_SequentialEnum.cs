using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class TryParse_SequentialEnum
{
    private static string Value = "Friday";
    
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
        => Enum.TryParse<SequentialEnum>(Value, out _);

    [Benchmark]
    public bool EnumsNet()
        => Enums.TryParse<SequentialEnum>(Value, out _);

    [Benchmark]
    public bool FastEnum()
        => FastEnumUtility.FastEnum.TryParse<SequentialEnum>(Value, out _);
    
    [Benchmark]
    public bool EnumExtensions()
        => SequentialEnumExtensions.TryParse(Value, out _);
}
