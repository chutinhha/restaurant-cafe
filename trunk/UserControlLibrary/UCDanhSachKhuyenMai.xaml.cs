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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachKhuyenMai.xaml
    /// </summary>
    public partial class UCDanhSachKhuyenMai : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BOMenuKhuyenMai BOMenuKhuyenMai = null;
        private List<Data.BOMenuKichThuocMon> lsArray = null;

        public UCDanhSachKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOMenuKhuyenMai = new Data.BOMenuKhuyenMai(mTransit);
            PhanQuyen();
        }

        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.KhuyenMai);
            if (!mPhanQuyen.ChiTietQuyen.ChoPhep)
                btnDanhSach.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them)
                btnThem.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Sua)
                btnSua.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Xoa)
                btnXoa.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them && !mPhanQuyen.ChiTietQuyen.Xoa && !mPhanQuyen.ChiTietQuyen.Sua)
                btnLuu.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = lsArray = BOMenuKhuyenMai.GetDanhSachKichThuocMon();            
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemKhuyenMai win = new UserControlLibrary.WindowThemKhuyenMai(null, mTransit, BOMenuKhuyenMai);
            if (win.ShowDialog() == true)
            {
                LoadDanhSach();
            }

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                UserControlLibrary.WindowThemKhuyenMai win = new UserControlLibrary.WindowThemKhuyenMai((Data.BOMenuKichThuocMon)lvData.SelectedItems[0], mTransit, BOMenuKhuyenMai);
                if (win.ShowDialog() == true)
                {
                    LoadDanhSach();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                if (lvData.SelectedItems.Count > 0)
                {
                    Data.BOMenuKichThuocMon item = (Data.BOMenuKichThuocMon)lvData.SelectedItems[0];
                    foreach (Data.BOMenuKhuyenMai line in item.DanhSachKhuyenMai)
                    {
                        if (line.MenuKhuyenMai.KhuyenMaiID > 0)
                        {
                            line.MenuKhuyenMai.Deleted = true;
                        }
                    }
                    lsArray.Remove(item);
                    lvData.Items.Refresh();
                }
            }

        }        

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            BOMenuKhuyenMai.Luu(lsArray);
            UserControlLibrary.WindowMessageBox win = new WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            win.ShowDialog();
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((mPhanQuyen.ChiTietQuyen.Them || mPhanQuyen.ChiTietQuyen.Xoa || mPhanQuyen.ChiTietQuyen.Sua) && e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (mPhanQuyen.ChiTietQuyen.Them && e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (mPhanQuyen.ChiTietQuyen.ChoPhep && e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (mPhanQuyen.ChiTietQuyen.Sua && e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            if (mPhanQuyen.ChiTietQuyen.Xoa && e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}
