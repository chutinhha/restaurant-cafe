using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowMenuChange.xaml
    /// </summary>
    public partial class WindowMenuChange : Window
    {
        public WindowMenuChange()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenu.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init();
        }

        private int LoaiNhomID = 0;
        private int NhomID = 0;

        private void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUNHOM)
            {
                ControlLibrary.UCNewNhom uc = new ControlLibrary.UCNewNhom(0);
                uc._Nhom = (Data.MENUNHOM)ob;
                LoaiNhomID = (int)uc._Nhom.LoaiNhomID;
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Content = "Cập nhật nhóm";
                btnXoa.Content = "Xóa nhóm";
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnXoa.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ob is Data.MENUMON)
            {
                ControlLibrary.UCNewMon uc = new ControlLibrary.UCNewMon(0);
                uc._Mon = (Data.MENUMON)ob;
                uc.OnEvenDanhSachBanClick += new ControlLibrary.UCNewMon.EvenDanhSachBanClick(uc_OnEvenDanhSachBanClick);
                svChinhSuaMenu.Children.Clear();
                svChinhSuaMenu.Children.Add(uc);
                btnCapNhat.Content = "Cập nhật món";
                btnXoa.Content = "Xóa món";
                btnCapNhat.Visibility = System.Windows.Visibility.Visible;
                btnXoa.Visibility = System.Windows.Visibility.Visible;
            }
        }

        void uc_OnEvenDanhSachBanClick(Data.MENUMON mon)
        {
            WindowDanhSachBan win = new WindowDanhSachBan(mon);
            win.ShowDialog();
        }

        private void btnNhomMoi_Click(object sender, RoutedEventArgs e)
        {
            ControlLibrary.UCNewNhom uc = new ControlLibrary.UCNewNhom(LoaiNhomID);
            svChinhSuaMenu.Children.Clear();
            svChinhSuaMenu.Children.Add(uc);
            btnCapNhat.Visibility = System.Windows.Visibility.Visible;
            btnCapNhat.Content = "Thêm nhóm";
            btnXoa.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnMonMoi_Click(object sender, RoutedEventArgs e)
        {
            ControlLibrary.UCNewMon uc = new ControlLibrary.UCNewMon(NhomID);
            svChinhSuaMenu.Children.Clear();
            svChinhSuaMenu.Children.Add(uc);
            btnCapNhat.Visibility = System.Windows.Visibility.Visible;
            btnCapNhat.Content = "Thêm món";
            btnXoa.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (svChinhSuaMenu.Children[0] is ControlLibrary.UCNewNhom)
            {
                ControlLibrary.UCNewNhom uc = (ControlLibrary.UCNewNhom)svChinhSuaMenu.Children[0];
                uc.CapNhat();
                uCMenu.RefershMenu(true);
            }
            else if (svChinhSuaMenu.Children[0] is ControlLibrary.UCNewMon)
            {
                ControlLibrary.UCNewMon uc = (ControlLibrary.UCNewMon)svChinhSuaMenu.Children[0];
                uc.CapNhat();
                uCMenu.RefershMenu(false);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (svChinhSuaMenu.Children[0] is ControlLibrary.UCNewNhom)
            {
                ControlLibrary.UCNewNhom uc = (ControlLibrary.UCNewNhom)svChinhSuaMenu.Children[0];
                uc.Xoa();
                uCMenu.RefershMenu(true);
            }
            else if (svChinhSuaMenu.Children[0] is ControlLibrary.UCNewMon)
            {
                ControlLibrary.UCNewMon uc = (ControlLibrary.UCNewMon)svChinhSuaMenu.Children[0];
                uc.Xoa();
                uCMenu.RefershMenu(false);
            }
        }

        private void Thoát_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}