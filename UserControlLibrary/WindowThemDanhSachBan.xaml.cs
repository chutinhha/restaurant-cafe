using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

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
                txtSoLuongBan.Text = _Item.MenuKichThuocMon.SoLuongBanBan.ToString();
                ckChoPhepTonKho.IsChecked = (bool)_Item.MenuKichThuocMon.ChoPhepTonKho;
                ckBan.IsChecked = _Item.MenuKichThuocMon.Visual;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Danh Sách Bán";
            }
            else
            {
                if (cbbLoaiBan.Items.Count > 0)
                    cbbLoaiBan.SelectedIndex = 0;
                txtTenLoaiBan.Text = "";
                txtGiaMacDinh.Text = "0";
                txtSoLuongBan.Text = "1";
                ckBan.IsChecked = true;
                ckChoPhepTonKho.IsChecked = true;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Danh Sách Bán";
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
            Data.LOAIBAN loaiBan = (Data.LOAIBAN)cbbLoaiBan.SelectedItem;
            _Item.LoaiBan = loaiBan;
            _Item.MenuKichThuocMon.DonViID = _Item.LoaiBan.DonViID;
            _Item.MenuKichThuocMon.TenLoaiBan = txtTenLoaiBan.Text;
            _Item.MenuKichThuocMon.KichThuocLoaiBan = System.Convert.ToInt32(txtKichThuocLoaiBan.Text);
            _Item.MenuKichThuocMon.KichThuocLoaiBan = (int)_Item.MenuKichThuocMon.KichThuocLoaiBan * (int)loaiBan.KichThuocBan;
            _Item.MenuKichThuocMon.GiaBanMacDinh = System.Convert.ToDecimal(txtGiaMacDinh.Text);
            _Item.MenuKichThuocMon.SoLuongBanBan = System.Convert.ToInt32(txtSoLuongBan.Text);
            _Item.MenuKichThuocMon.Visual = (bool)ckBan.IsChecked;
            _Item.MenuKichThuocMon.ChoPhepTonKho = (bool)ckChoPhepTonKho.IsChecked;
            _Item.MenuKichThuocMon.ThoiGia = false;
            _Item.MenuKichThuocMon.Deleted = false;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenLoaiBan.Text == "")
            {
                lbStatus.Text = "Tên đơn vị không được bỏ trống";
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
                    case (int)Data.EnumLoaiBan.Cai:
                    case (int)Data.EnumLoaiBan.DinhLuong:
                        txtKichThuocLoaiBan.Text = "1";
                        break;
                    case (int)Data.EnumLoaiBan.Gram:
                    case (int)Data.EnumLoaiBan.Millilit:
                    case (int)Data.EnumLoaiBan.Kg:
                    case (int)Data.EnumLoaiBan.Lit:
                    case (int)Data.EnumLoaiBan.Gio:
                    case (int)Data.EnumLoaiBan.Phut:
                    case (int)Data.EnumLoaiBan.Giay:
                        if (_Item == null)
                            txtKichThuocLoaiBan.Text = "1";
                        else if (_Item.MenuKichThuocMon.LoaiBanID == lb.LoaiBanID)
                            txtKichThuocLoaiBan.Text = ((int)(_Item.MenuKichThuocMon.KichThuocLoaiBan / lb.KichThuocBan)).ToString();
                        else
                            txtKichThuocLoaiBan.Text = "1";
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadLoaiBan()
        {
            cbbLoaiBan.ItemsSource = Data.BOLoaiBan.GetAllNoTracking(mTransit, (int)mMon.MenuMon.DonViID);
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}