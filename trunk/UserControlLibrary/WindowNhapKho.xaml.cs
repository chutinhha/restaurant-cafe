using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowNhapKho.xaml
    /// </summary>
    public partial class WindowNhapKho : Window
    {
        public List<Data.BOChiTietNhapKho> lsArray = null;
        public List<Data.BOChiTietNhapKho> lsArrayDeleted = null;
        private Data.BOChiTietNhapKho BOChiTietNhapKho = null;
        private Data.BONhapKho BONhapKho = null;
        private Data.BOQuanLyKho BOQuanLyKho = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.Transit mTransit = null;

        public WindowNhapKho(Data.Transit transit, Data.BONhapKho bONhapKho)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuanLyKho = new Data.BOQuanLyKho(transit);
            BOChiTietNhapKho = new Data.BOChiTietNhapKho(transit);
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
            BONhapKho = bONhapKho;
        }

        public Data.BONhapKho _Item { get; set; }

        public void LoadDanhSach()
        {
            if (_Item != null)
            {
                lvData.ItemsSource = lsArray = BOChiTietNhapKho.GetAll((int)_Item.NhapKho.NhapKhoID, mTransit).ToList();
                lvData.Items.Refresh();
            }
            else
            {
                lsArray = new List<Data.BOChiTietNhapKho>();
                lvData.ItemsSource = lsArray;
                lvData.Items.Refresh();
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (lsArray != null && lsArray.Count > 0)
            {
                if (CheckValues())
                {
                    if (_Item == null)
                    {
                        _Item = new Data.BONhapKho();
                        _Item.NhapKho.Visual = true;
                        _Item.NhapKho.Deleted = false;
                        _Item.NhapKho.Edit = false;
                        _Item.NhapKho.NhanVienID = mTransit.NhanVien.NhanVienID;
                    }
                    GetValues();
                    BONhapKho.Them(_Item, lsArray, mTransit);
                    BOQuanLyKho.NhapKho(lsArray, mTransit);
                    UserControlLibrary.WindowMessageBox.ShowDialog(lbTieuDe.Text + " thành công");
                    DialogResult = true;
                }
            }
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, false, true, false, true);
            if (win.ShowDialog() == true)
            {
                Data.BOChiTietNhapKho item = new Data.BOChiTietNhapKho();
                item.ChiTietNhapKho.TONKHO = new Data.TONKHO();
                item.ChiTietNhapKho.TONKHO.SoLuongNhap = 1;
                item.ChiTietNhapKho.TONKHO.GiaNhap = 0;
                item.ChiTietNhapKho.TONKHO.GiaBan = win._ItemKichThuocMon.MenuKichThuocMon.GiaBanMacDinh;
                item.ChiTietNhapKho.TONKHO.NgaySanXuat = DateTime.Now;
                item.ChiTietNhapKho.TONKHO.NgayHetHan = DateTime.Now;
                item.ChiTietNhapKho.TONKHO.DonViTinh = win._ItemKichThuocMon.KichThuocLoaiBan;
                item.ChiTietNhapKho.TONKHO.MonID = win._ItemKichThuocMon.MenuMon.MonID;
                item.MenuMon = win._ItemKichThuocMon.MenuMon;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemKichThuocMon.MenuMon.DonViID).ToList();
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

        }

        private bool CheckValues()
        {
            return true;
        }

        private void GetValues()
        {
            _Item.NhapKho.KhoID = 0;
            _Item.NhapKho.NhaCungCapID = 0;
            _Item.NhapKho.ThoiGian = DateTime.Now;
            if (cbbKhoHang.Items.Count > 0)
            {
                _Item.NhapKho.KhoID = (int)cbbKhoHang.SelectedValue;
                _Item.Kho = (Data.KHO)cbbKhoHang.SelectedItem;
            }
            if (cbbNhaCungCap.Items.Count > 0)
            {
                _Item.NhapKho.NhaCungCapID = (int)cbbNhaCungCap.SelectedValue;
                _Item.NhaCungCap = (Data.NHACUNGCAP)cbbNhaCungCap.SelectedItem;
            }
        }

        private void LoadKhoHang()
        {
            cbbKhoHang.ItemsSource = Data.BOKho.GetAllNoTracking(mTransit);
            if (cbbKhoHang.Items.Count > 0)
                cbbKhoHang.SelectedIndex = 0;
        }

        private void LoadNhaCungCap()
        {
            cbbNhaCungCap.ItemsSource = Data.BONhaCungCap.GetAllNoTracking(mTransit);
            if (cbbNhaCungCap.Items.Count > 0)
                cbbNhaCungCap.SelectedIndex = 0;
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                if (cbbKhoHang.Items.Count > 0)
                    cbbKhoHang.SelectedIndex = 0;
                if (cbbNhaCungCap.Items.Count > 0)
                    cbbNhaCungCap.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm nhập kho";
            }
            else
            {
                cbbNhaCungCap.SelectedValue = _Item.NhapKho.NhaCungCapID;
                cbbKhoHang.SelectedValue = _Item.NhapKho.KhoID;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa nhập kho";
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
            LoadKhoHang();
            LoadNhaCungCap();
            SetValues();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnLuu_Click(null, null);
                return;
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }
        }
    }
}