using System;

namespace zClip_Desktop.Inferfaces
{
    public interface IClipboardService
    {
        event EventHandler<ClipboardService.ClipboardEventArgs> OnClipboardChanged;
        void Start();
        void Stop();
        void Clear();
    }
}