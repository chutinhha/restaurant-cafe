using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyMayIn.xaml
    /// </summary>
    public partial class WindowQuanLyMayIn : Window
    {
        private Data.Transit mTransit;
        private UserControlLibrary.UCMayIn ucMayIn = null;
        private UserControlLibrary.UCMenuMayIn ucMenuMayIn = null;

        public WindowQuanLyMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMayIn_Click(object sender, RoutedEventArgs e)
        {
            if (ucMayIn == null)
            {
                ucMayIn = new UserControlLibrary.UCMayIn(mTransit);
            }
            spNoiDung.Children.Clear();
            ucMayIn.Height = spNoiDung.ActualHeight;
            ucMayIn.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucMayIn);
        }

        private void btnMenuMayIn_Click(object sender, RoutedEventArgs e)
        {
            if (ucMenuMayIn == null)
            {
                ucMenuMayIn = new UserControlLibrary.UCMenuMayIn(mTransit);
            }
            spNoiDung.Children.Clear();
            ucMenuMayIn.Height = spNoiDung.ActualHeight;
            ucMenuMayIn.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucMenuMayIn);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnMayIn_Click(sender, e);
            uCTile.TenChucNang = "Quản Lý Máy In";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCMayIn)
                ucMayIn.Window_KeyDown(sender, e);
        }

    }
}