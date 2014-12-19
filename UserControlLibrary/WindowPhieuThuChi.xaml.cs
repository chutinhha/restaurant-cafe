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
    /// Interaction logic for WindowPhieuThuChi.xaml
    /// </summary>
    public partial class WindowPhieuThuChi : Window
    {
        private Data.Transit mTransit;

        public Data.BOThuChi _Item { get; set; }
        private int LoaiThuChiID = 0;
        private Data.BOThuChi BOThuChi = null;

        public WindowPhieuThuChi(Data.Transit transit, Data.BOThuChi bOThuChi, int loaiThuChiID)
        {
            InitializeComponent();
            mTransit = transit;
            LoaiThuChiID = loaiThuChiID;
            BOThuChi = bOThuChi;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                if (_Item == null)
                {
                    _Item = new Data.BOThuChi();
                    _Item.ThuChi.Visual = true;
                    _Item.ThuChi.Deleted = false;
                    _Item.ThuChi.Edit = false;
                    _Item.ThuChi.NhanVienID = mTransit.NhanVien.NhanVienID;
                    _Item.ThuChi.ThoiGian = DateTime.Now;
                    _Item.ThuChi.LoaiThuChiID = LoaiThuChiID;
                    _Item.NhanVien.TenNhanVien = mTransit.NhanVien.TenNhanVien;
                    _Item.LoaiThuChi.TenLoaiThuChi = BOThuChi.GetLoaiThuChi(LoaiThuChiID).TenLoaiThuChi;
                }
                GetValues();
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtGhiChu.Text = "";
                txtTongTien.Text = "0";
                btnLuu.Content = mTransit.StringButton.Them;
                if (LoaiThuChiID == 1)
                    lbTieuDe.Text = "Thêm phiếu thu";
                else
                    lbTieuDe.Text = "Thêm phiếu chi";

            }
            else
            {
                txtGhiChu.Text = _Item.ThuChi.GhiChu;
                txtTongTien.Text = _Item.ThuChi.TongTien.ToString();
                btnLuu.Content = mTransit.StringButton.Luu;
                if (LoaiThuChiID == 1)
                    lbTieuDe.Text = "Sửa phiếu thu";
                else
                    lbTieuDe.Text = "Sửa phiếu chi";
            }
        }

        private void GetValues()
        {
            _Item.ThuChi.GhiChu = txtGhiChu.Text;
            if (txtTongTien.Text == "")
                txtTongTien.Text = "0";
            _Item.ThuChi.TongTien = Convert.ToDecimal(txtTongTien.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTongTien.Text == "")
            {
                lbStatus.Text = "Số tiền không thể bỏ trống";
                return false;
            }
            return true;
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

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
