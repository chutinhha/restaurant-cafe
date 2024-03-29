﻿using System;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyKhachHang.xaml
    /// </summary>
    public partial class WindowQuanLyKhachHang : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            Data.BOChiTietQuyen quyenLoaiKhachHang = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.KhachHang.LoaiKhachHang);
            btnLoaiKhachHang.Tag = quyenLoaiKhachHang;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.KhachHang.LoaiKhachHang) || !quyenLoaiKhachHang.ChiTietQuyen.ChoPhep)
            {
                btnLoaiKhachHang.Visibility = System.Windows.Visibility.Collapsed;
            }

            Data.BOChiTietQuyen quyenKhacHang = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.KhachHang.KhachHang);
            btnLoaiKhachHang.Tag = quyenKhacHang;
            if (!mTransit.KiemTraChucNang((int)Data.TypeChucNang.KhachHang.KhachHang) || !quyenKhacHang.ChiTietQuyen.ChoPhep)
            {
                btnKhachHang.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private UserControlLibrary.UCLoaiKhachHang ucLoaiKhachHang = null;
        private UserControlLibrary.UCKhachHang ucKhachHang = null;
        private void btnLoaiKhachHang_Click(object sender, RoutedEventArgs e)
        {
            if (ucLoaiKhachHang == null)
            {
                ucLoaiKhachHang = new UserControlLibrary.UCLoaiKhachHang(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucLoaiKhachHang);
        }

        private void btnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            if (ucKhachHang == null)
            {
                ucKhachHang = new UserControlLibrary.UCKhachHang(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucKhachHang);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnKhachHang.Visibility != System.Windows.Visibility.Collapsed)
                btnKhachHang_Click(sender, e);
            else if (btnLoaiKhachHang.Visibility != System.Windows.Visibility.Collapsed)
                btnLoaiKhachHang_Click(sender, e);

            uCTile.TenChucNang = "Quản Lý Khách Hàng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);

        }

        void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCLoaiKhachHang)
                ucLoaiKhachHang.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCKhachHang)
                ucKhachHang.Window_KeyDown(sender, e);
        }
    }
}
