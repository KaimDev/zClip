using System.IO;
using System.Threading;
using FluentAssertions;
using Xunit;
using zClip_Desktop;
using zClip_Desktop.Inferfaces;
using NetArchTest.Rules;
using ClipboardService = zClip_Desktop.ClipboardService;

namespace zClip_Tests
{
    public class ClipboardServiceTests
    {
        private IClipboardService _clipboardService;
        
        public ClipboardServiceTests()
        {
            var serviceCollections = ServiceCollections.GetInstance();
            _clipboardService = serviceCollections.GetContainer().Resolve(typeof(ClipboardService), null) as IClipboardService;
        }

        [Fact]
        public void ClipboardService_Should_Implement_Interface()
        {
            Types.InAssembly(typeof(ClipboardService).Assembly)
                .That()
                .ImplementInterface(typeof(IClipboardService))
                .Should()
                .BeSealed()
                .GetResult()
                .IsSuccessful
                .Should()
                .BeTrue();
        }
        
        [Theory]
        [InlineData("Test")]
        [InlineData("Test1")]
        [InlineData("Test2")]
        public void ClipboardService_Should_Invoke_Event(string text)
        {
            var eventRaised = false;
            _clipboardService.OnClipboardChanged += (sender, args) => eventRaised = true;
            
            _clipboardService.Start();
            TextCopy.ClipboardService.SetTextAsync(text);
            Thread.Sleep(2000);
            _clipboardService.Stop();
            
            eventRaised.Should().BeTrue();
        }
        
        [Fact]
        public void ClipboardService_Should_Not_Invoke_Event()
        {
            var eventRaised = false;
            _clipboardService.OnClipboardChanged += (sender, args) => eventRaised = true;
            
            _clipboardService.Clear();
            _clipboardService.Start();
            _clipboardService.Stop();
            
            eventRaised.Should().BeFalse();
        }
    }
}