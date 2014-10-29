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
    /// Interaction logic for WindowQuanLyMayIn.xaml
    /// </summary>
    public partial class WindowQuanLyMayIn : Window
    {
        private Data.Transit mTransit;
        private UserControlLibrary.UCMayIn ucMayIn = null;
        private UserControlLibrary.UCMenuMayIn ucMenuMayIn = null;
        public WindowQuanLyMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMenuMayIn_Click(object sender, RoutedEventArgs e)
        {
            if (ucMenuMayIn == null)
            {
                ucMenuMayIn = new UserControlLibrary.UCMenuMayIn(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucMenuMayIn);
        }

        private void btnMayIn_Click(object sender, RoutedEventArgs e)
        {
            if (ucMayIn == null)
            {
                ucMayIn = new UserControlLibrary.UCMayIn(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucMayIn);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnMayIn_Click(sender, e);            
        }
    }
}
