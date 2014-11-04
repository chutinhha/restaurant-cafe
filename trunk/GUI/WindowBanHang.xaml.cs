using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowBanHang.xaml
    /// </summary>
    public partial class WindowBanHang : Window
    {
        private Data.Transit mTransit = null;

        public WindowBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenuBanHang.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(uCMenuBanHang_OnEventMenu);
            uCMenuBanHang.Init(mTransit);
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            uCTile.TenChucNang = "Bán hàng";
            GanChucNang();
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void uCMenuBanHang_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                Data.POSChiTietBanHang item = new Data.POSChiTietBanHang();
                Data.MENUKICHTHUOCMON ktm = (Data.MENUKICHTHUOCMON)ob;
                item.MENUKICHTHUOCMON = new Data.MENUKICHTHUOCMON();
                item.MENUKICHTHUOCMON.GiaBanMacDinh = ktm.GiaBanMacDinh;
                item.MENUKICHTHUOCMON.SoLuongBanBan = ktm.SoLuongBanBan;
                item.SoLuongBan = ktm.SoLuongBanBan;
                item.GiaBan = ktm.GiaBanMacDinh;
                item.MENUKICHTHUOCMON.TenLoaiBan = ktm.TenLoaiBan;
                item.MENUKICHTHUOCMON.MENUMON = new Data.MENUMON();
                item.MENUKICHTHUOCMON.MENUMON.TenDai = ktm.MENUMON.TenDai;
                AddChiTietBanHang(item);
            }
        }

        private Data.CHITIETBANHANG mChiTietBanHang = null;

        private void btnChucNang_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch ((Data.EnumChucNang)btn.CommandParameter)
            {
                case Data.EnumChucNang.XoaMon:
                    XoaMon();
                    break;
                case Data.EnumChucNang.XoaToanBoMon:
                    XoaToanBoMon();
                    break;
                case Data.EnumChucNang.TinhTien:
                    break;
                case Data.EnumChucNang.LuuHoaDon:
                    break;
                case Data.EnumChucNang.ThayDoiGia:
                    break;
                case Data.EnumChucNang.ChuyenBan:
                    break;
                case Data.EnumChucNang.TachBan:
                    break;
                default:
                    break;
            }
        }

        private void XoaMon()
        {
            if (lvData.SelectedItems.Count > 0)
            {
                lvData.Items.Remove(lvData.SelectedItems[0]);
            }
        }

        private void XoaToanBoMon()
        {
            lvData.Items.Clear();
        }

        private Data.POSChiTietBanHang mPOSChiTietBanHang = null;

        private void AddChiTietBanHang(Data.POSChiTietBanHang item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            lvData.Items.Add(item);
            if (lvData.Items.Count > 0)
            {
                lvData.SelectedIndex = lvData.Items.Count - 1;
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mPOSChiTietBanHang = (Data.POSChiTietBanHang)lvData.SelectedItems[0];
                ThayDoiQty();
            }
        }

        private bool IsThayDoiSoLuong = true;
        private void ThayDoiQty()
        {
            if (mPOSChiTietBanHang != null)
            {
                IsThayDoiSoLuong = false;
                txtSoLuong.Text = mPOSChiTietBanHang.SoLuongBan.ToString();
                txtTenMon.Text = mPOSChiTietBanHang.TenMon.ToString();
                txtSoLuong.Focus();
                TextBox_PreviewMouseDown(txtSoLuong, null);
                IsThayDoiSoLuong = true;
            }
        }

        private void TextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
            uCKeyPad._TextBox = txt;
        }

        private void txtSoLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mPOSChiTietBanHang != null && IsThayDoiSoLuong)
            {
                string str = "1";
                if (txtSoLuong.Text != "")
                {
                    str = txtSoLuong.Text;
                }
                mPOSChiTietBanHang.SoLuongBan = System.Convert.ToInt32(str);
                if (lvData.SelectedItems.Count > 0)
                {
                    lvData.SelectedItems[0] = mPOSChiTietBanHang;
                    lvData.Items.Refresh();
                }
            }
        }

        private void GanChucNang()
        {
            btnChucNang_5.CommandParameter = Data.EnumChucNang.XoaMon;
            btnChucNang_6.CommandParameter = Data.EnumChucNang.XoaToanBoMon;
        }
    }
}