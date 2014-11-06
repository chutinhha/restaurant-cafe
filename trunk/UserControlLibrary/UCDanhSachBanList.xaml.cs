using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachBanList.xaml
    /// </summary>
    public partial class UCDanhSachBanList : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.MENUKICHTHUOCMON mItem = null;
        private Data.MENUMON mMon = null;
        private List<Data.MENUKICHTHUOCMON> lsArrayDeleted = null;

        public delegate void OnExit();

        public event OnExit OnEventExit;

        public UCDanhSachBanList()
        {
            InitializeComponent();
        }

        public void Init(Data.MENUMON mon, Data.Transit transit)
        {
            if (OnEventExit == null)
                btnHuy.Visibility = System.Windows.Visibility.Hidden;
            mTransit = transit;
            mMon = mon;
            btnDanhSachGia.Visibility = System.Windows.Visibility.Hidden;
        }

        public void LoadDanhSach()
        {
            List<Data.MENUKICHTHUOCMON> lsArray = Data.BOMenuKichThuocMon.GetAll(mMon.MonID, mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.MENUKICHTHUOCMON item)
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
                mItem = (Data.MENUKICHTHUOCMON)li.Tag;
                if (mItem.KichThuocMonID > 0)
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
                mItem = (Data.MENUKICHTHUOCMON)li.Tag;

                UserControlLibrary.WindowThemDanhSachBan win = new UserControlLibrary.WindowThemDanhSachBan(mMon, mTransit);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.Edit = true;
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
                mItem = (Data.MENUKICHTHUOCMON)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.MENUKICHTHUOCMON>();
                }
                if (mItem.KichThuocMonID > 0)
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
            List<Data.MENUKICHTHUOCMON> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.MENUKICHTHUOCMON)li.Tag;
                if (mItem.KichThuocMonID == 0 || mItem.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.MENUKICHTHUOCMON>();
                    lsArray.Add(mItem);
                }
            }
            Data.BOMenuKichThuocMon.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
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
                mItem = (Data.MENUKICHTHUOCMON)li.Tag;
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