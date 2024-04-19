using System;
using System.ComponentModel;
using System.Net;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Helpers;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public class ListenerService : IListenerService
    {
        private Uri _baseUrl;
        private HttpListener _listener = new HttpListener();
        private BackgroundWorker _worker = new BackgroundWorker();
        
        public ListenerService(OwnIpAddress ownIpAddress)
        {
            _baseUrl = new Uri($"http://{ownIpAddress.IpAddress}:{OwnIpAddress.Port}/");
        }
        
        public event EventHandler<ListenerEventArgs> OnListenerChange;
        
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void ReceiveClipboardContent()
        {
            throw new NotImplementedException();
        }

        public void TestConnectionFromTarget()
        {
            throw new NotImplementedException();
        }
    }
}