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
    /// Interaction logic for WindowThongTinCongTy.xaml
    /// </summary>
    public partial class WindowThongTinCongTy : Window
    {
        private Data.Transit mTransit = null;
        public WindowThongTinCongTy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Cài đặt thông tin";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            DialogResult = false;
        }

        private void btnCaiDatThongTinCongTy_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatThongTinDoanhNghiep win = new UserControlLibrary.WindowCaiDatThongTinDoanhNghiep(mTransit);
            win.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCaiDatGiaoDienBanHang_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatGiaoDienBanHang win = new UserControlLibrary.WindowCaiDatGiaoDienBanHang(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatChucNangHienThi_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatChucNangHienThi win = new UserControlLibrary.WindowCaiDatChucNangHienThi(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatBan_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatBan win = new UserControlLibrary.WindowCaiDatBan(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatMayInNhaBep_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatMayInNhaBep win = new UserControlLibrary.WindowCaiDatMayInNhaBep(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatMayInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatMayInHoaDon win = new UserControlLibrary.WindowCaiDatMayInHoaDon(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatThucDon_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatThucDon win = new UserControlLibrary.WindowCaiDatThucDon(mTransit);
            win.ShowDialog();
        }
    }
}
