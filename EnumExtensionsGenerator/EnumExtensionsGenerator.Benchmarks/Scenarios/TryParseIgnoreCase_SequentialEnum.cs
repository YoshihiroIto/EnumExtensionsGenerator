using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class TryParseIgnoreCase_SequentialEnum
{
    private static string Value = "FRIDAY";
    
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
        => Enum.TryParse<SequentialEnum>(Value, true, out _);

    [Benchmark]
    public bool EnumsNet()
        => Enums.TryParse<SequentialEnum>(Value, true, out _);

    [Benchmark]
    public bool FastEnum()
        => FastEnumUtility.FastEnum.TryParse<SequentialEnum>(Value, true, out _);
    
    [Benchmark]
    public bool EnumExtensions()
        => SequentialEnumExtensions.TryParseIgnoreCase(Value, out _);
}
