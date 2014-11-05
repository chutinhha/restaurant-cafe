using System.Windows;
using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachBan.xaml
    /// </summary>
    public partial class UCDanhSachBan : UserControl
    {
        private Data.MENUMON mMon = null;
        private Data.Transit mTransit = null;

        public UCDanhSachBan(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUMON)
            {
                mMon = (Data.MENUMON)ob;

                uCDanhSachBanList.Init(mMon, mTransit);
                uCDanhSachBanList.LoadDanhSach();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenu.OnEventMenu += new UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            uCDanhSachBanList.Window_KeyDown(sender, e);
        }
    }
}