using System.Windows;
using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenuMayIn.xaml
    /// </summary>
    public partial class UCMenuMayIn : UserControl
    {
        private Data.Transit mTransit = null;

        public UCMenuMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenuSetMayIn.Init(mTransit);
            UCMenu.OnEventMenu += new UserControlLibrary.UCMenu.EventMenu(UCMenu_OnEventMenu);
            UCMenu.Init(mTransit);
        }

        private Data.MENUMON _Mon = null;

        private void UCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUMON)
            {
                _Mon = (Data.MENUMON)ob;
                uCMenuSetMayIn.SetValues(_Mon);
            }
        }

        private void uCMenuSetMayIn_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}