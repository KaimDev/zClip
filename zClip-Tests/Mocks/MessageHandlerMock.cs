using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace zClip_Tests.Mocks
{
    public class MessageHandlerMock : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        
        public MessageHandlerMock(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = _statusCode
            });
        }
    }
}