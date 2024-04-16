using System;
using zClip_Desktop.Services;

namespace zClip_Desktop.Interfaces
{
    public interface IClipboardService
    {
        event EventHandler<ClipboardService.ClipboardEventArgs> OnClipboardChanged;
        void Start();
        void Stop();
        void Clear();
    }
}