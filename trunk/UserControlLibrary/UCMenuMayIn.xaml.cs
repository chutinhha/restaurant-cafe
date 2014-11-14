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
            UCMenu._OnEventMenuMon += new UserControlLibrary.UCMenu.EventMenuMon(UCMenu__OnEventMenuMon);            
            UCMenu.Init(mTransit);
        }

        void UCMenu__OnEventMenuMon(Data.BOMenuMon ob)
        {
            _Mon = ob;
            uCMenuSetMayIn.SetValues(_Mon);
        }

        private Data.BOMenuMon _Mon = null;        

        private void uCMenuSetMayIn_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}