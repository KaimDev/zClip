using Unity;
using zClip_Desktop.Helpers;
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
            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            _serviceCollections = ServiceCollections.GetInstance();
            var ownIpAddress = _serviceCollections.GetContainer().Resolve<OwnIpAddress>();
            
            _ipAddress = ownIpAddress.IpAddress;
            TB_IpName.Text = _ipAddress;
        }
    }
}