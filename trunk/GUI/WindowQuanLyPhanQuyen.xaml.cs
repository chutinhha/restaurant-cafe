using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyPhanQuyen.xaml
    /// </summary>
    public partial class WindowQuanLyPhanQuyen : Window
    {
        private Data.Transit mTransit = null;

        private UserControlLibrary.UCQuyen ucQuyen = null;

        public WindowQuanLyPhanQuyen(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.SetTransit(mTransit);
            uCTile.TenChucNang = "Quản Lý Phân quyền";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            if (!mTransit.MenuGiaoDien.Quyen.DanhSachQuyen)
            {
                btnQuyen.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void btnQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (ucQuyen == null)
            {
                ucQuyen = new UserControlLibrary.UCQuyen(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucQuyen);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCQuyen)
                ucQuyen.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnQuyen_Click(sender, e);
        }
    }
}