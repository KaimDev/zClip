using Unity;
using zClip_Desktop.Extensions;
using zClip_Desktop.Helpers;
using zClip_Desktop.Inferfaces;

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
            ConfigureServices();
            ConfigureComponents();
        }

        public void ConfigureServices()
        {
            // Get the service collections
            _serviceCollections = ServiceCollections.GetInstance();
            var container = _serviceCollections.GetContainer();

            // Configure and start the server
            container.ConfigureHttpServer();
            
            // Configure the clipboard service
            container.ConfigureClipboardService();
        }

        public void ConfigureComponents()
        {
            TB_IpName.Text = _ipAddress;
        }
    }
}