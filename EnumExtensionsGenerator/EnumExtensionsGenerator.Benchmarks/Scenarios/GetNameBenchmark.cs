using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class GetNameBenchmark
{
    private const Fruits Value = Fruits.Pineapple;
    
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<Fruits>();
        _ = Enums.GetNames<Fruits>();
        _ = FastEnumUtility.FastEnum.GetNames<Fruits>();
        _ = FruitsExtensions.NamesSpan;
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
        => FruitsExtensions.ToName(Value);
}
