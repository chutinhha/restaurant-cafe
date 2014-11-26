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
        private List<Data.BONhanVien> lsArrayDeleted = null;
        private Data.BONhanVien mItem = null;
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

        private void AddList(Data.BONhanVien item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            mItem = null;
            lsArrayDeleted = null;
            LoadDanhSach();
        }
        private void Luu()
        {
            List<Data.BONhanVien> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.BONhanVien)li.Tag;
                if (mItem.NhanVien.NhanVienID == 0 || mItem.NhanVien.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.BONhanVien>();
                    lsArray.Add(mItem);
                }
            }
            BONhanVien.Luu(lsArray, lsArrayDeleted, mTransit);
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
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BONhanVien)li.Tag;

                UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.NhanVien.Edit = true;
                    li.Tag = win._Item;
                    li.Content = win._Item;
                    lvData.Items.Refresh();
                }
            }

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

            UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit);
            if (win.ShowDialog() == true)
            {
                AddList(win._Item);
            }

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.BONhanVien)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.BONhanVien>();
                }
                if (mItem.NhanVien.NhanVienID > 0)
                    lsArrayDeleted.Add(mItem);
                lvData.Items.Remove(lvData.SelectedItems[0]);
                if (lvData.Items.Count > 0)
                {
                    lvData.SelectedIndex = 0;
                }
            }


        }

        private void LoadDanhSach()
        {
            IQueryable<Data.BONhanVien> lsArray = BONhanVien.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void lvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BONhanVien)li.Tag;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}