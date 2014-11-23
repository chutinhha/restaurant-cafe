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

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowTinhTien.xaml
    /// </summary>
    public partial class WindowTinhTien : Window
    {
        private Data.Transit mTransit;
        private Data.BOBanHang mBOBanHang;
        private Data.BOXuliTinhTien mBOXuliTinhTien;
        public WindowTinhTien(Data.Transit transit,Data.BOBanHang banhang)
        {
            mTransit = transit;
            mBOBanHang = banhang;
            mBOXuliTinhTien = new Data.BOXuliTinhTien(mTransit, mBOBanHang);            
            InitializeComponent();            
        }        
        private void InitData()
        {
            cboThe.ItemsSource = Data.BOThe.GetAllVisual(mTransit);            
        }
        private void ReLoadData()
        {
            if (txtTongTien!=null)
            {
                txtTongTien.Text ="Tổng Tiền: "+ Utilities.MoneyFormat.ConvertToStringFull(mBOXuliTinhTien.TongTien);
            }
            if (txtTongTienPhaiTra!=null)
            {
                txtTongTienPhaiTra.Text = Utilities.MoneyFormat.ConvertToStringFull(mBOXuliTinhTien.TongTienPhaiTra);    
            }
            if (lblTienThua!=null)
            {
                lblTienThua._DecimalValue = mBOXuliTinhTien.TienTraLai;
            }
            if (mBOXuliTinhTien.TienTraLai>0)
            {
                lblTienThua.Visibility = System.Windows.Visibility.Visible;
                lblTienThuaLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lblTienThua.Visibility = System.Windows.Visibility.Hidden;
                lblTienThuaLabel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            txtSoTien._UCMoneyKeyPad = uCMoneyKeyPad1;
            txtSoTien._UCKeyPad = uCKeyPad1;
            txtGiamGia._UCKeyPad = uCKeyPad1;
            //ReLoadData();
            txtSoTien.Text = Utilities.MoneyFormat.ConvertToString(mBOXuliTinhTien.TongTienPhaiTra);
            if (mBOXuliTinhTien.GiamGiaPhanTram > 0)
                txtGiamGia.Text = Utilities.NumberFormat.FormatToString(mBOXuliTinhTien.GiamGiaPhanTram);
            InitData();
        }

        private void txtGiamGia_TextChanged(object sender, TextChangedEventArgs e)
        {            
            mBOXuliTinhTien.TienGiam = Utilities.MoneyFormat.ConvertToDecimal(txtGiamGia.Text)*mBOXuliTinhTien.TongTien/100;
            ReLoadData();
        }

        private void txtSoTien_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboThe.SelectedValue!=null)
            {
                mBOXuliTinhTien.TienThe = Utilities.MoneyFormat.ConvertToDecimal(txtSoTien.Text);
            }
            else
            {
                mBOXuliTinhTien.TienKhachDua = Utilities.MoneyFormat.ConvertToDecimal(txtSoTien.Text);
            }
            ReLoadData();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (mBOXuliTinhTien.TienKhachDua >= mBOXuliTinhTien.TongTienPhaiTra || mBOXuliTinhTien.TienThe >= mBOXuliTinhTien.TongTienPhaiTra)
            {
                mBOBanHang.BANHANG = mBOXuliTinhTien.BanHang;
                this.DialogResult = true;
            }
        }

        private void cboThe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mBOXuliTinhTien.BanHang.TheID = (int)cboThe.SelectedValue;
            txtSoTien.Text = Utilities.MoneyFormat.ConvertToString(mBOXuliTinhTien.TongTienPhaiTra);
        }

        private void btnChonKhachHang_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowTimKhachHang win = new UserControlLibrary.WindowTimKhachHang(mTransit);
            if (win.ShowDialog() == true)
            {
                mBOXuliTinhTien.BanHang.KhachHangID = win._KhachHang.KhachHangID;
                //mBOXuliTinhTien.BanHang.KHACHHANG = win._KhachHang;
                btnChonKhachHang.Content = win._KhachHang.TenKhachHang;
            }
        }       
        
    }
}
