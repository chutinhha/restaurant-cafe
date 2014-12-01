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
    /// Interaction logic for WindowThemChuyenKho.xaml
    /// </summary>
    public partial class WindowThemChuyenKho : Window
    {
        private Data.Transit mTransit;

        public Data.BOChuyenKho _Item { get; set; }
        Data.BOChuyenKho BOChuyenKho = null;
        Data.BOQuanLyKho BOQuanLyKho = null;

        public WindowThemChuyenKho(Data.Transit transit, Data.BOChuyenKho bOChuyenKho)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuanLyKho = new Data.BOQuanLyKho(transit);
            BOChuyenKho = bOChuyenKho;
        }

        private void LoadKhoHang()
        {
            cbbKhoDi.ItemsSource = Data.BOKho.GetAllNoTracking(mTransit);
            if (cbbKhoDi.Items.Count > 0)
                cbbKhoDi.SelectedIndex = 0;
            cbbKhoDen.ItemsSource = Data.BOKho.GetAllNoTracking(mTransit);
            if (cbbKhoDen.Items.Count > 0)
                cbbKhoDen.SelectedIndex = 0;
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhoHang();            
            SetValues();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BOChiTietChuyenKho> lsChiTietNhapKho = (List<Data.BOChiTietChuyenKho>)btnDanhSachChiTiet.Tag;
            if (lsChiTietNhapKho != null && lsChiTietNhapKho.Count > 0)
            {
                if (CheckValues())
                {
                    if (_Item == null)
                    {
                        _Item = new Data.BOChuyenKho();
                        _Item.ChuyenKho.Visual = true;
                        _Item.ChuyenKho.Deleted = false;
                        _Item.ChuyenKho.Edit = false;
                        _Item.ChuyenKho.NhanVienID = mTransit.NhanVien.NhanVienID;
                    }
                    GetValues();
                    BOChuyenKho.Them(_Item, lsChiTietNhapKho, mTransit);
                    BOQuanLyKho.ChuyenKho(lsChiTietNhapKho, mTransit);
                    UserControlLibrary.WindowMessageBox.ShowDialog(lbTieuDe.Text + " thành công");                    
                    DialogResult = true;                    
                }
            }
            else
            {
                lbStatus.Text = "Chưa chọn danh sách món";
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                if (cbbKhoDi.Items.Count > 0)
                    cbbKhoDi.SelectedIndex = 0;
                if (cbbKhoDen.Items.Count > 0)
                    cbbKhoDen.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm nhập kho";
            }
            else
            {
                cbbKhoDen.SelectedValue = _Item.ChuyenKho.KhoDenID;
                cbbKhoDi.SelectedValue = _Item.ChuyenKho.KhoDiID;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa nhập kho";
            }
        }

        private void GetValues()
        {
            _Item.ChuyenKho.KhoDiID = 0;
            _Item.ChuyenKho.KhoDenID = 0;
            _Item.ChuyenKho.NgayChuyen = DateTime.Now;
            if (cbbKhoDi.Items.Count > 0)
            {
                _Item.ChuyenKho.KhoDiID = (int)cbbKhoDen.SelectedValue;
                _Item.KhoDi = (Data.KHO)cbbKhoDi.SelectedItem;
            }
            if (cbbKhoDen.Items.Count > 0)
            {
                _Item.ChuyenKho.KhoDenID = (int)cbbKhoDen.SelectedValue;
                _Item.KhoDen = (Data.KHO)cbbKhoDen.SelectedItem;
            }
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            return true;
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

        private void btnDanhSachChiTiet_Click(object sender, RoutedEventArgs e)
        {
            WindowDanhSachChuyenKhoChiTiet win = new WindowDanhSachChuyenKhoChiTiet();
            win.Init(_Item, mTransit);
            if (win.ShowDialog() == true)
            {
                btnDanhSachChiTiet.Tag = win.lsArray;
            }

        }
    }
}
