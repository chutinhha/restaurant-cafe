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

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowSoDoBan.xaml
    /// </summary>
    public partial class WindowSoDoBan : Window
    {
        private Data.Transit mTransit;
        public WindowSoDoBan(Data.Transit tran)
        {
            mTransit = tran;
            InitializeComponent();
        }                
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.Init(mTransit);
            uCListArea1.Init(mTransit);
            uCListArea1._UCFloorPlan = uCFloorPlan1;            
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
