using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachDinhLuong.xaml
    /// </summary>
    public partial class UCDanhSachDinhLuong : UserControl
    {
        private List<Data.BODinhLuong> lsArrayDeleted = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.BOMenuKichThuocMon mKichThuocMon = null;
        private Data.Transit mTransit = null;
        private Data.BODinhLuong BODinhLuong = new Data.BODinhLuong();
        public UCDanhSachDinhLuong()
        {
            InitializeComponent();
        }

        public void Init(Data.BOMenuKichThuocMon kichThuocMon, Data.Transit transit)
        {
            mTransit = transit;
            BODinhLuong = new Data.BODinhLuong(transit);
            mKichThuocMon = kichThuocMon;
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit);
            if (mKichThuocMon != null)
            {
                btnLuu.Visibility = System.Windows.Visibility.Visible;
                btnThemMon.Visibility = System.Windows.Visibility.Visible;
                btnDanhSach.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void LoadDanhSach()
        {
            txtTenMon.Text = mKichThuocMon.MenuMon.TenDai + " (" + mKichThuocMon.MenuKichThuocMon.TenLoaiBan + ")";
            lvData.ItemsSource = BODinhLuong.GetAll((int)mKichThuocMon.MenuKichThuocMon.KichThuocMonID, mTransit);
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BODinhLuong> ls = new List<Data.BODinhLuong>();
            foreach (Data.BODinhLuong s in lvData.Items)
            {
                ls.Add(s);
            }
            BODinhLuong.Luu(ls, lsArrayDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, true);
            if (win.ShowDialog() == true)
            {
                ListViewItem li = new ListViewItem();
                Data.BODinhLuong item = new Data.BODinhLuong();
                item.MenuMon = win._ItemMon.MenuMon;
                li.Content = item;
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BODinhLuong item = ((Button)sender).DataContext as Data.BODinhLuong;
            if (item.DinhLuong.ID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.BODinhLuong>();
                lsArrayDeleted.Add(item);
            }
            lvData.Items.Remove(item);
            lvData.Items.Refresh();
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.BODinhLuong item = ((ComboBox)sender).DataContext as Data.BODinhLuong;
            if (item != null)
            {
                switch (item.DinhLuong.LoaiBanID)
                {
                    case 1:
                        item.DinhLuong.Edit = false;
                        item.DinhLuong.KichThuocBan = 1;
                        break;

                    case 2:
                    case 3:
                        item.DinhLuong.Edit = true;
                        if (item.DinhLuong.ID == 0)
                            item.DinhLuong.KichThuocBan = 1000;
                        break;
                }
                lvData.Items.Refresh();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}