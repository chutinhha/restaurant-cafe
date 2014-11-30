using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLySoDoBan.xaml
    /// </summary>
    public partial class WindowQuanLySoDoBan : Window
    {
        private Data.Transit mTransit = null;

        public WindowQuanLySoDoBan(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            if (!mTransit.MenuGiaoDien.QuanLySoDoBan.SoDoBan)
            {
                btnSoDoBan.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mTransit.MenuGiaoDien.QuanLySoDoBan.QuanLyKhu)
            {
                btnQuanLyKhu.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private UserControlLibrary.UCKhu ucKhu = null;
        private UserControlLibrary.UCSoDoBan ucSoDoBan = null;

        private void btnQuanLyKhu_Click(object sender, RoutedEventArgs e)
        {
            if (ucKhu == null)
            {
                ucKhu = new UserControlLibrary.UCKhu(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucKhu);
        }

        private void btnSoDoBan_Click(object sender, RoutedEventArgs e)
        {
            if (ucSoDoBan == null)
            {
                ucSoDoBan = new UserControlLibrary.UCSoDoBan(mTransit);
                ucSoDoBan.Width = spNoiDung.RenderSize.Width;
                ucSoDoBan.Height = spNoiDung.RenderSize.Height;                
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucSoDoBan);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSoDoBan_Click(sender, e);
            uCTile.TenChucNang = "Quản Lý Sơ đồ bàn";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCKhu)
                ucKhu.Window_KeyDown(sender, e);
        }
    }
}