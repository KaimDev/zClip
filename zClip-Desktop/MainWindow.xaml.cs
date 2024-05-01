using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using zClip_Desktop.Extensions;
using zClip_Desktop.Helpers;
using zClip_Desktop.Interfaces;
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
            // Configure Sync Button Handler
            BSync.Text = ButtonState.RequestSync;
            
            // Get the Own ip address and display it
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
            if (BSync.Text == ButtonState.CancelSync)
            {
                BSync.Text = ButtonState.RequestSync;
                TbTargetIp.IsEnabled = true;
                _serviceCollections.GetContainer().Resolve<ISyncService>().Disconnect();
                _serviceCollections.DestroyInstance();
                return;
            }
            
            var targetIpAddress = TbTargetIp.Text;
            
            if (targetIpAddress.Trim().Length == 0)
            {
                MessageBox.Show("Target ip address is required", "Alert", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            string lanIpPattern = @"^(192\.168\.\d{1,3}\.\d{1,3})$";

            var regexResponse = Regex.Match(targetIpAddress, lanIpPattern);

            if (!regexResponse.Success)
            {
                MessageBox.Show("Target ip address is incorrect", "Alert", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            
            BSync.Text = ButtonState.CancelSync;
            TbTargetIp.IsEnabled = false;

            CompleteServiceCollection(targetIpAddress);
            InitializeSync();
        }

        private void CompleteServiceCollection(string targetIpAddress)
        {
            _serviceCollections.GetContainer().RegisterTargetIp(targetIpAddress);
            _serviceCollections.GetContainer().RegisterClientType();
            _serviceCollections.GetContainer().RegisterSyncService();
        }

        private void InitializeSync()
        {
            _serviceCollections.GetContainer().Resolve<ISyncService>().SyncDevice();
        }
    }
}