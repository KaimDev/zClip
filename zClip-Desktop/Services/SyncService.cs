using System;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Helpers;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public class SyncService : ISyncService
    {
        public event EventHandler<SyncEventArgs> OnSyncMessage;

        private IClientService _clientService;
        private IListenerService _listenerService;
        private IClipboardService _clipboardService;
        private ISecurityService _securityService;

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
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}