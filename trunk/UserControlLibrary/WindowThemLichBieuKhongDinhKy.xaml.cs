using System;
using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLichBieuKhongDinhKy.xaml
    /// </summary>
    public partial class WindowThemLichBieuKhongDinhKy : Window
    {
        private Data.Transit mTransit;

        public Data.LICHBIEUKHONGDINHKY _Item { get; set; }

        public WindowThemLichBieuKhongDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                    _Item = new Data.LICHBIEUKHONGDINHKY();
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
                ckHoatDong.IsChecked = true;
                dtpNgay.SelectedDate = DateTime.Now;
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);

                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Lịch Biểu Không Định Kỳ";
            }
            else
            {
                txtTenLichBieu.Text = _Item.TenLichBieu;
                cbbLoaiGia.SelectedValue = _Item.LoaiGiaID;
                ckHoatDong.IsChecked = _Item.Visual;
                dtpNgay.SelectedDate = _Item.Ngay;
                timeBatDau.TimeCurent = (TimeSpan)_Item.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)_Item.GioKetThuc;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Lịch Biểu Không Định Kỳ";
            }
        }

        private void GetValues()
        {
            _Item.TenLichBieu = txtTenLichBieu.Text;
            _Item.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            _Item.Ngay = dtpNgay.SelectedDate;
            _Item.GioBatDau = timeBatDau.TimeCurent;
            _Item.GioKetThuc = timeKetThuc.TimeCurent;
            _Item.Visual = ckHoatDong.IsChecked;
            _Item.Visual = true;
            _Item.Deleted = false;
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
            cbbLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAllNoTracking(mTransit);
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
        }
    }
}