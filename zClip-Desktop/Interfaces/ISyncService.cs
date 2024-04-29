using System;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface ISyncService
    {
        event EventHandler<SyncEventArgs> OnSyncMessage;
        void SyncDevice();
        void Disconnect();
    }
}