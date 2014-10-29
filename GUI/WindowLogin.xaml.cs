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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private Data.Transit mTransit = null;
        public WindowLogin()
        {
            InitializeComponent();
            mTransit = new Data.Transit();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process[] lsProcess = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in lsProcess)
            {
                if (process.ProcessName == "GUI")
                    process.Kill();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserID._UCKeyPad = uCKeyPad;
            txtPassword._UCKeyPad = uCKeyPad;
        }
    }
}
