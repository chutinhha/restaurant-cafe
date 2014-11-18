using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNhomQuyen.xaml
    /// </summary>
    public partial class UCQuyen : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.QUYEN mItem = null;
        private List<Data.QUYEN> lsArrayDeleted = null;
        private Data.BOQuyen BOQuyen = null;

        public UCQuyen(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuyen = new Data.BOQuyen(mTransit);
            btnQuyenNhanVien.Visibility = System.Windows.Visibility.Hidden;
            btnCaiDatChucNang.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LoadDanhSach()
        {
            IQueryable<Data.QUYEN> lsArray = BOQuyen.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.QUYEN item)
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
                mItem = (Data.QUYEN)li.Tag;
                if (mItem.MaQuyen > 0)
                {
                    btnQuyenNhanVien.Visibility = System.Windows.Visibility.Visible;
                    btnCaiDatChucNang.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    btnQuyenNhanVien.Visibility = System.Windows.Visibility.Hidden;
                    btnCaiDatChucNang.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemQuyen win = new UserControlLibrary.WindowThemQuyen(mTransit);
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
                mItem = (Data.QUYEN)li.Tag;

                UserControlLibrary.WindowThemQuyen win = new UserControlLibrary.WindowThemQuyen(mTransit);
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
                mItem = (Data.QUYEN)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.QUYEN>();
                }
                if (mItem.MaQuyen > 0)
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
            List<Data.QUYEN> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.QUYEN)li.Tag;
                if (mItem.MaQuyen == 0 || mItem.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.QUYEN>();
                    lsArray.Add(mItem);
                }
            }
            BOQuyen.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
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

        private void btnQuyenNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.QUYEN)li.Tag;
                WindowThemQuyenNhanVien win = new WindowThemQuyenNhanVien(mItem, mTransit);
                win.ShowDialog();
            }
        }

        private void btnCaiDatChucNang_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.QUYEN)li.Tag;
                WindowThemCaiDatChucNang win = new WindowThemCaiDatChucNang(mItem, mTransit);
                win.ShowDialog();
            }
        }
    }
}