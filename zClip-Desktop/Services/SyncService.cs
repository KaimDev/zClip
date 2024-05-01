using System;
using System.Threading;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Interfaces;
using static System.Net.HttpStatusCode;

namespace zClip_Desktop.Services
{
    public class SyncService : ISyncService
    {
        public event EventHandler<SyncEventArgs> OnSyncMessage;

        private readonly IClientService _clientService;
        private readonly IListenerService _listenerService;
        private readonly IClipboardService _clipboardService;
        private readonly ISecurityService _securityService;

        private CancellationToken _cancellationToken;

        public SyncService(
            IClientService clientService,
            IListenerService listenerService,
            IClipboardService clipboardService,
            ISecurityService securityService)
        {
            _clientService = clientService;
            _listenerService = listenerService;
            _clipboardService = clipboardService;
            _securityService = securityService;

            _clientService.OnClientChange += OnClientChanged;
            _listenerService.OnListenerChange += OnListenerChanged;
            _clipboardService.OnClipboardChanged += OnClipboardChanged;
            _securityService.OnSecurityChange += OnSecurityChanged;
        }

        public async void SyncDevice()
        {
            try
            {
                await _clientService.TestConnectionToTarget();

                _cancellationToken.ThrowIfCancellationRequested();

                _clipboardService.Start();
                _listenerService.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                SyncEventArgs syncEventArgs = new SyncEventArgs
                {
                    IsError = true,
                    Message = e.Message
                };

                OnSyncMessage?.Invoke(this, syncEventArgs);
            }
        }

        public void Disconnect()
        {
            _clipboardService.Stop();
            _listenerService.Stop();
        }

        private void OnClientChanged(object sender, ClientEventArgs eventArgs)
        {
            SyncEventArgs syncEventArgs = new SyncEventArgs();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = cancellationTokenSource.Token;

            switch (eventArgs.StatusCode)
            {
                case (int)InternalServerError:
                    syncEventArgs.IsError = true;
                    syncEventArgs.Message = eventArgs.Message;
                    cancellationTokenSource.Cancel();
                    break;

                default:
                    syncEventArgs.Message = eventArgs.Message;
                    break;
            }

            OnSyncMessage?.Invoke(this, syncEventArgs);
        }

        private async void OnClipboardChanged(object sender, ClipboardEventArgs eventArgs)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(10));
            _cancellationToken = cancellationTokenSource.Token;
            
            await _clientService.SendClipboardContent(eventArgs.Text, _cancellationToken);
        }

        private void OnListenerChanged(object sender, ListenerEventArgs eventArgs)
        {
            _clipboardService.SetClipboard(eventArgs.ClipboardText);
        }

        private void OnSecurityChanged(object sender, SecurityEventArgs eventArgs)
        {
        }
    }
}