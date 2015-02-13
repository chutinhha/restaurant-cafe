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
    /// Interaction logic for WindowThemXyLyKho.xaml
    /// </summary>
    public partial class WindowXyLyKho : Window
    {
        public List<Data.BOXuLyKhoChiTiet> lsArray = null;
        public List<Data.BOXuLyKhoChiTiet> lsArrayDeleted = null;
        private Data.BOXuLyKhoChiTiet BOXuLyKhoChiTiet = null;
        private Data.BOXuLyKho BOXuLyKho = null;
        private Data.BOQuanLyKho BOQuanLyKho = null;
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.Transit mTransit = null;

        public WindowXyLyKho(Data.Transit transit, Data.BOXuLyKho bOXuLyKho)
        {
            InitializeComponent();
            mTransit = transit;
            //BOQuanLyKho = new Data.BOQuanLyKho(transit);
            BOXuLyKhoChiTiet = new Data.BOXuLyKhoChiTiet(transit);
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
            BOXuLyKho = bOXuLyKho;
        }

        public Data.BOXuLyKho _Item { get; set; }

        public void LoadDanhSach()
        {
            lsArrayDeleted = null;
            if (_Item != null)
            {
                lvData.ItemsSource = lsArray = BOXuLyKhoChiTiet.GetAll((int)_Item.XuLyKho.ChinhKhoID, mTransit).ToList();
                lvData.Items.Refresh();
            }
            else
            {
                lsArray = new List<Data.BOXuLyKhoChiTiet>();
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
                        _Item = new Data.BOXuLyKho();
                        _Item.XuLyKho.Visual = true;
                        _Item.XuLyKho.Deleted = false;
                        _Item.XuLyKho.Edit = false;
                        _Item.XuLyKho.NhanVienID = mTransit.NhanVien.NhanVienID;
                    }
                    GetValues();
                    BOXuLyKho.Them(_Item, lsArray, mTransit);
                    //BOQuanLyKho.NhapKho(lsArray, mTransit);
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
                Data.BOXuLyKhoChiTiet item = new Data.BOXuLyKhoChiTiet();
                item.TonKho = new Data.TONKHO();
                item.TonKho.SoLuongNhap = 1;
                item.TonKho.GiaNhap = 0;
                item.TonKho.GiaBan = win._ItemKichThuocMon.MenuKichThuocMon.GiaBanMacDinh;
                item.TonKho.NgaySanXuat = DateTime.Now;
                item.TonKho.NgayHetHan = DateTime.Now;
                item.TonKho.DonViTinh = win._ItemKichThuocMon.KichThuocLoaiBan;
                item.TonKho.MonID = win._ItemKichThuocMon.MenuMon.MonID;
                item.MenuMon = win._ItemKichThuocMon.MenuMon;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemKichThuocMon.MenuMon.DonViID).ToList();
                if (item.ListLoaiBan.Count > 0)
                {
                    item.LoaiBan = item.ListLoaiBan.Where(s => s.LoaiBanID == win._ItemKichThuocMon.MenuKichThuocMon.LoaiBanID).FirstOrDefault();
                    item.TonKho.LoaiBanID = item.LoaiBan.LoaiBanID;
                    item.TonKho.DonViID = item.LoaiBan.DonViID;
                }
                if (lsArray == null)
                    lsArray = new List<Data.BOXuLyKhoChiTiet>();
                lsArray.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BOXuLyKhoChiTiet item = ((Button)sender).DataContext as Data.BOXuLyKhoChiTiet;
            if (item.XuLyKhoChiTiet.ChinhKhoID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.BOXuLyKhoChiTiet>();
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
            _Item.XuLyKho.KhoID = 0;
            _Item.XuLyKho.LoaiID = 0;
            _Item.XuLyKho.ThoiGian = DateTime.Now;
            if (cbbKhoHang.Items.Count > 0)
            {
                _Item.XuLyKho.KhoID = (int)cbbKhoHang.SelectedValue;
                _Item.Kho = (Data.KHO)cbbKhoHang.SelectedItem;
            }
            if (cbbLoaiPhatSinh.Items.Count > 0)
            {
                _Item.XuLyKho.LoaiID = (int)cbbLoaiPhatSinh.SelectedValue;
                _Item.Loai = (Data.XULYKHOLOAI)cbbLoaiPhatSinh.SelectedItem;
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
            cbbLoaiPhatSinh.ItemsSource = Data.BOXuLyKhoLoai.GetQueryNoTracking(mTransit);
            if (cbbLoaiPhatSinh.Items.Count > 0)
                cbbLoaiPhatSinh.SelectedIndex = 0;
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                if (cbbKhoHang.Items.Count > 0)
                    cbbKhoHang.SelectedIndex = 0;
                if (cbbLoaiPhatSinh.Items.Count > 0)
                    cbbLoaiPhatSinh.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm nhập kho";
            }
            else
            {
                cbbLoaiPhatSinh.SelectedValue = _Item.XuLyKho.LoaiID;
                cbbKhoHang.SelectedValue = _Item.XuLyKho.KhoID;
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
