# EnumExtensionsGenerator

Currently being implemented.
Stay tuned!



// * Summary *

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1555/22H2/2022Update/SunValley2)
Intel Core i7-8850H CPU 2.60GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.203
  [Host]   : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  ShortRun : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

|         Method |       Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|--------------- |-----------:|----------:|----------:|------:|----------:|------------:|
|         DotNet | 19.8946 ns | 5.8417 ns | 0.3202 ns |  1.00 |         - |          NA |
|       EnumsNet |  2.2255 ns | 0.6901 ns | 0.0378 ns |  0.11 |         - |          NA |
|       FastEnum |  3.4650 ns | 3.6111 ns | 0.1979 ns |  0.17 |         - |          NA |
| EnumExtensions |  0.7073 ns | 0.1123 ns | 0.0062 ns |  0.04 |         - |          NA |

