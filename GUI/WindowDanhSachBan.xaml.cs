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
    /// Interaction logic for WindowDanhSachBan.xaml
    /// </summary>
    public partial class WindowDanhSachBan : Window
    {
        public Data.MENUMON _Mon { get; set; }
        public WindowDanhSachBan(Data.MENUMON mon)
        {
            InitializeComponent();
            _Mon = mon;
        }

        private void btnThemLoaiBan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoaLoaiBan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThemGiaBan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoaGiaBan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
