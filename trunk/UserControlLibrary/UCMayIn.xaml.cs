using System.Windows;
using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMayIn.xaml
    /// </summary>
    public partial class UCMayIn : UserControl
    {
        private Data.Transit mTransit = null;

        public UCMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void lvMayIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LoadPrinter()
        {
            
            
        }
    }
}