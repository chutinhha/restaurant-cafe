using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachDinhLuong.xaml
    /// </summary>
    public partial class UCDanhSachDinhLuong : UserControl
    {
        private List<Data.DINHLUONG> lsArrayDeleted = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private List<ShowData> lsShowData = null;
        private Data.BOMenuKichThuocMon mKichThuocMon = null;
        private Data.Transit mTransit = null;

        public UCDanhSachDinhLuong()
        {
            InitializeComponent();
        }

        public void Init(Data.BOMenuKichThuocMon kichThuocMon, Data.Transit transit)
        {
            mTransit = transit;
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
            lsShowData = new List<ShowData>();
            lsShowData.Clear();
            List<Data.DINHLUONG> lsArray = Data.BODinhLuong.GetAll((int)mKichThuocMon.MenuKichThuocMon.KichThuocMonID, mTransit);
            foreach (Data.DINHLUONG mi in lsArray)
            {
                ShowData item = new ShowData(lsLoaiBan);
                item.TenMon = mi.MENUMON.TenDai;
                item.LoaiBanID = (int)mi.LoaiBanID;
                item.KichThuocBan = (int)mi.KichThuocBan;
                item.ID = mi.ID;
                item.MonID = (int)mi.MonID;
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.DINHLUONG> lsArray = new List<Data.DINHLUONG>();
            foreach (ShowData s in lvData.Items)
            {
                Data.DINHLUONG dl = new Data.DINHLUONG();
                dl.KichThuocMonChinhID = mKichThuocMon.MenuKichThuocMon.KichThuocMonID;
                dl.ID = s.ID;
                dl.LoaiBanID = s.LoaiBanID;
                dl.KichThuocBan = s.KichThuocBan;
                dl.MonID = s.MonID;
                dl.Deleted = false;
                dl.Visual = true;
                dl.Edit = false;
                lsArray.Add(dl);
            }
            Data.BODinhLuong.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, true);
            if (win.ShowDialog() == true)
            {
                ShowData item = new ShowData(lsLoaiBan);
                item.TenMon = win._ItemMon.MenuMon.TenNgan;
                item.KichThuocBan = 1;
                item.MonID = win._ItemMon.MenuMon.MonID;
                lsShowData.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            ShowData item = ((Button)sender).DataContext as ShowData;
            if (item.ID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.DINHLUONG>();
                Data.DINHLUONG dl = new Data.DINHLUONG();
                dl.Deleted = true;
                dl.ID = item.ID;
                lsArrayDeleted.Add(dl);
            }
            lsShowData.Remove(item);
            lvData.Items.Refresh();
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowData item = ((ComboBox)sender).DataContext as ShowData;
            if (item != null)
            {
                switch (item.LoaiBanID)
                {
                    case 1:
                        item.IsEnabled = false;
                        item.KichThuocBan = 1;
                        break;

                    case 2:
                    case 3:
                        item.IsEnabled = true;
                        if (item.ID == 0)
                            item.KichThuocBan = 1000;
                        break;
                }
                lvData.Items.Refresh();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private class ShowData
        {
            public ShowData(List<Data.LOAIBAN> loaiBan)
            {
                Loaiban = loaiBan;
                TenMon = "";
                KichThuocBan = 0;
                LoaiBanID = 1;
                IsEnabled = true;
                ID = 0;
            }

            public int ID { get; set; }

            public bool IsEnabled { get; set; }

            public int KichThuocBan { get; set; }

            public List<Data.LOAIBAN> Loaiban { get; set; }

            public int LoaiBanID { get; set; }

            public int MonID { get; set; }

            public string TenMon { get; set; }

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