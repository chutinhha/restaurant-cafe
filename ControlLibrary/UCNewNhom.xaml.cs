using System;
using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewNhom.xaml
    /// </summary>
    public partial class UCNewNhom : UserControl
    {
        private int LoaiNhomID = 0;

        public UCNewNhom(int loaiNhomID)
        {
            InitializeComponent();
            LoaiNhomID = loaiNhomID;
        }

        public Data.MENUNHOM _Nhom { get; set; }

        public void CapNhat()
        {
            if (_Nhom != null)
            {
                GetData();
                Data.BOMenuNhom.CapNhat(_Nhom);
            }
            else
            {
                GetData();
                Data.BOMenuNhom.Them(_Nhom);
            }
        }

        public void GetData()
        {
            if (_Nhom == null)
            {
                _Nhom = new Data.MENUNHOM();
                _Nhom.Deleted = false;
                _Nhom.Visual = true;
                _Nhom.MayIn = 0;
                _Nhom.GiamGia = 0;
                _Nhom.LoaiNhomID = LoaiNhomID;
            }
            _Nhom.TenDai = txtTenDai.Text;
            _Nhom.TenNgan = txtTenNgan.Text;
            if (txtSapXep.Text == "")
                _Nhom.SapXep = 0;
            else
                _Nhom.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            Data.BOMenuNhom.Xoa(_Nhom.NhomID);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Nhom != null)
            {
                txtTenDai.Text = _Nhom.TenDai;
                txtTenNgan.Text = _Nhom.TenNgan;
                txtSapXep.Text = _Nhom.SapXep.ToString();
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = "";
            }
        }
    }
}