using zClip_Desktop.Helpers;

namespace zClip_Desktop
{
    public class SyncService
    {
        private static SyncService _instance = null;
        private static TargetIpAddress _targetIp = null;

        // TODO: Add HttpServer to Inject
        // TODO: Create HttpClient to Inject too
        private SyncService(TargetIpAddress targetIp)
        {
            _targetIp = targetIp;
        }
        
        public static SyncService GetInstance(TargetIpAddress targetIp)
        {
            if (_instance == null)
            {
                _instance = new SyncService(targetIp);
            }
            return _instance;
        }
        
        public void DestroyInstance()
        {
            _targetIp = null;
            _instance = null;
        }
        
        public TargetIpAddress GetDeviceIP()
        {
            return _targetIp;
        }
    }
}