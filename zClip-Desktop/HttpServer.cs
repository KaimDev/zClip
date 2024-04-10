using System;
using System.ComponentModel;
using System.Net;
using System.Text;

using static zClip_Desktop.Constants.Commons;

namespace zClip_Desktop
{
    public class HttpServer
    {
        Uri baseUrl = new Uri(BaseUrl);

        // Create a new HttpListener instance
        HttpListener listener = new HttpListener();
        
        BackgroundWorker worker = new BackgroundWorker();

        public HttpServer()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            listener.Prefixes.Add(baseUrl.ToString());

            // Start listening for incoming requests
            listener.Start();
            Console.WriteLine("HTTP server started. Listening on " + baseUrl);

            // Handle incoming requests
            while (true)
            {
                // Wait for an incoming request
                HttpListenerContext context = listener.GetContext();

                // Get the request object and response object
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // Construct a response
                string responseString = "<html><body><h1>Hello from C# HTTP Server!</h1></body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                // Set the response headers
                response.ContentType = "text/html";
                response.ContentLength64 = buffer.Length;

                // Write the response to the client
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }

        private void worker_RunWorkerCompleted(object sender, 
            RunWorkerCompletedEventArgs e)
        {
            listener.Stop();
        }

    }
}