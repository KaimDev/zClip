using System.Threading;
using FluentAssertions;
using Xunit;
using zClip_Desktop.Interfaces;
using NetArchTest.Rules;
using zClip_Desktop.Services;

namespace zClip_Tests
{
    public class ClipboardServiceTests
    {
        private readonly IClipboardService _clipboardService;
        
        public ClipboardServiceTests()
        {
            _clipboardService = new ClipboardService();
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
            TextCopy.ClipboardService.SetText(text);
            Thread.Sleep(2000);
            _clipboardService.Stop();
            
            eventRaised.Should().BeTrue();
        }
        
        [Fact]
        public void ClipboardService_Should_Not_Invoke_Event()
        {
            var eventRaised = false;
            
            _clipboardService.Start();
            _clipboardService.Clear();
            _clipboardService.OnClipboardChanged += (sender, args) => eventRaised = true;
            _clipboardService.Stop();
            
            eventRaised.Should().BeFalse();
        }
    }
}