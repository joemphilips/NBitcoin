using BenchmarkDotNet.Attributes;

namespace NBitcoin.Benchmark
{
	public class ScriptVerifyBenchmark
	{
#if !NOCONSENSUSLIB
		public Script ScriptPubKey { get; }
		public ScriptVerifyBenchmark()
		{
			ScriptPubKey = new Script();
		}

		// [Benchmark]
		// public bool Verify() => Script.VerifyScript()

#endif
	}
}