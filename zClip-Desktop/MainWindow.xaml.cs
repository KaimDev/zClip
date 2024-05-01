using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using zClip_Desktop.CustomEventArgs;
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

        private void OnSyncChanged(object sender, SyncEventArgs eventArgs) => HandleSyncChanges(eventArgs);

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

            if (!ZClipSettings.HasInternet)
            {
                TbIpName.TextDecorations = TextDecorations.Underline;
            }
        }

        private void RequestConnection_OnClick(object sender, RoutedEventArgs e)
        {
            if (BSync.Text == ButtonState.CancelSync)
            {
                CancelSync();
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

        private void CancelSync()
        {
            Dispatcher.Invoke(() =>
            {
                BSync.Text = ButtonState.RequestSync;
                TbTargetIp.Text = string.Empty;
                TbTargetIp.IsEnabled = true;
            });
                
            _serviceCollections.GetContainer().Resolve<ISyncService>().Disconnect();
            _serviceCollections.DestroyInstance();
        }

        private void CompleteServiceCollection(string targetIpAddress)
        {
            _serviceCollections.GetContainer().RegisterTargetIp(targetIpAddress);
            _serviceCollections.GetContainer().RegisterClientType();
            _serviceCollections.GetContainer().RegisterSyncService();
        }

        private void InitializeSync()
        {
            var syncService = _serviceCollections.GetContainer().Resolve<ISyncService>();
            syncService.SyncDevice();
            syncService.OnSyncMessage += OnSyncChanged;
        }

        private void HandleSyncChanges(SyncEventArgs eventArgs)
        {
            MessageBox.Show(eventArgs.Message, "Message", MessageBoxButton.OK,
                MessageBoxImage.Information);

            if (eventArgs.IsError)
                CancelSync();
        }
    }
}