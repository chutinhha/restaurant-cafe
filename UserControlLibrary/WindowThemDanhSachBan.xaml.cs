using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemDanhSachBan.xaml
    /// </summary>
    public partial class WindowThemDanhSachBan : Window
    {
        private Data.Transit mTransit;

        public Data.BOMenuKichThuocMon _Item { get; set; }

        public Data.BOMenuMon mMon { get; set; }

        public WindowThemDanhSachBan(Data.BOMenuMon mon, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mMon = mon;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiBan();
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
                    _Item = new Data.BOMenuKichThuocMon();
                    _Item.MenuKichThuocMon.Visual = true;
                    _Item.MenuKichThuocMon.Deleted = false;
                    _Item.MenuKichThuocMon.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            txtTenMon.Text = mMon.MenuMon.TenDai;
            if (_Item != null)
            {
                cbbLoaiBan.SelectedValue = _Item.MenuKichThuocMon.LoaiBanID;
                cbbLoaiBan_SelectionChanged(null, null);
                txtTenLoaiBan.Text = _Item.MenuKichThuocMon.TenLoaiBan;
                txtGiaMacDinh.Text = _Item.MenuKichThuocMon.GiaBanMacDinh.ToString();
                txtTonKhoToiDa.Text = _Item.MenuKichThuocMon.TonKhoToiDa.ToString();
                txtTonKhoToiThieu.Text = _Item.MenuKichThuocMon.TonKhoToiThieu.ToString();
                txtSoLuongBan.Text = _Item.MenuKichThuocMon.SoLuongBanBan.ToString();
                ckBan.IsChecked = _Item.MenuKichThuocMon.Visual;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Loại Giá";
            }
            else
            {
                if (cbbLoaiBan.Items.Count > 0)
                    cbbLoaiBan.SelectedIndex = 0;
                txtTenLoaiBan.Text = "";
                txtGiaMacDinh.Text = "0";
                txtTonKhoToiDa.Text = "0";
                txtTonKhoToiThieu.Text = "0";
                txtSoLuongBan.Text = "1";
                ckBan.IsChecked = true;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Sửa Loại Giá";
            }
        }

        private void GetValues()
        {
            if (_Item == null)
            {
                _Item = new Data.BOMenuKichThuocMon();
            }
            _Item.MenuKichThuocMon.MonID = mMon.MenuMon.MonID;
            _Item.MenuKichThuocMon.LoaiBanID = (int)cbbLoaiBan.SelectedValue;
            _Item.MenuKichThuocMon.TenLoaiBan = txtTenLoaiBan.Text;
            _Item.MenuKichThuocMon.KichThuocLoaiBan = System.Convert.ToInt32(txtKichThuocLoaiBan.Text);
            _Item.MenuKichThuocMon.GiaBanMacDinh = System.Convert.ToDecimal(txtGiaMacDinh.Text);
            _Item.MenuKichThuocMon.TonKhoToiDa = System.Convert.ToInt32(txtTonKhoToiDa.Text);
            _Item.MenuKichThuocMon.TonKhoToiThieu = System.Convert.ToInt32(txtTonKhoToiThieu.Text);
            _Item.MenuKichThuocMon.SoLuongBanBan = System.Convert.ToInt32(txtSoLuongBan.Text);
            _Item.MenuKichThuocMon.Visual = ckBan.IsChecked;
            _Item.MenuKichThuocMon.ThoiGia = false;
            _Item.MenuKichThuocMon.Deleted = false;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenLoaiBan.Text == "")
            {
                lbStatus.Text = "Tên loại bán không được bỏ trống";
                return false;
            }
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

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbLoaiBan.SelectedValue != null)
            {
                Data.LOAIBAN lb = (Data.LOAIBAN)cbbLoaiBan.SelectedItem;
                switch (lb.LoaiBanID)
                {
                    case 1:
                        txtKichThuocLoaiBan.IsEnabled = false;
                        txtKichThuocLoaiBan.Text = "1";
                        break;

                    case 2:
                    case 3:
                        txtKichThuocLoaiBan.IsEnabled = true;
                        if (_Item == null)
                            txtKichThuocLoaiBan.Text = lb.KichThuocBan.ToString();
                        else if (_Item.MenuKichThuocMon.LoaiBanID == lb.LoaiBanID)
                            txtKichThuocLoaiBan.Text = _Item.MenuKichThuocMon.KichThuocLoaiBan.ToString();
                        else
                            txtKichThuocLoaiBan.Text = lb.KichThuocBan.ToString();
                        break;
                }
            }
        }

        private void LoadLoaiBan()
        {
            cbbLoaiBan.ItemsSource = Data.BOLoaiBan.GetAllNoTracking(mTransit);
        }
    }
}