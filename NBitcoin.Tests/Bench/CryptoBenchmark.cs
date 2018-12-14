using NBitcoin.Crypto;
using BenchmarkDotNet.Attributes;
using NBitcoin.Tests.Generators;
using FsCheck.Xunit;
using System;

namespace NBitcoin.Tests.Bench
{
	[CoreJob, ClrJob, MonoJob]
	public class CryptoBenchMark
	{
		public class SHA256Bench
		{
			private byte[] data;
			private const int N = 100000;

			public SHA256Bench()
			{
				data = new byte[N];
				new Random(42).NextBytes(data);
			}
			[Benchmark]
			public byte[] Hash256Test() => Hashes.SHA256(data);
		}
	}
}