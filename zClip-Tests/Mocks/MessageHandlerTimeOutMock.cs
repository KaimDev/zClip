using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace zClip_Tests.Mocks
{
    public class MessageHandlerTimeOutMock : HttpMessageHandler
    {
        private readonly TimeSpan _delay;

        public MessageHandlerTimeOutMock(TimeSpan delay)
        {
            _delay = delay;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Task.Delay(_delay, cancellationToken);

            return await Task.FromResult(new HttpResponseMessage());
        }
    }
}