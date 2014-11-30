﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKhu.xaml
    /// </summary>
    public partial class WindowThemKhu : Window
    {
        private byte[] mHinh = null;
        private Data.Transit mTransit;

        public Data.BOKhu _Item { get; set; }

        public WindowThemKhu(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiGia();
            SetValues();
        }

        private void LoadLoaiGia()
        {
            cbbLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAll(mTransit);
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
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
                    _Item = new Data.BOKhu();
                    _Item.Khu.Deleted = false;
                    _Item.Khu.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtTenKhu.Text = "";
                ckMacDinh.IsChecked = false;
                ckChoPhepHienThi.IsChecked = true;
                if (cbbLoaiGia.Items.Count > 0)
                {
                    cbbLoaiGia.SelectedIndex = 0;
                }
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Khu";
            }
            else
            {
                txtTenKhu.Text = _Item.Khu.TenKhu;
                ckMacDinh.IsChecked = _Item.Khu.MacDinhSoDoBan;
                cbbLoaiGia.SelectedValue = _Item.Khu.LoaiGiaID;
                ckChoPhepHienThi.IsChecked = _Item.Khu.Visual;
                if (_Item.Khu.Hinh != null && _Item.Khu.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.Khu.Hinh);
                }             
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Khu";
            }
        }

        private void GetValues()
        {
            _Item.Khu.TenKhu = txtTenKhu.Text;
            _Item.Khu.MacDinhSoDoBan = (bool)ckMacDinh.IsChecked;
            _Item.Khu.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            _Item.LoaiGia = (Data.MENULOAIGIA)cbbLoaiGia.SelectedItem;
            _Item.Khu.Visual = (bool)ckChoPhepHienThi.IsChecked;
            _Item.Khu.Hinh = mHinh;
        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhAnh.ImageBitmap, 1024, 768, 0);
            mHinh = Utilities.ImageHandler.ImageToByte(img);            
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenKhu.Text == "")
            {
                lbStatus.Text = "Tên khu không được bỏ trống";
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

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}