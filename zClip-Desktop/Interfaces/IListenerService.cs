using System;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface IListenerService
    {
        event EventHandler<ListenerEventArgs> OnListenerChange;
        void Start();
        void Stop();
        void ReceiveClipboardContent();
        void TestConnectionFromTarget();
    }
}