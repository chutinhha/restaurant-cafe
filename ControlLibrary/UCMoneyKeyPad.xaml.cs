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

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMoneyKeyPad.xaml
    /// </summary>
    public partial class UCMoneyKeyPad : UserControl
    {
        public POSTextBox _TextBox { get; set; }
        public UCMoneyKeyPad()
        {
            InitializeComponent();
        }

        private void pOSButtonMoney5_Click(object sender, RoutedEventArgs e)
        {            
            if (_TextBox!=null)
            {
                POSButtonMoney btn = sender as POSButtonMoney;                
                double value = 0;
                value += Utilities.MoneyFormat.ConvertToDouble(_TextBox.Text);
                value += btn._SoTien;
                btn.Focus();
                _TextBox.Text = Utilities.MoneyFormat.ConvertToString(value);
                _TextBox.Focus();
            }
        }
    }
}
