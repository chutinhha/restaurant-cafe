using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLichBieuKhongDinhKy.xaml
    /// </summary>
    public partial class WindowThemLichBieuKhongDinhKy : Window
    {
        private Data.Transit mTransit;
        private Data.BOLichBieuKhongDinhKy BOLichBieuKhongDinhKy = null;

        public Data.BOLichBieuKhongDinhKy _Item { get; set; }

        public WindowThemLichBieuKhongDinhKy(Data.Transit transit, Data.BOLichBieuKhongDinhKy bOLichBieuKhongDinhKy)
        {
            InitializeComponent();
            mTransit = transit;
            BOLichBieuKhongDinhKy = bOLichBieuKhongDinhKy;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                    _Item = new Data.BOLichBieuKhongDinhKy();
                    _Item.LichBieuKhongDinhKy.Visual = true;
                    _Item.LichBieuKhongDinhKy.Deleted = false;
                    _Item.LichBieuKhongDinhKy.Edit = false;
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
                ckHoatDong.IsChecked = true;
                dtpNgayBatDau.SelectedDate = DateTime.Now;
                dtpNgayKetThuc.SelectedDate = DateTime.Now;
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);

                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Lịch Biểu Không Định Kỳ";
            }
            else
            {
                txtTenLichBieu.Text = _Item.LichBieuKhongDinhKy.TenLichBieu;
                cbbLoaiGia.SelectedValue = _Item.LichBieuKhongDinhKy.LoaiGiaID;
                ckHoatDong.IsChecked = _Item.LichBieuKhongDinhKy.Visual;
                dtpNgayBatDau.SelectedDate = _Item.LichBieuKhongDinhKy.NgayBatDau;
                dtpNgayKetThuc.SelectedDate = _Item.LichBieuKhongDinhKy.NgayKetThuc;
                timeBatDau.TimeCurent = (TimeSpan)_Item.LichBieuKhongDinhKy.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)_Item.LichBieuKhongDinhKy.GioKetThuc;
                btnLuu.Content = mTransit.StringButton.Luu;
                if (_Item.LichBieuKhongDinhKy.KhuID == null)
                    cbbKhu.SelectedValue = 0;
                else
                    cbbKhu.SelectedValue = _Item.LichBieuKhongDinhKy.KhuID;
                lbTieuDe.Text = "Sửa Lịch Biểu Không Định Kỳ";
            }
        }

        private void GetValues()
        {
            _Item.LichBieuKhongDinhKy.TenLichBieu = txtTenLichBieu.Text;
            _Item.LichBieuKhongDinhKy.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            _Item.LichBieuKhongDinhKy.NgayBatDau = dtpNgayBatDau.SelectedDate;
            _Item.LichBieuKhongDinhKy.NgayKetThuc = dtpNgayKetThuc.SelectedDate;
            _Item.LichBieuKhongDinhKy.GioBatDau = timeBatDau.TimeCurent;
            _Item.LichBieuKhongDinhKy.GioKetThuc = timeKetThuc.TimeCurent;
            _Item.LichBieuKhongDinhKy.Visual = (bool)ckHoatDong.IsChecked;
            _Item.LichBieuKhongDinhKy.Deleted = false;
            if ((int)cbbKhu.SelectedValue == 0)
                _Item.LichBieuKhongDinhKy.KhuID = null;
            else
                _Item.LichBieuKhongDinhKy.KhuID = (int)cbbKhu.SelectedValue;
            _Item.TenKhu = cbbKhu.Text;
            _Item.MenuLoaiGia.Ten = cbbLoaiGia.Text;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenLichBieu.Text == "")
            {
                lbStatus.Text = "Tên lịch biểu không được bỏ trống";
                return false;
            }

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
            cbbLoaiGia.ItemsSource = BOLichBieuKhongDinhKy.GetMenuLoaiGia();
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
        }
        private void LoadKhu()
        {
            List<Data.KHU> lsArray = BOLichBieuKhongDinhKy.GetKhu().ToList();
            lsArray.Insert(0, new Data.KHU() { TenKhu = "Tất cả khu", KhuID = 0 });
            cbbKhu.ItemsSource = lsArray;
            if (cbbKhu.Items.Count > 0)
            {
                cbbKhu.SelectedIndex = 0;
            }
        }
    }
}