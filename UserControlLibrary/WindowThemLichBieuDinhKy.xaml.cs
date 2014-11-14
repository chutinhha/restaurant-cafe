using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLichBieuDinhKy.xaml
    /// </summary>
    public partial class WindowThemLichBieuDinhKy : Window
    {
        private Data.Transit mTransit;

        public Data.LICHBIEUDINHKY _Item { get; set; }

        public WindowThemLichBieuDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTheLoai();
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
                    _Item = new Data.LICHBIEUDINHKY();
                    _Item.Visual = true;
                    _Item.Deleted = false;
                    _Item.Edit = false;
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
                {
                    cbbLoaiGia.SelectedIndex = 0;
                }
                if (cbbTheLoai.Items.Count > 0)
                {
                    cbbTheLoai.SelectedIndex = 0;
                }
                txtUuTien.Text = "1";
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);

                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Lịch Biểu Định Kỳ";
            }
            else
            {
                txtTenLichBieu.Text = _Item.TenLichBieu;
                cbbLoaiGia.SelectedValue = _Item.LoaiGiaID;
                cbbTheLoai.SelectedValue = _Item.TheLoaiID;
                cbbTheLoai_SelectionChanged(null, null);
                cbbGiaTriBatDau.SelectedValue = _Item.GiaTriBatDau;
                cbbGiaTriKetThuc.SelectedValue = _Item.GiaTriKetThuc;
                txtUuTien.Text = _Item.UuTien.ToString();
                timeBatDau.TimeCurent = (TimeSpan)_Item.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)_Item.GioKetThuc;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Thêm Lịch Biểu Định Kỳ";
            }
        }

        private void GetValues()
        {
            _Item.TenLichBieu = txtTenLichBieu.Text;
            _Item.TheLoaiID = (int)cbbTheLoai.SelectedValue;
            _Item.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            _Item.GiaTriBatDau = (int)cbbGiaTriBatDau.SelectedValue;
            _Item.GiaTriKetThuc = (int)cbbGiaTriKetThuc.SelectedValue;
            _Item.UuTien = System.Convert.ToInt32(txtUuTien.Text);
            _Item.Visual = true;
            _Item.Deleted = false;
            _Item.GioBatDau = timeBatDau.TimeCurent;
            _Item.GioKetThuc = timeKetThuc.TimeCurent;

            switch (_Item.TheLoaiID)
            {
                case 1:
                    if (_Item.GiaTriBatDau == _Item.GiaTriKetThuc)
                    {
                        _Item.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        _Item.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 2:
                    if (_Item.GiaTriBatDau == _Item.GiaTriKetThuc)
                    {
                        _Item.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        _Item.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 3:
                    _Item.TenHienThi = cbbGiaTriBatDau.Text + " " + cbbGiaTriKetThuc.Text;
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