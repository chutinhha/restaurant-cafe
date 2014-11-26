using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowBanHang.xaml
    /// </summary>
    public partial class WindowBanHang : Window
    {
        private ControlLibrary.POSButtonTable mPOSButtonTable;
        private bool IsThayDoiSoLuong = true;
        private Data.Transit mTransit = null;
        private ProcessOrder.ProcessOrder mProcessOrder;
        public WindowBanHang(Data.Transit transit,ControlLibrary.POSButtonTable table)
        {
            mTransit = transit;
            mPOSButtonTable = table;
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            uCMenuBanHang.SetTransit(mTransit);
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
            if (btn.CommandParameter==null)
            {
                return;
            }
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
                case Data.EnumChucNang.TamTinh:
                    TamTinh();
                    break;
                case Data.EnumChucNang.ThayDoiGia:
                    break;
                case Data.EnumChucNang.ChuyenBan:
                    break;
                case Data.EnumChucNang.TachBan:
                    break;
                case Data.EnumChucNang.DongBan:
                    DongBan();
                    break;
                case Data.EnumChucNang.ChonGia:
                    ChonGia();
                    break;
                default:
                    break;
            }
        }
        private void ChonGia(){
            if (mProcessOrder.KiemTraHoaDonDaHoanThanh())
            {
                MessageBox.Show("Hóa đơn đã thanh toán, không thể thay đổi", "Lưu ý!");
                return;
            }
            if (lvData.SelectedItems.Count > 0)
            {
                Data.BOChiTietBanHang chitiet = (Data.BOChiTietBanHang)lvData.SelectedItems[0];
                UserControlLibrary.WindowBanHangTheoGia win = new UserControlLibrary.WindowBanHangTheoGia(mTransit, chitiet.MENUKICHTHUOCMON);
                if (win.ShowDialog()==true)
                {
                    chitiet.ChangePriceChiTietBanHang(win._MenuGia.Gia);
                    lvData.Items.Refresh();
                    ReloadData();
                }
            }
        }
        private void DongBan()
        {
            if (mProcessOrder.BanHang.TrangThaiID==3)
            {
                if (mProcessOrder.DongBan()>0)
                {
                    mPOSButtonTable._ButtonTableStatusColor = (ControlLibrary.POSButtonTable.POSButtonTableStatusColor)mProcessOrder.BanHang.TrangThaiID;                                            
                }
            }
            this.Close();
        }
        public void TamTinh()
        {
            if (mProcessOrder.KiemTraDanhSachMon()>0)
            {
                WindowTamTinh win = new WindowTamTinh(mTransit, mProcessOrder.GetBanHang());
                if (win.ShowDialog() == true)
                {
                    if (mProcessOrder.TamTinh()> 0)
                    {
                        mPOSButtonTable._ButtonTableStatusColor = (ControlLibrary.POSButtonTable.POSButtonTableStatusColor)mProcessOrder.BanHang.TrangThaiID;
                        this.Close();
                        
                    }
                }                
            }
            else
            {
                MessageBox.Show("Không thể tính tiền hóa hơn ! Vui lòng chọn món", "Chú ý!");
            }   
        }
        private void GuiNhaBep()
        {
            if (mProcessOrder.KiemTraDanhSachMon()==0)
            {
                MessageBox.Show("Không thể gửi ra nhà bếp ! Vui lòng chọn món","Chú ý!");
                return;
            }
            if (mProcessOrder.SendOrder()>0)
            {
                mPOSButtonTable._ButtonTableStatusColor = (ControlLibrary.POSButtonTable.POSButtonTableStatusColor)mProcessOrder.BanHang.TrangThaiID;
            }
            this.Close();
        }
        private void TinhTien()
        {
            if (mProcessOrder.KiemTraDanhSachMon() > 0)
            {
                WindowTinhTien win = new WindowTinhTien(mTransit,mProcessOrder.GetBanHang());
                if (win.ShowDialog()==true)
                {
                    mProcessOrder.TinhTien();
                    mPOSButtonTable._ButtonTableStatusColor = (ControlLibrary.POSButtonTable.POSButtonTableStatusColor)mProcessOrder.BanHang.TrangThaiID;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể tính tiền hóa hơn ! Vui lòng chọn món", "Chú ý!");
            }            
        }
        private void XoaMon()
        {
            if (mProcessOrder.KiemTraHoaDonDaHoanThanh())
            {
                MessageBox.Show("Hóa đơn đã thanh toán, không thể thay đổi","Lưu ý!");
                return;
            }
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
            XoaTextThongTinMon();
        }

        private void XoaToanBoMon()
        {
            if (mProcessOrder.KiemTraHoaDonDaHoanThanh())
            {
                MessageBox.Show("Hóa đơn đã thanh toán, không thể thay đổi", "Lưu ý!");
                return;
            }
            foreach (Data.BOChiTietBanHang item in lvData.Items)
            {                
                mProcessOrder.XoaChiTietBanHang(item);
            }
            lvData.Items.Clear();
            XoaTextThongTinMon();
        }
        private void XoaTextThongTinMon()
        {
            if (lvData.Items.Count==0)
            {
                txtTenMon.Text = "";
                txtSoLuong.Text = "";
            }
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
            if (mProcessOrder.AddChiTietBanHang(item)==0)
            {
                lvData.Items.Add(item);    
            }
            else
            {
                lvData.Items.Refresh();
            }
            if (lvData.Items.Count > 0)
            {
                lvData.SelectedIndex = lvData.Items.Count - 1;
            }
            ReloadData();
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
                mProcessOrder.CurrentChiTietBanHang.ChangeQtyChiTietBanHang(System.Convert.ToInt32(str));
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
            btnChucNang_0.CommandParameter = Data.EnumChucNang.TinhTien;
            btnChucNang_1.CommandParameter = Data.EnumChucNang.LuuHoaDon;
            btnChucNang_2.CommandParameter = Data.EnumChucNang.TamTinh;
            btnChucNang_5.CommandParameter = Data.EnumChucNang.XoaMon;
            btnChucNang_6.CommandParameter = Data.EnumChucNang.XoaToanBoMon;
            btnChucNang_7.CommandParameter = Data.EnumChucNang.ChonGia;
            btnChucNang_9.CommandParameter = Data.EnumChucNang.DongBan;            
        }

        private void uCMenuBanHang__OnEventMenuKichThuocMon(Data.BOMenuKichThuocMon ob)
        {
            Data.BOChiTietBanHang item = new Data.BOChiTietBanHang(ob, mTransit);
            AddChiTietBanHang(item);            
        }
    }
}