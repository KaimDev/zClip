using Unity;
using zClip_Desktop.Extensions;
using zClip_Desktop.Interfaces;
using zClip_Desktop.Services;

namespace zClip_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ListenerService _listenerService;
        private ServiceCollections _serviceCollections;

        private string _ipAddress;

        public MainWindow()
        {
            InitializeComponent();
            ConfigureServices();
            ConfigureComponents();
        }

        private void ConfigureServices()
        {
            // Get the service collections
            _serviceCollections = ServiceCollections.GetInstance();
            var container = _serviceCollections.GetContainer();

            // Configure and start the server
            container.ConfigureListenerService();
            
            // Configure the clipboard service
            container.ConfigureClipboardService();
        }

        public void ConfigureComponents()
        {
            TB_IpName.Text = _ipAddress;
        }
    }
}