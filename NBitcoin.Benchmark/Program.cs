using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using NBitcoin.Benchmark.Serialization;

namespace NBitcoin.Benchmark
{
	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<HashesBenchmark>();
			BenchmarkRunner.Run<TransactionSerializationBenchmark>();
		}
	}
}
