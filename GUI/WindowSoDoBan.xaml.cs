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
    /// Interaction logic for WindowSoDoBan.xaml
    /// </summary>
    public partial class WindowSoDoBan : Window
    {
        private Data.Transit mTransit;
        public WindowSoDoBan(Data.Transit tran)
        {
            mTransit = tran;
            InitializeComponent();
            ucTile.SetTransit(tran);
            ucTile.TenChucNang = "Sơ đồ bàn";

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.Init(mTransit);
            uCListArea1.Init(mTransit);
            uCListArea1._UCFloorPlan = uCFloorPlan1;

            if (mTransit.NhanVien.CapDo < (int)Data.EnumLoaiNhanVien.NhanVien)
                btnThoat.Content = "Màn hình chính";
            else
                btnThoat.Content = "Thoát";
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            if (mTransit.NhanVien.CapDo < (int)Data.EnumLoaiNhanVien.NhanVien)
                DialogResult = false;
            else
                DialogResult = true;
        }

        private void uCFloorPlan1__OnEventFloorPlan(ControlLibrary.POSButtonTable tbl)
        {
            mTransit.Ban = tbl._Ban;
            WindowBanHang win = new WindowBanHang(mTransit, tbl);
            win.ShowDialog();
        }

    }
}
