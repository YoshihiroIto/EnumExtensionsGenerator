using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class ParseIgnoreCase_SequentialEnum
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
    public SequentialEnum DotNet()
        => Enum.Parse<SequentialEnum>(Value, true);

    [Benchmark]
    public SequentialEnum EnumsNet()
        => Enums.Parse<SequentialEnum>(Value, true);

    [Benchmark]
    public SequentialEnum FastEnum()
        => FastEnumUtility.FastEnum.Parse<SequentialEnum>(Value, true);
    
    [Benchmark]
    public SequentialEnum EnumExtensions()
        => SequentialEnumExtensions.ParseIgnoreCase(Value);
}
