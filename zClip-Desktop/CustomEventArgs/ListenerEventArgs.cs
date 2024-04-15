using System;

namespace zClip_Desktop.CustomEventArgs
{
    public class ListenerEventArgs : EventArgs
    {
        public string ClipboardText { get; set; }
    }
}