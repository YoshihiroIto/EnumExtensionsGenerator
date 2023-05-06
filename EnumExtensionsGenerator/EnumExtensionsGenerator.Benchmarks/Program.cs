using BenchmarkDotNet.Running;
using EnumExtensionsGenerator.Benchmarks.Scenarios;

var switcher = new BenchmarkSwitcher(new[]
{
    typeof(SequentialEnum_GetNameBenchmark),
    typeof(UnsequentialEnum_GetNameBenchmark),
});

switcher.Run(args);
