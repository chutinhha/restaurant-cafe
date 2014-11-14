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
            uCMenu._OnEventMenuMon += new UCMenu.EventMenuMon(uCMenu__OnEventMenuMon);
        }

        void uCMenu__OnEventMenuMon(Data.BOMenuMon ob)
        {
            uCDanhSachBanList.Init(ob, mTransit);
            uCDanhSachBanList.LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenu.Init(mTransit);
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            uCDanhSachBanList.Window_KeyDown(sender, e);
        }
    }
}