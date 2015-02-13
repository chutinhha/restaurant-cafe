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
                txtTongTien.Text = "Tổng Tiền: " + Utilities.MoneyFormat.ConvertToStringFull(mBOXuliTinhTien.TongTienChuaGiamGia);
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
            txtGiamGia._MaxValue = 100;
            txtThe._UCKeyPad = uCKeyPad1;
            txtThe._UCMoneyKeyPad = uCMoneyKeyPad1;

            txtSoTien.Text = Utilities.MoneyFormat.ConvertToString(mBOXuliTinhTien.TongTienPhaiTra);
            if (mBOXuliTinhTien.GiamGiaPhanTram > 0)
                txtGiamGia.Text = Utilities.NumberFormat.FormatToString(mBOXuliTinhTien.GiamGiaPhanTram);

            if (mBOBanHang.BANHANG.KhachHangID != null)
                txtGiamGia.IsEnabled = false;            

            InitData();
            LoadKhachHang();
        }

        private void txtGiamGia_TextChanged(object sender, TextChangedEventArgs e)
        {
            mBOXuliTinhTien.GiamGiaPhanTram = Utilities.MoneyFormat.ConvertToInt(txtGiamGia.Text);
            ReLoadData();
        }

        private void txtSoTien_TextChanged(object sender, TextChangedEventArgs e)
        {
            mBOXuliTinhTien.TienKhachDua = Utilities.MoneyFormat.ConvertToDecimal(txtSoTien.Text);
            ReLoadData();
        }

        private void txtThe_TextChanged(object sender, TextChangedEventArgs e)
        {
            mBOXuliTinhTien.TienThe = Utilities.MoneyFormat.ConvertToDecimal(txtThe.Text);
            ReLoadData();
        }   

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;            
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if ((mBOXuliTinhTien.TienKhachDua+mBOXuliTinhTien.TienThe) >= mBOXuliTinhTien.TongTienPhaiTra && mBOXuliTinhTien.TienThe<=mBOXuliTinhTien.TongTienPhaiTra)
            {
                mBOXuliTinhTien.Copy(mBOXuliTinhTien.BanHang, mBOBanHang.BANHANG);            
                this.DialogResult = true;
            }
        }

        private void cboThe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboThe.SelectedItem!=null)
            {
                mBOXuliTinhTien.BanHang.TheID = (int)cboThe.SelectedValue;
                txtThe.IsEnabled = true;                                
            }
        }
        private void LoadKhachHang()
        {
            if (mBOBanHang.KHACHHANG != null)
            {
                btnChonKhachHang.Content = mBOBanHang.KHACHHANG.TenKhachHang;
            }
        }
        private void btnChonKhachHang_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowTimKhachHang win = new UserControlLibrary.WindowTimKhachHang(mTransit);
            if (win.ShowDialog() == true)
            {
                mBOXuliTinhTien.BanHang.KhachHangID = win._KhachHang.KhachHang.KhachHangID;
                mBOBanHang.KHACHHANG = win._KhachHang.KhachHang;

                txtGiamGia.Text = Utilities.NumberFormat.FormatToString(win._KhachHang.LoaiKhachHang.PhanTramGiamGia);
                txtGiamGia.IsEnabled = false;

                LoadKhachHang();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key==Key.Space)
            {
                btnDongY_Click(null, null);
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
