using System;
using System.Threading.Tasks;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface IClientService
    {
        event EventHandler<ClientEventArgs> OnClientChange;
        void Start();
        void Stop();
        Task SendClipboardContent(string clipboardContent);
        Task TestForTargetConnection();
    }
}