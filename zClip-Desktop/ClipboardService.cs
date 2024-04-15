using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using zClip_Desktop.Inferfaces;

namespace zClip_Desktop
{
    public sealed class ClipboardService : IClipboardService
    {
        public event EventHandler<ClipboardEventArgs> OnClipboardChanged;

        private BackgroundWorker _backgroundWorker;

        public void Start()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            _backgroundWorker.CancelAsync();
        }

        public void Clear()
        {
            var staThread = new Thread(Clipboard.Clear);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string lastText = string.Empty;
            while (!_backgroundWorker.CancellationPending)
            {
                string temp = string.Empty;

                // Create a new thread in STA mode to get the clipboard text
                var staThread = new Thread(() =>
                {
                    temp = Clipboard.GetText();

                    if (temp == string.Empty)
                        return;

                    if (temp != lastText)
                    {
                        lastText = temp;
                        Console.WriteLine(lastText);
                        ClipboardChanged(lastText);
                    }
                });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                Thread.Sleep(1000);
            }
        }

        private void ClipboardChanged(string text)
        {
            ClipboardEventArgs args = new ClipboardEventArgs(text);
            OnClipboardChanged?.Invoke(this, args);
        }

        public class ClipboardEventArgs : EventArgs
        {
            public ClipboardEventArgs(string text)
            {
                Text = text;
            }

            public string Text { get; set; }
        }
    }
}