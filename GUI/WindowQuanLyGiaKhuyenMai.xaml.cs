using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyGiaKhuyenMai.xaml
    /// </summary>
    public partial class WindowQuanLyGiaKhuyenMai : Window
    {
        private Data.Transit mTransit = null;
        private UserControlLibrary.UCDanhSachBan ucDanhSachBan = null;

        private UserControlLibrary.UCDanhSachKhuyenMai ucDanhSachKhuyenMai = null;

        private UserControlLibrary.UCLichBieuDinhKy ucLichBieuDinhKy = null;

        private UserControlLibrary.UCLichBieuKhongDinhKy ucLichBieuKhongDinhKy = null;

        private UserControlLibrary.UCLoaiGia ucLoaiGia = null;


        public WindowQuanLyGiaKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            if (!mTransit.MenuGiaoDien.GiaKhuyenMai.LoaiGia)
            {
                btnLoaiGia.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mTransit.MenuGiaoDien.GiaKhuyenMai.LichBieuDinhKy)
            {
                btnLichBieuDinhKy.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mTransit.MenuGiaoDien.GiaKhuyenMai.LichBieuKhongDinKy)
            {
                btnLichBieuKhongDinhKy.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mTransit.MenuGiaoDien.GiaKhuyenMai.DanhSachBan)
            {
                btnDanhSachBan.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mTransit.MenuGiaoDien.GiaKhuyenMai.KhuyenMai)
            {
                btnKhuyenMai.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void btnDanhSachBan_Click(object sender, RoutedEventArgs e)
        {
            if (ucDanhSachBan == null)
            {
                ucDanhSachBan = new UserControlLibrary.UCDanhSachBan(mTransit);
            }
            spNoiDung.Children.Clear();
            ucDanhSachBan.Height = spNoiDung.ActualHeight;
            ucDanhSachBan.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucDanhSachBan);
        }

        private void btnKhuyenMai_Click(object sender, RoutedEventArgs e)
        {
            if (ucDanhSachKhuyenMai == null)
            {
                ucDanhSachKhuyenMai = new UserControlLibrary.UCDanhSachKhuyenMai(mTransit);
            }
            spNoiDung.Children.Clear();
            ucDanhSachKhuyenMai.Height = spNoiDung.ActualHeight;
            ucDanhSachKhuyenMai.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucDanhSachKhuyenMai);
        }

        private void btnLichBieuDinhKy_Click(object sender, RoutedEventArgs e)
        {
            if (ucLichBieuDinhKy == null)
            {
                ucLichBieuDinhKy = new UserControlLibrary.UCLichBieuDinhKy(mTransit);
            }
            spNoiDung.Children.Clear();
            ucLichBieuDinhKy.Height = spNoiDung.ActualHeight;
            ucLichBieuDinhKy.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucLichBieuDinhKy);
        }

        private void btnLichBieuKhongDinhKy_Click(object sender, RoutedEventArgs e)
        {
            if (ucLichBieuKhongDinhKy == null)
            {
                ucLichBieuKhongDinhKy = new UserControlLibrary.UCLichBieuKhongDinhKy(mTransit);
            }
            spNoiDung.Children.Clear();
            ucLichBieuKhongDinhKy.Height = spNoiDung.ActualHeight;
            ucLichBieuKhongDinhKy.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucLichBieuKhongDinhKy);
        }

        private void btnLoaiGia_Click(object sender, RoutedEventArgs e)
        {
            if (ucLoaiGia == null)
            {
                ucLoaiGia = new UserControlLibrary.UCLoaiGia(mTransit);
            }
            spNoiDung.Children.Clear();
            ucLoaiGia.Height = spNoiDung.ActualHeight;
            ucLoaiGia.Width = spNoiDung.ActualWidth;
            spNoiDung.Children.Add(ucLoaiGia);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCLichBieuDinhKy)
                ucLichBieuDinhKy.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCLichBieuKhongDinhKy)
                ucLichBieuKhongDinhKy.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCLoaiGia)
                ucLoaiGia.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCDanhSachBan)
                ucDanhSachBan.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            spNoiDung.Height = spNoiDung.ActualHeight;
            spNoiDung.Width = spNoiDung.ActualWidth;
            btnDanhSachBan_Click(sender, e);
            uCTile.TenChucNang = "Quản Lý Giá, Khuyễn Mãi";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }
    }
}