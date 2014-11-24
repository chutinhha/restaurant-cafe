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
            Data.BOChiTietQuyen quyenLoaiGia = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.LoaiGia);
            btnLoaiGia.Tag = quyenLoaiGia;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Gia.LoaiGia) || !quyenLoaiGia.ChiTietQuyen.ChoPhep)
            {
                btnLoaiGia.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenLichBieuDinhKy = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.LichBieuDinhKy);
            btnLichBieuDinhKy.Tag = quyenLichBieuDinhKy;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Gia.LichBieuDinhKy) || !quyenLichBieuDinhKy.ChiTietQuyen.ChoPhep)
            {
                btnLichBieuDinhKy.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenLichBieuKhongDinhKy = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.LichBieuKhongDinhKy);
            btnLichBieuKhongDinhKy.Tag = quyenLichBieuKhongDinhKy;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Gia.LichBieuKhongDinhKy) || !quyenLichBieuKhongDinhKy.ChiTietQuyen.ChoPhep)
            {
                btnLichBieuKhongDinhKy.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenDanhSachBan = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachBan);
            btnDanhSachBan.Tag = quyenDanhSachBan;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Gia.DanhSachBan) || !quyenDanhSachBan.ChiTietQuyen.ChoPhep)
            {
                btnDanhSachBan.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenKhuyenMai = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.KhuyenMai);
            btnKhuyenMai.Tag = quyenKhuyenMai;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.Gia.KhuyenMai) || !quyenKhuyenMai.ChiTietQuyen.ChoPhep)
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
            uCTile.TenChucNang = "Quản Lý Giá, Khuyễn Mãi";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            if (btnLoaiGia.Visibility != System.Windows.Visibility.Collapsed)
                btnLoaiGia_Click(sender, e);
            else if (btnLichBieuDinhKy.Visibility != System.Windows.Visibility.Collapsed)
                btnLichBieuDinhKy_Click(sender, e);
            else if (btnLichBieuKhongDinhKy.Visibility != System.Windows.Visibility.Collapsed)
                btnLichBieuKhongDinhKy_Click(sender, e);
            else if (btnDanhSachBan.Visibility != System.Windows.Visibility.Collapsed)
                btnDanhSachBan_Click(sender, e);
            else if (btnKhuyenMai.Visibility != System.Windows.Visibility.Collapsed)
                btnKhuyenMai_Click(sender, e);
        }
    }
}