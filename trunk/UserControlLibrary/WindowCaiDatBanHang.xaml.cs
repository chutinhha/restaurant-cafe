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
        private Data.CAIDATBANHANG mCAIDATBANHANG;
        public WindowCaiDatBanHang(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mCAIDATBANHANG = mTransit.KaraokeEntities.CAIDATBANHANGs.FirstOrDefault();
            if (mCAIDATBANHANG==null)
            {
                mCAIDATBANHANG = new Data.CAIDATBANHANG();
            }
            txtPhiDichVu.Text = mCAIDATBANHANG.PhiDichVu + "";
            txtThueVAT.Text = mCAIDATBANHANG.ThueVAT + "";
            chkPhiDichVu.IsChecked = mCAIDATBANHANG.ChoPhepPhiDichVu;
            chkThueVAT.IsChecked = mCAIDATBANHANG.ChoPhepThueVAT;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtPhiDichVu.Text!="")
            {
                mCAIDATBANHANG.PhiDichVu = Utilities.MoneyFormat.ConvertToInt(txtPhiDichVu.Text);
            }
            if (txtThueVAT.Text!="")
            {
                mCAIDATBANHANG.ThueVAT = Utilities.MoneyFormat.ConvertToInt(txtThueVAT.Text);
            }
            mCAIDATBANHANG.ChoPhepPhiDichVu = chkPhiDichVu.IsChecked.Value;
            mCAIDATBANHANG.ChoPhepThueVAT = chkThueVAT.IsChecked.Value;
            if (mCAIDATBANHANG.ID==0)
            {
                mTransit.KaraokeEntities.CAIDATBANHANGs.AddObject(mCAIDATBANHANG);
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
