using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System;
using System.Linq;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachDinhLuong.xaml
    /// </summary>
    public partial class UCDanhSachDinhLuong : UserControl
    {
        private List<Data.BODinhLuong> lsArray = null;
        private List<Data.BODinhLuong> lsArrayDeleted = null;
        private Data.BOMenuKichThuocMon mKichThuocMon = null;
        private Data.Transit mTransit = null;
        private Data.BODinhLuong BODinhLuong = new Data.BODinhLuong();
        private List<Data.LOAIBAN> lsLoaiBan = null;
        public UCDanhSachDinhLuong()
        {
            InitializeComponent();
        }

        public void Init(Data.BOMenuKichThuocMon kichThuocMon, Data.Transit transit)
        {
            mTransit = transit;
            BODinhLuong = new Data.BODinhLuong(transit);
            mKichThuocMon = kichThuocMon;
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
            if (mKichThuocMon != null)
            {
                btnLuu.Visibility = System.Windows.Visibility.Visible;
                btnThemMon.Visibility = System.Windows.Visibility.Visible;
                btnDanhSach.Visibility = System.Windows.Visibility.Visible;
            }
            PhanQuyen();
        }

        Data.BOChiTietQuyen mPhanQuyen = null;

        private bool IsSua = true, IsXoa = true;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.KhuyenMai);
            if (!mPhanQuyen.ChiTietQuyen.ChoPhep)
                btnDanhSach.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them)
                btnThemMon.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Sua)
                IsSua = false;
            if (!mPhanQuyen.ChiTietQuyen.Xoa)
                IsXoa = false;
            if (!mPhanQuyen.ChiTietQuyen.Them && !mPhanQuyen.ChiTietQuyen.Xoa && !mPhanQuyen.ChiTietQuyen.Sua)
                btnLuu.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void LoadDanhSach()
        {
            txtTenMon.Text = mKichThuocMon.MenuMon.TenDai + " (" + mKichThuocMon.MenuKichThuocMon.TenLoaiBan + ")";
            lsArray = BODinhLuong.GetAll((int)mKichThuocMon.MenuKichThuocMon.KichThuocMonID, mTransit).ToList();
            foreach (Data.BODinhLuong item in lsArray)
            {
                item.DinhLuong.KichThuocBan = (int)item.DinhLuong.KichThuocBan / item.LoaiBan.KichThuocBan;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == mKichThuocMon.MenuMon.DonViID).ToList();
                item.IsSua = IsSua;
                item.IsXoa = IsXoa ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            lvData.ItemsSource = lsArray;
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            btnLuu.Focus();
            List<Data.BODinhLuong> ls = new List<Data.BODinhLuong>();
            foreach (Data.BODinhLuong s in lvData.Items)
            {
                s.DinhLuong.KichThuocBan = s.DinhLuong.KichThuocBan * s.LoaiBan.KichThuocBan;
                ls.Add(s);
            }
            BODinhLuong.Luu(ls, lsArrayDeleted, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, true, true, false, true);
            if (win.ShowDialog() == true)
            {
                Data.BODinhLuong item = new Data.BODinhLuong();
                item.MenuMon = win._ItemMon.MenuMon;
                item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemMon.MenuMon.DonViID).ToList();
                item.DinhLuong.MonID = item.MenuMon.MonID;
                item.DinhLuong.Visual = true;
                item.DinhLuong.Deleted = false;
                item.DinhLuong.SoLuong = 0;
                item.IsSua = true;
                item.IsXoa = System.Windows.Visibility.Visible;
                item.DinhLuong.KichThuocMonChinhID = mKichThuocMon.MenuKichThuocMon.KichThuocMonID;
                if (item.ListLoaiBan.Count > 0)
                    item.DinhLuong.LoaiBanID = item.ListLoaiBan[0].LoaiBanID;
                lsArray.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BODinhLuong item = ((Button)sender).DataContext as Data.BODinhLuong;
            if (item.DinhLuong.ID > 0)
            {
                if (lsArrayDeleted == null)
                    lsArrayDeleted = new List<Data.BODinhLuong>();
                lsArrayDeleted.Add(item);
            }
            lsArray.Remove(item);
            lvData.Items.Refresh();
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.BODinhLuong item = ((ComboBox)sender).DataContext as Data.BODinhLuong;
            if (item != null)
            {
                switch (item.DinhLuong.LoaiBanID)
                {
                    case (int)Data.EnumLoaiBan.Cai:
                    case (int)Data.EnumLoaiBan.DinhLuong:
                    case (int)Data.EnumLoaiBan.Gram:
                    case (int)Data.EnumLoaiBan.Millilit:
                    case (int)Data.EnumLoaiBan.Kg:
                    case (int)Data.EnumLoaiBan.Lit:
                    case (int)Data.EnumLoaiBan.Gio:
                    case (int)Data.EnumLoaiBan.Phut:
                    case (int)Data.EnumLoaiBan.Giay:
                        item.DinhLuong.Edit = true;
                        if (item.DinhLuong.ID == 0)
                            item.DinhLuong.KichThuocBan = 1;
                        else if (item.DinhLuong.LoaiBanID != item.LoaiBan.LoaiBanID)
                            item.DinhLuong.KichThuocBan = 1;
                        break;
                    default:
                        break;
                }
                item.LoaiBan = item.ListLoaiBan.Where(o => o.LoaiBanID == item.DinhLuong.LoaiBanID).FirstOrDefault();
                lvData.Items.Refresh();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }
    }
}