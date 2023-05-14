using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class ParseIgnoreCase_SequentialEnum
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
    public SequentialEnum DotNet()
        => Enum.Parse<SequentialEnum>("FRIDAY", true);

    [Benchmark]
    public SequentialEnum EnumsNet()
        => Enums.Parse<SequentialEnum>("FRIDAY", true);

    [Benchmark]
    public SequentialEnum FastEnum()
        => FastEnumUtility.FastEnum.Parse<SequentialEnum>("FRIDAY", true);
    
    [Benchmark]
    public SequentialEnum EnumExtensions()
        => SequentialEnumExtensions.ParseIgnoreCase("FRIDAY");
}
