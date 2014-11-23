using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowMenuChange.xaml
    /// </summary>
    public partial class WindowMenuChange : Window
    {
        private Data.Transit mTransit = null;

        public WindowMenuChange(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            uCTile.TenChucNang = "Quản lý thực đơn";
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenu._OnEventMenuMon += new UserControlLibrary.UCMenu.EventMenuMon(uCMenu__OnEventMenuMon);
            uCMenu._OnEventMenuNhom += new UserControlLibrary.UCMenu.EventMenuGroup(uCMenu__OnEventMenuNhom);
            uCMenu.Init(mTransit);
        }

        void uCMenu__OnEventMenuNhom(Data.BOMenuNhom ob)
        {
            UserControlLibrary.UCNewNhom uc = new UserControlLibrary.UCNewNhom(mTransit, uCMenu.BOMenuNhom);
            uc._Nhom = (Data.BOMenuNhom)ob;
            mMenuNhom = uc._Nhom.MenuNhom;
            svChinhSuaMenu.Children.Clear();
            svChinhSuaMenu.Children.Add(uc);
            btnCapNhat.Content = "Cập nhật nhóm";
            btnXoa.Content = "Xóa nhóm";
            btnCapNhat.Visibility = System.Windows.Visibility.Visible;
            btnXoa.Visibility = System.Windows.Visibility.Visible;
        }

        void uCMenu__OnEventMenuMon(Data.BOMenuMon ob)
        {
            UserControlLibrary.UCNewMon uc = new UserControlLibrary.UCNewMon(mMenuNhom, mTransit, uCMenu.BOMenuMon);
            uc._Mon = (Data.BOMenuMon)ob;
            svChinhSuaMenu.Children.Clear();
            svChinhSuaMenu.Children.Add(uc);
            btnCapNhat.Content = "Cập nhật món";
            btnXoa.Content = "Xóa món";
            btnCapNhat.Visibility = System.Windows.Visibility.Visible;
            btnXoa.Visibility = System.Windows.Visibility.Visible;
        }

        private Data.MENUNHOM mMenuNhom = null;

        private void btnNhomMoi_Click(object sender, RoutedEventArgs e)
        {
            lbStatus.Text = "";
            UserControlLibrary.UCNewNhom uc = new UserControlLibrary.UCNewNhom(mTransit, uCMenu.BOMenuNhom);
            svChinhSuaMenu.Children.Clear();
            svChinhSuaMenu.Children.Add(uc);
            btnCapNhat.Visibility = System.Windows.Visibility.Visible;
            btnCapNhat.Content = "Thêm nhóm";
            btnXoa.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnMonMoi_Click(object sender, RoutedEventArgs e)
        {
            if (mMenuNhom != null)
            {
                lbStatus.Text = "";
                UserControlLibrary.UCNewMon uc = new UserControlLibrary.UCNewMon(mMenuNhom, mTransit, uCMenu.BOMenuMon);
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnCapNhat.Content = "Thêm món";
                btnXoa.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                lbStatus.Text = "Chưa chọn nhóm";
            }
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (svChinhSuaMenu.Children[0] is UserControlLibrary.UCNewNhom)
            {
                UserControlLibrary.UCNewNhom uc = (UserControlLibrary.UCNewNhom)svChinhSuaMenu.Children[0];
                uc.CapNhat();
                lbStatus.Text = "Cập nhật nhóm thành công";
                uCMenu.RefershMenu(true);
            }
            else if (svChinhSuaMenu.Children[0] is UserControlLibrary.UCNewMon)
            {
                UserControlLibrary.UCNewMon uc = (UserControlLibrary.UCNewMon)svChinhSuaMenu.Children[0];
                uc.CapNhat();
                lbStatus.Text = "Cập nhật món thành công";
                uCMenu.RefershMenu(false);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (svChinhSuaMenu.Children[0] is UserControlLibrary.UCNewNhom)
            {
                UserControlLibrary.UCNewNhom uc = (UserControlLibrary.UCNewNhom)svChinhSuaMenu.Children[0];
                uc.Xoa();
                lbStatus.Text = "Xóa nhóm thành công";
                uCMenu.RefershMenu(true);
            }
            else if (svChinhSuaMenu.Children[0] is UserControlLibrary.UCNewMon)
            {
                UserControlLibrary.UCNewMon uc = (UserControlLibrary.UCNewMon)svChinhSuaMenu.Children[0];
                uc.Xoa();
                lbStatus.Text = "Xóa món thành công";
                uCMenu.RefershMenu(false);
            }
        }
    }
}