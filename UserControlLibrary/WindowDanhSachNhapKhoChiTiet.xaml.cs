using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowDanhSachNhapKhoChiTiet.xaml
    /// </summary>
    public partial class WindowDanhSachNhapKhoChiTiet : Window
    {
        public List<Data.CHITIETNHAPKHO> lsArray = null;
        public List<Data.CHITIETNHAPKHO> lsArrayDeleted = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private List<ShowData> lsShowData = null;
        private Data.NHAPKHO mNhapKho = null;
        private Data.Transit mTransit = null;

        public WindowDanhSachNhapKhoChiTiet()
        {
            InitializeComponent();
        }

        public void Init(Data.NHAPKHO nhapKho, Data.Transit transit)
        {
            mTransit = transit;
            mNhapKho = nhapKho;
            lsLoaiBan = Data.BOLoaiBan.GetAll(null, mTransit);
            if (mNhapKho != null)
            {
                btnLuu.Visibility = System.Windows.Visibility.Visible;
                btnThemMon.Visibility = System.Windows.Visibility.Visible;
                btnDanhSach.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void LoadDanhSach()
        {
            if (mNhapKho != null)
            {
                lsShowData = new List<ShowData>();
                lsShowData.Clear();
                List<Data.CHITIETNHAPKHO> lsArray = Data.BOChiTietNhapKho.GetAll((int)mNhapKho.NhapKhoID, mTransit);
                foreach (Data.CHITIETNHAPKHO mi in lsArray)
                {
                    ShowData item = new ShowData(lsLoaiBan);
                    item.TenMon = mi.MENUMON.TenDai;
                    item.LoaiBanID = (int)mi.LoaiBanID;
                    item.KichThuocBan = (int)mi.KichThuocBan;
                    item.ChiTietNhapKhoID = mi.ChiTietNhapKhoID;
                    item.NgayHetHan = (DateTime)mi.NgayHetHan;
                    item.NgaySanXuat = (DateTime)mi.NgaySanXuat;
                    item.MonID = (int)mi.MonID;
                    lsShowData.Add(item);
                }
                lvData.ItemsSource = lsShowData;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            lsArray = new List<Data.CHITIETNHAPKHO>();
            foreach (ShowData s in lvData.Items)
            {
                Data.CHITIETNHAPKHO dl = new Data.CHITIETNHAPKHO();
                dl.ChiTietNhapKhoID = s.ChiTietNhapKhoID;
                dl.LoaiBanID = s.LoaiBanID;
                dl.KichThuocBan = s.KichThuocBan;
                dl.NgaySanXuat = s.NgaySanXuat;
                dl.NgayHetHan = s.NgayHetHan;
                dl.SoLuong = s.SoLuong;
                dl.MonID = s.MonID;
                dl.GiaMua = s.GiaMua;
                dl.GiaBan = s.GiaBan;
                dl.Deleted = false;
                dl.Visual = true;
                dl.Edit = false;
                lsArray.Add(dl);
            }
            DialogResult = true;
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, true);
            if (win.ShowDialog() == true)
            {
                ShowData item = new ShowData(lsLoaiBan);
                item.TenMon = win._ItemMon.TenNgan;
                item.KichThuocBan = 1;
                item.MonID = win._ItemMon.MonID;
                item.SoLuong = 1;
                item.GiaBan = 0;
                item.GiaMua = 0;
                item.NgayHetHan = DateTime.Now;
                item.NgaySanXuat = DateTime.Now;
                if (lsShowData == null)
                {
                    lsShowData = new List<ShowData>();
                    lvData.ItemsSource = lsShowData;
                }
                lsShowData.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            ShowData item = ((Button)sender).DataContext as ShowData;
            if (item.ChiTietNhapKhoID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.CHITIETNHAPKHO>();
                Data.CHITIETNHAPKHO dl = new Data.CHITIETNHAPKHO();
                dl.Deleted = true;
                dl.ChiTietNhapKhoID = item.ChiTietNhapKhoID;
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
                        if (item.ChiTietNhapKhoID == 0)
                            item.KichThuocBan = 1000;
                        break;
                }
                lvData.Items.Refresh();
            }
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
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
                ChiTietNhapKhoID = 0;
            }

            public int ChiTietNhapKhoID { get; set; }

            public int GiaBan { get; set; }

            public int GiaMua { get; set; }

            public bool IsEnabled { get; set; }

            public int KichThuocBan { get; set; }

            public List<Data.LOAIBAN> Loaiban { get; set; }

            public int LoaiBanID { get; set; }

            public int MonID { get; set; }

            public DateTime NgayHetHan { get; set; }

            public DateTime NgaySanXuat { get; set; }

            public int SoLuong { get; set; }
            public string TenMon { get; set; }
        }
    }
}