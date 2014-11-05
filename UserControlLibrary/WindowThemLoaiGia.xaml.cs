using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLoaiGia.xaml
    /// </summary>
    public partial class WindowThemLoaiGia : Window
    {
        private Data.Transit mTransit;
        public Data.MENULOAIGIA _Item { get; set; }
        public WindowThemLoaiGia(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (_Item == null)
                {
                    _Item = new Data.MENULOAIGIA();
                    _Item.Visual = true;
                    _Item.Deleted = false;
                    _Item.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }

        }

        private void SetValues()
        {
            if (_Item != null)
            {
                txtDienGiai.Text = _Item.DienGiai;
                txtLoaiGia.Text = _Item.Ten;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Loại Giá";
            }
            else
            {
                txtDienGiai.Text = "";
                txtLoaiGia.Text = "";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Sửa Loại Giá";
            }
        }

        private void GetValues()
        {
            _Item.DienGiai = txtDienGiai.Text;
            _Item.Ten = txtLoaiGia.Text;
            _Item.Deleted = false;
            _Item.Visual = true;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtLoaiGia.Text == "")
            {
                lbStatus.Text = "Loại giá không được bỏ trống";
                return false;
            }
            return true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnLuu_Click(null, null);
                return;
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }

        }
    }
}
