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
    /// Interaction logic for WindowQuanLyKhachHang.xaml
    /// </summary>
    public partial class WindowQuanLyKhachHang : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private UserControlLibrary.UCLoaiKhachHang ucLoaiKhachHang = null;
        private UserControlLibrary.UCKhachHang ucKhachHang = null;
        private void btnLoaiKhachHang_Click(object sender, RoutedEventArgs e)
        {
            if (ucLoaiKhachHang == null)
            {
                ucLoaiKhachHang = new UserControlLibrary.UCLoaiKhachHang(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucLoaiKhachHang);
        }

        private void btnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            if (ucKhachHang == null)
            {
                ucKhachHang = new UserControlLibrary.UCKhachHang(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucKhachHang);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnKhachHang_Click(sender, e);
        }
    }
}
