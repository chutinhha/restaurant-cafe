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
            uCMenu.OnEventMenu += new UserControlLibrary.UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
        }

        private int LoaiNhomID = 0;
        private int NhomID = 0;

        private void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUNHOM)
            {

                UserControlLibrary.UCNewNhom uc = new UserControlLibrary.UCNewNhom(LoaiNhomID, mTransit);
                uc._Nhom = (Data.MENUNHOM)ob;
                NhomID = uc._Nhom.NhomID;
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Content = "Cập nhật nhóm";
                btnXoa.Content = "Xóa nhóm";
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnXoa.Visibility = System.Windows.Visibility.Visible;

            }
            else if (ob is Data.MENUMON)
            {

                UserControlLibrary.UCNewMon uc = new UserControlLibrary.UCNewMon(NhomID, mTransit);
                uc._Mon = (Data.MENUMON)ob;
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Content = "Cập nhật món";
                btnXoa.Content = "Xóa món";
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnXoa.Visibility = System.Windows.Visibility.Visible;

            }
            else if (ob is int)
            {
                LoaiNhomID = (int)ob;
            }
        }

        private void btnNhomMoi_Click(object sender, RoutedEventArgs e)
        {
            lbStatus.Text = "";
            if (LoaiNhomID != 0)
            {
                UserControlLibrary.UCNewNhom uc = new UserControlLibrary.UCNewNhom(LoaiNhomID, mTransit);
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnCapNhat.Content = "Thêm nhóm";
                btnXoa.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                lbStatus.Text = "Chưa chọn loại nhóm";
            }
        }

        private void btnMonMoi_Click(object sender, RoutedEventArgs e)
        {
            if (NhomID != 0)
            {
                lbStatus.Text = "";
                UserControlLibrary.UCNewMon uc = new UserControlLibrary.UCNewMon(NhomID, mTransit);
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