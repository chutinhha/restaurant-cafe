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
    /// Interaction logic for WindowQuanLyThuChi.xaml
    /// </summary>
    public partial class WindowQuanLyThuChi : Window
    {
        private Data.Transit mTransit = null;
        private Data.BONhanVien BONhanVien = null;

        public WindowQuanLyThuChi(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BONhanVien = new Data.BONhanVien(transit);
            uCTile.TenChucNang = "Quản lý thu chi";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            Data.BOChiTietQuyen quyenNhanVien = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.ThuChi.ThuChi);
            btnThuChi.Tag = quyenNhanVien;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.ThuChi.ThuChi) || !quyenNhanVien.ChiTietQuyen.ChoPhep)
            {
                btnThuChi.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private UserControlLibrary.UCThuChi ucThuChi = null;

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void btnThuChi_Click(object sender, RoutedEventArgs e)
        {
            if (ucThuChi == null)
            {
                ucThuChi = new UserControlLibrary.UCThuChi(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucThuChi);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCThuChi)
                ucThuChi.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnThuChi.Visibility == System.Windows.Visibility.Visible)
                btnThuChi_Click(null, null);
        }
    }
}
