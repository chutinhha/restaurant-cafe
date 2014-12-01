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
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowDanhSachChuyenKhoChiTiet.xaml
    /// </summary>
    public partial class WindowDanhSachChuyenKhoChiTiet : Window
    {
        public List<Data.BOChiTietChuyenKho> lsArray = null;
        public List<Data.BOChiTietChuyenKho> lsArrayDeleted = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.BOChuyenKho mChuyenKho = null;
        private Data.Transit mTransit = null;
        private Data.BOChiTietChuyenKho BOChiTietChuyenKho = null;

        public WindowDanhSachChuyenKhoChiTiet()
        {
            InitializeComponent();
        }

        public void Init(Data.BOChuyenKho chuyenKho, Data.Transit transit)
        {
            mTransit = transit;
            BOChiTietChuyenKho = new Data.BOChiTietChuyenKho(transit);
            mChuyenKho = chuyenKho;
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
            if (mChuyenKho != null)
            {
                btnLuu.Visibility = System.Windows.Visibility.Visible;
                btnThemMon.Visibility = System.Windows.Visibility.Visible;
                btnDanhSach.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void LoadDanhSach()
        {
            if (mChuyenKho != null)
            {
                lvData.ItemsSource = lsArray = BOChiTietChuyenKho.GetAll((int)mChuyenKho.ChuyenKho.ChuyenKhoID, mTransit).ToList();
                lvData.Items.Refresh();
            }
            else
            {
                lsArray = new List<Data.BOChiTietChuyenKho>();
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
                    item.LoaiBan = item.ListLoaiBan.Where(s => s.LoaiBanID == item.ChiTietChuyenKho.TONKHO.LoaiBanID).FirstOrDefault();
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
                Data.BOChiTietChuyenKho item = new Data.BOChiTietChuyenKho();
                item.ChiTietChuyenKho.TONKHO = new Data.TONKHO();
                item.ChiTietChuyenKho.TONKHO.SoLuongNhap = 1;
                item.ChiTietChuyenKho.TONKHO.GiaNhap = 0;
                item.ChiTietChuyenKho.TONKHO.GiaBan = 0;
                item.ChiTietChuyenKho.TONKHO.NgaySanXuat = DateTime.Now;
                item.ChiTietChuyenKho.TONKHO.NgayHetHan = DateTime.Now;
                item.ChiTietChuyenKho.TONKHO.MonID = win._ItemMon.MenuMon.MonID;
                item.MenuMon = win._ItemMon.MenuMon;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemMon.MenuMon.DonViID).ToList();
                if (item.ListLoaiBan.Count > 0)
                {
                    item.ChiTietChuyenKho.TONKHO.LoaiBanID = item.ListLoaiBan[0].LoaiBanID;
                    item.LoaiBan = item.ListLoaiBan[0];
                    item.ChiTietChuyenKho.TONKHO.DonViID = item.LoaiBan.DonViID;
                }
                if (lsArray == null)
                    lsArray = new List<Data.BOChiTietChuyenKho>();
                lsArray.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BOChiTietChuyenKho item = ((Button)sender).DataContext as Data.BOChiTietChuyenKho;
            if (item.ChiTietChuyenKho.ChiTietChuyenKhoID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.BOChiTietChuyenKho>();
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
