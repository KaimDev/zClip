using Unity;
using zClip_Desktop.Extensions;

namespace zClip_Desktop
{
    public class ServiceCollections
    {
        private static ServiceCollections _instance = null;
        private readonly IUnityContainer _container;
        
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
            _container.ConfigureOwnIp();
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