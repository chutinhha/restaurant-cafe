using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowNhapKho.xaml
    /// </summary>
    public partial class WindowNhapKho : Window
    {
        private Data.KaraokeEntities mKaraokeEntities;        
        private List<Data.LOAIBAN> lsLoaiBan = null;
        private Data.BONhapKho mBONhapKho;
        private Data.Transit mTransit = null;

        public WindowNhapKho(Data.Transit transit)
        {
            InitializeComponent();
            mKaraokeEntities = new Data.KaraokeEntities();
            mBONhapKho = new Data.BONhapKho(mKaraokeEntities);
            mTransit = transit;            
            lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit).ToList();
            LoadDanhSach();
        }        

        public void LoadDanhSach()
        {
            //lsArrayDeleted = null;
            //if (_Item != null)
            //{
            //    lvData.ItemsSource = lsArray = BOChiTietNhapKho.GetAll((int)_Item.NhapKho.NhapKhoID, mTransit).ToList();
            //    lvData.Items.Refresh();
            //}
            //else
            //{
            //    lsArray = new List<Data.BOChiTietNhapKho>();
            //    lvData.ItemsSource = lsArray;
            //    lvData.Items.Refresh();
            //}
            lvData.ItemsSource = mBONhapKho.ListChiTietNhapKho;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (mBONhapKho.ListChiTietNhapKho.Count>0)
            {
                if (CheckValues())
                {                    
                    mBONhapKho.NhapKho.NhanVienID = mTransit.NhanVien.NhanVienID;
                    GetValues();
                    mBONhapKho.LuuNhapKho();
                    UserControlLibrary.WindowMessageBox.ShowDialog(lbTieuDe.Text + " thành công");
                    DialogResult = true;
                }
            }
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, false, true, false, true);
            if (win.ShowDialog() == true)
            {
                Data.BOChiTietNhapKho item = new Data.BOChiTietNhapKho();                
                item.MenuKichThuocMon = win._ItemKichThuocMon;                
                item.ChiTietNhapKho.KichThuocMonID = win._ItemKichThuocMon.MenuKichThuocMon.KichThuocMonID;
                mBONhapKho.ListChiTietNhapKho.Add(item);
                lvData.Items.Refresh();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Data.BOChiTietNhapKho item = ((Button)sender).DataContext as Data.BOChiTietNhapKho;
            mBONhapKho.ListChiTietNhapKho.Remove(item);
            lvData.Items.Refresh();
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool CheckValues()
        {
            return true;
        }

        private void GetValues()
        {
            mBONhapKho.NhapKho.KhoID = 0;
            mBONhapKho.NhapKho.NhaCungCapID = 0;
            mBONhapKho.NhapKho.ThoiGian = DateTime.Now;
            if (cbbKhoHang.Items.Count > 0)
            {
                mBONhapKho.NhapKho.KhoID = (int)cbbKhoHang.SelectedValue;
                mBONhapKho.Kho = (Data.KHO)cbbKhoHang.SelectedItem;
            }
            if (cbbNhaCungCap.Items.Count > 0)
            {
                mBONhapKho.NhapKho.NhaCungCapID = (int)cbbNhaCungCap.SelectedValue;
                mBONhapKho.NhaCungCap = (Data.NHACUNGCAP)cbbNhaCungCap.SelectedItem;
            }
        }

        private void LoadKhoHang()
        {
            cbbKhoHang.ItemsSource = Data.BOKho.GetAllNoTracking(mTransit);
            if (cbbKhoHang.Items.Count > 0)
                cbbKhoHang.SelectedIndex = 0;
        }

        private void LoadNhaCungCap()
        {
            cbbNhaCungCap.ItemsSource = Data.BONhaCungCap.GetAllNoTracking(mTransit);
            if (cbbNhaCungCap.Items.Count > 0)
                cbbNhaCungCap.SelectedIndex = 0;
        }

        //private void SetValues()
        //{
        //    if (_Item == null)
        //    {
        //        if (cbbKhoHang.Items.Count > 0)
        //            cbbKhoHang.SelectedIndex = 0;
        //        if (cbbNhaCungCap.Items.Count > 0)
        //            cbbNhaCungCap.SelectedIndex = 0;
        //        btnLuu.Content = mTransit.StringButton.Them;
        //        lbTieuDe.Text = "Thêm nhập kho";
        //    }
        //    else
        //    {
        //        cbbNhaCungCap.SelectedValue = _Item.NhapKho.NhaCungCapID;
        //        cbbKhoHang.SelectedValue = _Item.NhapKho.KhoID;
        //        btnLuu.Content = mTransit.StringButton.Luu;
        //        lbTieuDe.Text = "Sửa nhập kho";
        //    }
        //}

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
            LoadKhoHang();
            LoadNhaCungCap();
            //SetValues();
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
    }
}