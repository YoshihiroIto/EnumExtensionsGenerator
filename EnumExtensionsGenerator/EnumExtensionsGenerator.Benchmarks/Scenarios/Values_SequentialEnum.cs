﻿using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[MemoryDiagnoser]
[ShortRunJob]
public class Values_SequentialEnum
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
    public IReadOnlyList<SequentialEnum> DotNet()
        => Enum.GetValues<SequentialEnum>();

    [Benchmark]
    public IReadOnlyList<SequentialEnum> EnumsNet()
        => Enums.GetValues<SequentialEnum>();

    [Benchmark]
    public IReadOnlyList<SequentialEnum> FastEnum()
        => FastEnumUtility.FastEnum.GetValues<SequentialEnum>();
    
    [Benchmark]
    public IReadOnlyList<SequentialEnum> EnumExtensions()
        => SequentialEnumExtensions.Values;
}
