using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using zClip_Desktop.Extensions;
using zClip_Desktop.Helpers;
using zClip_Desktop.Services;

namespace zClip_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
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
            TbIpName.Text = _ipAddress;

            if (!ZClipSettings.IsEthernet)
            {
                TbIpName.TextDecorations = TextDecorations.Underline;
            }
        }

        private void RequestConnection_OnClick(object sender, RoutedEventArgs e)
        {
            var targetIpAddress = TbTargetIp.Text;
            
            if (targetIpAddress.Trim().Length == 0)
            {
                MessageBox.Show("Target ip address is required", "Alert", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            string lanIpPattern = @"^(192\.168\.\d{1,3}\.\d{1,3})$";

            var regexResonse = Regex.Match(targetIpAddress, lanIpPattern);

            if (!regexResonse.Success)
            {
                MessageBox.Show("Target ip address is incorrect", "Alert", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            
            CompleteServiceCollection(targetIpAddress);
        }

        private void CompleteServiceCollection(string targetIpAddress)
        {
            _serviceCollections.GetContainer().RegisterTargetIp(targetIpAddress);
            _serviceCollections.GetContainer().RegisterClientType();
            _serviceCollections.GetContainer().RegisterSyncService();
        }
    }
}