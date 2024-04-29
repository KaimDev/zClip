using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Unity;
using Xunit;
using zClip_Desktop.Helpers;
using zClip_Desktop.Services;

namespace zClip_Tests
{
    public class ListenerServiceTests
    {
        private readonly ListenerService _listenerService;
        private readonly OwnIpAddress _ownIpAddress;
        private readonly string ipAddress;

        public ListenerServiceTests()
        {
            var serviceCollections = ServiceCollections.GetInstance();
            _listenerService = serviceCollections.GetContainer().Resolve<ListenerService>();
            _ownIpAddress = serviceCollections.GetContainer().Resolve<OwnIpAddress>();

            ipAddress = $"http://{_ownIpAddress.IpAddress}:{OwnIpAddress.Port}/";
        }

        [Fact]
        public async void ListenerService_Should_Return_200()
        {
            var client = new HttpClient();

            _listenerService.Start();
            var response = await client.GetAsync(ipAddress);
            _listenerService.Stop();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Test1")]
        [InlineData("Test2")]
        public async void ListenerService_Should_Return_202(string text)
        {
            var client = new HttpClient();
            StringContent stringContent = new StringContent(
                JsonSerializer.Serialize(new { ClipboardText = text}), Encoding.UTF8, "application/json");
            
            _listenerService.Start();
            var response = await client.PostAsync(ipAddress, stringContent);
            _listenerService.Stop();
            
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [Fact]
        public async void ListenerService_Should_Return_404()
        {
            var client = new HttpClient();
            
            _listenerService.Start();
            var response = await client.GetAsync($"{ipAddress}/a");
            _listenerService.Stop();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void ListenerService_Should_Return_500()
        {
            var client = new HttpClient();

            string content = "<h1>Hello World</h1>";
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "text/html");
            
            _listenerService.Start();
            var response = await client.PostAsync(ipAddress, stringContent);
            _listenerService.Stop();

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}