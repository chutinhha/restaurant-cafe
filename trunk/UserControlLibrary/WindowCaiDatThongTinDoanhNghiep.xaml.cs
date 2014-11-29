using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowCaiDatThongTinDoanhNghiep.xaml
    /// </summary>
    public partial class WindowCaiDatThongTinDoanhNghiep : Window
    {
        Data.Transit mTransit = null;
        public WindowCaiDatThongTinDoanhNghiep(Data.Transit transit)
        {
            InitializeComponent();
            BOCaiDatThongTinCongTy = new Data.BOCaiDatThongTinCongTy(transit);
            mTransit = transit;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            _Item.TenCongTy = txtTenDoanhNghiep.Text;
            _Item.TenVietTat = txtTenVietTat.Text;
            _Item.MaSoThue = txtMaSoThue.Text;
            _Item.NguoiDaiDien = txtTenNguoiDaiDien.Text;

            _Item.DienThoaiBan = txtDienThoaiBan.Text;
            _Item.DienThoaiDiDong = txtDienThoaiDiDong.Text;
            _Item.Email = txtEmail.Text;
            _Item.Fax = txtFax.Text;

            _Item.DiaChi = txtDiaChi.Text;

            if (btnHinhDaiDien.ImageBitmap != null)
            {
                _Item.Hinh = Utilities.ImageHandler.ImageToByte(btnHinhDaiDien.ImageBitmap);
            }

            if (btnLogo.ImageBitmap != null)
            {
                _Item.Logo = Utilities.ImageHandler.ImageToByte(btnLogo.ImageBitmap);
            }

            BOCaiDatThongTinCongTy.CapNhat(_Item, mTransit);
            DialogResult = true;
        }

        private Data.CAIDATTHONGTINCONGTY _Item = null;
        private Data.BOCaiDatThongTinCongTy BOCaiDatThongTinCongTy = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Item = BOCaiDatThongTinCongTy.GetAll(mTransit);
            if (_Item != null)
            {
                txtTenDoanhNghiep.Text = _Item.TenCongTy;
                txtTenVietTat.Text = _Item.TenVietTat;
                txtMaSoThue.Text = _Item.MaSoThue;
                txtTenNguoiDaiDien.Text = _Item.NguoiDaiDien;

                txtDienThoaiBan.Text = _Item.DienThoaiBan;
                txtDienThoaiDiDong.Text = _Item.DienThoaiDiDong;
                txtEmail.Text = _Item.Email;
                txtFax.Text = _Item.Fax;

                txtDiaChi.Text = _Item.DiaChi;

                if (_Item.Hinh != null && _Item.Hinh.Length > 0)
                {
                    btnHinhDaiDien.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.Hinh);
                }

                if (_Item.Logo != null && _Item.Logo.Length > 0)
                {
                    btnLogo.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.Logo);
                }
            }
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

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
