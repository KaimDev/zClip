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
            InitializeServices();
            ConfigureComponents();
        }

        public void InitializeServices()
        {
            // Get the service collections
            _serviceCollections = ServiceCollections.GetInstance();
            var container = _serviceCollections.GetContainer();

            // Configure and start the server
            _ipAddress = container.Resolve<OwnIpAddress>().IpAddress;
            container.ConfigureHttpServer(_ipAddress);
            
            // Configure the clipboard service
            container.ConfigureClipboardService();
            (container.Resolve(typeof(IClipboardService), null) as IClipboardService).Start();
        }

        public void ConfigureComponents()
        {
            TB_IpName.Text = _ipAddress;
        }
    }
}