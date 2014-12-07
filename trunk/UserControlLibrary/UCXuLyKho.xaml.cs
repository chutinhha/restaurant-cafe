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
    /// Interaction logic for UCXuLyKho.xaml
    /// </summary>
    public partial class UCXuLyKho : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BONhapKho mItem = null;
        private List<Data.BONhapKho> lsArrayDeleted = null;
        private Data.BONhapKho BONhapKho = null;

        public UCXuLyKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            dtpThoiGian.SelectedDate = DateTime.Now;
            BONhapKho = new Data.BONhapKho(transit);
        }

        private void LoadDanhSach()
        {
            lsArrayDeleted = null;
            lvData.ItemsSource = BONhapKho.GetAll(mTransit, (DateTime)dtpThoiGian.SelectedDate);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.BONhapKho)lvData.SelectedItems[0];
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowNhapKho win = new UserControlLibrary.WindowNhapKho(mTransit, BONhapKho);
            if (win.ShowDialog() == true)
            {
                LoadDanhSach();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BONhapKho)li.Tag;

                UserControlLibrary.WindowNhapKho win = new UserControlLibrary.WindowNhapKho(mTransit, BONhapKho);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.NhapKho.Edit = true;
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
                mItem = (Data.BONhapKho)lvData.SelectedItems[0];
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.BONhapKho>();
                }
                if (mItem.NhapKho.NhapKhoID > 0)
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
            List<Data.BONhapKho> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.BONhapKho)li.Tag;
                if (mItem.NhapKho.NhapKhoID == 0 || mItem.NhapKho.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.BONhapKho>();
                    lsArray.Add(mItem);
                }
            }
            BONhapKho.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                //btnLuu_Click(null, null);
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
                //btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                //btnXoa_Click(null, null);
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

        private void dtpThoiGian_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.ItemsSource != null)
                LoadDanhSach();
        }
    }
}
