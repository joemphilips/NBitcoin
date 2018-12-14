using BenchmarkDotNet.Attributes;
using FsCheck;
using NBitcoin.Tests.Generators;
using NBitcoin;

namespace NBitcoin.Benchmark.Serialization
{
	[CoreJob, ClrJob, MonoJob]
	public class TransactionSerializationBenchmark
	{
		private Gen<Transaction> TX;

		private Network network;

		public TransactionSerializationBenchmark()
		{
			TX = SegwitTransactionGenerators.TX();
			network = Network.Main;
		}

		[Benchmark]
		public Gen<Transaction> Serialize()
		{
			var result = network.CreateTransaction();
			return TX.Select(tx => { result.FromBytes(tx.ToBytes()); return result; });
		}
	}
}