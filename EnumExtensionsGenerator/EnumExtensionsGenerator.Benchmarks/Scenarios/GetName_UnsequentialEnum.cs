﻿using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class GetName_UnsequentialEnum
{
    private static UnsequentialEnum Value = UnsequentialEnum.Friday;
    
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<UnsequentialEnum>();
        _ = Enums.GetNames<UnsequentialEnum>();
        _ = FastEnumUtility.FastEnum.GetNames<UnsequentialEnum>();
        _ = UnsequentialEnumExtensions.NamesSpan;
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
        => UnsequentialEnumExtensions.ToName(Value);
}
