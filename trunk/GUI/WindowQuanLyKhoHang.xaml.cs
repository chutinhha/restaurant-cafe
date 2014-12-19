using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyKhoHang.xaml
    /// </summary>
    public partial class WindowQuanLyKhoHang : Window
    {
        private Data.Transit mTransit = null;

        private UserControlLibrary.UCKho ucKho = null;

        private UserControlLibrary.UCNhaCungCap ucNhaCungCap = null;

        private UserControlLibrary.UCNhapKho ucNhapKho = null;
        private UserControlLibrary.UCChuyenKho ucChuyenKho = null;
        private UserControlLibrary.UCXuLyKho ucXuLyKho = null;

        public WindowQuanLyKhoHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            PhanQuyen();
        }

        private void btnChuyenKho_Click(object sender, RoutedEventArgs e)
        {
            if (ucChuyenKho == null)
            {
                ucChuyenKho = new UserControlLibrary.UCChuyenKho(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucChuyenKho);
        }

        private void btnKho_Click(object sender, RoutedEventArgs e)
        {
            if (ucKho == null)
            {
                ucKho = new UserControlLibrary.UCKho(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucKho);
        }

        private void btnNhaCungCap_Click(object sender, RoutedEventArgs e)
        {
            if (ucNhaCungCap == null)
            {
                ucNhaCungCap = new UserControlLibrary.UCNhaCungCap(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucNhaCungCap);
        }

        private void btnNhapKho_Click(object sender, RoutedEventArgs e)
        {
            if (ucNhapKho == null)
            {
                ucNhapKho = new UserControlLibrary.UCNhapKho(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucNhapKho);
        }

        private void btnTonKho_Click(object sender, RoutedEventArgs e)
        {
            Report.BaoCaoTonKho.WindowBaoCaoTonKho win = new Report.BaoCaoTonKho.WindowBaoCaoTonKho(mTransit);
            win.ShowDialog();
        }

        private void btnXuLyKho_Click(object sender, RoutedEventArgs e)
        {
            if (ucXuLyKho == null)
            {
                ucXuLyKho = new UserControlLibrary.UCXuLyKho(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucXuLyKho);
        }

        private void PhanQuyen()
        {
            Data.BOChiTietQuyen quyenLoaiKhachHang = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.TonKho);
            btnTonKho.Tag = quyenLoaiKhachHang;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.TonKho) || !quyenLoaiKhachHang.ChiTietQuyen.ChoPhep)
            {
                btnTonKho.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenNhaKho = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.NhaKho);
            btnTonKho.Tag = quyenNhaKho;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.NhaKho) || !quyenNhaKho.ChiTietQuyen.ChoPhep)
            {
                btnNhaKho.Visibility = System.Windows.Visibility.Collapsed;
            }
            Data.BOChiTietQuyen quyenNhapKho = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.NhapKho);
            btnTonKho.Tag = quyenNhapKho;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.NhapKho) || !quyenNhapKho.ChiTietQuyen.ChoPhep)
            {
                btnNhapKho.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenChuyenKho = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.ChuyenKho);
            btnTonKho.Tag = quyenChuyenKho;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.ChuyenKho) || !quyenChuyenKho.ChiTietQuyen.ChoPhep)
            {
                btnChuyenKho.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenXuLyKho = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.XuLyKho);
            btnTonKho.Tag = quyenXuLyKho;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.XuLyKho) || !quyenXuLyKho.ChiTietQuyen.ChoPhep)
            {
                btnXuLyKho.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenNhaCungCap = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Kho.NhaCungCap);
            btnTonKho.Tag = quyenNhaCungCap;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Kho.NhaCungCap) || !quyenNhaCungCap.ChiTietQuyen.ChoPhep)
            {
                btnNhaCungCap.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCKho)
                ucKho.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCNhaCungCap)
                ucNhaCungCap.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCNhapKho)
                ucNhapKho.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCXuLyKho)
                ucXuLyKho.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCChuyenKho)
                ucChuyenKho.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnKho_Click(sender, e);
            uCTile.TenChucNang = "Quản Lý kho hàng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }
    }
}