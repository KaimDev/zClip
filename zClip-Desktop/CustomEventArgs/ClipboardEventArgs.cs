using System;

namespace zClip_Desktop.CustomEventArgs
{
    public class ClipboardEventArgs : EventArgs
    {
        public ClipboardEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}