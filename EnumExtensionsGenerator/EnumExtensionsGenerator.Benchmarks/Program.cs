using BenchmarkDotNet.Running;
using EnumExtensionsGenerator.Benchmarks.Scenarios;

var switcher = new BenchmarkSwitcher(new[]
{
    typeof(SequentialEnum_GetName),
    typeof(UnsequentialEnum_GetName),
    typeof(SequentialEnum_IsDefined),
    typeof(UnsequentialEnum_IsDefined),
});

switcher.Run(args);
