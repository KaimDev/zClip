using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Helpers;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public class ClientService : IClientService
    {
        public event EventHandler<ClientEventArgs> OnClientChange;

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        private readonly Uri _targetIpAddress;

        private readonly HttpClient _httpClient;
        public ClientService(TargetIpAddress targetIpAddress, HttpClient httpClient)
        {
            _targetIpAddress = new Uri($"http://{targetIpAddress.IpAddress}:{OwnIpAddress.Port}");
            _httpClient = httpClient;
        }

        public async Task SendClipboardContent(string clipboardContent)
        {
            StringContent jsonContent = new StringContent(
                JsonSerializer.Serialize(new
                {
                    message = clipboardContent
                }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_targetIpAddress, jsonContent);

            ClientServiceChanged(response);
        }

        public async Task TestConnectionToTarget()
        {
            var response = await _httpClient.GetAsync(_targetIpAddress);
            
            ClientServiceChanged(response);
        }

        private void ClientServiceChanged(dynamic content)
        {
            switch (content)
            {
                case HttpResponseMessage response:
                    EventIsAHttpResponse(response);
                    break;
                default:
                    return;
            }
        }

        private async void EventIsAHttpResponse(HttpResponseMessage response)
        {
            var clientArgs = new ClientEventArgs();
            var statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.Accepted)
            {
                clientArgs.StatusCode = (int)statusCode;
                clientArgs.Message = "Clipboard has been sent";
            }
            else if (statusCode == HttpStatusCode.OK)
            {
                clientArgs.StatusCode = (int)statusCode;
                clientArgs.Message = "The connection is successful";
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                clientArgs.StatusCode = (int)statusCode;
                clientArgs.Message = "Not Found";
            }
            else
            {
                // var jsonContent = await response.Content.ReadAsStringAsync();
                // var message = JsonSerializer.Deserialize<string>(jsonContent);
                // clientArgs.Message = message;
                
                // TODO: DEFAULT CASE
            }

            OnClientChange?.Invoke(this, clientArgs);
        }
    }
}