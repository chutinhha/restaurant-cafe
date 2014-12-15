using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WebServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private POSWebServer mWebServer;
        private System.Windows.Forms.NotifyIcon mNotifyIcon;
        public MainWindow()
        {            
            InitializeComponent();
            mNotifyIcon = new System.Windows.Forms.NotifyIcon();
            mNotifyIcon.Icon = Utilities.ImageHandler.GetIcon(@"/SystemImages;component/Images/Server.ico");
            mNotifyIcon.Visible = true;
            mNotifyIcon.DoubleClick +=
                delegate(object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }
        protected override void OnStateChanged(EventArgs e)
        {                        
            base.OnStateChanged(e);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblIp.Content = NetWorking.LocalIPAddress();
            txtPort.Text = "8080";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mWebServer = new POSWebServer(lblIp.Content.ToString(), Utilities.MoneyFormat.ConvertToInt(txtPort.Text));                        
                mWebServer.Run();                
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mWebServer.Stop();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mNotifyIcon.Dispose();
        }

        private void btnThuNho_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            if (mWebServer!=null)
            {
                mWebServer.Stop();
            }
            this.Close();
        }        
    }
}
