using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyNhanVien.xaml
    /// </summary>
    public partial class WindowQuanLyNhanVien : Window
    {
        private Data.NHANVIEN mNhanVien = null;
        private Data.Transit mTransit = null;
        List<Data.NHANVIEN> lsNhanVienXoa = null;

        public WindowQuanLyNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Quản lý nhân viên";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void LoadDanhSachNhanVien()
        {
            List<Data.NHANVIEN> lsArray = Data.BONhanVien.GetAll(mTransit);
            lvNhanVien.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.NHANVIEN item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvNhanVien.Items.Add(li);
        }


        private void lvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvNhanVien.SelectedItems[0];
                mNhanVien = (Data.NHANVIEN)li.Tag;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSachNhanVien();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit);
            if (win.ShowDialog() == true)
            {
                AddList(win._NhanVien);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvNhanVien.SelectedItems[0];
                mNhanVien = (Data.NHANVIEN)li.Tag;

                UserControlLibrary.WindowThemNhanVien win = new UserControlLibrary.WindowThemNhanVien(mTransit);
                win._NhanVien = mNhanVien;
                if (win.ShowDialog() == true)
                {
                    win._NhanVien.Edit = true;
                    li.Tag = win._NhanVien;
                    li.Content = win._NhanVien;
                    lvNhanVien.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                mNhanVien = (Data.NHANVIEN)((ListViewItem)lvNhanVien.SelectedItems[0]).Tag;
                if (lsNhanVienXoa == null)
                {
                    lsNhanVienXoa = new List<Data.NHANVIEN>();
                }
                if (mNhanVien.NhanVienID > 0)
                    lsNhanVienXoa.Add(mNhanVien);
                lvNhanVien.Items.Remove(lvNhanVien.SelectedItems[0]);
                if (lvNhanVien.Items.Count > 0)
                {
                    lvNhanVien.SelectedIndex = 0;
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.NHANVIEN> lsNhanVien = null;
            foreach (ListViewItem li in lvNhanVien.Items)
            {
                mNhanVien = (Data.NHANVIEN)li.Tag;
                if (mNhanVien.NhanVienID == 0 || mNhanVien.Edit == true)
                {
                    if (lsNhanVien == null)
                        lsNhanVien = new List<Data.NHANVIEN>();
                    lsNhanVien.Add(mNhanVien);
                }
            }
            Data.BONhanVien.Luu(lsNhanVien, lsNhanVienXoa, mTransit);
            LoadDanhSachNhanVien();
            MessageBox.Show("Lưu thành công");
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            mNhanVien = null;
            lsNhanVienXoa = null;
            LoadDanhSachNhanVien();
        }
    }
}