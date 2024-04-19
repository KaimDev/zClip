using zClip_Desktop.Services;

namespace zClip_Desktop.CustomEventArgs
{
    public class ClientEventArgs
    {
        public string Message { get; set; }

        public int? StatusCode { get; set; }

        public ClientEventArgs() {}
        
        public ClientEventArgs(string message)
        {
            Message = message;
        }
    }
}