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
    /// Interaction logic for WindowChucNangSoDoBan.xaml
    /// </summary>
    public partial class WindowChucNangSoDoBan : Window
    {
        private ControlLibrary.UCFloorPlan mUCFloorPlan;
        private Data.Transit mTransit;
        public WindowChucNangSoDoBan(ControlLibrary.UCFloorPlan uc,Data.Transit transit)
        {
            mUCFloorPlan = uc;
            mTransit = transit;
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnChuyenBan_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            UserControlLibrary.WindowBanHangChuyenBan win1 = new UserControlLibrary.WindowBanHangChuyenBan(mTransit, mUCFloorPlan);
            win1.ShowDialog();
        }

        private void btnTachBan_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            UserControlLibrary.WindowBanHangChonBan win1 = new UserControlLibrary.WindowBanHangChonBan(mTransit, true);
            if (win1.ShowDialog() == true)
            {
                UserControlLibrary.WindowBanHangTachBan win2 = new UserControlLibrary.WindowBanHangTachBan(mUCFloorPlan, mTransit, win1._TachGopBan);
                win2.ShowDialog();
            }
        }

        private void btnGopBan_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            UserControlLibrary.WindowBanHangChonBan win1 = new UserControlLibrary.WindowBanHangChonBan(mTransit, false);
            if (win1.ShowDialog() == true)
            {
                UserControlLibrary.WindowBanHangGopBan win2 = new UserControlLibrary.WindowBanHangGopBan(mUCFloorPlan, mTransit, win1._TachGopBan);
                win2.ShowDialog();
            }
        }

        private void btnBanDaDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            UserControlLibrary.WindowBanHangLichSuBanHang win = new UserControlLibrary.WindowBanHangLichSuBanHang(mTransit);
            win.ShowDialog();
        }
    }
}
