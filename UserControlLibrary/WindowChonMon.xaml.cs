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
    /// Interaction logic for WindowChonMon.xaml
    /// </summary>
    public partial class WindowChonMon : Window
    {
        public Data.MENUKICHTHUOCMON _ItemKichThuocMon = null;
        public Data.MENUMON _ItemMon = null;
        private Data.Transit mTransit = null;
        bool IsMon = true;
        public WindowChonMon(Data.Transit transit, bool isMon)
        {
            InitializeComponent();
            mTransit = transit;
            IsMon = isMon;
            uCMenu._IsBanHang = !IsMon;
            uCMenu.OnEventMenu += new UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
        }

        void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                if (!IsMon)
                {
                    _ItemKichThuocMon = (Data.MENUKICHTHUOCMON)ob;
                    txtTenLoaiBan.Text = _ItemKichThuocMon.MENUMON.TenDai + " (" + _ItemKichThuocMon.TenLoaiBan + ")";
                }
            }
            else if (ob is Data.MENUMON)
            {
                if (IsMon)
                {
                    _ItemMon = (Data.MENUMON)ob;
                    txtTenLoaiBan.Text = _ItemMon.TenDai;
                }
            }

        }

        private void btnChonMon_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
