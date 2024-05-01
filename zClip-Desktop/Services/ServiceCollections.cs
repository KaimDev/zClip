using Unity;
using zClip_Desktop.Extensions;

namespace zClip_Desktop.Services
{
    public class ServiceCollections
    {
        private static ServiceCollections _instance = null;
        private IUnityContainer _container;
        
        private ServiceCollections()
        {
            _container = new UnityContainer();
            ConfigureServices();
        }
        
        public static ServiceCollections GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ServiceCollections();
            }
            return _instance;
        }
        
        private void ConfigureServices()
        {
            _container.RegisterOwnIp();
            _container.RegisterListenerService();
            _container.RegisterClipboardService();
            _container.RegisterSecurityService();
        }

        public void DestroyServices()
        {
            _container = null;
        }
        
        public IUnityContainer GetContainer()
        {
            return _container;
        }
        
        public void DestroyInstance()
        {
            _instance = null;
        }
    }
}