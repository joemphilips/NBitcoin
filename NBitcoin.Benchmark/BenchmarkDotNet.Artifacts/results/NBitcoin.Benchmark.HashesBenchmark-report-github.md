``` ini

BenchmarkDotNet=v0.11.3, OS=macOS Mojave 10.14 (18A391) [Darwin 18.0.0]
Intel Core i7-6820HQ CPU 2.70GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.403
  [Host] : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  Core   : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|      Method |     Mean |    Error |   StdDev |
|------------ |---------:|---------:|---------:|
| Hash256Test | 289.1 us | 2.741 us | 2.430 us |
| Hash160Test | 294.6 us | 9.296 us | 8.241 us |
