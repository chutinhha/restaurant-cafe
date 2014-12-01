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
    /// Interaction logic for UCChuyenKho.xaml
    /// </summary>
    public partial class UCChuyenKho : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BOChuyenKho mItem = null;
        private List<Data.BOChuyenKho> lsArrayDeleted = null;
        private Data.BOChuyenKho BOChuyenKho = null;

        public UCChuyenKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            dtpThoiGian.SelectedDate = DateTime.Now;
            BOChuyenKho = new Data.BOChuyenKho(transit);
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = BOChuyenKho.GetAll(mTransit, (DateTime)dtpThoiGian.SelectedDate);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.BOChuyenKho)lvData.SelectedItems[0];
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemChuyenKho win = new UserControlLibrary.WindowThemChuyenKho(mTransit, BOChuyenKho);
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
                mItem = (Data.BOChuyenKho)li.Tag;

                UserControlLibrary.WindowThemChuyenKho win = new UserControlLibrary.WindowThemChuyenKho(mTransit, BOChuyenKho);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.ChuyenKho.Edit = true;
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
                mItem = (Data.BOChuyenKho)lvData.SelectedItems[0];
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.BOChuyenKho>();
                }
                if (mItem.ChuyenKho.ChuyenKhoID > 0)
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
            List<Data.BOChuyenKho> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.BOChuyenKho)li.Tag;
                if (mItem.ChuyenKho.ChuyenKhoID == 0 || mItem.ChuyenKho.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.BOChuyenKho>();
                    lsArray.Add(mItem);
                }
            }
            BOChuyenKho.Luu(lsArray, lsArrayDeleted, mTransit);
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
    }
}
