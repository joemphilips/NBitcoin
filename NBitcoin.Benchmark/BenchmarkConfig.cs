using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;

namespace NBitcoin.Benchmark
{
	public class BenchmarkConfig : ManualConfig
	{
		public BenchmarkConfig()
		{
			Add(DefaultConfig.Instance.GetLoggers().ToArray());
			Add(DefaultConfig.Instance.GetExporters().ToArray());
			Add(RPlotExporter.Default);
		}
	}
}