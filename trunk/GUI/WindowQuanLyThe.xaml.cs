using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyThe.xaml
    /// </summary>
    public partial class WindowQuanLyThe : Window
    {
        private Data.Transit mTransit = null;

        private UserControlLibrary.UCThe ucThe = null;

        public WindowQuanLyThe(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.SetTransit(transit);
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            uCTile.TenChucNang = "Quản lý thẻ";
            PhanQuyen();
        }

        private void btnThe_Click(object sender, RoutedEventArgs e)
        {
            if (ucThe == null)
            {
                ucThe = new UserControlLibrary.UCThe();
                ucThe.Init(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucThe);
        }

        private void PhanQuyen()
        {
            if (!mTransit.MenuGiaoDien.The.QuanLyThe)
            {
                btnThe.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCThe)
                ucThe.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnThe_Click(null, null);
        }
    }
}