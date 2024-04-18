using BenchmarkDotNet.Attributes;
using zClip_Desktop.Services;
using static zClip_BenchMark.LoremIpsum;

namespace zClip_BenchMark
{
    public class ClipboardBenchMark
    {
        private readonly ClipboardService _clipboardServiceTests = new ClipboardService();
        
        [Benchmark]
        public void GetALargeTextFromClipboard()
        {
            _clipboardServiceTests.Start();

            TextCopy.ClipboardService.SetText(LoremIpsumText);
            
            _clipboardServiceTests.Stop();
        }
    }
}