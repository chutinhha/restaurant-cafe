using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNhanVien.xaml
    /// </summary>
    public partial class UCNhanVien : UserControl
    {
        private Data.BONhanVien BONhanVien = null;
        private List<Data.BONhanVien> lsArray = null;
        private Data.Transit mTransit = null;

        public UCNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BONhanVien = new Data.BONhanVien(transit);
            PhanQuyen();
        }
        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.NhanVien.NhanVien);
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
            BONhanVien.Refresh();
            LoadDanhSach();
        }
        private void Luu()
        {
            BONhanVien.Luu(lsArray);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (mPhanQuyen.ChiTietQuyen.DangNhap)
            {
                UserControlLibrary.WindowLoginDialog loginWindow = new UserControlLibrary.WindowLoginDialog(mTransit);
                if (loginWindow.ShowDialog() == false)
                {
                    Luu();
                }
            }
            else Luu();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

            if (lvData.SelectedItems.Count > 0)
            {
                UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit, BONhanVien);
                win._Item = (Data.BONhanVien)lvData.SelectedItems[0];
                if (win.ShowDialog() == true)
                {
                    lvData.Items.Refresh();
                }
            }

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

            UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit, BONhanVien);
            if (win.ShowDialog() == true)
            {
                lsArray.Add(win._Item);
                lvData.Items.Refresh();
            }

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                if (lvData.SelectedItems.Count > 0)
                {
                    Data.BONhanVien item = (Data.BONhanVien)lvData.SelectedItems[0];
                    if (item.NhanVien.NhanVienID > 0)
                    {
                        item.NhanVien.Deleted = true;
                    }
                    lsArray.Remove(item);
                    lvData.Items.Refresh();
                }
            }

        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = lsArray = BONhanVien.GetAll(mTransit).ToList();
            lvData.Items.Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}