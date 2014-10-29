using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyNhanVien.xaml
    /// </summary>
    public partial class WindowQuanLyNhanVien : Window
    {
        private Data.NHANVIEN mNhanVien = null;
        private Data.Transit mTransit = null;

        public WindowQuanLyNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
            mNhanVien = null;
            SetValues();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (mNhanVien == null)
            {
                mNhanVien = new Data.NHANVIEN();
                mNhanVien.Deleted = false;
                mNhanVien.Visual = true;
                GetValues();
                Data.BONhanVien.Them(mNhanVien);
                LoadDanhSachNhanVien();
                btnMoi_Click(sender, e);
                lbStatus.Text = "Thêm thành công";
            }
            else
            {
                GetValues();
                Data.BONhanVien.CapNhat(mNhanVien);
                LoadDanhSachNhanVien();
                lbStatus.Text = "Cập nhật thành công";
            }
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                mNhanVien = (Data.NHANVIEN)((ListViewItem)lvNhanVien.SelectedItems[0]).Tag;
                Data.BONhanVien.Xoa(mNhanVien.NhanVienID);
                LoadDanhSachNhanVien();
                btnMoi_Click(sender, e);
            }
        }

        private void GetValues()
        {
            mNhanVien.TenNhanVien = txtTenNhanVien.Text;
            mNhanVien.TenDangNhap = txtTenDangNhap.Text;
            mNhanVien.LoaiNhanVienID = (int)cbbLoaiNhanVien.SelectedValue;
            if (txtMatKhau.Password != "")
            {
                mNhanVien.MatKhau = Utilities.SecurityKaraoke.GetMd5Hash(txtMatKhau.Password, mTransit.HashMD5);
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
            else if (mNhanVien.NhanVienID == 0 && txtMatKhau.Password == "")
            {
                lbStatus.Text = "Mật khẩu không được bỏ trống";
                return false;
            }
            else if (mNhanVien.NhanVienID == 0 && txtMatKhau.Password.Length < 4)
            {
                lbStatus.Text = "Mật khẩu không được nhỏ hơn 4 ký tự";
                return false;
            }

            return true;
        }

        private void LoadDanhSachNhanVien()
        {
            List<Data.NHANVIEN> lsArray = Data.BONhanVien.GetAll();
            lvNhanVien.Items.Clear();
            foreach (var item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvNhanVien.Items.Add(li);
            }
        }

        private void LoadLoaiNhanVien()
        {
            cbbLoaiNhanVien.ItemsSource = Data.BOLoaiNhanVien.GetAll();
            if (cbbLoaiNhanVien.Items.Count > 0)
                cbbLoaiNhanVien.SelectedIndex = 0;
        }

        private void lvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                mNhanVien = (Data.NHANVIEN)((ListViewItem)lvNhanVien.SelectedItems[0]).Tag;
                SetValues();
            }
        }

        private void SetValues()
        {
            if (mNhanVien == null)
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedIndex = 0;
                }
                txtTenNhanVien.Text = "";
                txtTenDangNhap.Text = "";
                txtMatKhau.Password = "";
                btnThem.Content = "Thêm nhân viên";
            }
            else
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedValue = mNhanVien.LoaiNhanVienID;
                }
                txtTenNhanVien.Text = mNhanVien.TenNhanVien;
                txtTenDangNhap.Text = mNhanVien.TenDangNhap;
                btnThem.Content = "Cập nhật nhân viên";
                txtMatKhau.Password = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiNhanVien();
            LoadDanhSachNhanVien();
        }
    }
}