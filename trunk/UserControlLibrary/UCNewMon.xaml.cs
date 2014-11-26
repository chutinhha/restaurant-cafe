using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.Generic;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewMon.xaml
    /// </summary>
    public partial class UCNewMon : UserControl
    {
        private BitmapImage mBitmapImage = null;
        private Data.Transit mTransit = null;
        private Data.MENUNHOM mMenuNhom = null;
        public Data.BOMenuMon BOMenuMon = null;
        public Data.BOMenuItemMayIn BOMenuItemMayIn = null;

        public UCNewMon(Data.MENUNHOM menuNhom, Data.Transit transit, Data.BOMenuMon bOMenuMon)
        {
            InitializeComponent();
            mMenuNhom = menuNhom;
            mTransit = transit;
            BOMenuMon = bOMenuMon;
            BOMenuItemMayIn = new Data.BOMenuItemMayIn();
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            if (!mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachBan).ChiTietQuyen.ChoPhep)
                btnDanhSachBan.Visibility = System.Windows.Visibility.Collapsed;
            if (!mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.MayIn.CaiDatThucDonMayIn).ChiTietQuyen.ChoPhep)
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Collapsed;
        }


        public Data.BOMenuMon _Mon { get; set; }

        public void CapNhat()
        {
            if (_Mon != null)
            {
                GetValues();
                BOMenuMon.Sua(_Mon, mTransit);
            }
            else
            {
                GetValues();
                ThemMayIn();
                ThemDanhSachBan();
                BOMenuMon.Them(_Mon, mTransit);
            }
        }

        private void ThemMayIn()
        {
            IQueryable<Data.MAYIN> lsMayIn = Data.BOMayIn.GetAllNoTracking(mTransit, false);
            if (lsMayIn.Count() > 0)
                _Mon.MenuMon.MENUITEMMAYINs.Add(new Data.MENUITEMMAYIN() { MayInID = lsMayIn.FirstOrDefault().MayInID });
        }

        private void ThemDanhSachBan()
        {
            IQueryable<Data.LOAIBAN> lsLoaiBan = Data.BOLoaiBan.GetAllNoTracking(mTransit, (int)_Mon.MenuMon.DonViID);
            if (lsLoaiBan.Count() > 0)
            {
                _Mon.MenuMon.SapXepKichThuocMon = 2;
                _Mon.MenuMon.SoLuongKichThuocMon = 1;
                Data.LOAIBAN item = lsLoaiBan.FirstOrDefault();
                _Mon.MenuMon.MENUKICHTHUOCMONs.Add(new Data.MENUKICHTHUOCMON() { TenLoaiBan = "", LoaiBanID = item.LoaiBanID, DonViID = item.DonViID, GiaBanMacDinh = 0, ChoPhepTonKho = true, ThoiGia = false, KichThuocLoaiBan = item.KichThuocBan, SoLuongBanBan = 1, SapXep = 1, Visual = true, Edit = false, Deleted = false });
            }
        }

        public void Xoa()
        {
            BOMenuMon.Xoa(_Mon, mTransit);
        }

        private void btnCaiDatMayIn_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuSetMayIn win = new WindowMenuSetMayIn(_Mon, mTransit);
            win.ShowDialog();
        }

        private void btnDanhSachBan_Click(object sender, RoutedEventArgs e)
        {
            WindowDanhSachBan win = new WindowDanhSachBan(_Mon, mTransit);
            win.ShowDialog();
        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            mBitmapImage = btnHinhAnh.ImageBitmap;
        }

        private void LoadDonVi()
        {
            cbbKieuBan.ItemsSource = mTransit.ListDonVi;
        }

        private void GetValues()
        {
            if (_Mon == null)
            {
                _Mon = new Data.BOMenuMon();
                _Mon.MenuMon.Deleted = false;
                _Mon.MenuMon.NhomID = mMenuNhom.NhomID;
            }
            _Mon.MenuMon.TenDai = txtTenDai.Text;
            _Mon.MenuMon.TenNgan = txtTenNgan.Text;
            _Mon.MenuMon.DonViID = (int)cbbKieuBan.SelectedValue;
            if (mBitmapImage != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(mBitmapImage, 120, 90, 0);
                _Mon.MenuMon.Hinh = Utilities.ImageHandler.ImageToByte(img);
            }
            if (txtSapXep.Text == "")
                _Mon.MenuMon.SapXep = 0;
            else
                _Mon.MenuMon.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
            if (txtTonKhoToiDa.Text == "")
                _Mon.MenuMon.SapXep = 0;
            else
                _Mon.MenuMon.TonKhoToiDa = Convert.ToInt32(txtTonKhoToiDa.Text.Trim());
            if (txtTonKhoToiThieu.Text == "")
                _Mon.MenuMon.SapXep = 0;
            else
                _Mon.MenuMon.TonKhoToiThieu = Convert.ToInt32(txtTonKhoToiThieu.Text.Trim());
            _Mon.MenuMon.Visual = (bool)ckBan.IsChecked;
        }

        private void SetValues()
        {
            if (_Mon != null)
            {
                txtTenDai.Text = _Mon.MenuMon.TenDai;
                txtTenNgan.Text = _Mon.MenuMon.TenNgan;
                txtSapXep.Text = _Mon.MenuMon.SapXep.ToString();
                txtTonKhoToiDa.Text = _Mon.MenuMon.TonKhoToiDa.ToString();
                txtTonKhoToiThieu.Text = _Mon.MenuMon.TonKhoToiThieu.ToString();
                cbbKieuBan.SelectedValue = _Mon.MenuMon.DonViID;
                ckBan.IsChecked = _Mon.MenuMon.Visual;
                if (_Mon.MenuMon.Hinh != null && _Mon.MenuMon.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Mon.MenuMon.Hinh);
                }
                if (mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.MayIn.CaiDatThucDonMayIn).ChiTietQuyen.ChoPhep)
                    btnCaiDatMayIn.Visibility = System.Windows.Visibility.Visible;
                if (mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachBan).ChiTietQuyen.ChoPhep)
                    btnDanhSachBan.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtTonKhoToiDa.Text = "0";
                txtTonKhoToiThieu.Text = "0";
                if (cbbKieuBan.Items.Count > 0)
                    cbbKieuBan.SelectedIndex = 0;
                txtSapXep.Text = mMenuNhom.SapXepMon.ToString();
                ckBan.IsChecked = true;
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Collapsed;
                btnDanhSachBan.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDonVi();
            SetValues();
        }
        
        private void txtTenNgan_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTenDai.Text == "")
                txtTenDai.Text = txtTenNgan.Text;
        }        
    }
}