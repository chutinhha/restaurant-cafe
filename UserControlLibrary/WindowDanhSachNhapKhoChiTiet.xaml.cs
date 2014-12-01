using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowDanhSachNhapKhoChiTiet.xaml
    /// </summary>
    public partial class WindowDanhSachNhapKhoChiTiet : Window
    {
        public List<Data.BOChiTietNhapKho> lsArray = null;
        public List<Data.BOChiTietNhapKho> lsArrayDeleted = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.BONhapKho mNhapKho = null;
        private Data.Transit mTransit = null;
        private Data.BOChiTietNhapKho BOChiTietNhapKho = null;

        public WindowDanhSachNhapKhoChiTiet()
        {
            InitializeComponent();
        }

        public void Init(Data.BONhapKho nhapKho, Data.Transit transit)
        {
            mTransit = transit;
            BOChiTietNhapKho = new Data.BOChiTietNhapKho(transit);
            mNhapKho = nhapKho;
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
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
                lvData.ItemsSource = lsArray = BOChiTietNhapKho.GetAll((int)mNhapKho.NhapKho.NhapKhoID, mTransit).ToList();
                lvData.Items.Refresh();
            }
            else
            {
                lsArray = new List<Data.BOChiTietNhapKho>();
                lvData.ItemsSource = lsArray;
                lvData.Items.Refresh();
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
            if (lsArray.Count > 0)
            {
                foreach (var item in lsArray)
                {
                    item.LoaiBan = item.ListLoaiBan.Where(s => s.LoaiBanID == item.ChiTietNhapKho.TONKHO.LoaiBanID).FirstOrDefault();
                }
                DialogResult = true;
            }
            else
            {
                DialogResult = false;

            }
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, true, true, false, true);            
            if (win.ShowDialog() == true)
            {
                Data.BOChiTietNhapKho item = new Data.BOChiTietNhapKho();
                item.ChiTietNhapKho.TONKHO = new Data.TONKHO();
                item.ChiTietNhapKho.TONKHO.SoLuongNhap = 1;
                item.ChiTietNhapKho.TONKHO.GiaNhap = 0;
                item.ChiTietNhapKho.TONKHO.GiaBan = 0;
                item.ChiTietNhapKho.TONKHO.NgaySanXuat = DateTime.Now;
                item.ChiTietNhapKho.TONKHO.NgayHetHan = DateTime.Now;
                item.ChiTietNhapKho.TONKHO.MonID = win._ItemMon.MenuMon.MonID;
                item.MenuMon = win._ItemMon.MenuMon;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemMon.MenuMon.DonViID).ToList();
                if (item.ListLoaiBan.Count > 0)
                {
                    item.ChiTietNhapKho.TONKHO.LoaiBanID = item.ListLoaiBan[0].LoaiBanID;
                    item.LoaiBan = item.ListLoaiBan[0];
                    item.ChiTietNhapKho.TONKHO.DonViID = item.LoaiBan.DonViID;
                }
                if (lsArray == null)
                    lsArray = new List<Data.BOChiTietNhapKho>();
                lsArray.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BOChiTietNhapKho item = ((Button)sender).DataContext as Data.BOChiTietNhapKho;
            if (item.ChiTietNhapKho.ChiTietNhapKhoID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.BOChiTietNhapKho>();
                lsArrayDeleted.Add(item);

            }
            lsArray.Remove(item);
            lvData.Items.Refresh();
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.BOChiTietNhapKho item = ((ComboBox)sender).DataContext as Data.BOChiTietNhapKho;
            if (item != null)
            {
                switch (item.ChiTietNhapKho.TONKHO.LoaiBanID)
                {
                    case (int)Data.EnumLoaiBan.Cai:
                    case (int)Data.EnumLoaiBan.DinhLuong:
                    case (int)Data.EnumLoaiBan.Gram:
                    case (int)Data.EnumLoaiBan.Millilit:
                    case (int)Data.EnumLoaiBan.Kg:
                    case (int)Data.EnumLoaiBan.Lit:
                    case (int)Data.EnumLoaiBan.Gio:
                    case (int)Data.EnumLoaiBan.Phut:
                    case (int)Data.EnumLoaiBan.Giay:
                        item.ChiTietNhapKho.Edit = true;
                        if (item.ChiTietNhapKho.ChiTietNhapKhoID == 0)
                            item.ChiTietNhapKho.TONKHO.DonViTinh = 1;
                        else if (item.ChiTietNhapKho.TONKHO.LoaiBanID != item.LoaiBan.LoaiBanID)
                            item.ChiTietNhapKho.TONKHO.DonViTinh = 1;
                        break;
                    default:
                        break;
                }
                item.LoaiBan = item.ListLoaiBan.Where(s => s.LoaiBanID == item.ChiTietNhapKho.TONKHO.LoaiBanID).FirstOrDefault();
                item.ChiTietNhapKho.TONKHO.DonViID = item.LoaiBan.DonViID;
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
            LoadDanhSach();
        }
    }
}