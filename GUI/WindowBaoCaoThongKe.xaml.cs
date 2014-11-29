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
    /// Interaction logic for WindowBaoCaoThongKe.xaml
    /// </summary>
    public partial class WindowBaoCaoThongKe : Window
    {
        private Data.Transit mTransit = null;
        public WindowBaoCaoThongKe(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Báo cáo thống kê";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnLichSuDangNhap_Click(object sender, RoutedEventArgs e)
        {
            BaoCao.LichSuDangNhap.WindowLichSuDangNhap win = new BaoCao.LichSuDangNhap.WindowLichSuDangNhap(mTransit);
            win.ShowDialog();
        }

        private void btnBaoCaoLichSuBanHang_Click(object sender, RoutedEventArgs e)
        {
            //UserControlLibrary.WindowBaoCaoLichSuBanHang win = new UserControlLibrary.WindowBaoCaoLichSuBanHang(mTransit);
            //win.ShowDialog();
            BaoCao.LichSuBanHang.WindowLichSuBanHang win = new BaoCao.LichSuBanHang.WindowLichSuBanHang(mTransit);
            win.ShowDialog();
        }
    }
}
