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

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class WindowKeyboard : Window
    {
        public WindowKeyboard()
        {
            InitializeComponent();
        }

        public int FontSize { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Content.ToString())
            {
                case "1":
                    break;
            }

        }
    }
}
