using System;
using System.Windows;
using zClip_Desktop.Constants;

namespace zClip_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            HttpServer httpServer = new HttpServer();

            IpName.Text = Commons.BaseUrl;
        }
    }
}