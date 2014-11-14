using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyNhanVien.xaml
    /// </summary>
    public partial class WindowQuanLyNhanVien : Window
    {
        private Data.BONhanVien mItem = null;
        private Data.Transit mTransit = null;
        Data.BONhanVien BONhanVien = null;
        List<Data.BONhanVien> lsArrayDeleted = null;
        public WindowQuanLyNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BONhanVien = new Data.BONhanVien(transit);
            uCTile.TenChucNang = "Quản lý nhân viên";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            this.Close();
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

        private void AddList(Data.BONhanVien item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }


        private void lvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BONhanVien)li.Tag;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit);
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

        private void btnLuu_Click(object sender, RoutedEventArgs e)
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
            MessageBox.Show("Lưu thành công");
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
    }
}