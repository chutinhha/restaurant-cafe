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
    /// Interaction logic for WindowQuanLyDinhLuong.xaml
    /// </summary>
    public partial class WindowQuanLyDinhLuong : Window
    {
        private Data.Transit mTransit = null;

        private UserControlLibrary.UCDinhLuong ucDinhLuong = null;

        public WindowQuanLyDinhLuong(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.SetTransit(mTransit);
            uCTile.TenChucNang = "Quản lý định lượng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            Data.BOChiTietQuyen quyenLoaiKhachHang = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.DinhLuong.DinhLuong);
            btnDinhLuong.Tag = quyenLoaiKhachHang;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.DinhLuong.DinhLuong) || !quyenLoaiKhachHang.ChiTietQuyen.ChoPhep)
            {
                btnDinhLuong.Visibility = System.Windows.Visibility.Collapsed;
            }            
        }

        private void btnDinhLuong_Click(object sender, RoutedEventArgs e)
        {
            if (ucDinhLuong == null)
            {
                ucDinhLuong = new UserControlLibrary.UCDinhLuong();
                ucDinhLuong.Init(mTransit);
            }
            spNoiDung.Children.Clear();
            ucDinhLuong.Height = spNoiDung.ActualHeight;
            ucDinhLuong.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucDinhLuong);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCDinhLuong)
                ucDinhLuong.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDinhLuong_Click(sender, e);
        }
    }
}
