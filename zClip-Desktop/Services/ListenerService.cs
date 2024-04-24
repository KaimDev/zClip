using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Helpers;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public class ListenerService : IListenerService
    {
        public event EventHandler<ListenerEventArgs> OnListenerChange;
        
        private string _baseUrl;
        private HttpListener _listener;
        private BackgroundWorker _backgroundWorker = new BackgroundWorker();
        
        public ListenerService(OwnIpAddress ownIpAddress, HttpListener httpListener)
        {
            _baseUrl = $"http://{ownIpAddress.IpAddress}:{OwnIpAddress.Port}/";
            _listener = httpListener;
            _listener.Prefixes.Add(_baseUrl);
        }
        
        public void Start()
        {
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _listener.Start();

            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                HandleRequests(context);
            }
        }

        private void HandleRequests(HttpListenerContext context)
        {
            var request = context.Request;
            var method = request.HttpMethod;
            var url = request.RawUrl;
            
            if (method == HttpMethod.Get.Method && url == "/")
            {
                TestConnectionFromTarget(context);
            }
            else if (method == HttpMethod.Post.Method && url == "/")
            {
                ReceiveClipboardContent(context);
            }
            else
            {
                NotFoundResponse(context);
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void ReceiveClipboardContent(HttpListenerContext context)
        {
            var request = context.Request;

            // Get the input stream
            Stream inputStream = request.InputStream;

            // Create a new StreamReader and read the input stream
            StreamReader reader = new StreamReader(inputStream, request.ContentEncoding);
            string requestBody = reader.ReadToEnd();

            // TODO: Try Catch against not-json-texts
            var clipboardText = JsonSerializer.Deserialize<ListenerEventArgs>(requestBody);
            
            // Close the StreamReader and the InputStream
            reader.Close();
            inputStream.Close();

            var response = context.Response;

            response.StatusCode = (int)HttpStatusCode.Accepted;
            response.Close();

            ListenerServiceChanged(clipboardText);
        }

        public void TestConnectionFromTarget(HttpListenerContext context)
        {
            var response = context.Response;
            // Prepare the response
            string responseString = "<html><body><h1>Hello, World!</h1></body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // You must close the output stream.
            output.Close();
        }

        public void NotFoundResponse(HttpListenerContext context)
        {
            var response = context.Response;

            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Close();
        }

        private void ListenerServiceChanged(ListenerEventArgs clipboardContent)
        {
            OnListenerChange?.Invoke(this, clipboardContent);
        }
    }
}