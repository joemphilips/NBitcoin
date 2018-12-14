using BenchmarkDotNet.Running;

namespace NBitcoin.Tests.Bench
{
	public class Main
	{
		public void BenchmarkMain()
		{
			BenchmarkRunner.Run<CryptoBenchMark>();
		}
	}
}