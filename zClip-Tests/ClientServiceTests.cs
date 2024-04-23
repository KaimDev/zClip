using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Helpers;
using zClip_Desktop.Services;
using zClip_Tests.Mocks;

namespace zClip_Tests
{
    public class ClientServiceTests
    {
        private ClientService _clientService;
        private readonly TargetIpAddress _targetIpAddress = new TargetIpAddress("https://google.com");

        [Fact]
        public async Task Clipboard_Should_Have_Been_Sent_202()
        {
            var messageHandlerMock = new MessageHandlerMock(HttpStatusCode.Accepted);
            _clientService = new ClientService(_targetIpAddress, new HttpClient(messageHandlerMock));

            ClientEventArgs clientEventArgs = new ClientEventArgs();
            _clientService.OnClientChange += (sender, args) => clientEventArgs = args;

            await _clientService.SendClipboardContent("Test");

            clientEventArgs.StatusCode.Should().Be(202);
        }
    }
}