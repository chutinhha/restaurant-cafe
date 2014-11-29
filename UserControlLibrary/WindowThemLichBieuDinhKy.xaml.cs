using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLichBieuDinhKy.xaml
    /// </summary>
    public partial class WindowThemLichBieuDinhKy : Window
    {
        private Data.Transit mTransit;

        public Data.BOLichBieuDinhKy _Item { get; set; }

        public WindowThemLichBieuDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTheLoai();
            LoadKhu();
            LoadLoaiGia();
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
                    _Item = new Data.BOLichBieuDinhKy();
                    _Item.LichBieuDinhKy.Visual = true;
                    _Item.LichBieuDinhKy.Deleted = false;
                    _Item.LichBieuDinhKy.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtTenLichBieu.Text = "";
                if (cbbLoaiGia.Items.Count > 0)
                    cbbLoaiGia.SelectedIndex = 0;
                if (cbbTheLoai.Items.Count > 0)
                    cbbTheLoai.SelectedIndex = 0;
                if (cbbKhu.Items.Count > 0)
                    cbbKhu.SelectedIndex = 0;
                txtUuTien.Text = "1";
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Sửa Lịch Biểu Định Kỳ";
            }
            else
            {
                txtTenLichBieu.Text = _Item.LichBieuDinhKy.TenLichBieu;
                cbbLoaiGia.SelectedValue = _Item.LichBieuDinhKy.LoaiGiaID;
                cbbTheLoai.SelectedValue = _Item.LichBieuDinhKy.TheLoaiID;
                cbbTheLoai_SelectionChanged(null, null);
                cbbGiaTriBatDau.SelectedValue = _Item.LichBieuDinhKy.GiaTriBatDau;
                cbbGiaTriKetThuc.SelectedValue = _Item.LichBieuDinhKy.GiaTriKetThuc;
                txtUuTien.Text = _Item.LichBieuDinhKy.UuTien.ToString();
                timeBatDau.TimeCurent = (TimeSpan)_Item.LichBieuDinhKy.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)_Item.LichBieuDinhKy.GioKetThuc;
                btnLuu.Content = mTransit.StringButton.Luu;
                if (_Item.LichBieuDinhKy.KhuID == null)
                    cbbKhu.SelectedValue = 0;
                else
                    cbbKhu.SelectedValue = _Item.LichBieuDinhKy.KhuID;
                lbTieuDe.Text = "Thêm Lịch Biểu Định Kỳ";
            }
        }

        private void GetValues()
        {
            _Item.LichBieuDinhKy.TenLichBieu = txtTenLichBieu.Text;
            _Item.LichBieuDinhKy.TheLoaiID = (int)cbbTheLoai.SelectedValue;
            _Item.LichBieuDinhKy.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            _Item.LichBieuDinhKy.GiaTriBatDau = (int)cbbGiaTriBatDau.SelectedValue;
            _Item.LichBieuDinhKy.GiaTriKetThuc = (int)cbbGiaTriKetThuc.SelectedValue;
            _Item.LichBieuDinhKy.UuTien = System.Convert.ToInt32(txtUuTien.Text);
            _Item.LichBieuDinhKy.Visual = true;
            _Item.LichBieuDinhKy.Deleted = false;
            _Item.LichBieuDinhKy.GioBatDau = timeBatDau.TimeCurent;
            _Item.LichBieuDinhKy.GioKetThuc = timeKetThuc.TimeCurent;
            if ((int)cbbKhu.SelectedValue == 0)
                _Item.LichBieuDinhKy.KhuID = null;
            else
                _Item.LichBieuDinhKy.KhuID = (int)cbbKhu.SelectedValue;
            _Item.TenKhu = cbbKhu.Text;
            _Item.MenuLoaiGia.Ten = cbbLoaiGia.Text;

            switch (_Item.LichBieuDinhKy.TheLoaiID)
            {
                case 1:
                    if (_Item.LichBieuDinhKy.GiaTriBatDau == _Item.LichBieuDinhKy.GiaTriKetThuc)
                    {
                        _Item.LichBieuDinhKy.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        _Item.LichBieuDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 2:
                    if (_Item.LichBieuDinhKy.GiaTriBatDau == _Item.LichBieuDinhKy.GiaTriKetThuc)
                    {
                        _Item.LichBieuDinhKy.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        _Item.LichBieuDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 3:
                    _Item.LichBieuDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " " + cbbGiaTriKetThuc.Text;
                    break;

                default:
                    break;
            }
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenLichBieu.Text == "")
            {
                lbStatus.Text = "Tên lịch biểu không được bỏ trống";
                return false;
            }

            if (txtUuTien.Text == "")
                txtUuTien.Text = "1";
            return true;
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
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

        private void LoadLoaiGia()
        {
            cbbLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAllNoTracking(mTransit);
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
        }

        private void LoadLoaiLichBieuBatDau(int TheLoaiID)
        {
            cbbGiaTriBatDau.ItemsSource = Data.BOLoaiLichBieu.GetAll(TheLoaiID, mTransit);
            if (cbbGiaTriBatDau.Items.Count > 0)
                cbbGiaTriBatDau.SelectedIndex = 0;
        }

        private void LoadLoaiLichBieuKetThuc(int TheLoaiID)
        {
            cbbGiaTriKetThuc.ItemsSource = Data.BOLoaiLichBieu.GetAll(TheLoaiID, mTransit);
            if (cbbGiaTriKetThuc.Items.Count > 0)
                cbbGiaTriKetThuc.SelectedIndex = 0;
        }

        private void LoadTheLoai()
        {
            cbbTheLoai.ItemsSource = Data.BOTheLoaiLichBieu.GetAllNoTracking(mTransit);
            if (cbbTheLoai.Items.Count > 0)
            {
                cbbTheLoai.SelectedIndex = 0;
            }
        }

        private void LoadKhu()
        {
            List<Data.KHU> lsArray = Data.BOKhu.GetAllNoTrackingToList(mTransit);
            lsArray.Insert(0, new Data.KHU() { TenKhu = "Tất cả khu", KhuID = 0 });
            cbbKhu.ItemsSource = lsArray;
            if (cbbKhu.Items.Count > 0)
            {
                cbbKhu.SelectedIndex = 0;
            }
        }

        private void cbbTheLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbTheLoai.SelectedValue != null)
            {
                int TheLoaiID = (int)cbbTheLoai.SelectedValue;
                switch (TheLoaiID)
                {
                    case 1:
                        LoadLoaiLichBieuBatDau(1);
                        LoadLoaiLichBieuKetThuc(1);
                        lbGiaTriBatDau.Text = "Thứ bắt đầu";
                        lbGiaTriKetThuc.Text = "Thứ kết thúc";
                        break;

                    case 2:
                        LoadLoaiLichBieuBatDau(2);
                        LoadLoaiLichBieuKetThuc(2);
                        lbGiaTriBatDau.Text = "Ngày bắt đầu";
                        lbGiaTriKetThuc.Text = "Ngày kết thúc";
                        break;

                    case 3:
                        LoadLoaiLichBieuBatDau(2);
                        LoadLoaiLichBieuKetThuc(3);
                        lbGiaTriBatDau.Text = "Ngày";
                        lbGiaTriKetThuc.Text = "Tháng";
                        break;

                    default:
                        break;
                }
            }
        }
    }
}