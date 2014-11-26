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
    /// Interaction logic for WindowChuyenBan.xaml
    /// </summary>
    public partial class WindowChuyenBan : Window
    {
        private Data.BOBanHang mBanhang;
        private Data.Transit mTransit;
        public WindowChuyenBan(Data.Transit tran)
        {
            mTransit = tran;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhu();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = cboKhu2.ItemsSource = Data.BOKhu.GetAllVisual(mTransit);
            cboBan1.ItemsSource = null;
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem!=null && cboBan2.SelectedItem!=null)
            {
                Data.BAN ban=(Data.BAN)cboBan2.SelectedItem;
                mBanhang.ChuyenBan(ban);
                LoadKhu();
            }
        }

        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem!=null)
            {
                cboKhu2.SelectedItem = cboKhu1.SelectedItem;
                cboBan1.ItemsSource = Data.BOBan.GetAllTableInOrderPerArea((Data.KHU)cboKhu1.SelectedItem, mTransit);
            }
        }

        private void cboKhu2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu2.SelectedItem!=null)
            {
                cboBan2.ItemsSource=Data.BOBan.GetAllTableNotInOrderPerArea((Data.KHU)cboKhu2.SelectedItem, mTransit);
            }
        }

        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem!=null)
            {
                mBanhang = new Data.BOBanHang(mTransit);
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                mBanhang.LoadBanHang(ban);
                lvData.ItemsSource = mBanhang._ListChiTietBanHang;                
            }
            else
            {
                lvData.ItemsSource = null;
            }
        }
    }
}
