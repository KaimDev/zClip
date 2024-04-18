using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace zClip_BenchMark
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            BenchmarkRunner.Run<ClipboardBenchMark>(config, args);
        }
    }
}