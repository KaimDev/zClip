using System;
using System.Net;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface IListenerService
    {
        event EventHandler<ListenerEventArgs> OnListenerChange;
        void Start();
        void Stop();
        void ReceiveClipboardContent(HttpListenerContext context);
        void TestConnectionFromTarget(HttpListenerContext context);
        void NotFoundResponse(HttpListenerContext context);
    }
}