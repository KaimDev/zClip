using Unity;
using zClip_Desktop.Helpers;

namespace zClip_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private HttpServer _httpServer;
        private ServiceCollections _serviceCollections;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
        }

        public void InitializeServices()
        {
            _serviceCollections = ServiceCollections.GetInstance();
            
            var ipAddress = _serviceCollections.GetContainer().Resolve<OwnIpAddress>().IpAddress;
            _httpServer = new HttpServer(ipAddress);
        }
    }
}