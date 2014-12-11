using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNhomQuyen.xaml
    /// </summary>
    public partial class UCQuyen : UserControl
    {
        private Data.Transit mTransit = null;
        private List<Data.QUYEN> lsArray = null;
        private Data.BOQuyen BOQuyen = null;

        public UCQuyen(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuyen = new Data.BOQuyen(mTransit);
            btnQuyenNhanVien.Visibility = System.Windows.Visibility.Hidden;
            btnCaiDatChucNang.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = lsArray = BOQuyen.GetAll().ToList();
            lvData.Items.Refresh();
        }


        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                Data.QUYEN item = (Data.QUYEN)lvData.SelectedItems[0];
                if (item.MaQuyen > 0)
                {
                    btnQuyenNhanVien.Visibility = System.Windows.Visibility.Visible;
                    btnCaiDatChucNang.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    btnQuyenNhanVien.Visibility = System.Windows.Visibility.Hidden;
                    btnCaiDatChucNang.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemQuyen win = new UserControlLibrary.WindowThemQuyen(mTransit);
            if (win.ShowDialog() == true)
            {
                lsArray.Add(win._Item);
                lvData.Items.Refresh();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                UserControlLibrary.WindowThemQuyen win = new UserControlLibrary.WindowThemQuyen(mTransit);
                win._Item = (Data.QUYEN)lvData.SelectedItems[0];
                if (win.ShowDialog() == true)
                {
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                if (lvData.SelectedItems.Count > 0)
                {
                    Data.QUYEN item = (Data.QUYEN)lvData.SelectedItems[0];
                    if (item.MaQuyen > 0)
                    {
                        item.Deleted = true;
                    }
                    lsArray.Remove(item);
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            BOQuyen.Luu(lsArray);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            BOQuyen.Refresh();
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void btnQuyenNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                Data.QUYEN item = (Data.QUYEN)lvData.SelectedItems[0];
                WindowThemQuyenNhanVien win = new WindowThemQuyenNhanVien(item, mTransit);
                win.ShowDialog();
            }
        }

        private void btnCaiDatChucNang_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                Data.QUYEN item = (Data.QUYEN)lvData.SelectedItems[0];
                WindowThemCaiDatChucNang win = new WindowThemCaiDatChucNang(item, mTransit);
                win.ShowDialog();
            }
        }
    }
}