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
    /// Interaction logic for WindowThemNhanVien.xaml
    /// </summary>
    public partial class WindowThemNhanVien : Window
    {
        private Data.Transit mTransit;
        public Data.NHANVIEN _NhanVien { get; set; }
        public WindowThemNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiNhanVien();
            SetValues();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (_NhanVien == null)
                {
                    _NhanVien = new Data.NHANVIEN();
                    _NhanVien.Visual = true;
                    _NhanVien.Deleted = false;
                    _NhanVien.Edit = false;
                    _NhanVien.NhanVienID = 0;
                }

                GetValues();
                DialogResult = true;
            }

        }

        private void LoadLoaiNhanVien()
        {
            cbbLoaiNhanVien.ItemsSource = Data.BOLoaiNhanVien.GetAll(mTransit);
            if (cbbLoaiNhanVien.Items.Count > 0)
                cbbLoaiNhanVien.SelectedIndex = 0;
        }

        private void SetValues()
        {
            if (_NhanVien == null)
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedIndex = 0;
                }
                txtTenNhanVien.Text = "";
                txtTenDangNhap.Text = "";
                txtMatKhau.Password = "";
                btnLuu.Content = "Thêm";
                lbTieuDe.Text = "Thêm Nhân Viên";
            }
            else
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedValue = _NhanVien.LoaiNhanVienID;
                }
                txtTenNhanVien.Text = _NhanVien.TenNhanVien;
                txtTenDangNhap.Text = _NhanVien.TenDangNhap;
                btnLuu.Content = "Lưu";
                lbTieuDe.Text = "Sửa Nhân Viên";
                txtMatKhau.Password = null;
            }
        }

        private void GetValues()
        {
            _NhanVien.TenNhanVien = txtTenNhanVien.Text;
            _NhanVien.TenDangNhap = txtTenDangNhap.Text;
            _NhanVien.LoaiNhanVienID = (int)cbbLoaiNhanVien.SelectedValue;
            if (txtMatKhau.Password != "")
            {
                _NhanVien.MatKhau = Utilities.SecurityKaraoke.GetMd5Hash(txtMatKhau.Password, mTransit.HashMD5);
            }
            if (_NhanVien.LOAINHANVIEN == null)
            {
                _NhanVien.LOAINHANVIEN = new Data.LOAINHANVIEN();
                Data.LOAINHANVIEN lnv = (Data.LOAINHANVIEN)cbbLoaiNhanVien.SelectedItem;
                _NhanVien.LOAINHANVIEN.TenLoaiNhanVien = lnv.TenLoaiNhanVien;
            }
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenNhanVien.Text == "")
            {
                lbStatus.Text = "Tên nhân viên không được bỏ trống";
                return false;
            }
            else if (txtTenDangNhap.Text == "")
            {
                lbStatus.Text = "Tên đăng nhập không được bỏ trống";
                return false;
            }
            else if (txtTenDangNhap.Text.Length < 4)
            {
                lbStatus.Text = "Tên đăng nhập không được nhỏ hơn 4 ký tự";
                return false;
            }
            else if (_NhanVien == null && txtMatKhau.Password == "")
            {
                lbStatus.Text = "Mật khẩu không được bỏ trống";
                return false;
            }
            else if (_NhanVien == null && txtMatKhau.Password.Length < 4)
            {
                lbStatus.Text = "Mật khẩu không được nhỏ hơn 4 ký tự";
                return false;
            }

            return true;
        }
    }
}
