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
    /// Interaction logic for WindowMessageBox.xaml
    /// </summary>
    public partial class WindowMessageBox : Window
    {
        public WindowMessageBox(string messgage)
        {
            InitializeComponent();
            var formattedText = new FormattedText(
                messgage,
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(lbData.FontFamily, lbData.FontStyle, lbData.FontWeight, lbData.FontStretch),
                lbData.FontSize,
                Brushes.Black);
            double width = formattedText.Width + 20;
            if (lbData.Width<width)
            {
                lbData.Width=this.Width = width;    
            }            
            lbData.Content = messgage;
        }
        public static void ShowDialog(string message)
        {
            WindowMessageBox win = new WindowMessageBox(message);
            win.ShowDialog();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnOK_Click(null, null);
                return;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                     
        }
    }
}
