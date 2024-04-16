using System;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface IClipboardService
    {
        event EventHandler<ClipboardEventArgs> OnClipboardChanged;
        void Start();
        void Stop();
        void Clear();
    }
}