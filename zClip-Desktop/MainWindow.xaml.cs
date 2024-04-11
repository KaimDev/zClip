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

        private string _ipAddress;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
            ConfigureComponents();
        }

        public void InitializeServices()
        {
            _serviceCollections = ServiceCollections.GetInstance();
            
            _ipAddress = _serviceCollections.GetContainer().Resolve<OwnIpAddress>().IpAddress;
            _httpServer = new HttpServer(_ipAddress);
        }

        public void ConfigureComponents()
        {
            TB_IpName.Text = _ipAddress;
        }
    }
}