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
    /// Interaction logic for WindowCaiDatGioKaraoke.xaml
    /// </summary>
    public partial class WindowCaiDatGioKaraoke : Window
    {
        private Data.Transit mTransit;
        public WindowCaiDatGioKaraoke(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSoPhutToiThieu.Text = mTransit.CaiDatBanHang.SoPhutToiThieu + "";            
            Data.MENUMON mon = (from x in mTransit.KaraokeEntities.MENUMONs where x.MonID == mTransit.CaiDatBanHang.MonTinhGio select x).FirstOrDefault();
            if (mon != null)
            {
                txtMonTinhGio.Text = mon.TenDai;
            }            
            cboNhom.ItemsSource = Data.BOMenuNhom.GetAll(mTransit.KaraokeEntities);
            cboNhom.SelectedValuePath = "NhomID";
            cboNhom.DisplayMemberPath = "TenDai";
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtSoPhutToiThieu.Text != "")
            {
                mTransit.CaiDatBanHang.SoPhutToiThieu = Utilities.MoneyFormat.ConvertToInt(txtSoPhutToiThieu.Text);
            }
            if (cboMon.SelectedValue!=null)
            {
                mTransit.CaiDatBanHang.MonTinhGio = (int?)cboMon.SelectedValue;    
            }            
            if (mTransit.CaiDatBanHang.ID == 0)
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

        private void cboNhom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboNhom.SelectedItem != null)
            {
                Data.MENUNHOM nhom = (Data.MENUNHOM)cboNhom.SelectedItem;
                cboMon.ItemsSource = Data.BOMenuMon.GetAll(mTransit.KaraokeEntities, nhom);
                cboMon.SelectedValuePath = "MonID";
                cboMon.DisplayMemberPath = "TenDai";
            }
        }

        private void cboMon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMon.SelectedItem != null)
            {
                Data.MENUMON mon = (Data.MENUMON)cboMon.SelectedItem;
                txtMonTinhGio.Text = mon.TenDai;
            }
        }
    }
}
