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
using System.Windows.Threading;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCTile.xaml
    /// </summary>
    public partial class UCTile : UserControl
    {
        public UCTile()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer newTimer = new DispatcherTimer();
            newTimer.Interval = System.TimeSpan.FromSeconds(1);
            newTimer.Tick += newTimer_Tick;
            newTimer.Start();
        }

        private void newTimer_Tick(object sender, object e)
        {
            lbTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");            
        }
    }
}
