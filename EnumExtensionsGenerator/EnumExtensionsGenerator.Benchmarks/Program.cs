using BenchmarkDotNet.Running;
using EnumExtensionsGenerator.Benchmarks.Scenarios;

var switcher = new BenchmarkSwitcher(new[]
{
    typeof(GetName_SequentialEnum),
    typeof(GetName_UnsequentialEnum),
    typeof(IsDefined_SequentialEnum),
    typeof(IsDefined_UnsequentialEnum),
    typeof(Names_SequentialEnum),
    typeof(Names_UnsequentialEnum),
    typeof(Values_SequentialEnum),
    typeof(Values_UnsequentialEnum),
});

switcher.Run(args);
