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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCLoaiKhachHang.xaml
    /// </summary>
    public partial class UCLoaiKhachHang : UserControl
    {
        private Data.Transit mTransit = null;
        public UCLoaiKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
            mLoaiKhachHang = null;
            SetValues();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (mLoaiKhachHang == null)
                {
                    mLoaiKhachHang = new Data.LOAIKHACHHANG();
                    GetValues();
                    Data.BOLoaiKhachHang.Them(mLoaiKhachHang);
                    lbStatus.Text = "Thêm thành công";
                    LoadDanhSachLoaiKhachHang();
                    btnMoi_Click(sender, e);
                }
                else
                {
                    GetValues();
                    Data.BOLoaiKhachHang.Sua(mLoaiKhachHang);
                    lbStatus.Text = "Cập nhật thành công";
                    LoadDanhSachLoaiKhachHang();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLoaiKhachHang = (Data.LOAIKHACHHANG)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOLoaiKhachHang.Xoa(mLoaiKhachHang.LoaiKhachHangID);
                lbStatus.Text = "Xóa thành công";
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLoaiKhachHang = (Data.LOAIKHACHHANG)((ListViewItem)lvData.SelectedItems[0]).Tag;
                SetValues();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mLoaiKhachHang = null;
            btnMoi_Click(sender, e);
            SetValues();
            LoadDanhSachLoaiKhachHang();
        }

        private void LoadDanhSachLoaiKhachHang()
        {
            System.Collections.Generic.List<Data.LOAIKHACHHANG> lsArray = Data.BOLoaiKhachHang.GetAll();
            lvData.Items.Clear();
            foreach (Data.LOAIKHACHHANG item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvData.Items.Add(li);
            }
        }

        private Data.LOAIKHACHHANG mLoaiKhachHang = null;
        private void SetValues()
        {
            if (mLoaiKhachHang == null)
            {

                txtLoaiKhachHang.Text = "";
                txtPhanTramGiam.Text = "0";
                btnThem.Content = "Thêm loại khách hàng";
            }
            else
            {
                txtLoaiKhachHang.Text = mLoaiKhachHang.TenLoaiKhachHang;
                txtPhanTramGiam.Text = mLoaiKhachHang.PhanTramGiamGia.ToString();
                btnThem.Content = "Cập nhật máy in";
            }
        }

        private void GetValues()
        {
            mLoaiKhachHang.Visual = true;
            mLoaiKhachHang.Deleted = false;
            mLoaiKhachHang.TenLoaiKhachHang = txtLoaiKhachHang.Text;
            mLoaiKhachHang.PhanTramGiamGia = System.Convert.ToInt32(txtPhanTramGiam.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtLoaiKhachHang.Text == "")
            {
                lbStatus.Text = "Loại khách hàng không được bỏ trống";
                return false;
            }

            if (txtPhanTramGiam.Text == "")
                txtPhanTramGiam.Text = "0";
            return true;
        }
    }
}
