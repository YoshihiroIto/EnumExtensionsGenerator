using BenchmarkDotNet.Running;
using EnumExtensionsGenerator.Benchmarks.Scenarios;

var switcher = new BenchmarkSwitcher(new[]
{
    typeof(GetNameBenchmark),
});

switcher.Run(args);
