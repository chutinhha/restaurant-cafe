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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvMayIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UCMenu.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(UCMenu_OnEventMenu);
            UCMenu.Init();

        }

        private Data.MENUMON _Mon = null;
        void UCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUMON)
            {
                _Mon = (Data.MENUMON)ob;
            }
        }
    }
}
