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
    /// Interaction logic for WindowCaiDatChucNangHienThi.xaml
    /// </summary>
    public partial class WindowCaiDatChucNangHienThi : Window
    {
        public WindowCaiDatChucNangHienThi(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOChucNang = new Data.BOChucNang(transit);
        }
        private Data.Transit mTransit = null;
        private Data.BOChucNang BOChucNang = null;
        private List<Data.CHUCNANG> lsArray = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lvData.ItemsSource = lsArray = BOChucNang.GetAll(mTransit).ToList();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            BOChucNang.LuuChucNangHienThi(lsArray);
            DialogResult = true;
        }
    }
}
