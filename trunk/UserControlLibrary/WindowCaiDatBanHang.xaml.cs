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
    /// Interaction logic for WindowCaiDatBanHang.xaml
    /// </summary>
    public partial class WindowCaiDatBanHang : Window
    {
        Data.Transit mTransit;        
        public WindowCaiDatBanHang(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {                        
            txtPhiDichVu.Text = mTransit.CaiDatBanHang.PhiDichVu + "";
            txtThueVAT.Text = mTransit.CaiDatBanHang.ThueVAT + "";            
            chkPhiDichVu.IsChecked = mTransit.CaiDatBanHang.ChoPhepPhiDichVu;
            chkThueVAT.IsChecked = mTransit.CaiDatBanHang.ChoPhepThueVAT;                        
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtPhiDichVu.Text!="")
            {
                mTransit.CaiDatBanHang.PhiDichVu = Utilities.MoneyFormat.ConvertToInt(txtPhiDichVu.Text);
            }
            if (txtThueVAT.Text!="")
            {
                mTransit.CaiDatBanHang.ThueVAT = Utilities.MoneyFormat.ConvertToInt(txtThueVAT.Text);
            }            
            mTransit.CaiDatBanHang.ChoPhepPhiDichVu = chkPhiDichVu.IsChecked.Value;
            mTransit.CaiDatBanHang.ChoPhepThueVAT = chkThueVAT.IsChecked.Value;
            if (mTransit.CaiDatBanHang.ID==0)
            {
                mTransit.KaraokeEntities.CAIDATBANHANGs.AddObject(mTransit.CaiDatBanHang);
            }
            mTransit.KaraokeEntities.SaveChanges();
            this.DialogResult = true;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

    
    }
}
