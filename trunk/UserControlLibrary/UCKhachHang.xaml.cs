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
    /// Interaction logic for UCKhachHang.xaml
    /// </summary>
    public partial class UCKhachHang : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.KHACHHANG mKhachHang = null;
        public UCKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
            mKhachHang = null;
            SetValues();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (mKhachHang == null)
                {
                    mKhachHang = new Data.KHACHHANG();
                    GetValues();
                    Data.BOKhachHang.Them(mKhachHang, mTransit);
                    lbStatus.Text = "Thêm thành công";
                    LoadDanhSachKhachHang();
                    btnMoi_Click(sender, e);
                }
                else
                {
                    GetValues();
                    Data.BOKhachHang.Sua(mKhachHang, mTransit);
                    lbStatus.Text = "Cập nhật thành công";
                    LoadDanhSachKhachHang();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mKhachHang = (Data.KHACHHANG)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOKhachHang.Xoa(mKhachHang.KhachHangID, mTransit);
                lbStatus.Text = "Xóa thành công";
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mKhachHang = (Data.KHACHHANG)((ListViewItem)lvData.SelectedItems[0]).Tag;
                SetValues();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mKhachHang = null;
            btnMoi_Click(sender, e);
            SetValues();
            LoadLoaiKhachHang();
            LoadDanhSachKhachHang();
        }

        private void LoadDanhSachKhachHang()
        {
            System.Collections.Generic.List<Data.KHACHHANG> lsArray = Data.BOKhachHang.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (Data.KHACHHANG item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvData.Items.Add(li);
            }
        }
        private void SetValues()
        {
            if (mKhachHang == null)
            {
                txtTenKhachHang.Text = "";
                txtEmail.Text = "";
                txtFax.Text = "";
                txtSoNha.Text = "";
                txtTenDuong.Text = "";
                txtDienThoaiBan.Text = "";
                txtDienThoaiDong.Text = "";
                txtDuNo.Text = "";
                txtDuNoToiThieu.Text = "";
                if (cbbLoaiKhachHang.Items.Count > 0)
                    cbbLoaiKhachHang.SelectedIndex = 0;

                btnThem.Content = "Thêm loại khách hàng";
            }
            else
            {
                txtTenKhachHang.Text = mKhachHang.TenKhachHang;
                txtEmail.Text = mKhachHang.Email;
                txtFax.Text = mKhachHang.Fax;
                txtSoNha.Text = mKhachHang.SoNha;
                txtTenDuong.Text = mKhachHang.TenDuong;
                txtDienThoaiBan.Text = mKhachHang.Phone;
                txtDienThoaiDong.Text = mKhachHang.Phone;
                txtDuNo.Text = mKhachHang.DuNo.ToString();
                txtDuNoToiThieu.Text = mKhachHang.DuNoToiThieu.ToString();
                cbbLoaiKhachHang.SelectedValue = mKhachHang.LoaiKhachHangID;
                btnThem.Content = "Cập nhật máy in";
            }
        }

        private void GetValues()
        {
            mKhachHang.Visual = true;
            mKhachHang.Deleted = false;
            mKhachHang.TenKhachHang = txtTenKhachHang.Text;
            mKhachHang.Email = txtEmail.Text;
            mKhachHang.Fax = txtFax.Text;
            mKhachHang.SoNha = txtSoNha.Text;
            mKhachHang.TenDuong = txtTenDuong.Text;
            mKhachHang.Mobile = txtDienThoaiBan.Text;
            mKhachHang.Phone = txtDienThoaiDong.Text;
            mKhachHang.LoaiKhachHangID = (int)cbbLoaiKhachHang.SelectedValue;
            mKhachHang.DuNo = System.Convert.ToDecimal(txtDuNo.Text);
            mKhachHang.DuNoToiThieu = System.Convert.ToDecimal(txtDuNoToiThieu.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenKhachHang.Text == "")
            {
                lbStatus.Text = "Tên khách hàng không được bỏ trống";
                return false;
            }

            if (txtDuNo.Text == "")
                txtDuNo.Text = "0";
            if (txtDuNoToiThieu.Text == "")
                txtDuNoToiThieu.Text = "1000";
            return true;
        }

        private void LoadLoaiKhachHang()
        {
            cbbLoaiKhachHang.ItemsSource = Data.BOLoaiKhachHang.GetAll(mTransit);
            if (cbbLoaiKhachHang.Items.Count > 0)
                cbbLoaiKhachHang.SelectedIndex = 0;
        }
    }
}
