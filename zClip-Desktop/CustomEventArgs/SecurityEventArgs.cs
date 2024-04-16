using zClip_Desktop.Types;

namespace zClip_Desktop.CustomEventArgs
{
    public class SecurityEventArgs
    {
        public string Message { get; set; }
        public SecurityType SecurityType { get; set; }
        public bool IsSuccess { get; set; }
    }
}