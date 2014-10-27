using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowDanhSachBan.xaml
    /// </summary>
    public partial class WindowDanhSachBan : Window
    {
        private Data.MENUKICHTHUOCMON _MenuKichThuoMon = null;

        private bool LoadAllLoaiBan = false;

        public WindowDanhSachBan(Data.MENUMON mon)
        {
            InitializeComponent();
            _Mon = mon;
        }

        public Data.MENUMON _Mon { get; set; }

        private void btnLoaiBanMoi_Click(object sender, RoutedEventArgs e)
        {
            _MenuKichThuoMon = null;
            SetDataKichThuocMon();
        }

        private Data.MENUGIA _MenuGia = null;

        private void btnThemGiaBan_Click(object sender, RoutedEventArgs e)
        {
            if (cbbLoaiGiaBan.Items.Count > 0)
            {
                _MenuGia = Data.BOMenuGia.GetByID((int)cbbLoaiGiaBan.SelectedValue, _MenuKichThuoMon.KichThuocMonID);
                if (txtGiaBan.Text == "")
                {
                    txtGiaBan.Text = "0";
                }
                if (_MenuGia == null)
                {
                    _MenuGia = new Data.MENUGIA();
                    _MenuGia.Gia = System.Convert.ToDecimal(txtGiaBan.Text);
                    _MenuGia.KichThuocMonID = _MenuKichThuoMon.KichThuocMonID;
                    _MenuGia.LoaiGiaID = (int)cbbLoaiGiaBan.SelectedValue;
                    Data.BOMenuGia.Them(_MenuGia);
                }
                else
                {
                    _MenuGia.Gia = System.Convert.ToDecimal(txtGiaBan.Text);
                    Data.BOMenuGia.CapNhat(_MenuGia);
                }
            }
            LoadDanhSachGiaBan();
            _MenuGia = null;

        }

        private void btnThemLoaiBan_Click(object sender, RoutedEventArgs e)
        {
            btnThemLoaiBan.IsEnabled = false;
            if (_MenuKichThuoMon == null)
            {
                _MenuKichThuoMon = new Data.MENUKICHTHUOCMON();
                GetDataKichThuocMon();
                Data.BOMenuKichThuocMon.Them(_MenuKichThuoMon);
            }
            else
            {
                GetDataKichThuocMon();
                Data.BOMenuKichThuocMon.CapNhat(_MenuKichThuoMon);
            }
            LoadKichThuocMonBan();
            LoadDanhSachLoaiBan();
            btnThemLoaiBan.IsEnabled = true;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoaGiaBan_Click(object sender, RoutedEventArgs e)
        {
            if (lvLoaiGiaBan.SelectedItems.Count > 0)
            {
                _MenuGia = (Data.MENUGIA)((ListViewItem)lvLoaiGiaBan.SelectedItems[0]).Tag;
                Data.BOMenuGia.Xoa(_MenuGia.GiaID);
                LoadDanhSachGiaBan();
                cbbLoaiGiaBan.SelectedIndex = 0;
                _MenuGia = null;

            }
        }

        private void btnXoaLoaiBan_Click(object sender, RoutedEventArgs e)
        {
            if (lvLoaiBan.SelectedItems.Count > 0)
            {
                _MenuKichThuoMon = (Data.MENUKICHTHUOCMON)((ListViewItem)lvLoaiBan.SelectedItems[0]).Tag;
                Data.BOMenuKichThuocMon.Xoa(_MenuKichThuoMon.KichThuocMonID);
                LoadKichThuocMonBan();
                LoadDanhSachLoaiBan();
            }
        }

        private void GetDataKichThuocMon()
        {
            _MenuKichThuoMon.MonID = _Mon.MonID;
            _MenuKichThuoMon.LoaiBanID = (int)cbbLoaiBan.SelectedValue;
            _MenuKichThuoMon.TonKhoToiDa = System.Convert.ToInt32(txtSoLuongTonKhoToiDa.Text);
            _MenuKichThuoMon.TonKhoToiThieu = System.Convert.ToInt32(txtSoLuongTonKhoToiThieu.Text);
            _MenuKichThuoMon.KichThuocBan = System.Convert.ToInt32(txtSoLuongBan.Text);
            _MenuKichThuoMon.KichThuocMon = 0;
            _MenuKichThuoMon.Deleted = false;
            _MenuKichThuoMon.ThoiGia = false;
            _MenuKichThuoMon.Visual = true;
        }

        private int[] GetIDsLoaiBan()
        {
            if (lvLoaiBan.Items.Count > 0)
            {
                int[] IDs = new int[lvLoaiBan.Items.Count];
                int i = 0;
                foreach (ListViewItem li in lvLoaiBan.Items)
                {
                    Data.MENUKICHTHUOCMON ktm = (Data.MENUKICHTHUOCMON)li.Tag;
                    IDs[i++] = (int)ktm.LoaiBanID;
                }
                return IDs;
            }
            else
                return null;
        }

        private bool KiemTraGiaTriLoaiBan()
        {
            lbStatusLoaiBan.Text = "";
            if (txtSoLuongBan.Text == "")
            {
                lbStatusLoaiBan.Text = "Số lượng bán không được bỏ trống";
                return false;
            }
            else if (txtSoLuongTonKhoToiDa.Text == "")
            {
                lbStatusLoaiBan.Text = "Số lượng tồn kho tối da không được bỏ trống";
                return false;
            }
            else if (txtSoLuongTonKhoToiThieu.Text == "")
            {
                lbStatusLoaiBan.Text = "Số lượng tồn kho tối thiểu không được bỏ trống";
                return false;
            }
            return true;
        }

        private void LoadDanhSachLoaiBan()
        {
            List<Data.LOAIBAN> lsLoaiBan = null;
            if (LoadAllLoaiBan)
                lsLoaiBan = Data.BOLoaiBan.GetAll(null);
            else
                lsLoaiBan = Data.BOLoaiBan.GetAll(GetIDsLoaiBan());
            cbbLoaiBan.ItemsSource = lsLoaiBan;
            if (lsLoaiBan.Count > 0)
            {
                cbbLoaiBan.SelectedIndex = 0;
                btnThemLoaiBan.IsEnabled = true;
                cbbLoaiBan.IsEnabled = true;
                txtSoLuongBan.IsEnabled = true;
                txtSoLuongTonKhoToiDa.IsEnabled = true;
                txtSoLuongTonKhoToiThieu.IsEnabled = true;
            }
            else
            {
                btnThemLoaiBan.IsEnabled = false;
                cbbLoaiBan.IsEnabled = false;
                txtSoLuongBan.IsEnabled = false;
                txtSoLuongTonKhoToiDa.IsEnabled = false;
                txtSoLuongTonKhoToiThieu.IsEnabled = false;
            }
        }

        private void LoadKichThuocMonBan()
        {
            List<Data.MENUKICHTHUOCMON> lsArray = Data.BOMenuKichThuocMon.GetAllName(_Mon.MonID);
            lvLoaiBan.Items.Clear();
            foreach (Data.MENUKICHTHUOCMON item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvLoaiBan.Items.Add(li);
            }
        }

        private void lvLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLoaiBan.SelectedItems.Count > 0)
            {
                _MenuKichThuoMon = (Data.MENUKICHTHUOCMON)((ListViewItem)lvLoaiBan.SelectedItems[0]).Tag;
                LoadDanhSachLoaiBan();
                SetDataKichThuocMon();
            }
        }

        private void SetDataKichThuocMon()
        {
            if (_MenuKichThuoMon != null)
            {
                LoadAllLoaiBan = true;
                LoadDanhSachLoaiBan();
                LoadAllLoaiBan = false;
                txtSoLuongBan.Text = _MenuKichThuoMon.KichThuocBan.ToString();
                txtSoLuongTonKhoToiDa.Text = _MenuKichThuoMon.TonKhoToiDa.ToString();
                txtSoLuongTonKhoToiThieu.Text = _MenuKichThuoMon.TonKhoToiThieu.ToString();
                cbbLoaiBan.SelectedValue = _MenuKichThuoMon.LoaiBanID;
                btnThemLoaiBan.Content = "Cập nhật loại bán";
                txtLoaiBan.Text = _MenuKichThuoMon.LOAIBAN.TenLoaiBan;
                LoadDanhSachGiaBan();
                _MenuGia = null;
            }
            else
            {

                txtSoLuongBan.Text = "1";
                txtSoLuongTonKhoToiDa.Text = "50";
                txtSoLuongTonKhoToiThieu.Text = "5";
                btnThemLoaiBan.Content = "Thêm loại bán";
                txtLoaiBan.Text = "";
                LoadDanhSachLoaiBan();
                LoadKichThuocMonBan();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTenMon.Text = _Mon.TenDai;
            LoadKichThuocMonBan();
            LoadDanhSachLoaiBan();
            LoadLoaiGia();
        }

        private void LoadDanhSachGiaBan()
        {
            if (_MenuKichThuoMon != null)
            {
                List<Data.MENUGIA> lsArray = Data.BOMenuGia.GetAll(_MenuKichThuoMon.KichThuocMonID);
                lvLoaiGiaBan.Items.Clear();
                foreach (Data.MENUGIA item in lsArray)
                {
                    ListViewItem li = new ListViewItem();
                    li.Content = item;
                    li.Tag = item;
                    lvLoaiGiaBan.Items.Add(li);
                }
            }
        }

        public void LoadLoaiGia()
        {
            cbbLoaiGiaBan.ItemsSource = Data.BOMenuLoaiGia.GetAll();
            if (cbbLoaiGiaBan.Items.Count > 0)
                cbbLoaiGiaBan.SelectedIndex = 0;
        }

        private void lvLoaiGiaBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLoaiGiaBan.SelectedItems.Count > 0)
            {
                _MenuGia = (Data.MENUGIA)((ListViewItem)lvLoaiGiaBan.SelectedItems[0]).Tag;
                cbbLoaiGiaBan.SelectedValue = _MenuGia.LoaiGiaID;
                txtGiaBan.Text = _MenuGia.Gia.ToString();
            }
        }

        private void cbbLoaiGiaBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_MenuKichThuoMon != null)
            {
                _MenuGia = Data.BOMenuGia.GetByID((int)cbbLoaiGiaBan.SelectedValue, _MenuKichThuoMon.KichThuocMonID);
                if (_MenuGia != null)
                    txtGiaBan.Text = _MenuGia.Gia.ToString();
                else
                    txtGiaBan.Text = "0";
            }
        }

    }
}