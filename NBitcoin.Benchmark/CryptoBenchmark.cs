using NBitcoin.Crypto;
using BenchmarkDotNet.Attributes;
using System;

namespace NBitcoin.Benchmark
{
	[CoreJob, ClrJob, MonoJob]
	public class HashesBenchmark
	{
		private byte[] data;
		private RandomUtils.UnsecureRandom unsecureR;
		private const int N = 100000;

		public HashesBenchmark()
		{
			data = new byte[N];
			new Random(42).NextBytes(data);
			unsecureR = new RandomUtils.UnsecureRandom()
		}
		[Benchmark]
		public uint256 Hash256Test() => Hashes.Hash256(data);
		[Benchmark]
		public uint160 Hash160Test() => Hashes.Hash160(data);

		[Benchmark]
		public void UnsecureRandom() => ;
	}
}