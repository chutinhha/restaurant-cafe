using System.Windows;
using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachBan.xaml
    /// </summary>
    public partial class UCDanhSachBan : UserControl
    {
        private Data.MENUMON mMon = null;
        private Data.MENUKICHTHUOCMON mKichThuocMon = null;
        private Data.Transit mTransit = null;

        public UCDanhSachBan(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnTaoMoi_Click(object sender, RoutedEventArgs e)
        {
            mKichThuocMon = null;
            SetValues();
        }

        private void btnThemMoi_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (mKichThuocMon != null)
                {
                    GetValues();
                    Data.BOMenuKichThuocMon.CapNhat(mKichThuocMon, mTransit);
                    LoadDanhSachKichThuocMon();
                    btnTaoMoi_Click(sender, e);
                    lbStatus.Text = "Thêm thành công";
                }
                else
                {
                    GetValues();
                    Data.BOMenuKichThuocMon.Them(mKichThuocMon, mTransit);
                    btnTaoMoi_Click(null, null);
                    LoadDanhSachKichThuocMon();
                    lbStatus.Text = "Cập nhật thành công";
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (mKichThuocMon != null)
            {
                Data.BOMenuKichThuocMon.Xoa(mKichThuocMon.KichThuocMonID, mTransit);
                btnTaoMoi_Click(null, null);
                LoadDanhSachKichThuocMon();
            }
        }

        private void LoadDanhSachKichThuocMon()
        {
            lvData.ItemsSource = Data.BOMenuKichThuocMon.GetAllName(mMon.MonID, mTransit);
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

        private void GetValues()
        {
            if (mKichThuocMon == null)
            {
                mKichThuocMon = new Data.MENUKICHTHUOCMON();
            }
            mKichThuocMon.MonID = mMon.MonID;
            mKichThuocMon.LoaiBanID = (int)cbbLoaiBan.SelectedValue;
            mKichThuocMon.TenLoaiBan = txtTenLoaiBan.Text;
            mKichThuocMon.KichThuocLoaiBan = System.Convert.ToInt32(txtKichThuocLoaiBan.Text);
            mKichThuocMon.GiaBanMacDinh = System.Convert.ToDecimal(txtGiaMacDinh.Text);
            mKichThuocMon.TonKhoToiDa = System.Convert.ToInt32(txtTonKhoToiDa.Text);
            mKichThuocMon.TonKhoToiThieu = System.Convert.ToInt32(txtTonKhoToiThieu.Text);
            mKichThuocMon.SoLuongBanBan = System.Convert.ToInt32(txtSoLuongBan.Text);
            mKichThuocMon.Visual = ckBan.IsChecked;
            mKichThuocMon.ThoiGia = false;
            mKichThuocMon.Deleted = false;
        }

        private void SetValues()
        {
            if (mKichThuocMon != null)
            {
                cbbLoaiBan.SelectedValue = mKichThuocMon.LoaiBanID;
                txtTenLoaiBan.Text = mKichThuocMon.TenLoaiBan;
                txtGiaMacDinh.Text = mKichThuocMon.GiaBanMacDinh.ToString();
                txtTonKhoToiDa.Text = mKichThuocMon.TonKhoToiDa.ToString();
                txtTonKhoToiThieu.Text = mKichThuocMon.TonKhoToiThieu.ToString();
                txtSoLuongBan.Text = mKichThuocMon.SoLuongBanBan.ToString();
                ckBan.IsChecked = ckBan.IsChecked;
                btnThem.Content = "Cập nhật";
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
                btnThem.Content = "Thêm mới";
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                mKichThuocMon = (item as Data.MENUKICHTHUOCMON);
                SetValues();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiBan();
            uCMenu.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
            mKichThuocMon = null;
            SetValues();
        }

        private void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUMON)
            {
                mMon = (Data.MENUMON)ob;
                txtTenMon.Text = mMon.TenDai;
                LoadDanhSachKichThuocMon();
                btnTaoMoi_Click(null, null);
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
                        if (mKichThuocMon == null)
                            txtKichThuocLoaiBan.Text = lb.KichThuocBan.ToString();
                        else if (mKichThuocMon.LoaiBanID == lb.LoaiBanID)
                            txtKichThuocLoaiBan.Text = mKichThuocMon.KichThuocLoaiBan.ToString();
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