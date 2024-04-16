using System;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface IClientService
    {
        event EventHandler<ClientEventArgs> OnClientChange;
        void Start();
        void Stop();
        void SendClipboardContent();
        void TestForTargetConnection();
    }
}