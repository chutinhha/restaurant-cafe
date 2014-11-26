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
    /// Interaction logic for WindowTest.xaml
    /// </summary>
    public partial class WindowTest : Window
    {
        public WindowTest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog()==true)
            {
                BitmapImage img=new BitmapImage(new Uri(openFileDialog.FileName));
                //image1.Source = Utilities.ImageHandler.GetBitmap(openFileDialog.FileName);
                image1.Source = Utilities.ImageHandler.CreateResizedImage(img, 64, 64, 0);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {            
        }
    }
}
