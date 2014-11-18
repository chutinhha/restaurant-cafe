using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowBanHang.xaml
    /// </summary>
    public partial class WindowBanHang : Window
    {
        private bool IsThayDoiSoLuong = true;
        private Data.Transit mTransit = null;
        private ProcessOrder.ProcessOrder mProcessOrder;
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
            uCTile.SetTransit(mTransit);
            mProcessOrder = new ProcessOrder.ProcessOrder(mTransit);
            GanChucNang();
            LoadBanHang();
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
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
                    TinhTien();
                    break;
                case Data.EnumChucNang.LuuHoaDon:
                    GuiNhaBep();
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
            mProcessOrder.SendOrder();
            this.Close();
        }
        private void TinhTien()
        {
            WindowTinhTien win = new WindowTinhTien(mTransit, mProcessOrder.GetBanHang());
            win.ShowDialog(); ;
            //mProcessOrder.TinhTien();
            //this.Close();
        }
        private void XoaMon()
        {
            //Khong duoc xoa het
            if (lvData.SelectedItems.Count > 0)
            {
                Data.BOChiTietBanHang chitiet = (Data.BOChiTietBanHang)lvData.SelectedItems[0];
                mProcessOrder.XoaChiTietBanHang(chitiet);
                lvData.Items.Remove(chitiet);
                ReloadData();
                if (lvData.Items.Count > 0)
                {
                    lvData.SelectedIndex = lvData.Items.Count - 1;
                }
            }
        }

        private void XoaToanBoMon()
        {
            lvData.Items.Clear();
        }

        private void LoadBanHang()
        {
            txtMaHoaDon.Text = "HĐ: " + mProcessOrder.BanHang.MaHoaDon.ToString();
            txtTenNhanVien.Text = "NV: " + mTransit.NhanVien.TenNhanVien;
            txtTenBan.Text = mTransit.Ban.TenBan;
            ReloadData();
            lvData.Items.Clear();
            foreach (var item in mProcessOrder.ListChiTietBanHang)
            {
                lvData.Items.Add(item);
            }

        }
        private void ReloadData()
        {
            txtTongTien.Text = Utilities.MoneyFormat.ConvertToStringFull(mProcessOrder.GetBanHang().TongTien());
        }
        private void AddChiTietBanHang(Data.BOChiTietBanHang item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            lvData.Items.Add(item);
            mProcessOrder.AddChiTietBanHang(item);
            if (lvData.Items.Count > 0)
            {
                lvData.SelectedIndex = lvData.Items.Count - 1;
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mProcessOrder.CurrentChiTietBanHang = (Data.BOChiTietBanHang)lvData.SelectedItems[0];
                ThayDoiQty();
            }
        }

        private void ThayDoiQty()
        {
            if (mProcessOrder.CurrentChiTietBanHang != null)
            {
                IsThayDoiSoLuong = false;
                txtSoLuong.Text = mProcessOrder.CurrentChiTietBanHang.CHITIETBANHANG.SoLuongBan.ToString();
                txtTenMon.Text = mProcessOrder.CurrentChiTietBanHang.TenMon.ToString();
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
            if (mProcessOrder.CurrentChiTietBanHang != null && IsThayDoiSoLuong)
            {
                string str = "1";
                if (txtSoLuong.Text != "")
                {
                    str = txtSoLuong.Text;
                }
                mProcessOrder.CurrentChiTietBanHang.ChangeQty(System.Convert.ToInt32(str));
                ReloadData();
                if (lvData.SelectedItems.Count > 0)
                {
                    lvData.SelectedItems[0] = mProcessOrder.CurrentChiTietBanHang;
                    lvData.Items.Refresh();
                }
            }
        }

        private void GanChucNang()
        {
            btnChucNang_5.CommandParameter = Data.EnumChucNang.XoaMon;
            btnChucNang_6.CommandParameter = Data.EnumChucNang.XoaToanBoMon;
            btnChucNang_0.CommandParameter = Data.EnumChucNang.TinhTien;
            btnChucNang_1.CommandParameter = Data.EnumChucNang.LuuHoaDon;
        }

        private void uCMenuBanHang__OnEventMenuKichThuocMon(Data.BOMenuKichThuocMon ob)
        {
            Data.BOChiTietBanHang item = new Data.BOChiTietBanHang(ob, mTransit);
            AddChiTietBanHang(item);
            ReloadData();
        }
    }
}