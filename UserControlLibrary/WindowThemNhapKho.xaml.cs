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
    /// Interaction logic for WindowThemNhapKho.xaml
    /// </summary>
    public partial class WindowThemNhapKho : Window
    {
        private Data.Transit mTransit;

        public Data.BONhapKho _Item { get; set; }
        Data.BONhapKho BONhapKho = null;
        Data.BOQuanLyKho BOQuanLyKho = null;

        public WindowThemNhapKho(Data.Transit transit, Data.BONhapKho bONhapKho)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuanLyKho = new Data.BOQuanLyKho(transit);
            BONhapKho = bONhapKho;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhoHang();
            LoadNhaCungCap();
            SetValues();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (_Item == null)
                {
                    _Item = new Data.BONhapKho();
                    _Item.NhapKho.Visual = true;
                    _Item.NhapKho.Deleted = false;
                    _Item.NhapKho.Edit = false;
                }
                GetValues();
                List<Data.BOChiTietNhapKho> lsChiTietNhapKho = (List<Data.BOChiTietNhapKho>)btnDanhSachChiTiet.Tag;
                BONhapKho.Them(_Item, lsChiTietNhapKho, mTransit);
                BOQuanLyKho.NhapKho(lsChiTietNhapKho, mTransit);
                MessageBox.Show(lbTieuDe.Text + " thành công");
                DialogResult = true;
            }
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

        private void GetValues()
        {
            _Item.NhapKho.Visual = true;
            _Item.NhapKho.Deleted = false;
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
            WindowDanhSachNhapKhoChiTiet win = new WindowDanhSachNhapKhoChiTiet();
            win.Init(_Item, mTransit);
            if (win.ShowDialog() == true)
            {
                btnDanhSachChiTiet.Tag = win.lsArray;
            }

        }
    }
}
