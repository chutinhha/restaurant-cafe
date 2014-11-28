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
    /// Interaction logic for WindowGopBan.xaml
    /// </summary>
    public partial class WindowBanHangGopBan : Window
    {
        private Data.BOTachGopBan mBOGopBan;
        private Data.Transit mTransit;
        private ControlLibrary.UCFloorPlan mUCFloorPlan;
        public WindowBanHangGopBan(ControlLibrary.UCFloorPlan uc,Data.Transit transit,Data.BOTachGopBan gopban)
        {            
            mTransit = transit;
            mUCFloorPlan = uc;
            mBOGopBan = gopban;
            InitializeComponent();
        }


        private void LoadKhu()
        {
            cboKhu1.ItemsSource = mBOGopBan.GetAllKhuVisual();
            cboBan1.ItemsSource = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblBanGop.Content = mBOGopBan.BanHang.TenBan;
            LoadKhu();
        }
        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem != null)
            {            
                cboBan1.ItemsSource = mBOGopBan.GetAllTableInOrderPerArea((Data.KHU)cboKhu1.SelectedItem,mBOGopBan.BanHang.BANHANG.BanHangID);
            }
        }
      
        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                Data.BOBanHang banhang = mBOGopBan.GetBanHang(ban);                
                lvData1.ItemsSource = banhang._ListChiTietBanHang;
                lvData1.Tag = banhang;
            }
            else
            {
                lvData1.ItemsSource = null;
            }
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (mBOGopBan.KiemTra())
            {
                mBOGopBan.XuliGopBan();
                this.DialogResult = true;
                mUCFloorPlan.LoadAlllStatus();
                UserControlLibrary.WindowMessageBox.ShowDialog("Gộp bàn thành công !");                
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn gộp !");                
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem!=null)
            {
                if (lvData1.Tag!=null)
                {
                    Data.BOBanHang bh =(Data.BOBanHang) lvData1.Tag;
                    mBOGopBan.AddBanHang(bh);
                    lvData2.ItemsSource = mBOGopBan._ListBan;
                    lvData2.Items.Refresh();
                }
            }
        }       
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData2.SelectedItem!=null)
            {
                Data.BOBanHang bh = (Data.BOBanHang)lvData2.SelectedItem;
                mBOGopBan.XoaBanHang(bh);
                lvData2.Items.Refresh();
                //lvData2.Items.Remove(li);
            }
        }
    }
}
