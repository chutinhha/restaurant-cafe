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

        public Data.MENUKICHTHUOCMON _Item { get; set; }

        public Data.MENUMON mMon { get; set; }

        public WindowThemDanhSachBan(Data.MENUMON mon, Data.Transit transit)
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
                    _Item = new Data.MENUKICHTHUOCMON();
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
            txtTenMon.Text = mMon.TenDai;
            if (_Item != null)
            {
                cbbLoaiBan.SelectedValue = _Item.LoaiBanID;
                cbbLoaiBan_SelectionChanged(null, null);
                txtTenLoaiBan.Text = _Item.TenLoaiBan;
                txtGiaMacDinh.Text = _Item.GiaBanMacDinh.ToString();
                txtTonKhoToiDa.Text = _Item.TonKhoToiDa.ToString();
                txtTonKhoToiThieu.Text = _Item.TonKhoToiThieu.ToString();
                txtSoLuongBan.Text = _Item.SoLuongBanBan.ToString();
                ckBan.IsChecked = _Item.Visual;
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
                _Item = new Data.MENUKICHTHUOCMON();
            }
            _Item.MonID = mMon.MonID;
            _Item.LoaiBanID = (int)cbbLoaiBan.SelectedValue;
            _Item.TenLoaiBan = txtTenLoaiBan.Text;
            _Item.KichThuocLoaiBan = System.Convert.ToInt32(txtKichThuocLoaiBan.Text);
            _Item.GiaBanMacDinh = System.Convert.ToDecimal(txtGiaMacDinh.Text);
            _Item.TonKhoToiDa = System.Convert.ToInt32(txtTonKhoToiDa.Text);
            _Item.TonKhoToiThieu = System.Convert.ToInt32(txtTonKhoToiThieu.Text);
            _Item.SoLuongBanBan = System.Convert.ToInt32(txtSoLuongBan.Text);
            _Item.Visual = ckBan.IsChecked;
            _Item.ThoiGia = false;
            _Item.Deleted = false;
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
                        else if (_Item.LoaiBanID == lb.LoaiBanID)
                            txtKichThuocLoaiBan.Text = _Item.KichThuocLoaiBan.ToString();
                        else
                            txtKichThuocLoaiBan.Text = lb.KichThuocBan.ToString();
                        break;
                }
            }
        }

        private void LoadLoaiBan()
        {
            cbbLoaiBan.ItemsSource = Data.BOLoaiBan.GetAll(null, mTransit);
        }
    }
}