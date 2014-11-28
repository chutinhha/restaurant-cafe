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
    /// Interaction logic for WindowBanHangTachBan.xaml
    /// </summary>
    public partial class WindowBanHangTachBan : Window
    {
        private Data.Transit mTransit;
        private Data.BOTachGopBan mBOTachGopBan;
        private ControlLibrary.UCFloorPlan mUCFloorPlan;
        public WindowBanHangTachBan(ControlLibrary.UCFloorPlan uc,Data.Transit transit, Data.BOTachGopBan tachban)
        {
            mTransit = transit;
            mUCFloorPlan = uc;
            mBOTachGopBan = tachban;            
            InitializeComponent();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = mBOTachGopBan.GetAllKhuVisual();
            cboBan1.ItemsSource = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblBanTach.Content = mBOTachGopBan.BanHang.TenBan;
            lvData1.ItemsSource = mBOTachGopBan.BanHang._ListChiTietBanHang;
            lvData3.ItemsSource = mBOTachGopBan._ListBan;
            LoadKhu();
        }
        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem != null)
            {
                cboBan1.ItemsSource = mBOTachGopBan.GetAllTableNotInOrderPerArea((Data.KHU)cboKhu1.SelectedItem);
            }
        }

        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                Data.BOBanHang banhang = mBOTachGopBan.GetTachBan(ban);
                if (banhang!=null)
                {
                    lvData2.ItemsSource = banhang._ListChiTietBanHang;
                }
                else
                {
                    lvData2.ItemsSource = null;
                }
                //lvData1.Tag = banhang;
            }
            else
            {
                lvData2.ItemsSource = null;
            }
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (mBOTachGopBan.KiemTra())
            {
                mBOTachGopBan.XuliTachBan();
                this.DialogResult = true;
                mUCFloorPlan.LoadAlllStatus();
                UserControlLibrary.WindowMessageBox.ShowDialog("Tách bàn thành công !");
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn gộp !");
            }
        }       

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem==null)
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn cần tách");
                return;
            }
            if (lvData1.SelectedItems.Count>0)
            {
                Data.BOChiTietBanHang chitiet = (Data.BOChiTietBanHang)lvData1.SelectedItems[0];
                mBOTachGopBan.BanHang.DeleteChiTietBanHang(chitiet);
                lvData1.Items.Refresh();
                if (mBOTachGopBan.ThemTachBan(chitiet, (Data.BAN)cboBan1.SelectedItem)==false)
                {
                    lvData2.ItemsSource = mBOTachGopBan._CurrentBanHang._ListChiTietBanHang;
                }
                else
                {
                    lvData2.Items.Refresh();
                }
                if (lvData1.Items.Count>0)
                {
                    lvData1.SelectedItem = lvData1.Items.Count - 1;
                }
                lvData3.Items.Refresh();
            }
        }

        private void btnHuyMon_Click(object sender, RoutedEventArgs e)
        {
            if (lvData2.SelectedItem==null)
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn món !");
            }
            else
            {
                Data.BOChiTietBanHang chitiet = (Data.BOChiTietBanHang)lvData2.SelectedItem;
                mBOTachGopBan.XoaTachBan(chitiet);                
                mBOTachGopBan.BanHang.AddChiTietBanHang(chitiet);
                lvData1.Items.Refresh();
                lvData2.Items.Refresh();
                lvData3.Items.Refresh();
            }
        }
    }
}

