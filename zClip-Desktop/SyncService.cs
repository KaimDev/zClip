namespace zClip_Desktop
{
    public class SyncService
    {
        private static SyncService _instance = null;
        private static string _deviceIP = null;

        private SyncService(string deviceIP)
        {
            _deviceIP = deviceIP;
        }
        
        public static SyncService GetInstance(string deviceIP)
        {
            if (_instance == null)
            {
                _instance = new SyncService(deviceIP);
            }
            return _instance;
        }
        
        public void DestroyInstance()
        {
            _instance = null;
        }
        
        public string GetDeviceIP()
        {
            return _deviceIP;
        }
    }
}