using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachBanList.xaml
    /// </summary>
    public partial class UCDanhSachBanList : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BOMenuKichThuocMon mItem = null;
        private Data.BOMenuMon mMon = null;
        private List<Data.BOMenuKichThuocMon> lsArrayDeleted = null;
        private Data.BOMenuKichThuocMon BOMenuKichThuocMon = null;

        public delegate void OnExit();

        public event OnExit OnEventExit;

        public UCDanhSachBanList()
        {
            InitializeComponent();
        }

        public void Init(Data.BOMenuMon mon)
        {
            if (OnEventExit == null)
                btnHuy.Visibility = System.Windows.Visibility.Hidden;
            BOMenuKichThuocMon = new Data.BOMenuKichThuocMon(mTransit);
            mMon = mon;
            btnDanhSachGia.Visibility = System.Windows.Visibility.Hidden;
        }

        public void SetTransit(Data.Transit transit)
        {
            mTransit = transit;
            PhanQuyen();
        }

        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachBan);
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

            if (!mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachGia).ChiTietQuyen.ChoPhep)
                btnDanhSachGia.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void LoadDanhSach()
        {
            IQueryable<Data.BOMenuKichThuocMon> lsArray = BOMenuKichThuocMon.GetAll(mMon.MenuMon.MonID, true, true, true, true, true, mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.BOMenuKichThuocMon item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BOMenuKichThuocMon)li.Tag;
                if (mItem.MenuKichThuocMon.KichThuocMonID > 0)
                    btnDanhSachGia.Visibility = System.Windows.Visibility.Visible;
                else
                    btnDanhSachGia.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemDanhSachBan win = new UserControlLibrary.WindowThemDanhSachBan(mMon, mTransit);
            if (win.ShowDialog() == true)
            {
                AddList(win._Item);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BOMenuKichThuocMon)li.Tag;

                UserControlLibrary.WindowThemDanhSachBan win = new UserControlLibrary.WindowThemDanhSachBan(mMon, mTransit);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.MenuKichThuocMon.Edit = true;
                    li.Tag = win._Item;
                    li.Content = win._Item;
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.BOMenuKichThuocMon)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.BOMenuKichThuocMon>();
                }
                if (mItem.MenuKichThuocMon.KichThuocMonID > 0)
                    lsArrayDeleted.Add(mItem);
                lvData.Items.Remove(lvData.SelectedItems[0]);
                if (lvData.Items.Count > 0)
                {
                    lvData.SelectedIndex = 0;
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BOMenuKichThuocMon> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.BOMenuKichThuocMon)li.Tag;
                if (mItem.MenuKichThuocMon.KichThuocMonID == 0 || mItem.MenuKichThuocMon.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.BOMenuKichThuocMon>();
                    lsArray.Add(mItem);
                }
            }
            BOMenuKichThuocMon.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
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

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            mItem = null;
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void btnDanhSachGia_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BOMenuKichThuocMon)li.Tag;
                WindowDanhSachGia win = new WindowDanhSachGia(mItem, mTransit);
                win.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventExit != null)
            {
                OnEventExit();
            }
        }
    }
}