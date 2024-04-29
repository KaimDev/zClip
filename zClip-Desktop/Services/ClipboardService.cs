using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public sealed class ClipboardService : IClipboardService
    {
        public event EventHandler<ClipboardEventArgs> OnClipboardChanged;

        private BackgroundWorker _backgroundWorker;
        private Dispatcher _staThreadDispatcher;
        private ManualResetEvent _dispatcherInitialized;

        public void Start()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_GetClipboard;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            _backgroundWorker.CancelAsync();
        }

        public void Clear()
        {
            _dispatcherInitialized.WaitOne();
            _staThreadDispatcher.Invoke(Clipboard.Clear);
        }

        private void BackgroundWorker_GetClipboard(object sender, DoWorkEventArgs e)
        {
            string lastText = string.Empty;

            _dispatcherInitialized = new ManualResetEvent(false);

            var staThread = new Thread(() =>
            {
                _staThreadDispatcher = Dispatcher.CurrentDispatcher;
                _dispatcherInitialized.Set();
                Dispatcher.Run();
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();

            _dispatcherInitialized.WaitOne();

            while (!_backgroundWorker.CancellationPending)
            {
                string temp = _staThreadDispatcher.Invoke(Clipboard.GetText);

                if (temp == string.Empty)
                    continue;

                if (temp != lastText)
                {
                    lastText = temp;
                    Console.WriteLine(lastText);
                    ClipboardChanged(lastText);
                }

                Thread.Sleep(1000);
            }

            _staThreadDispatcher.InvokeShutdown();
            staThread.Join();
        }

        private void ClipboardChanged(string text)
        {
            ClipboardEventArgs args = new ClipboardEventArgs(text);
            OnClipboardChanged?.Invoke(this, args);
        }

        public void SetClipboard(string text)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_SetClipboard;
            _backgroundWorker.RunWorkerAsync(text);
        }

        private void BackgroundWorker_SetClipboard(object sender, DoWorkEventArgs e)
        {
            var staThread = new Thread(() =>
            {
                var text = (string)e.Argument;
                Clipboard.SetText(text);
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
        }
    }
}