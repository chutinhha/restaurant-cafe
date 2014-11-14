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
        private Data.BOChiTietBanHang mChiTietBanHang = null;
        public WindowBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //uCMenuBanHang.OnEventMenu += new UserControlLibrary.UCMenu.EventMenu(uCMenuBanHang_OnEventMenu);
            uCMenuBanHang.Init(mTransit);
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            uCTile.TenChucNang = "Bán hàng";
            mTransit.BanHang = new Data.BOBanHang(mTransit);
            GanChucNang();
            LoadBanHang(mTransit.BanHang);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void uCMenuBanHang_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                Data.MENUKICHTHUOCMON ktm = (Data.MENUKICHTHUOCMON)ob;
                Data.BOChiTietBanHang item = new Data.BOChiTietBanHang(ktm, mTransit);
                AddChiTietBanHang(item);
            }
        }


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
                    GuiNhaBep();
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
        private void GuiNhaBep()
        {
            mTransit.BanHang.GuiNhaBep();
            this.Close();
        }
        private void XoaMon()
        {
            if (lvData.SelectedItems.Count > 0)
            {
                Data.BOChiTietBanHang chitiet = (Data.BOChiTietBanHang)lvData.SelectedItems[0];
                chitiet.XoaMon = true;
                lvData.Items.Remove(chitiet);
            }
        }

        private void XoaToanBoMon()
        {
            lvData.Items.Clear();
        }


        private void LoadBanHang(Data.BOBanHang banHang)
        {
            txtMaHoaDon.Text = "Mã Hóa Đơn: " + banHang.BANHANG.MaHoaDon.ToString();
            txtTenNhanVien.Text = "Nhân Viên: " + mTransit.NhanVien.TenNhanVien;
            lvData.Items.Clear();
            foreach (var item in mTransit.BanHang._ListChiTietBanHang)
            {
                lvData.Items.Add(item);
            }
        }

        private void AddChiTietBanHang(Data.BOChiTietBanHang item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            lvData.Items.Add(item);
            mTransit.BanHang.AddChiTietBanHang(item);
            if (lvData.Items.Count > 0)
            {
                lvData.SelectedIndex = lvData.Items.Count - 1;
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mChiTietBanHang = (Data.BOChiTietBanHang)lvData.SelectedItems[0];
                ThayDoiQty();
            }
        }

        private bool IsThayDoiSoLuong = true;
        private void ThayDoiQty()
        {
            if (mChiTietBanHang != null)
            {
                IsThayDoiSoLuong = false;
                txtSoLuong.Text = mChiTietBanHang.CHITIETBANHANG.SoLuongBan.ToString();
                txtTenMon.Text = mChiTietBanHang.TenMon.ToString();
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
            if (mChiTietBanHang != null && IsThayDoiSoLuong)
            {
                string str = "1";
                if (txtSoLuong.Text != "")
                {
                    str = txtSoLuong.Text;
                }
                mChiTietBanHang.CHITIETBANHANG.SoLuongBan = System.Convert.ToInt32(str);
                mChiTietBanHang.CHITIETBANHANG.ThanhTien = mChiTietBanHang.CHITIETBANHANG.SoLuongBan * mChiTietBanHang.CHITIETBANHANG.GiaBan;
                if (lvData.SelectedItems.Count > 0)
                {
                    lvData.SelectedItems[0] = mChiTietBanHang;
                    lvData.Items.Refresh();
                }
            }
        }

        private void GanChucNang()
        {
            btnChucNang_5.CommandParameter = Data.EnumChucNang.XoaMon;
            btnChucNang_6.CommandParameter = Data.EnumChucNang.XoaToanBoMon;
            btnChucNang_0.CommandParameter = Data.EnumChucNang.TinhTien;
        }
    }
}