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
    /// Interaction logic for WindowQuanLyGiaKhuyenMai.xaml
    /// </summary>
    public partial class WindowQuanLyGiaKhuyenMai : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyGiaKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnKhuyenMai_Click(object sender, RoutedEventArgs e)
        {
            if (ucKhuyenMai == null)
            {
                ucKhuyenMai = new UserControlLibrary.UCKhuyenMai(mTransit);
            }
            spNoiDung.Children.Clear();
            ucKhuyenMai.Height = spNoiDung.ActualHeight;
            ucKhuyenMai.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucKhuyenMai);
        }

        private void btnLichBieuDinhKy_Click(object sender, RoutedEventArgs e)
        {
            if (ucLichBieuDinhKy == null)
            {
                ucLichBieuDinhKy = new UserControlLibrary.UCLichBieuDinhKy(mTransit);
            }
            spNoiDung.Children.Clear();
            ucLichBieuDinhKy.Height = spNoiDung.ActualHeight;
            ucLichBieuDinhKy.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucLichBieuDinhKy);
        }

        private void btnLichBieuKhongDinhKy_Click(object sender, RoutedEventArgs e)
        {
            if (ucUCLichBieuKhongDinhKy == null)
            {
                ucUCLichBieuKhongDinhKy = new UserControlLibrary.UCLichBieuKhongDinhKy(mTransit);
            }
            spNoiDung.Children.Clear();
            ucUCLichBieuKhongDinhKy.Height = spNoiDung.ActualHeight;
            ucUCLichBieuKhongDinhKy.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucUCLichBieuKhongDinhKy);
        }

        private UserControlLibrary.UCLoaiGia ucLoaiGia = null;
        private UserControlLibrary.UCKhuyenMai ucKhuyenMai = null;
        private UserControlLibrary.UCLichBieuDinhKy ucLichBieuDinhKy = null;
        private UserControlLibrary.UCLichBieuKhongDinhKy ucUCLichBieuKhongDinhKy = null;
        private UserControlLibrary.UCQuanLyGia ucQuanLyGia = null;
        private UserControlLibrary.UCDanhSachBan ucDanhSachBan = null;
        private void btnLoaiGia_Click(object sender, RoutedEventArgs e)
        {
            if (ucLoaiGia == null)
            {
                ucLoaiGia = new UserControlLibrary.UCLoaiGia(mTransit);
            }
            spNoiDung.Children.Clear();
            ucLoaiGia.Height = spNoiDung.ActualHeight;
            ucLoaiGia.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucLoaiGia);
        }

        private void btnDanhSachBan_Click(object sender, RoutedEventArgs e)
        {            
            if (ucDanhSachBan == null)
            {
                ucDanhSachBan = new UserControlLibrary.UCDanhSachBan(mTransit);
            }
            spNoiDung.Children.Clear();
            ucDanhSachBan.Height = spNoiDung.ActualHeight;
            ucDanhSachBan.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucDanhSachBan);
        }

        private void btnQuanLyGia_Click(object sender, RoutedEventArgs e)
        {
            if (ucQuanLyGia == null)
            {
                ucQuanLyGia = new UserControlLibrary.UCQuanLyGia(mTransit);
            }            
            ucQuanLyGia.Height = spNoiDung.ActualHeight;
            ucQuanLyGia.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Clear();            
            spNoiDung.Children.Add(ucQuanLyGia);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            spNoiDung.Height = spNoiDung.ActualHeight;
            spNoiDung.Width = spNoiDung.ActualWidth;
            btnQuanLyGia_Click(sender, e);
        }
    }
}
